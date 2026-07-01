using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using Redemption.Items.Usable.Potions;

namespace RagnarokOfRedemptionAPI.Content.RecipeChanges
{
    public class BloodOrbRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {

            int bloodOrbID = ModContent.ItemType<CalamityMod.Items.Materials.BloodOrb>();

            int[] potionsToDuplicate = new int[]
            {
                ModContent.ItemType<CharismaPotion>(),
                ModContent.ItemType<HydraCorrosionPotion>(),
                ModContent.ItemType<SkirmishPotion>(),
                ModContent.ItemType<VendettaPotion>(),
                ModContent.ItemType<VigourousPotion>(),
            };

            foreach (int potionType in potionsToDuplicate)
            {
                Recipe recipe = Recipe.Create(potionType, 2);
                recipe.AddIngredient(potionType, 1);
                recipe.AddIngredient(bloodOrbID, 10);
                recipe.AddTile(TileID.AlchemyTable);
                recipe.AddCondition(Condition.DownedSkeletron);
                recipe.Register();
            }
        }
    }
}