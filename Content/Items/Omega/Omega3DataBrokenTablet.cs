using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RagnarokOfRedemptionAPI.Content.Items.Omega
{
    public class Omega3DataBrokenTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.rare = ItemRarityID.Red;
            Item.value = 0;
            Item.maxStack = 1;
        }
    }
}