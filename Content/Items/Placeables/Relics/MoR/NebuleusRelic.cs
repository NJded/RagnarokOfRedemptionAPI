using Terraria.ModLoader;
using RagnarokOfRedemptionAPI.Content.Tiles.Relics.MoR;
using InfernumMode.Content.Items.Relics;
using Terraria.Localization;

namespace RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR
{
    public class NebuleusRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetOrRegister(RagnarokOfRedemptionAPI.Instance.GetLocalizationKey($"Items.{this.Name}.Tooltip")).WithFormatArgs(PersonalMessage);
        public override int TileID => ModContent.TileType<NebuleusRelicTile>();
    }
}