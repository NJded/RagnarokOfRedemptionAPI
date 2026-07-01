using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Usable;
using Redemption.Items.Materials.HM;
using Redemption.Tiles.Furniture.Lab;

namespace RagnarokOfRedemptionAPI.Content.Items.Omega
{
    public class Omega1DataTablet : OmegaDataTablet
    {
        public override int TabletTier => 1;
        
        public override List<string> GetBlockedWeaponKeys()
        {
            return new List<string>
            {
                nameof(TinyCleaver),
                nameof(PulseBlade),
                nameof(CorruptedDoubleRifle)
            };
        }
    }

    public class Omega1DataTabletRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<Omega1DataTablet>())
                .AddIngredient(ModContent.ItemType<Omega1DataBrokenTablet>(), 1)
                .AddIngredient(ModContent.ItemType<EnergyCell>(), 3)
                .AddIngredient(ModContent.ItemType<CorruptedXenomite>(), 7)
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ModContent.ItemType<CarbonMyofibre>(), 14)
                .AddIngredient(ModContent.ItemType<AIChip>(), 1)
                .AddTile(ModContent.TileType<GirusCorruptorTile>())
                .Register();
        }
    }
}