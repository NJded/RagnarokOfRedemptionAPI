using CalamityMod.Items.LoreItems;
using CalamityMod.Rarities;
using CalamityMod;
using Redemption.Items.Placeable.Trophies;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace RagnarokOfRedemptionAPI.Content.Items.LOOOOOOOORE
{
    public class AncientDeityDuoLore : LoreItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.rare = ModContent.RarityType<PureGreen>();
            Item.consumable = false;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UkonKirvesTrophy>(), 1)
                .AddTile(TileID.Bookcases)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AkanKirvesTrophy>(), 1)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}