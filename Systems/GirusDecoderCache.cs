using CalamityMod.TileEntities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using RagnarokOfRedemptionAPI.Content.Items.Omega;

namespace RagnarokOfRedemptionAPI.Systems
{
    public class GirusDecoderSaveData : ModSystem
    {
        public static Dictionary<Point, bool> SavedGirusDecoderData = new Dictionary<Point, bool>();
        public static Dictionary<Point, int> SavedOmegaSchematics = new Dictionary<Point, int>();

        public override void SaveWorldData(TagCompound tag)
        {
            var girusDecoderList = new List<TagCompound>();
            foreach (var kvp in SavedGirusDecoderData)
            {
                var entry = new TagCompound();
                entry["posX"] = kvp.Key.X;
                entry["posY"] = kvp.Key.Y;
                entry["hasGirusDecoder"] = kvp.Value;
                if (SavedOmegaSchematics.TryGetValue(kvp.Key, out int schematic))
                {
                    entry["omegaSchematic"] = schematic;
                }
                girusDecoderList.Add(entry);
            }
            tag["GirusDecoderData"] = girusDecoderList;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            SavedGirusDecoderData.Clear();
            SavedOmegaSchematics.Clear();
            
            if (tag.ContainsKey("GirusDecoderData"))
            {
                var girusDecoderList = tag.GetList<TagCompound>("GirusDecoderData");
                foreach (var entry in girusDecoderList)
                {
                    int posX = entry.GetInt("posX");
                    int posY = entry.GetInt("posY");
                    bool hasDecoder = entry.GetBool("hasGirusDecoder");
                    int schematic = entry.ContainsKey("omegaSchematic") ? entry.GetInt("omegaSchematic") : 0;
                    
                    Point pos = new Point(posX, posY);
                    SavedGirusDecoderData[pos] = hasDecoder;
                    if (schematic != 0)
                    {
                        SavedOmegaSchematics[pos] = schematic;
                    }
                }
            }
        }
    }

    public class GirusDecoderCache : ModSystem
    {
        private static Dictionary<int, bool> HasGirusDecoderMap = new Dictionary<int, bool>();
        private static Dictionary<int, int> ActiveOmegaSchematics = new Dictionary<int, int>();
        private static Dictionary<int, Point> CodebreakerTilePositions = new Dictionary<int, Point>();

        public override void ClearWorld()
        {
            HasGirusDecoderMap.Clear();
            ActiveOmegaSchematics.Clear();
            CodebreakerTilePositions.Clear();
        }

        public override void OnWorldLoad()
        {
            RestoreFromSavedData();
        }

        public override void PostUpdateWorld()
        {
            foreach (var entity in Terraria.DataStructures.TileEntity.ByID.Values)
            {
                if (entity is TECodebreaker codebreaker)
                {
                    Point tilePos = new Point(codebreaker.Position.X, codebreaker.Position.Y);

                    if (!HasGirusDecoderMap.ContainsKey(codebreaker.ID))
                    {
                        if (GirusDecoderSaveData.SavedGirusDecoderData.TryGetValue(tilePos, out bool hasDecoder) && hasDecoder)
                        {
                            HasGirusDecoderMap[codebreaker.ID] = true;
                            CodebreakerTilePositions[codebreaker.ID] = tilePos;
                            
                            if (GirusDecoderSaveData.SavedOmegaSchematics.TryGetValue(tilePos, out int schematic))
                            {
                                ActiveOmegaSchematics[codebreaker.ID] = schematic;
                            }
                        }
                    }

                    if (HasGirusDecoderMap.ContainsKey(codebreaker.ID))
                    {
                        CodebreakerTilePositions[codebreaker.ID] = tilePos;
                    }
                }
            }

            List<int> toRemove = new List<int>();
            foreach (var kvp in HasGirusDecoderMap)
            {
                int id = kvp.Key;
                if (!CodebreakerTilePositions.ContainsKey(id))
                    continue;
                    
                Point pos = CodebreakerTilePositions[id];
                bool exists = CheckCodebreakerAtPosition(pos);
                
                if (!exists)
                {
                    Vector2 worldPos = new Vector2(pos.X * 16, pos.Y * 16);
                    DropGirusDecoderAtPosition(worldPos);
                    if (ActiveOmegaSchematics.TryGetValue(id, out int schematicID) && schematicID != 0)
                    {
                        DropSchematicAtPosition(worldPos, schematicID);
                    }

                    GirusDecoderSaveData.SavedGirusDecoderData.Remove(pos);
                    GirusDecoderSaveData.SavedOmegaSchematics.Remove(pos);
                    
                    toRemove.Add(id);
                }
            }
            
            foreach (int id in toRemove)
            {
                HasGirusDecoderMap.Remove(id);
                ActiveOmegaSchematics.Remove(id);
                CodebreakerTilePositions.Remove(id);
            }
        }

