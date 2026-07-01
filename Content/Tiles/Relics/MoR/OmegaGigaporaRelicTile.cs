using Terraria.ModLoader;
using RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR; 
using InfernumMode.Content.Tiles.Relics;

namespace RagnarokOfRedemptionAPI.Content.Tiles.Relics.MoR
{
    public class OmegaGigaporaRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<OmegaGigaporaRelic>();
        public override string RelicTextureName => "RagnarokOfRedemptionAPI/Content/Tiles/Relics/MoR/OmegaGigaporaRelicTile";
    }
}