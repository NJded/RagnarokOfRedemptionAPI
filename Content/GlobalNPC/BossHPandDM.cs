using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace RagnarokOfRedemptionAPI.Content.NPCDrops
{
    public class BossHPandDM : GlobalNPC
    {
        private static bool? _calamity;
        private static bool? _infernum;
        private static bool? _thorium;
        private static bool? _sots;

        private static bool Calamity => _calamity ??= ModLoader.HasMod("CalamityMod");
        private static bool Infernum => _infernum ??= ModLoader.HasMod("InfernumMode");
        private static bool Thorium => _thorium ??= ModLoader.HasMod("ThoriumMod");
        private static bool Sots => _sots ??= ModLoader.HasMod("SOTS");

        private static readonly HashSet<string> bossNames = new()
        {
            "Akka","Ukko","OmegaCleaver","Wielder","Erhan","Erhan_Spirit","Gigapora",
            "Gigapora_BodySegment_Core","Gigapora_BodySegment_Tail","Gigapora_ShieldCore","Keeper","KeeperSpirit",
            "KS3","Nebuleus","Nebuleus2","OO","PZ","SoI","Thorn",
			"ProtectorVolt","IrradiatedBehemoth","JanitorBot","BlisteredFish","MACEProject",
			"SkullDigger","FowlEmperor","EaglecrestGolem","Calavia"
        };

        private static readonly HashSet<string> universalBosses = new()
        {
            "Akka","Ukko","Nebuleus","Nebuleus2","OO","PZ"
        };

        public override void SetDefaults(NPC npc)
        {

            if (npc.ModNPC != null && npc.ModNPC.Mod.Name == "Redemption" && bossNames.Contains(npc.ModNPC.Name))
            {
                float hpMult = 1f;
                float dmgMult = 1f;
                float universalMult = 1f;

                if (Calamity) hpMult += 0.20f;
                if (Infernum) hpMult += 0.25f;
                if (Thorium) hpMult += 0.10f;
                if (Sots) hpMult += 0.05f;

                if (Calamity) dmgMult += 0.10f;
                if (Infernum) dmgMult += 0.15f;
                if (Thorium) dmgMult += 0.10f;
                if (Sots) dmgMult += 0.05f;

                if (universalBosses.Contains(npc.ModNPC.Name))
                {
                    if (Calamity) universalMult += 0.05f;
                    if (Infernum) universalMult += 0.05f;
                }

                npc.lifeMax = (int)(npc.lifeMax * hpMult * universalMult);
                npc.damage = (int)(npc.damage * dmgMult);
            }
        }
    }
}