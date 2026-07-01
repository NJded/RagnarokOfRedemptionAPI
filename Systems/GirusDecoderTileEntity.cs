using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using CalamityMod.TileEntities;

namespace RagnarokOfRedemptionAPI.Systems
{
    public class GirusDecoderData : ModTileEntity
    {
        public bool HasGirusDecoder = false;
        public int ActiveOmegaSchematic = 0;

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = CalamityMod.CalamityUtils.ParanoidTileRetrieval(x, y);
            return tile.HasTile && tile.TileType == ModContent.TileType<CalamityMod.Tiles.DraedonSummoner.CodebreakerTile>();
        }

        public override void SaveData(TagCompound tag)
        {
            tag["HasGirusDecoder"] = HasGirusDecoder;
            tag["ActiveOmegaSchematic"] = ActiveOmegaSchematic;
        }

        public override void LoadData(TagCompound tag)
        {
            HasGirusDecoder = tag.GetBool("HasGirusDecoder");
            ActiveOmegaSchematic = tag.GetInt("ActiveOmegaSchematic");
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(HasGirusDecoder);
            writer.Write(ActiveOmegaSchematic);
        }

        public override void NetReceive(BinaryReader reader)
        {
            HasGirusDecoder = reader.ReadBoolean();
            ActiveOmegaSchematic = reader.ReadInt32();
        }
    }
}