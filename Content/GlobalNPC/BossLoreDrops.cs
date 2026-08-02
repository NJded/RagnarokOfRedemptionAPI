using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using CalamityMod;
using RagnarokOfRedemptionAPI.Content.Items.LOOOOOOOORE;
using RagnarokOfRedemptionAPI.Content.Items.Omega;
using RagnarokOfRedemptionAPI.Systems;
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

namespace RagnarokOfRedemptionAPI.Content.Global
{
    public class GlobalLoreDrop : GlobalNPC
    {
        private static bool _isProcessingAncientDuo = false;

        public override void ModifyNPCLoot(Terraria.NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc.type == ModContent.NPCType<Calavia>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedCalavia,
                    ModContent.ItemType<CalaviaLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<EaglecrestGolem>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedEaglecrestGolem,
                    ModContent.ItemType<EaglecrestGolemLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<Erhan>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedErhan,
                    ModContent.ItemType<ErhanLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<ErhanSpirit>())
            {
                
            }

            if (npc.type == ModContent.NPCType<FowlEmperor>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedFowlEmperor,
                    ModContent.ItemType<FowlEmperorLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<Keeper>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedKeeper,
                    ModContent.ItemType<TheKeeperLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<KeeperSpirit>())
            {
                
            }

            if (npc.type == ModContent.NPCType<KS3>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedSlayer,
                    ModContent.ItemType<KingSlayerIIILore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<KS3_Clone>())
            {
                
            }

            if (npc.type == ModContent.NPCType<Nebuleus>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedNebuleus,
                    ModContent.ItemType<NebuleusLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<Nebuleus_Clone>())
            {
                
            }

            if (npc.type == ModContent.NPCType<Nebuleus2>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedNebuleus2,
                    ModContent.ItemType<NebuleusLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<Nebuleus2_Clone>())
            {
                
            }

            if (npc.type == ModContent.NPCType<OmegaCleaver>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedOmega1,
                    ModContent.ItemType<stOmegaPrototypeLore>(), true, DropHelper.FirstKillText);
                
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Omega1DataBrokenTablet>()));
            }

            if (npc.type == ModContent.NPCType<Gigapora>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedOmega2,
                    ModContent.ItemType<ndOmegaPrototypeLore>(), true, DropHelper.FirstKillText);
                
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Omega2DataBrokenTablet>()));
            }

            if (npc.type == ModContent.NPCType<OO>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedOmega3,
                    ModContent.ItemType<rdOmegaPrototypeLore>(), true, DropHelper.FirstKillText);
                
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Omega3DataBrokenTablet>()));
            }

            if (npc.type == ModContent.NPCType<PZ>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedPZ,
                    ModContent.ItemType<PatientZeroLore>(), true, DropHelper.FirstKillText);
                
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedPZ,
                    ModContent.ItemType<AbandonedLaboratoryLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<SoI>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedSeed,
                    ModContent.ItemType<SeedofInfectionLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<SkullDigger>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedSkullDigger,
                    ModContent.ItemType<SkullDiggerLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<Thorn>())
            {
                npcLoot.AddConditionalPerPlayer(() => !RedemptionDownedBossSystem.downedThorn,
                    ModContent.ItemType<ThornLore>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<Ukko>() || npc.type == ModContent.NPCType<Akka>())
            {
                
            }
        }

        public override void OnKill(NPC npc)
        {
            
            if (npc.type == ModContent.NPCType<Calavia>())
            {
                RedemptionDownedBossSystem.downedCalavia = true;
            }

            else if (npc.type == ModContent.NPCType<EaglecrestGolem>())
            {
                RedemptionDownedBossSystem.downedEaglecrestGolem = true;
            }

            else if (npc.type == ModContent.NPCType<Erhan>())
            {
                RedemptionDownedBossSystem.downedErhan = true;
            }

            else if (npc.type == ModContent.NPCType<ErhanSpirit>())
            {
                
            }

            else if (npc.type == ModContent.NPCType<FowlEmperor>())
            {
                RedemptionDownedBossSystem.downedFowlEmperor = true;
            }

            else if (npc.type == ModContent.NPCType<Keeper>())
            {
                RedemptionDownedBossSystem.downedKeeper = true;
            }

            else if (npc.type == ModContent.NPCType<KeeperSpirit>())
            {
                
            }

            else if (npc.type == ModContent.NPCType<KS3>())
            {
                RedemptionDownedBossSystem.downedSlayer = true;
            }

            else if (npc.type == ModContent.NPCType<KS3_Clone>())
            {
                
            }

            else if (npc.type == ModContent.NPCType<Nebuleus>())
            {
                RedemptionDownedBossSystem.downedNebuleus = true;
            }

            else if (npc.type == ModContent.NPCType<Nebuleus_Clone>())
            {
                
            }

            else if (npc.type == ModContent.NPCType<Nebuleus2>())
            {
                RedemptionDownedBossSystem.downedNebuleus2 = true;
            }

            else if (npc.type == ModContent.NPCType<Nebuleus2_Clone>())
            {
                
            }

            else if (npc.type == ModContent.NPCType<OmegaCleaver>())
            {
                RedemptionDownedBossSystem.downedOmega1 = true;
            }

            else if (npc.type == ModContent.NPCType<Gigapora>())
            {
                RedemptionDownedBossSystem.downedOmega2 = true;
            }

            else if (npc.type == ModContent.NPCType<OO>())
            {
                RedemptionDownedBossSystem.downedOmega3 = true;
            }

            else if (npc.type == ModContent.NPCType<PZ>())
            {
                RedemptionDownedBossSystem.downedPZ = true;
            }

            else if (npc.type == ModContent.NPCType<SoI>())
            {
                RedemptionDownedBossSystem.downedSeed = true;
            }

            else if (npc.type == ModContent.NPCType<SkullDigger>())
            {
                RedemptionDownedBossSystem.downedSkullDigger = true;
            }

            else if (npc.type == ModContent.NPCType<Thorn>())
            {
                RedemptionDownedBossSystem.downedThorn = true;
            }

            else if (npc.type == ModContent.NPCType<Ukko>() || npc.type == ModContent.NPCType<Akka>())
            {
                ProcessAncientDeityDuoDeath(npc);
            }
        }

        private void ProcessAncientDeityDuoDeath(NPC npc)
        {
            
            if (_isProcessingAncientDuo) return;
            _isProcessingAncientDuo = true;

            try
            {
                bool isUkko = npc.type == ModContent.NPCType<Ukko>();
                int otherType = isUkko ? ModContent.NPCType<Akka>() : ModContent.NPCType<Ukko>();

                bool otherAlive = false;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == otherType)
                    {
                        otherAlive = true;
                        break;
                    }
                }

                if (!otherAlive && !RedemptionDownedBossSystem.downedADD)
                {
                    
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<AncientDeityDuoLore>());
                    RedemptionDownedBossSystem.downedADD = true;
                }

                if (isUkko)
                {
                    RedemptionDownedBossSystem.downedUkko = true;
                }
                else
                {
                    RedemptionDownedBossSystem.downedAkka = true;
                }
            }
            finally
            {
                _isProcessingAncientDuo = false;
            }
        }
    }
}
