using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.TileEntities;
using CalamityMod.Tiles.DraedonSummoner;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using CalamityMod;
using RagnarokOfRedemptionAPI.Systems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.DraedonMisc;
using Redemption.Items.Materials.HM;
using Redemption.Items.Placeable.Furniture.Lab;
using Redemption.Tiles.Furniture.Lab;

namespace RagnarokOfRedemptionAPI.Content.Items.Omega
{
    public class GirusDecoder : ModItem
    {
        public static readonly SoundStyle InstallSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/AdvancedDisplayInstall");

        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 9999;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player) => true;

        public override bool ConsumeItem(Player player)
        {
            Point pos = Main.MouseWorld.ToTileCoordinates();
            Tile tile = CalamityUtils.ParanoidTileRetrieval(pos.X, pos.Y);
            float range = ((Player.tileRangeX + Player.tileRangeY) / 2f + player.blockRange) * 16f;

            if (Main.myPlayer == player.whoAmI && player.WithinRange(Main.MouseWorld, range) &&
                tile.HasTile && tile.TileType == ModContent.TileType<CodebreakerTile>())
            {
                SoundEngine.PlaySound(InstallSound, Main.LocalPlayer.Center);

                var cb = CalamityUtils.FindTileEntity<TECodebreaker>(pos.X, pos.Y,
                    CodebreakerTile.Width, CodebreakerTile.Height, CodebreakerTile.SheetSquare);

                if (cb == null || GirusDecoderCache.HasGirusDecoder(cb)) return false;

                GirusDecoderCache.SetGirusDecoder(cb, true);
                cb.SyncConstituents((short)Main.myPlayer);
                
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Plating>(), 11)
                .AddIngredient(ModContent.ItemType<Capacitor>(), 1)
                .AddIngredient(ModContent.ItemType<AIChip>(), 3)
                .AddIngredient(ModContent.ItemType<CarbonMyofibre>(), 14)
                .AddIngredient(ModContent.ItemType<DubiousPlating>(), 23)
                .AddIngredient(ModContent.ItemType<MysteriousCircuitry>(), 27)
				.AddTile(ModContent.TileType<GirusCorruptorTile>())
                .Register();
        }
    }
}