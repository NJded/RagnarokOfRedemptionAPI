using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using CalamityMod;
using RagnarokOfRedemptionAPI.Content.Items.Placeables.Relics.MoR;
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
using InfernumSaveSystem = InfernumMode.Core.GlobalInstances.Systems.WorldSaveSystem;

namespace RagnarokOfRedemptionAPI.Content.Global
{
    [JITWhenModsEnabled("InfernumMode")]
    [ExtendsFromMod("InfernumMode")]
    public class GlobalRelicsDrop : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void ModifyNPCLoot(Terraria.NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            static bool isInfernum() => InfernumSaveSystem.InfernumModeEnabled;

            if (npc.type == ModContent.NPCType<Calavia>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<CalaviaRelic>());

            if (npc.type == ModContent.NPCType<EaglecrestGolem>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<EaglecrestGolemRelic>());

            if (npc.type == ModContent.NPCType<Erhan>() || npc.type == ModContent.NPCType<ErhanSpirit>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<ErhanRelic>());

            if (npc.type == ModContent.NPCType<FowlEmperor>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<FowlEmperorRelic>());

            if (npc.type == ModContent.NPCType<Keeper>() || npc.type == ModContent.NPCType<KeeperSpirit>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<KeeperRelic>());

            if (npc.type == ModContent.NPCType<KS3>() || npc.type == ModContent.NPCType<KS3_Clone>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<KingSlayerIIIRelic>());

            if (npc.type == ModContent.NPCType<Nebuleus>() || 
                npc.type == ModContent.NPCType<Nebuleus_Clone>() ||
                npc.type == ModContent.NPCType<Nebuleus2>() ||
                npc.type == ModContent.NPCType<Nebuleus2_Clone>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<NebuleusRelic>());

            if (npc.type == ModContent.NPCType<OmegaCleaver>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<OmegaCleaverRelic>());

            if (npc.type == ModContent.NPCType<Gigapora>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<OmegaGigaporaRelic>());

            if (npc.type == ModContent.NPCType<OO>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<OmegaObliteratorRelic>());

            if (npc.type == ModContent.NPCType<PZ>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<PatientZeroRelic>());

            if (npc.type == ModContent.NPCType<SoI>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<SeedofInfectionRelic>());

            if (npc.type == ModContent.NPCType<SkullDigger>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<SkullDiggerRelic>());

            if (npc.type == ModContent.NPCType<Thorn>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<ThornRelic>());

            if (npc.type == ModContent.NPCType<Basan>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<BasanRelic>());

            if (npc.type == ModContent.NPCType<Cockatrice>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<CockatriceRelic>());

            if (npc.type == ModContent.NPCType<Ukko>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<UkkoRelic>());

            if (npc.type == ModContent.NPCType<Akka>())
                npcLoot.AddIf(isInfernum, ModContent.ItemType<AkkaRelic>());
        }
    }
}