using CalamityMod.Items.LoreItems;
using CalamityMod.Rarities;
using CalamityMod;
using Redemption.Items.Placeable.Trophies;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Items.Weapons.PreHM.Magic;

namespace RagnarokOfRedemptionAPI.Content.Items.LOOOOOOOORE
{
    public class CalaviaLore : LoreItem
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
            Item.rare = ItemRarityID.LightRed;
            Item.consumable = false;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BladeOfTheMountain>(), 1)
                .AddTile(TileID.Bookcases)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PureIronCrossbow>(), 1)
                .AddTile(TileID.Bookcases)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Icefall>(), 1)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}