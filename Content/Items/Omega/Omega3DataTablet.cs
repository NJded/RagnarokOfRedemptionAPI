using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Materials.HM;
using Redemption.Items.Materials.PostML;
using Redemption.Tiles.Furniture.Lab;

namespace RagnarokOfRedemptionAPI.Content.Items.Omega
{
    public class Omega3DataTablet : OmegaDataTablet
    {
        public override int TabletTier => 3;
        
        public override List<string> GetBlockedWeaponKeys()
        {
            var weapons = new List<string>
            {
                "SunInThePalm",
                "OOFingergun",
                "BlastBattery"
            };

            if (ModLoader.HasMod("ThoriumMod") && ModLoader.HasMod("RedemptionBardHealer"))
            {
                Mod redemptionBardHealer = ModLoader.GetMod("RedemptionBardHealer");
                
                if (redemptionBardHealer.TryFind<ModItem>("ScorchingScalpel", out ModItem scorchingScalpel))
                    weapons.Add("ScorchingScalpel");
                
                if (redemptionBardHealer.TryFind<ModItem>("Oboeterator", out ModItem oboeterator))
                    weapons.Add("Oboeterator");
                
                if (redemptionBardHealer.TryFind<ModItem>("OmegaDefibrillator", out ModItem omegaDefibrillator))
                    weapons.Add("OmegaDefibrillator");
            }
            
            return weapons;
        }
    }

    public class Omega3DataTabletRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<Omega3DataTablet>())
                .AddIngredient(ModContent.ItemType<Omega3DataBrokenTablet>(), 1)
                .AddIngredient(ModContent.ItemType<OmegaPowerCell>(), 1)
                .AddIngredient(ModContent.ItemType<RoboBrain>(), 1)
                .AddIngredient(ModContent.ItemType<CorruptedXenomite>(), 7)
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ModContent.ItemType<Capacitor>(), 2)
                .AddIngredient(ModContent.ItemType<CarbonMyofibre>(), 12)
                .AddIngredient(ModContent.ItemType<Plutonium>(), 7)
                .AddTile(ModContent.TileType<GirusCorruptorTile>())
                .Register();
        }
    }
}