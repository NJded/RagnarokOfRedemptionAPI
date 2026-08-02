using Terraria.ModLoader;

namespace RagnarokOfRedemptionAPI.Systems
{
    public class RedemptionDownedBossSystem : ModSystem
    {
        public static bool downedCalavia = false;
        public static bool downedEaglecrestGolem = false;
        public static bool downedErhan = false;
        public static bool downedFowlEmperor = false;
        public static bool downedKeeper = false;
        public static bool downedSlayer = false;
        public static bool downedNebuleus = false;
        public static bool downedNebuleus2 = false;
        public static bool downedOmega1 = false;
        public static bool downedOmega2 = false;
        public static bool downedOmega3 = false;
        public static bool downedPZ = false;
        public static bool downedSeed = false;
        public static bool downedSkullDigger = false;
        public static bool downedThorn = false;
        public static bool downedBasan = false;
        public static bool downedCockatrice = false;
        public static bool downedUkko = false;
        public static bool downedAkka = false;
        public static bool downedADD = false;

        public override void OnWorldLoad()
        {
            downedCalavia = false;
            downedEaglecrestGolem = false;
            downedErhan = false;
            downedFowlEmperor = false;
            downedKeeper = false;
            downedSlayer = false;
            downedNebuleus = false;
            downedNebuleus2 = false;
            downedOmega1 = false;
            downedOmega2 = false;
            downedOmega3 = false;
            downedPZ = false;
            downedSeed = false;
            downedSkullDigger = false;
            downedThorn = false;
            downedBasan = false;
            downedCockatrice = false;
            downedUkko = false;
            downedAkka = false;
            downedADD = false;
        }

        public override void OnWorldUnload()
        {
            OnWorldLoad();
        }
    }
}