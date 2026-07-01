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
    public class EaglecrestGolemLore : LoreItem
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
            Item.rare = ItemRarityID.Blue;
            Item.consumable = false;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EaglecrestGolemTrophy>(), 1)
				.AddTile(TileID.Bookcases)
                .Register();
        }
    }
}