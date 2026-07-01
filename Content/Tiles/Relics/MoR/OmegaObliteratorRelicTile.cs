using Terraria.ModLoader;
using RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR; 
using InfernumMode.Content.Tiles.Relics;

namespace RagnarokOfRedemptionAPI.Content.Tiles.Relics.MoR
{
    public class OmegaObliteratorRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<OmegaObliteratorRelic>();
        public override string RelicTextureName => "RagnarokOfRedemptionAPI/Content/Tiles/Relics/MoR/OmegaObliteratorRelicTile";
    }
}