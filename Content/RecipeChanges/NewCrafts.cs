using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Usable;
using Redemption.Tiles.Furniture.Lab;
using Redemption.Tiles.Furniture.Misc;
using CalamityMod.Items.Materials;
using CalamityMod.Items.DraedonMisc;

namespace RagnarokOfRedemptionAPI.Content.RecipeChanges
{
    public class NewCrafts : ModSystem
    {
        public override void AddRecipes()
        {
            CreateMoltenScrapAltRecipe(ModContent.ItemType<DubiousPlating>(), 3);
            CreateMoltenScrapAltRecipe(ModContent.ItemType<MysteriousCircuitry>(), 3);
            CreateEnergyCellRecipe();
        }

        private void CreateMoltenScrapAltRecipe(int ingredientType, int amount)
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<MoltenScrap>(), 1);
            recipe.AddIngredient(ingredientType, amount);
            recipe.AddTile(ModContent.TileType<XeniumSmelterTile>());
            recipe.Register();
        }

        private void CreateEnergyCellRecipe()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EnergyCell>(), 3);
            recipe.AddIngredient(ModContent.ItemType<DraedonPowerCell>(), 25);
            recipe.AddTile(ModContent.TileType<EnergyStationTile>());
            recipe.Register();
        }
    }
}