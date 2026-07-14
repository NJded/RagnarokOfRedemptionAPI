using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace RagnarokOfRedemptionAPI.Content.Items.Accessories
{
    public class SwordmasterMutagen : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.HasMod("Redemption") &&
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
            player.GetDamage(DamageClass.Melee) += 0.25f;
            player.GetCritChance(DamageClass.Melee) += 15f;
        }

        public override void AddRecipes()
        {
            Mod redemption = ModLoader.GetMod("Redemption");
            Mod continent = ModLoader.GetMod("ContinentOfJourney");
            Mod calamity = ModLoader.GetMod("CalamityMod");

            int mutagenType = redemption.Find<ModItem>("MutagenMelee").Type;
            int badgeType = continent.Find<ModItem>("SwordmasterBadge").Type;
            int crownType = continent.Find<ModItem>("WillToCrown").Type;
            int tileType = calamity.Find<ModTile>("CosmicAnvil").Type;

            CreateRecipe()
                .AddIngredient(mutagenType)
                .AddIngredient(badgeType)
                .AddIngredient(crownType, 5)
                .AddTile(tileType)
                .Register();
        }
    }
}
