using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace RagnarokOfRedemptionAPI.Content.Items.Accessories
{
    public class AssassinsMutagen : ModItem
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
                    var thoriumPlayerType = thorium.GetType().Assembly.GetType("ThoriumMod.ThoriumPlayer");
                    if (thoriumPlayerType != null)
                    {
                        var modPlayersField = typeof(Player).GetField("modPlayers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (modPlayersField != null)
                        {
                            var modPlayers = (System.Collections.Generic.List<ModPlayer>)modPlayersField.GetValue(player);
                            foreach (var modPlayer in modPlayers)
                            {
                                if (modPlayer.GetType() == thoriumPlayerType)
                                {
                                    var techPointsMaxField = thoriumPlayerType.GetField("techPointsMax", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                                    if (techPointsMaxField != null)
                                    {
                                        int currentMax = (int)techPointsMaxField.GetValue(modPlayer);
                                        techPointsMaxField.SetValue(modPlayer, currentMax + 1);
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

            int mutagenType = redemptionBardHealer.Find<ModItem>("MutagenThrower").Type; 
            int badgeType = homeward.Find<ModItem>("RogueBadge").Type;
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
