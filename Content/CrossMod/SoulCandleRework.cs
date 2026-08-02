using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Redemption.Items.Placeable.Furniture.Misc;
using System.Collections.Generic;
using System.Reflection;
using Terraria.Localization;

namespace RagnarokOfRedemptionAPI.Content.CrossMod
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class CalamitySoulCandleRework : GlobalItem
    {
        private static bool _calamityLoaded = false;
        private static int _essenceEleum = 0;
        private static int _essenceHavoc = 0;
        private static int _essenceSunlight = 0;

        private static bool _initialized = false;

        public override void Load()
        {
            if (!_initialized)
            {
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                {
                    _calamityLoaded = true;
                    _essenceEleum = calamity.TryFind<ModItem>("EssenceofEleum", out var eleum) ? eleum.Type : 0;
                    _essenceHavoc = calamity.TryFind<ModItem>("EssenceofHavoc", out var havoc) ? havoc.Type : 0;
                    _essenceSunlight = calamity.TryFind<ModItem>("EssenceofSunlight", out var sunlight) ? sunlight.Type : 0;
                }
                _initialized = true;
            }
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<SoulCandle>();
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (!_calamityLoaded) return;

            try
            {
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                {
                    var calamityPlayerType = calamity.GetType().Assembly.GetType("CalamityMod.CalPlayer.CalamityPlayer");
                    if (calamityPlayerType != null)
                    {
                        var modPlayersField = typeof(Player).GetField("modPlayers", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (modPlayersField != null)
                        {
                            var modPlayers = modPlayersField.GetValue(player) as System.Collections.Generic.List<ModPlayer>;
                            if (modPlayers != null)
                            {
                                foreach (var modPlayer in modPlayers)
                                {
                                    if (modPlayer.GetType() == calamityPlayerType)
                                    {
                                        var dropRateField = calamityPlayerType.GetField("essenceDropRate", BindingFlags.Public | BindingFlags.Instance);
                                        if (dropRateField != null)
                                        {
                                            float currentRate = (float)dropRateField.GetValue(modPlayer);
                                            dropRateField.SetValue(modPlayer, currentRate + 0.1f);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type != ModContent.ItemType<SoulCandle>()) return;
            if (!_calamityLoaded) return;

            int insertIndex = tooltips.Count;
            for (int i = tooltips.Count - 1; i >= 0; i--)
            {
                if (tooltips[i].Name.StartsWith("Tooltip"))
                {
                    insertIndex = i + 1;
                    break;
                }
            }

            string text = Language.GetTextValue("Mods.RagnarokOfRedemptionAPI.ItemTooltips.SoulCandle.CalamityBonus");
            TooltipLine calamityLine = new TooltipLine(Mod, "SoulCandleCalamityBonus", text);
            tooltips.Insert(insertIndex, calamityLine);
        }
    }

    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class ThoriumSoulCandleRework : GlobalItem
    {
        private static bool _thoriumLoaded = false;
        private static int _soulOfPlight = 0;

        private static bool _initialized = false;

        public override void Load()
        {
            if (!_initialized)
            {
                if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
                {
                    _thoriumLoaded = true;
                    _soulOfPlight = thorium.TryFind<ModItem>("SoulofPlight", out var plight) ? plight.Type : 0;
                }
                _initialized = true;
            }
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<SoulCandle>();
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (!_thoriumLoaded || _soulOfPlight <= 0) return;

            try
            {
                if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
                {
                    var thoriumPlayerType = thorium.GetType().Assembly.GetType("ThoriumMod.ThoriumPlayer");
                    if (thoriumPlayerType != null)
                    {
                        var modPlayersField = typeof(Player).GetField("modPlayers", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (modPlayersField != null)
                        {
                            var modPlayers = modPlayersField.GetValue(player) as System.Collections.Generic.List<ModPlayer>;
                            if (modPlayers != null)
                            {
                                foreach (var modPlayer in modPlayers)
                                {
                                    if (modPlayer.GetType() == thoriumPlayerType)
                                    {
                                        var plightDropField = thoriumPlayerType.GetField("soulPlightDropRate", BindingFlags.Public | BindingFlags.Instance);
                                        if (plightDropField != null)
                                        {
                                            float currentRate = (float)plightDropField.GetValue(modPlayer);
                                            plightDropField.SetValue(modPlayer, currentRate + 0.1f);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type != ModContent.ItemType<SoulCandle>()) return;
            if (!_thoriumLoaded || _soulOfPlight <= 0) return;

            int insertIndex = tooltips.Count;
            for (int i = tooltips.Count - 1; i >= 0; i--)
            {
                if (tooltips[i].Name.StartsWith("Tooltip"))
                {
                    insertIndex = i + 1;
                    break;
                }
            }

            string text = Language.GetTextValue("Mods.RagnarokOfRedemptionAPI.ItemTooltips.SoulCandle.ThoriumBonus");
            TooltipLine thoriumLine = new TooltipLine(Mod, "SoulCandleThoriumBonus", text);
            tooltips.Insert(insertIndex, thoriumLine);
        }
    }

    [JITWhenModsEnabled("ContinentOfJourney")]
    [ExtendsFromMod("ContinentOfJourney")]
    public class JourneySoulCandleRework : GlobalItem
    {
        private static bool _journeyLoaded = false;
        private static int _essenceDarkness = 0;
        private static int _essenceLife = 0;
        private static int _essenceMatter = 0;
        private static int _essenceTime = 0;

        private static bool _initialized = false;

        public override void Load()
        {
            if (!_initialized)
            {
                if (ModLoader.TryGetMod("ContinentOfJourney", out Mod journey))
                {
                    _journeyLoaded = true;
                    _essenceDarkness = journey.TryFind<ModItem>("EssenceofDarkness", out var darkness) ? darkness.Type : 0;
                    _essenceLife = journey.TryFind<ModItem>("EssenceofLife", out var life) ? life.Type : 0;
                    _essenceMatter = journey.TryFind<ModItem>("EssenceofMatter", out var matter) ? matter.Type : 0;
                    _essenceTime = journey.TryFind<ModItem>("EssenceofTime", out var time) ? time.Type : 0;
                }
                _initialized = true;
            }
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<SoulCandle>();
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (!_journeyLoaded) return;

            try
            {
                var journeyPlayer = player.GetModPlayer<ContinentOfJourney.TemplatePlayer>();
                if (journeyPlayer != null)
                {
                    var essenceDropBonusField = typeof(ContinentOfJourney.TemplatePlayer).GetField("essenceDropBonus", BindingFlags.Public | BindingFlags.Instance);
                    if (essenceDropBonusField != null)
                    {
                        float currentBonus = (float)essenceDropBonusField.GetValue(journeyPlayer);
                        essenceDropBonusField.SetValue(journeyPlayer, currentBonus + 0.1f);
                    }
                }
            }
            catch { }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type != ModContent.ItemType<SoulCandle>()) return;
            if (!_journeyLoaded) return;

            int insertIndex = tooltips.Count;
            for (int i = tooltips.Count - 1; i >= 0; i--)
            {
                if (tooltips[i].Name.StartsWith("Tooltip"))
                {
                    insertIndex = i + 1;
                    break;
                }
            }

            string text = Language.GetTextValue("Mods.RagnarokOfRedemptionAPI.ItemTooltips.SoulCandle.JourneyBonus");
            TooltipLine continentLine = new TooltipLine(Mod, "SoulCandleJourneyBonus", text);
            tooltips.Insert(insertIndex, continentLine);
        }
    }
}