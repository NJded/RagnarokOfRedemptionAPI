using Terraria.ModLoader;
using RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR; 
using InfernumMode.Content.Tiles.Relics;

namespace RagnarokOfRedemptionAPI.Content.Tiles.Relics.MoR
{
    public class AkkaRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<AkkaRelic>();
        public override string RelicTextureName => "RagnarokOfRedemptionAPI/Content/Tiles/Relics/MoR/AkkaRelicTile";
    }
}