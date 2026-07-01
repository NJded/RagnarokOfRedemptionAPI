using System;
using System.Reflection;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using MonoMod.RuntimeDetour;
using Terraria.ModLoader;

namespace RagnarokOfRedemptionAPI.Mods.BossChecklist
{
    public class RedemptionBossChecklistChanger : ModSystem
    {
        private ILHook ilHook;

        public override void Load()
        {
            if (!ModLoader.TryGetMod("Redemption", out Mod redemption))
                return;

            var bcClass = redemption.Code.GetType("Redemption.CrossMod.WeakReferences");
            if (bcClass == null)
                return;

            var method = bcClass.GetMethod("PerformBossChecklistSupport", 
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

            if (method == null)
                return;

            ilHook = new ILHook(method, IL_EditBossChecklistKeys);
        }

        private void IL_EditBossChecklistKeys(ILContext il)
        {
            var c = new ILCursor(il);
            const int scanAhead = 80;

            var map = new[]
            {
                new { Key = "PZ", Expected = 19.1f, NewVal = 19.63f },
				new { Key = "EaglecrestGolem2", Expected = 20f, NewVal = 19.64f },
                new { Key = "AncientDeityDuo", Expected = 20.001f, NewVal = 19.65f },
                new { Key = "Nebuleus", Expected = 23f, NewVal = 21.1f }
            };

            foreach (var entry in map)
            {
                c.Index = 0;
                while (c.TryGotoNext(MoveType.After, i => i.MatchLdstr(entry.Key)))
                {
                    int ldstrIndex = c.Index - 1;
                    for (int offset = 1; offset <= scanAhead && (ldstrIndex + offset) < il.Instrs.Count; offset++)
                    {
                        var instr = il.Instrs[ldstrIndex + offset];
                        if (instr.OpCode == OpCodes.Ldc_R4 && instr.Operand is float foundFloat)
                        {
                            if (Math.Abs(foundFloat - entry.Expected) < 0.6f)
                            {
                                instr.Operand = entry.NewVal;
                                Mod.Logger.Info($"Changed {entry.Key} from {foundFloat} to {entry.NewVal}");
                            }
                            break;
                        }
                        if (instr.OpCode == OpCodes.Ldc_R8 && instr.Operand is double foundDouble)
                        {
                            float foundF = (float)foundDouble;
                            if (Math.Abs(foundF - entry.Expected) < 0.6f)
                            {
                                instr.OpCode = OpCodes.Ldc_R4;
                                instr.Operand = entry.NewVal;
                                Mod.Logger.Info($"Changed {entry.Key} from {foundF} to {entry.NewVal}");
                            }
                            break;
                        }
                    }
                }
            }
        }

        public override void Unload()
        {
            ilHook?.Dispose();
            ilHook = null;
        }
    }
}