        private void RestoreFromSavedData()
        {
            HasGirusDecoderMap.Clear();
            ActiveOmegaSchematics.Clear();
            CodebreakerTilePositions.Clear();

            foreach (var entity in Terraria.DataStructures.TileEntity.ByID.Values)
            {
                if (entity is TECodebreaker codebreaker)
                {
                    Point tilePos = new Point(codebreaker.Position.X, codebreaker.Position.Y);
                    
                    if (GirusDecoderSaveData.SavedGirusDecoderData.TryGetValue(tilePos, out bool hasDecoder) && hasDecoder)
                    {
                        HasGirusDecoderMap[codebreaker.ID] = true;
                        CodebreakerTilePositions[codebreaker.ID] = tilePos;
                        
                        if (GirusDecoderSaveData.SavedOmegaSchematics.TryGetValue(tilePos, out int schematic))
                        {
                            ActiveOmegaSchematics[codebreaker.ID] = schematic;
                        }
                    }
                }
            }
        }

        public static bool HasGirusDecoder(TECodebreaker codebreaker)
        {
            if (codebreaker == null) return false;
            return HasGirusDecoderMap.ContainsKey(codebreaker.ID) && HasGirusDecoderMap[codebreaker.ID];
        }

        public static void SetGirusDecoder(TECodebreaker codebreaker, bool value)
        {
            if (codebreaker == null) return;
            
            Point tilePos = new Point(codebreaker.Position.X, codebreaker.Position.Y);
            
            if (value)
            {
                HasGirusDecoderMap[codebreaker.ID] = true;
                CodebreakerTilePositions[codebreaker.ID] = tilePos;
                GirusDecoderSaveData.SavedGirusDecoderData[tilePos] = true;
            }
            else
            {
                GirusDecoderSaveData.SavedGirusDecoderData.Remove(tilePos);
                GirusDecoderSaveData.SavedOmegaSchematics.Remove(tilePos);
                
                HasGirusDecoderMap.Remove(codebreaker.ID);
                ActiveOmegaSchematics.Remove(codebreaker.ID);
                CodebreakerTilePositions.Remove(codebreaker.ID);
            }
        }

        public static int GetActiveOmegaSchematic(int codebreakerID)
        {
            if (ActiveOmegaSchematics.TryGetValue(codebreakerID, out int schematic))
                return schematic;
            return 0;
        }

        public static void SetActiveOmegaSchematic(int codebreakerID, int schematicType)
        {
            ActiveOmegaSchematics[codebreakerID] = schematicType;
            
            if (CodebreakerTilePositions.TryGetValue(codebreakerID, out Point tilePos))
            {
                if (schematicType != 0)
                {
                    GirusDecoderSaveData.SavedOmegaSchematics[tilePos] = schematicType;
                }
                else
                {
                    GirusDecoderSaveData.SavedOmegaSchematics.Remove(tilePos);
                }
            }
        }
        
        private static void DropGirusDecoderAtPosition(Vector2 position)
        {
            int x = (int)position.X;
            int y = (int)position.Y;
            Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(x, y), x, y, 16, 16, 
                ModContent.ItemType<GirusDecoder>());
        }
        
        private static void DropSchematicAtPosition(Vector2 position, int schematicID)
        {
            int x = (int)position.X;
            int y = (int)position.Y;
            Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(x, y), x, y, 16, 16, 
                schematicID);
        }
        
        private static bool CheckCodebreakerAtPosition(Point tilePos)
        {
            for (int i = 0; i < CalamityMod.Tiles.DraedonSummoner.CodebreakerTile.Width; i++)
            {
                for (int j = 0; j < CalamityMod.Tiles.DraedonSummoner.CodebreakerTile.Height; j++)
                {
                    int x = tilePos.X + i;
                    int y = tilePos.Y + j;
                    
                    if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
                    {
                        Tile tile = Main.tile[x, y];
                        if (tile.HasTile && tile.TileType == ModContent.TileType<CalamityMod.Tiles.DraedonSummoner.CodebreakerTile>())
                            return true;
                    }
                }
            }
            return false;
        }
    }
}