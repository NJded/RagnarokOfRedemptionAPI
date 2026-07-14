using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace RagnarokOfRedemptionAPI.Content.Items.Accessories
{
    public class PriestsMutagen : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.HasMod("Redemption") &&
                   ModLoader.HasMod("RedemptionBardHealer") &&
                   ModLoader.HasMod("ContinentOfJourney") &&
                   ModLoader.HasMod("HomewardRagnarok");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 25);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.25f;
            player.GetCritChance(DamageClass.Generic) += 0.15f;

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                try
                {
                    var healPlayerType = thorium.GetType().Assembly.GetType("ThoriumMod.HealPlayer");
                    if (healPlayerType != null)
                    {
                        var modPlayersField = typeof(Player).GetField("modPlayers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (modPlayersField != null)
                        {
                            var modPlayers = (System.Collections.Generic.List<ModPlayer>)modPlayersField.GetValue(player);
                            foreach (var modPlayer in modPlayers)
                            {
                                if (modPlayer.GetType() == healPlayerType)
                                {
                                    var healBonusField = healPlayerType.GetField("healBonus", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                                    if (healBonusField != null)
                                    {
                                        int currentBonus = (int)healBonusField.GetValue(modPlayer);
                                        healBonusField.SetValue(modPlayer, currentBonus + 2);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public override void AddRecipes()
        {
            Mod redemptionBardHealer = ModLoader.GetMod("RedemptionBardHealer"); 
            Mod continent = ModLoader.GetMod("ContinentOfJourney");
            Mod homeward = ModLoader.GetMod("HomewardRagnarok");

            int mutagenType = redemptionBardHealer.Find<ModItem>("MutagenHealer").Type; 
            int badgeType = homeward.Find<ModItem>("ClericBadge").Type;
            int crownType = continent.Find<ModItem>("WillToCrown").Type;
            int tileType = homeward.Find<ModTile>("TimelessFountainTile").Type;

            CreateRecipe()
                .AddIngredient(mutagenType)
                .AddIngredient(badgeType)
                .AddIngredient(crownType, 5)
                .AddTile(tileType)
                .Register();
        }
    }
}
