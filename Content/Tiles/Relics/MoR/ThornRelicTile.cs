using Terraria.ModLoader;
using RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR; 
using InfernumMode.Content.Tiles.Relics;

namespace RagnarokOfRedemptionAPI.Content.Tiles.Relics.MoR
{
    public class ThornRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<ThornRelic>();
        public override string RelicTextureName => "RagnarokOfRedemptionAPI/Content/Tiles/Relics/MoR/ThornRelicTile";
    }
}