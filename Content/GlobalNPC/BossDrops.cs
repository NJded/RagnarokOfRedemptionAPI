using Terraria;
using Terraria.ModLoader;
using RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR;
using RagnarokOfRedemptionAPI.Content.Items.Omega;
using RagnarokOfRedemptionAPI.Content.Items.LOOOOOOOORE;
using Redemption.NPCs.Bosses.ADD;
using Redemption.NPCs.Bosses.Cleaver;
using Redemption.NPCs.Bosses.Erhan;
using Redemption.NPCs.Bosses.Gigapora;
using Redemption.NPCs.Bosses.Keeper;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.Obliterator;
using Redemption.NPCs.Bosses.PatientZero;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.Neb.Clone;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.NPCs.Minibosses.Calavia;
using Redemption.NPCs.Minibosses.EaglecrestGolem;
using Redemption.NPCs.Minibosses.FowlEmperor;
using Redemption.NPCs.Minibosses.SkullDigger;
using Redemption.NPCs.FowlMorning;
using System.Collections.Generic;
using InfernumSaveSystem = InfernumMode.Core.GlobalInstances.Systems.WorldSaveSystem;
using System;

namespace RagnarokOfRedemptionAPI.Content.NPCDrops
{
    public class BossDefeatTracker : ModSystem
    {
        public static HashSet<int> defeatedBosses = new HashSet<int>();
        
        public override void OnWorldLoad()
        {
            defeatedBosses.Clear();
        }
        
        public static bool IsFirstKill(int npcType)
        {
            return !defeatedBosses.Contains(npcType);
        }
        
        public static void RegisterDefeat(int npcType)
        {
            defeatedBosses.Add(npcType);
        }
    }

    public class BossDrops : ModSystem
    {
        public static bool ancientDeityDuoDefeated = false;
        public override void OnWorldLoad()
        {
            ancientDeityDuoDefeated = false;
        }
    }

    public static class DifficultyHelper
    {
        public static bool IsInfernumOrRagnarok()
        {
            bool isInfernum = false;
            try
            {
                isInfernum = InfernumSaveSystem.InfernumModeEnabled;
            }
            catch { }

            bool isRagnarok = false;
            try
            {
                if (ModLoader.TryGetMod("InfernalEclipse", out Mod infernalEclipse))
                {
                    var property = infernalEclipse.GetType().GetProperty("RagnarokMode");
                    if (property != null)
                    {
                        isRagnarok = (bool)property.GetValue(null);
                    }
                }
            }
            catch { }

            return isInfernum || isRagnarok;
        }
    }

    public class CalaviaRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Calavia>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<CalaviaRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<CalaviaLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class EaglecrestGolemRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<EaglecrestGolem>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EaglecrestGolemRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EaglecrestGolemLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class ErhanRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Erhan>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ErhanRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ErhanLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class ErhanSpiritRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<ErhanSpirit>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ErhanRelic>());
            }
        }
    }

    public class FowlEmperorRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<FowlEmperor>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FowlEmperorRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FowlEmperorLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class KeeperRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Keeper>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<KeeperRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<TheKeeperLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class KeeperSpiritRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<KeeperSpirit>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<KeeperRelic>());
            }
        }
    }

    public class KingSlayerIIIRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<KS3>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<KingSlayerIIIRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<KingSlayerIIILore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class KS3_CloneRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<KS3_Clone>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<KingSlayerIIIRelic>());
            }
        }
    }

    public class NebuleusRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Nebuleus>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<NebuleusRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<NebuleusLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class NebuleusCloneRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Nebuleus_Clone>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<NebuleusRelic>());
            }
        }
    }

    public class Nebuleus2RelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Nebuleus2>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<NebuleusRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<NebuleusLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class Nebuleus2CloneRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Nebuleus2_Clone>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<NebuleusRelic>());
            }
        }
    }

    public class OmegaCleaverRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<OmegaCleaver>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<OmegaCleaverRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<stOmegaPrototypeLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Omega1DataBrokenTablet>());
        }
    }

    public class OmegaGigaporaRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Gigapora>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<OmegaGigaporaRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ndOmegaPrototypeLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Omega2DataBrokenTablet>());
        }
    }

    public class OmegaObliteratorRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<OO>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<OmegaObliteratorRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<rdOmegaPrototypeLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Omega3DataBrokenTablet>());
        }
    }

    public class PatientZeroRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<PZ>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PatientZeroRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PatientZeroLore>());
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<AbandonedLaboratoryLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class SoIRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<SoI>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SeedofInfectionRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SeedofInfectionLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class SkullDiggerRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<SkullDigger>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SkullDiggerRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SkullDiggerLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class ThornRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Thorn>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ThornRelic>());
            }
            if (BossDefeatTracker.IsFirstKill(npc.type))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<ThornLore>());
                BossDefeatTracker.RegisterDefeat(npc.type);
            }
        }
    }

    public class BasanRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Basan>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<BasanRelic>());
            }
        }
    }

    public class CockatriceRelicDrop : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Cockatrice>();
        }

        public override void OnKill(NPC npc)
        {
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<CockatriceRelic>());
            }
        }
    }

    public class AncientDeityDuoRelicDrop : GlobalNPC
    {
        private static bool _isProcessing = false;
        
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<Ukko>() || entity.type == ModContent.NPCType<Akka>();
        }
        
        public override void OnKill(NPC npc)
        {
            bool isUkko = npc.type == ModContent.NPCType<Ukko>();
            bool isAkka = npc.type == ModContent.NPCType<Akka>();
            
            if (DifficultyHelper.IsInfernumOrRagnarok())
            {
                if (isUkko)
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<UkkoRelic>());
                else if (isAkka)
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<AkkaRelic>());
            }
            
            if (_isProcessing) return;
            _isProcessing = true;
            
            try
            {
                bool otherAlive = false;
                int otherType = isUkko ? ModContent.NPCType<Akka>() : ModContent.NPCType<Ukko>();
                
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == otherType)
                    {
                        otherAlive = true;
                        break;
                    }
                }

                if (!otherAlive && !BossDrops.ancientDeityDuoDefeated)
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<AncientDeityDuoLore>());
                    BossDrops.ancientDeityDuoDefeated = true;
                }
            }
            finally
            {
                _isProcessing = false;
            }
        }
    }
}