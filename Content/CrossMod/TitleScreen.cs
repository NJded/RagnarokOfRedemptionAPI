using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Collections.Generic;

namespace RagnarokOfRedemptionAPI.Compat
{
    [JITWhenModsEnabled("InfernumMode", "Redemption")]
    [ExtendsFromMod("InfernumMode", "Redemption")]
    public class RedemptionBossCardsSystem : ModSystem
    {
        internal static Mod Redemption;
        internal static Mod Infernum;

        private static HashSet<int> _bossesInCombat = new HashSet<int>();

        public override void Load()
        {
            Redemption = ModLoader.GetMod("Redemption");
            Infernum = ModLoader.GetMod("InfernumMode");
        }

        public override void PostSetupContent()
        {
            if (Redemption == null || Infernum == null)
                return;

            AddRedemptionBossIntros();
        }

        private bool IsBossInCombat(int npcType)
        {
            if (_bossesInCombat.Contains(npcType))
                return true;

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.type == npcType)
                {
                    bool isInCombat = CheckBossCombatPhase(npc);
                    
                    if (isInCombat)
                    {
                        _bossesInCombat.Add(npcType);
                        return true;
                    }
                }
            }
            
            return false;
        }

        private bool CheckBossCombatPhase(NPC npc)
        {
            string npcName = npc.GivenOrTypeName;
            
            if (npcName.Contains("PZ") || npc.type == Redemption.Find<ModNPC>("PZ")?.Type)
            {
                return npc.ai[0] > 0;
            }

            if (npcName.Contains("Erhan"))
            {
                return npc.ai[0] > 0;
            }
            
            if (npcName.Contains("KS3"))
            {
                return npc.ai[0] > 0;
            }
            
            if (npcName.Contains("KS3_Clone"))
            {
                return npc.ai[0] > 0;
            }

            if (npcName.Contains("OO"))
            {
                return npc.ai[0] > 0;
            }
            
            return true;
        }

        private int GetNPCTypeByName(string npcName)
        {
            for (int i = 0; i < NPCLoader.NPCCount; i++)
            {
                ModNPC modNPC = NPCLoader.GetNPC(i);
                if (modNPC != null && modNPC.GetType().Name == npcName)
                    return i;
            }
            return 0;
        }

        private void AddRedemptionBossIntros()
        {
            int nebuleusCloneType = GetNPCTypeByName("Nebuleus_Clone");
            int nebuleus2CloneType = GetNPCTypeByName("Nebuleus2_Clone");
            
            var bosses = new List<(string InternalName, string LocalizationKey, Color Color1, Color? Color2, SoundStyle Tick, SoundStyle End, bool HasDialogIntro, int CustomType)>
            {
                ("SoI", "SoI", new Color(0xaf, 0xcd, 0x35), new Color(0x00, 0x92, 0x4f), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("FowlEmperor", "FowlEmperor", new Color(0xa7, 0x6c, 0x33), null, SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Thorn", "Thorn", new Color(0x4b, 0x5c, 0x3b), new Color(0x75, 0x32, 0x32), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Erhan", "Erhan", new Color(0x79, 0x5c, 0x12), new Color(0xcb, 0xb3, 0x49), SoundID.MenuTick, SoundID.Roar, true, 0),
                ("ErhanSpirit", "ErhanSpirit", new Color(0x79, 0x5c, 0x12), new Color(0xcb, 0xb3, 0x49), SoundID.MenuTick, SoundID.Roar, true, 0),
                ("Keeper", "Keeper", new Color(0x3e, 0x1d, 0x1a), new Color(0x7c, 0x47, 0x47), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("KeeperSpirit", "KeeperSpirit", new Color(0xc3, 0xe7, 0xf4), new Color(0xee, 0xfb, 0xff), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("SkullDigger", "SkullDigger", new Color(0x5f, 0x64, 0x73), null, SoundID.MenuTick, SoundID.Roar, false, 0),
                ("EaglecrestGolem", "EaglecrestGolem", new Color(0x40, 0x37, 0x38), null, SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Calavia", "Calavia", new Color(0xe2, 0xff, 0xff), null, SoundID.MenuTick, SoundID.Roar, false, 0),
                ("JanitorBot", "JanitorBot", new Color(0x95, 0x99, 0xa9), new Color(0xf2, 0xf5, 0xff), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("IrradiatedBehemoth", "IrradiatedBehemoth", new Color(0x36, 0xc1, 0x3b), null, SoundID.MenuTick, SoundID.Roar, false, 0),
                ("BlisteredFish", "BlisteredFish", new Color(0xaf, 0xcd, 0x35), new Color(0x00, 0x92, 0x4f), SoundID.MenuTick, SoundID.Roar, false, 0),
				("Stage3Scientist", "Stage3Scientist", new Color(0xaf, 0xcd, 0x35), new Color(0x00, 0x92, 0x4f), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("KS3", "KS3", new Color(0x00, 0x5f, 0x92), new Color(0x00, 0x5f, 0x92), SoundID.MenuTick, SoundID.Roar, true, 0),
                ("KS3_Clone", "KS3_Clone", new Color(0x00, 0x5f, 0x92), new Color(0x00, 0x5f, 0x92), SoundID.MenuTick, SoundID.Roar, true, 0),
                ("OmegaCleaver", "OmegaCleaver", new Color(0x96, 0x14, 0x36), new Color(0xdf, 0x3e, 0x37), SoundID.MenuTick, SoundID.Roar, true, 0),
                ("Gigapora", "Gigapora", new Color(0x96, 0x14, 0x36), new Color(0xdf, 0x3e, 0x37), SoundID.MenuTick, SoundID.Roar, true, 0),
                ("ProtectorVolt", "ProtectorVolt", new Color(0x24, 0x2b, 0x39), new Color(0x3e, 0x58, 0x5a), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("MACEProject", "MACEProject", new Color(0x39, 0x5c, 0xa2), new Color(0x4e, 0xad, 0xe0), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("OO", "OO", new Color(0x96, 0x14, 0x36), new Color(0xdf, 0x3e, 0x37), SoundID.MenuTick, SoundID.Roar, true, 0),
                ("PZ", "PZ", new Color(0x1f, 0x1e, 0x2e), new Color(0x00, 0x62, 0x5e), SoundID.MenuTick, SoundID.Roar, true, 0),
				("EaglecrestGolem2", "EaglecrestGolem2", new Color(0x40, 0x37, 0x38), null, SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Ukko", "Ukko", new Color(0xff, 0xe5, 0x7c), new Color(0xff, 0xf9, 0xe0), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Akka", "Akka", new Color(0x06, 0x33, 0x16), new Color(0x43, 0x71, 0x27), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Nebuleus", "Nebuleus", new Color(0x3c, 0x39, 0x75), new Color(0x72, 0x55, 0xbd), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Nebuleus2", "Nebuleus2", new Color(0x3c, 0x39, 0x75), new Color(0x72, 0x55, 0xbd), SoundID.MenuTick, SoundID.Roar, false, 0),
                ("Nebuleus_Clone", "Nebuleus_Clone", new Color(0xeb, 0x87, 0xdd), null, SoundID.MenuTick, SoundID.Roar, false, nebuleusCloneType),
                ("Nebuleus2_Clone", "Nebuleus2_Clone", new Color(0xeb, 0x87, 0xdd), null, SoundID.MenuTick, SoundID.Roar, false, nebuleus2CloneType),
            };

            foreach (var boss in bosses)
            {
                int npcType = boss.CustomType;
                
                if (npcType == 0 && boss.InternalName != "Nebuleus_Clone" && boss.InternalName != "Nebuleus2_Clone")
                {
                    npcType = Redemption.Find<ModNPC>(boss.InternalName)?.Type ?? 0;
                    if (npcType == 0)
                        npcType = Redemption.Find<ModNPC>(boss.InternalName + "Head")?.Type ?? 0;
                    if (npcType == 0)
                        npcType = Redemption.Find<ModNPC>(boss.InternalName + "Body")?.Type ?? 0;
                }
                
                if (npcType == 0 && boss.InternalName != "Nebuleus_Clone" && boss.InternalName != "Nebuleus2_Clone")
                    continue;

                Func<bool> condition;
                
                if (boss.HasDialogIntro)
                {
                    int capturedNpcType = boss.CustomType != 0 ? boss.CustomType : npcType;
                    condition = () => IsBossInCombat(capturedNpcType);
                }
                else
                {
                    int capturedType = boss.CustomType != 0 ? boss.CustomType : npcType;
                    condition = () => NPC.AnyNPCs(capturedType);
                }

                Func<float, float, Color> colorFunc;
                if (boss.Color2.HasValue)
                {
                    colorFunc = (progress, anim) => Color.Lerp(boss.Color1, boss.Color2.Value, anim);
                }
                else
                {
                    colorFunc = (progress, anim) => boss.Color1;
                }

                var text = Mod.GetLocalization($"Mods.RagnarokOfRedemptionAPI.TitleScreen.{boss.LocalizationKey}.Text");

                object introScreen = Infernum.Call(
                    "InitializeIntroScreen",
                    text,
                    450,
                    true,
                    condition,
                    colorFunc
                );

                Infernum.Call("SetupLetterAdditionSound", introScreen, (Func<SoundStyle>)(() => SoundID.MenuTick));
                Infernum.Call("IntroScreenSetupLetterAdditionSound", introScreen, new Func<SoundStyle>(() => boss.Tick));
                Infernum.Call("IntroScreenSetupMainSound", introScreen, 
                    new Func<int, int, float, float, bool>((t, _, __, ___) => t == 0), 
                    new Func<SoundStyle>(() => boss.End));

                Infernum.Call("IntroScreenSetupLetterDisplayCompletionRatio", introScreen, new Func<int, float>(t => t / 180f));
                Infernum.Call("IntroScreenSetupCompletionEffects", introScreen, new Action(() => { }));
                Infernum.Call("IntroScreenSetupTextScale", introScreen, 1.2f);

                Infernum.Call("RegisterIntroScreen", introScreen);
            }
        }
        
        public override void PostUpdateNPCs()
        {
            List<int> toRemove = new List<int>();
            foreach (int npcType in _bossesInCombat)
            {
                bool stillExists = false;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == npcType)
                    {
                        stillExists = true;
                        break;
                    }
                }
                if (!stillExists)
                    toRemove.Add(npcType);
            }
            
            foreach (int npcType in toRemove)
                _bossesInCombat.Remove(npcType);
        }
    }
}