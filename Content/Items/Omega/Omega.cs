using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RagnarokOfRedemptionAPI.Content.Items.Omega
{
    public class OmegaRecipeUnlockHandler : ModSystem
    {
        public static bool omega1Decrypted = false;
        public static bool omega2Decrypted = false;
        public static bool omega3Decrypted = false;

        public override void SaveWorldData(TagCompound tag)
        {
            tag["Omega1Decrypted"] = omega1Decrypted;
            tag["Omega2Decrypted"] = omega2Decrypted;
            tag["Omega3Decrypted"] = omega3Decrypted;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            omega1Decrypted = tag.GetBool("Omega1Decrypted");
            omega2Decrypted = tag.GetBool("Omega2Decrypted");
            omega3Decrypted = tag.GetBool("Omega3Decrypted");
        }

        public override void ClearWorld()
        {
            omega1Decrypted = false;
            omega2Decrypted = false;
            omega3Decrypted = false;
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = omega1Decrypted;
            flags[1] = omega2Decrypted;
            flags[2] = omega3Decrypted;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            omega1Decrypted = flags[0];
            omega2Decrypted = flags[1];
            omega3Decrypted = flags[2];
        }
    }

    public static class OmegaRecipeCondition
    {
        public static LocalizedText ConstructRecipeCondition(int tier, out Func<bool> condition)
        {
            condition = tier switch
            {
                1 => () => OmegaRecipeUnlockHandler.omega1Decrypted,
                2 => () => OmegaRecipeUnlockHandler.omega2Decrypted,
                3 => () => OmegaRecipeUnlockHandler.omega3Decrypted,
                _ => () => false
            };

            string key = tier switch
            {
                1 => "Mods.RagnarokOfRedemptionAPI.Conditions.OmegaTier1Decrypted",
                2 => "Mods.RagnarokOfRedemptionAPI.Conditions.OmegaTier2Decrypted",
                3 => "Mods.RagnarokOfRedemptionAPI.Conditions.OmegaTier3Decrypted",
                _ => "Mods.RagnarokOfRedemptionAPI.Conditions.OmegaDecrypted"
            };

            return Language.GetOrRegister(key);
        }
    }

    public static class OmegaData
    {
        private static Dictionary<int, int> _insertedSchematic = new Dictionary<int, int>();
        private static Dictionary<int, int> _decryptTimer = new Dictionary<int, int>();
        private static Dictionary<int, float> _decryptProgress = new Dictionary<int, float>();
        private static Dictionary<int, int> _totalPowerCost = new Dictionary<int, int>();
        private static Dictionary<int, int> _consumedPower = new Dictionary<int, int>();

        public static int GetInserted(TECodebreaker cb) => cb == null ? 0 : (_insertedSchematic.ContainsKey(cb.ID) ? _insertedSchematic[cb.ID] : 0);
        public static void SetInserted(TECodebreaker cb, int tier) { if (cb == null) return; if (tier == 0) _insertedSchematic.Remove(cb.ID); else _insertedSchematic[cb.ID] = tier; }
        
        public static int GetTimer(TECodebreaker cb) => cb == null ? 0 : (_decryptTimer.ContainsKey(cb.ID) ? _decryptTimer[cb.ID] : 0);
        public static void SetTimer(TECodebreaker cb, int timer) { if (cb == null) return; if (timer <= 0) _decryptTimer.Remove(cb.ID); else _decryptTimer[cb.ID] = timer; }
        
        public static float GetProgress(TECodebreaker cb) => cb == null ? 0f : (_decryptProgress.ContainsKey(cb.ID) ? _decryptProgress[cb.ID] : 0f);
        public static void SetProgress(TECodebreaker cb, float progress) { if (cb == null) return; if (progress <= 0) _decryptProgress.Remove(cb.ID); else _decryptProgress[cb.ID] = progress; }

        public static void StartDecrypt(TECodebreaker cb, int tier, int totalPowerCost)
        {
            int totalTime = 60 * 60;
            SetTimer(cb, totalTime);
            SetProgress(cb, 0f);
            _totalPowerCost[cb.ID] = totalPowerCost;
            _consumedPower[cb.ID] = 0;
            SetInserted(cb, tier);
        }

        public static void UpdateDecrypt(TECodebreaker cb)
        {
            int timer = GetTimer(cb);
            if (timer <= 0) return;

            timer--;
            SetTimer(cb, timer);

            int totalTime = 60 * 60;
            float newProgress = 1f - (float)timer / totalTime;
            SetProgress(cb, newProgress);

            if (_totalPowerCost.ContainsKey(cb.ID))
            {
                int totalCost = _totalPowerCost[cb.ID];
                int newConsumed = (int)(totalCost * newProgress);
                int oldConsumed = _consumedPower.ContainsKey(cb.ID) ? _consumedPower[cb.ID] : 0;
                int toConsume = newConsumed - oldConsumed;

                if (toConsume > 0 && cb.InputtedCellCount >= toConsume)
                {
                    cb.InputtedCellCount -= toConsume;
                    _consumedPower[cb.ID] = newConsumed;
                    cb.SyncContainedStuff();
                }
            }

            if (timer <= 0)
            {
                int tier = GetInserted(cb);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (tier == 1) OmegaRecipeUnlockHandler.omega1Decrypted = true;
                    if (tier == 2) OmegaRecipeUnlockHandler.omega2Decrypted = true;
                    if (tier == 3) OmegaRecipeUnlockHandler.omega3Decrypted = true;

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.WorldData);
                }

                string messageKey = $"Mods.RagnarokOfRedemptionAPI.Messages.OmegaTier{tier}Decrypted";
                string message = Language.GetTextValue(messageKey);
                
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player textPlayer = Main.player[i];
                    if (textPlayer.active)
                        CombatText.NewText(textPlayer.Hitbox, Color.Green, message, true);
                }

                _totalPowerCost.Remove(cb.ID);
                _consumedPower.Remove(cb.ID);
                SetInserted(cb, 0);
                
                Recipe.FindRecipes();
            }
        }
    }

    public class OmegaRecipePatcher : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod))
            {
                foreach (var recipe in Main.recipe)
                {
                    if (recipe.createItem.ModItem != null && recipe.createItem.ModItem.Mod == redemptionMod)
                    {
                        int tier = GetTierForItem(redemptionMod, recipe.createItem.type);
                        if (tier > 0)
                        {
                            recipe.AddCondition(OmegaRecipeCondition.ConstructRecipeCondition(tier, out Func<bool> condition), condition);
                        }
                    }
                }
            }

            if (ModLoader.HasMod("RedemptionBardHealer") && ModLoader.HasMod("ThoriumMod"))
            {
                if (ModLoader.TryGetMod("RedemptionBardHealer", out Mod bardHealerMod))
                {
                    foreach (var recipe in Main.recipe)
                    {
                        if (recipe.createItem.ModItem != null && recipe.createItem.ModItem.Mod == bardHealerMod)
                        {
                            int tier = GetTierForBardItem(bardHealerMod, recipe.createItem.type);
                            if (tier > 0)
                            {
                                recipe.AddCondition(OmegaRecipeCondition.ConstructRecipeCondition(tier, out Func<bool> condition), condition);
                            }
                        }
                    }
                }
            }
        }

        private int GetTierForItem(Mod redemptionMod, int itemType)
        {
            if (redemptionMod.TryFind<ModItem>("TinyCleaver", out ModItem i1) && itemType == i1.Type) return 1;
            if (redemptionMod.TryFind<ModItem>("PulseBlade", out ModItem i2) && itemType == i2.Type) return 1;
            if (redemptionMod.TryFind<ModItem>("CorruptedDoubleRifle", out ModItem i3) && itemType == i3.Type) return 1;

            if (redemptionMod.TryFind<ModItem>("CorruptedDAN", out ModItem i4) && itemType == i4.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("DrillRevolver", out ModItem i5) && itemType == i5.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("GigapeiliContactor", out ModItem i6) && itemType == i6.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("OmegaPickaxe", out ModItem i7) && itemType == i7.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("OversizedScrewdriver", out ModItem i8) && itemType == i8.Type) return 2;

            if (redemptionMod.TryFind<ModItem>("SunInThePalm", out ModItem i9) && itemType == i9.Type) return 3;
            if (redemptionMod.TryFind<ModItem>("OOFingergun", out ModItem i10) && itemType == i10.Type) return 3;
            if (redemptionMod.TryFind<ModItem>("BlastBattery", out ModItem i11) && itemType == i11.Type) return 3;
            
            return 0;
        }
        
        private int GetTierForBardItem(Mod bardHealerMod, int itemType)
        {
            if (bardHealerMod.TryFind<ModItem>("Shockriken", out ModItem shockriken) && itemType == shockriken.Type) return 2;
            if (bardHealerMod.TryFind<ModItem>("VlitchSynthesizer", out ModItem vlitchSynthesizer) && itemType == vlitchSynthesizer.Type) return 2;
            if (bardHealerMod.TryFind<ModItem>("KillswitchEngineer", out ModItem killswitchEngineer) && itemType == killswitchEngineer.Type) return 2;

            if (bardHealerMod.TryFind<ModItem>("ScorchingScalpel", out ModItem scorchingScalpel) && itemType == scorchingScalpel.Type) return 3;
            if (bardHealerMod.TryFind<ModItem>("Oboeterator", out ModItem oboeterator) && itemType == oboeterator.Type) return 3;
            if (bardHealerMod.TryFind<ModItem>("OmegaDefibrillator", out ModItem omegaDefibrillator) && itemType == omegaDefibrillator.Type) return 3;
            
            return 0;
        }
    }

    public class OmegaTooltipPatcher : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int tier = 0;
            Mod sourceMod = null;

            if (item.ModItem != null)
            {
                if (item.ModItem.Mod.Name == "Redemption")
                {
                    sourceMod = item.ModItem.Mod;
                    tier = GetTierForRedemptionItem(sourceMod, item.type);
                }
                else if (item.ModItem.Mod.Name == "RedemptionBardHealer" && ModLoader.HasMod("ThoriumMod"))
                {
                    sourceMod = item.ModItem.Mod;
                    tier = GetTierForBardItem(sourceMod, item.type);
                }
            }
            
            if (tier == 0) return;

            bool isDecrypted = tier switch
            {
                1 => OmegaRecipeUnlockHandler.omega1Decrypted,
                2 => OmegaRecipeUnlockHandler.omega2Decrypted,
                3 => OmegaRecipeUnlockHandler.omega3Decrypted,
                _ => false
            };

            if (!isDecrypted)
            {
                string message = "";

                string messageKey = tier switch
                {
                    1 => "Mods.RagnarokOfRedemptionAPI.Conditions.OmegaLocked.Tier1",
                    2 => "Mods.RagnarokOfRedemptionAPI.Conditions.OmegaLocked.Tier2",
                    3 => "Mods.RagnarokOfRedemptionAPI.Conditions.OmegaLocked.Tier3",
                    _ => ""
                };
                
                message = Language.GetTextValue(messageKey);

                if (string.IsNullOrEmpty(message) || message == messageKey)
                {
                    message = tier switch
                    {
                        1 => "You don't have sufficient knowledge to create this yet\nOmega Data Tablet 1 must be deciphered first",
                        2 => "You don't have sufficient knowledge to create this yet\nOmega Data Tablet 2 must be deciphered first",
                        3 => "You don't have sufficient knowledge to create this yet\nOmega Data Tablet 3 must be deciphered first",
                        _ => "You don't have sufficient knowledge to create this yet"
                    };
                }
                
                TooltipLine lockedLine = new TooltipLine(Mod, "OmegaLocked", message);
                lockedLine.OverrideColor = Color.Red;
                tooltips.Add(lockedLine);
            }
        }

        private int GetTierForRedemptionItem(Mod redemptionMod, int itemType)
        {
            if (redemptionMod.TryFind<ModItem>("TinyCleaver", out ModItem i1) && itemType == i1.Type) return 1;
            if (redemptionMod.TryFind<ModItem>("PulseBlade", out ModItem i2) && itemType == i2.Type) return 1;
            if (redemptionMod.TryFind<ModItem>("CorruptedDoubleRifle", out ModItem i3) && itemType == i3.Type) return 1;

            if (redemptionMod.TryFind<ModItem>("CorruptedDAN", out ModItem i4) && itemType == i4.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("DrillRevolver", out ModItem i5) && itemType == i5.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("GigapeiliContactor", out ModItem i6) && itemType == i6.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("OmegaPickaxe", out ModItem i7) && itemType == i7.Type) return 2;
            if (redemptionMod.TryFind<ModItem>("OversizedScrewdriver", out ModItem i8) && itemType == i8.Type) return 2;

            if (redemptionMod.TryFind<ModItem>("SunInThePalm", out ModItem i9) && itemType == i9.Type) return 3;
            if (redemptionMod.TryFind<ModItem>("OOFingergun", out ModItem i10) && itemType == i10.Type) return 3;
            if (redemptionMod.TryFind<ModItem>("BlastBattery", out ModItem i11) && itemType == i11.Type) return 3;
            
            return 0;
        }
        
        private int GetTierForBardItem(Mod bardHealerMod, int itemType)
        {
            if (bardHealerMod.TryFind<ModItem>("Shockriken", out ModItem shockriken) && itemType == shockriken.Type) return 2;
            if (bardHealerMod.TryFind<ModItem>("VlitchSynthesizer", out ModItem vlitchSynthesizer) && itemType == vlitchSynthesizer.Type) return 2;
            if (bardHealerMod.TryFind<ModItem>("KillswitchEngineer", out ModItem killswitchEngineer) && itemType == killswitchEngineer.Type) return 2;

            if (bardHealerMod.TryFind<ModItem>("ScorchingScalpel", out ModItem scorchingScalpel) && itemType == scorchingScalpel.Type) return 3;
            if (bardHealerMod.TryFind<ModItem>("Oboeterator", out ModItem oboeterator) && itemType == oboeterator.Type) return 3;
            if (bardHealerMod.TryFind<ModItem>("OmegaDefibrillator", out ModItem omegaDefibrillator) && itemType == omegaDefibrillator.Type) return 3;
            
            return 0;
        }
    }

    public abstract class OmegaDataTablet : ModItem
    {
        public abstract int TabletTier { get; }
        public abstract List<string> GetBlockedWeaponKeys();

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.rare = ItemRarityID.Red;
            Item.maxStack = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            bool isDecrypted = TabletTier switch
            {
                1 => OmegaRecipeUnlockHandler.omega1Decrypted,
                2 => OmegaRecipeUnlockHandler.omega2Decrypted,
                3 => OmegaRecipeUnlockHandler.omega3Decrypted,
                _ => false
            };

            int nameIndex = tooltips.FindIndex(x => x.Name == "ItemName" && x.Mod == "Terraria");
            if (nameIndex != -1)
            {
                string displayNameKey = $"Mods.RagnarokOfRedemptionAPI.Items.Omega{TabletTier}DataTablet.DisplayName";
                string displayName = Language.GetTextValue(displayNameKey);
                if (displayName == displayNameKey)
                {
                    displayName = $"Omega Data Tablet {TabletTier}";
                }
                tooltips[nameIndex].Text = displayName;
            }

            int tooltipIndex = tooltips.FindIndex(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
            if (tooltipIndex != -1)
            {
                if (!isDecrypted)
                {
                    string tooltipKey = $"Mods.RagnarokOfRedemptionAPI.Items.Omega{TabletTier}DataTablet.Tooltip";
                    string tooltipText = Language.GetTextValue(tooltipKey);
                    if (tooltipText == tooltipKey)
                    {
                        tooltipText = "Requires a Codebreaker with a Girus Decoder to decrypt";
                    }
                    tooltips[tooltipIndex].Text = tooltipText;
                }
                else
                {
                    tooltips[tooltipIndex].Text = "already decrypted";
                    
                    TooltipLine unlockedLine = new TooltipLine(Mod, "UnlockedRecipes", "unlocked recipes");
                    tooltips.Insert(tooltipIndex + 1, unlockedLine);
                    
                    int insertIndex = tooltipIndex + 2;
                    foreach (string weaponKey in GetBlockedWeaponKeys())
                    {
                        string weaponNameKey = $"Mods.RagnarokOfRedemptionAPI.Items.Omega{TabletTier}DataTablet.Weapon.{weaponKey}";
                        string weaponName = Language.GetTextValue(weaponNameKey);

                        if (weaponName == weaponNameKey)
                        {
                            weaponName = weaponKey;
                        }

                        int itemType = 0;

                        if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod))
                        {
                            if (redemptionMod.TryFind<ModItem>(weaponKey, out ModItem modItem))
                            {
                                itemType = modItem.Type;
                            }
                        }

                        if (itemType == 0 && ModLoader.HasMod("RedemptionBardHealer"))
                        {
                            Mod bardHealerMod = ModLoader.GetMod("RedemptionBardHealer");
                            if (bardHealerMod.TryFind<ModItem>(weaponKey, out ModItem bardItem))
                            {
                                itemType = bardItem.Type;
                            }
                        }

                        string weaponDisplayText = itemType > 0 ? $"[i:{itemType}] {weaponName}" : weaponName;
                        
                        TooltipLine weaponLine = new TooltipLine(Mod, "OmegaWeapon", weaponDisplayText);
                        weaponLine.OverrideColor = Color.Red;
                        tooltips.Insert(insertIndex, weaponLine);
                        insertIndex++;
                    }
                }
            }
        }
    }
}