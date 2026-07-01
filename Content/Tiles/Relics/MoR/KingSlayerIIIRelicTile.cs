using Terraria.ModLoader;
using RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR; 
using InfernumMode.Content.Tiles.Relics;

namespace RagnarokOfRedemptionAPI.Content.Tiles.Relics.MoR
{
    public class KingSlayerIIIRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<KingSlayerIIIRelic>();
        public override string RelicTextureName => "RagnarokOfRedemptionAPI/Content/Tiles/Relics/MoR/KingSlayerIIIRelicTile";
    }
}