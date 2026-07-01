using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Tools.HM;
using Redemption.Items.Materials.HM;
using Redemption.Tiles.Furniture.Lab;

namespace RagnarokOfRedemptionAPI.Content.Items.Omega
{
    public class Omega2DataTablet : OmegaDataTablet
    {
        public override int TabletTier => 2;
        
        public override List<string> GetBlockedWeaponKeys()
        {
            var weapons = new List<string>
            {
                "CorruptedDAN",
                "DrillRevolver",
                "GigapeiliContactor",
                "OmegaPickaxe",
                "OversizedScrewdriver"
            };

            if (ModLoader.HasMod("ThoriumMod") && ModLoader.HasMod("RedemptionBardHealer"))
            {
                Mod redemptionBardHealer = ModLoader.GetMod("RedemptionBardHealer");
                
                if (redemptionBardHealer.TryFind<ModItem>("Shockriken", out ModItem shockriken))
                    weapons.Add("Shockriken");
                
                if (redemptionBardHealer.TryFind<ModItem>("VlitchSynthesizer", out ModItem vlitchSynthesizer))
                    weapons.Add("VlitchSynthesizer");
                
                if (redemptionBardHealer.TryFind<ModItem>("KillswitchEngineer", out ModItem killswitchEngineer))
                    weapons.Add("KillswitchEngineer");
            }
            
            return weapons;
        }
    }

    public class Omega2DataTabletRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<Omega2DataTablet>())
                .AddIngredient(ModContent.ItemType<Omega2DataBrokenTablet>(), 1)
                .AddIngredient(ModContent.ItemType<OmegaPowerCell>(), 1)
                .AddIngredient(ModContent.ItemType<CorruptedXenomite>(), 7)
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ModContent.ItemType<Capacitor>(), 3)
                .AddIngredient(ModContent.ItemType<AIChip>(), 1)
                .AddIngredient(ModContent.ItemType<Uranium>(), 7)
                .AddTile(ModContent.TileType<GirusCorruptorTile>())
                .Register();
        }
    }
}