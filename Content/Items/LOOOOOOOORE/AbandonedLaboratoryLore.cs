using CalamityMod.Items.LoreItems;
using CalamityMod.Rarities;
using CalamityMod;
using Redemption.Items.Placeable.Trophies;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable.Summons;

namespace RagnarokOfRedemptionAPI.Content.Items.LOOOOOOOORE
{
    public class AbandonedLaboratoryLore : LoreItem
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
            Item.rare = ItemRarityID.Pink;
            Item.consumable = false;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<LabHologramDevice>(), 1)
				.AddTile(TileID.Bookcases)
                .Register();
        }
    }
}