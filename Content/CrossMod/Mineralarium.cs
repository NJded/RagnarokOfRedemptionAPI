using Terraria;
using Terraria.ModLoader;
using Redemption.Tiles.Ores;
using InfernalEclipseAPI.Core.Systems.Hooks.ILTileChanges;
using System;

namespace RagnarokOfRedemptionAPI.Content.CrossMod
{
    [JITWhenModsEnabled("SOTS")]
    [ExtendsFromMod("SOTS")]
    public class MyMineralariumIntegration : ModSystem
    {
        public static bool downedSeedOfInfection = false;

        public override void PostSetupContent()
        {
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<UraniumTile>(), 4800, 1.2f, () => NPC.downedGolemBoss);
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<PlutoniumTile>(), 11160, 1.2f, () => NPC.downedMoonlord);
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<XenomiteShardTile>(), 2900, 1.2f, () => downedSeedOfInfection);
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<DragonLeadOreTile>(), 3500, 1.0f, () => NPC.downedBoss3 && GetRedemptionAlignment() < 0);
        }

        private static int GetRedemptionAlignment()
        {
            try
            {
                if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod))
                {
                    Type worldType = redemptionMod.Code.GetType("Redemption.Globals.RedeWorld");
                    var alignmentField = worldType?.GetField("Alignment", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (alignmentField != null)
                        return (int)alignmentField.GetValue(null);
                }
            }
            catch { }
            return 0;
        }
    }
}