using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.Localization;
using CalamityMod.TileEntities;
using CalamityMod;
using CalamityMod.UI.DraedonSummoning;
using CalamityMod.Items.DraedonMisc;
using RagnarokOfRedemptionAPI.Systems;
using RagnarokOfRedemptionAPI.Content.Items.Omega;

namespace RagnarokOfRedemptionAPI.UI
{
    public class GirusDecoderUI : ModSystem
    {
        private static bool IsUIActive = false;
        private static int ActiveOmegaSchematicID = 0;
        private static int DecryptionCountdown = 0;
        private static int DecryptionTotalTime = 0;
        private static float DecryptionProgress = 0f;
        private static bool IsDecrypting = false;
        private static float DecryptButtonScale = 1f;
        private static TECodebreaker CurrentCodebreaker = null;
        private static int CurrentCodebreakerID = -1;

        private static int TotalCellsRequired = 0;
        private static int CellsConsumedSoFar = 0;
        private static int CellsToConsumePerTick = 0;
        
        private static Texture2D cachedBackground;
        private static Texture2D cachedSlotBackground;
        private static Texture2D cachedDecryptIcon;
        private static Texture2D cachedBarBorder;
        private static Texture2D cachedBarFill;
        private static Texture2D cellTexture;
        
        private static bool texturesLoaded = false;
        
        private static readonly SoundStyle OmegaDecryptSound = SoundID.Item4;

        private static readonly Color DecryptPercentColor = new Color(255, 146, 135);

        private static Dictionary<int, int> DecryptionCosts = new Dictionary<int, int>
        {
            { ModContent.ItemType<Omega1DataTablet>(), 1100 },
            { ModContent.ItemType<Omega2DataTablet>(), 1550 },
            { ModContent.ItemType<Omega3DataTablet>(), 2700 }
        };

        private static Dictionary<int, int> DecryptionTimes = new Dictionary<int, int>
        {
            { ModContent.ItemType<Omega1DataTablet>(), 1500 },
            { ModContent.ItemType<Omega2DataTablet>(), 2400 },
            { ModContent.ItemType<Omega3DataTablet>(), 3600 }
        };

        public override void Load()
        {
            if (!Main.dedServ)
            {
                try
                {
                    cachedBackground = TryLoadTexture("RagnarokOfRedemptionAPI/Content/Items/Omega/GirusDecrypterBackground");
                    cachedSlotBackground = TryLoadTexture("RagnarokOfRedemptionAPI/Content/Items/Omega/EncryptedOmegaSchematicSlotBackground");
                    cachedDecryptIcon = TryLoadTexture("RagnarokOfRedemptionAPI/Content/Items/Omega/OmegaDecryptIcon");
                    cachedBarBorder = TryLoadTexture("RagnarokOfRedemptionAPI/Content/Items/Omega/GirusDecoderDecryptionBar");
                    cachedBarFill = TryLoadTexture("RagnarokOfRedemptionAPI/Content/Items/Omega/GirusDecoderDecryptionBarCharge");
                    cellTexture = ModContent.Request<Texture2D>("CalamityMod/Items/DraedonMisc/DraedonPowerCell").Value;
                    
                    texturesLoaded = cachedBackground != null && cachedSlotBackground != null && cachedDecryptIcon != null;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to load GirusDecoder textures: " + e.Message);
                    texturesLoaded = false;
                }
            }
        }
        
        private static Texture2D TryLoadTexture(string path)
        {
            try
            {
                return ModContent.Request<Texture2D>(path, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            }
            catch
            {
                return null;
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            UpdateGirusDecoderState();
        }

        private static void UpdateGirusDecoderState()
        {
            TECodebreaker foundCodebreaker = null;
            int foundID = -1;
            
            foreach (var entity in Terraria.DataStructures.TileEntity.ByID.Values)
            {
                if (entity is TECodebreaker codebreaker)
                {
                    if (GirusDecoderCache.HasGirusDecoder(codebreaker))
                    {
                        foundCodebreaker = codebreaker;
                        foundID = codebreaker.ID;
                        break;
                    }
                }
            }
            
            if (foundCodebreaker != null)
            {
                CurrentCodebreaker = foundCodebreaker;
                CurrentCodebreakerID = foundID;
                ActiveOmegaSchematicID = GirusDecoderCache.GetActiveOmegaSchematic(CurrentCodebreakerID);

                OmegaData.UpdateDecrypt(CurrentCodebreaker);

                int timer = OmegaData.GetTimer(CurrentCodebreaker);
                if (timer > 0)
                {
                    IsDecrypting = true;
                    int totalTime = 60 * 60;
                    DecryptionProgress = OmegaData.GetProgress(CurrentCodebreaker);
                    DecryptionCountdown = timer;
                    DecryptionTotalTime = totalTime;
                }
                else
                {
                    if (IsDecrypting && timer <= 0)
                    {
                        IsDecrypting = false;
                        DecryptionCountdown = 0;
                        DecryptionProgress = 0f;
                        CompleteDecryption();
                    }
                }

                bool isCodebreakerUIOpen = (CodebreakerUI.ViewedTileEntityID != -1 && 
                    Terraria.DataStructures.TileEntity.ByID.ContainsKey(CodebreakerUI.ViewedTileEntityID) && 
                    Terraria.DataStructures.TileEntity.ByID[CodebreakerUI.ViewedTileEntityID] == CurrentCodebreaker);

                bool isCommunicating = CodebreakerUI.DisplayingCommunicationText;
                
                IsUIActive = isCodebreakerUIOpen && !isCommunicating;
            }
            else
            {
                CurrentCodebreaker = null;
                CurrentCodebreakerID = -1;
                IsUIActive = false;
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (index != -1)
            {
                layers.Insert(index + 1, new LegacyGameInterfaceLayer(
                    "RagnarokOfRedemptionAPI: GirusDecoder UI",
                    delegate
                    {
                        if (IsUIActive && CurrentCodebreaker != null && texturesLoaded)
                        {
                            DrawGirusDecoderPanel();
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        private static void DrawGirusDecoderPanel()
        {
            if (CurrentCodebreaker == null) return;
            if (cachedBackground == null || cachedSlotBackground == null) return;
            
            SpriteBatch spriteBatch = Main.spriteBatch;

            Vector2 codebreakerPos = GetCodebreakerPosition();

            float codebreakerWidth = 400f;
            float codebreakerHeight = 256f;

            Vector2 codebreakerTopRight = new Vector2(
                codebreakerPos.X + codebreakerWidth,
                codebreakerPos.Y
            );

            Vector2 panelPos = new Vector2(
                codebreakerTopRight.X - cachedBackground.Width + 100f,
                codebreakerTopRight.Y + 2f
            );
            
            spriteBatch.Draw(cachedBackground, panelPos, null, Color.White, 0f,
                Vector2.Zero, 1f, 0, 0f);
            
            Rectangle backgroundArea = new Rectangle(
                (int)panelPos.X,
                (int)panelPos.Y,
                cachedBackground.Width,
                cachedBackground.Height
            );
            
            if (backgroundArea.Contains(Main.MouseScreen.ToPoint()))
                Main.blockMouse = true;

            float slotX = panelPos.X + (cachedBackground.Width * 0.5f - cachedSlotBackground.Width * 0.5f) + 10f;
            float slotY = panelPos.Y + 20f;
            Vector2 slotPos = new Vector2(slotX, slotY);
            
            spriteBatch.Draw(cachedSlotBackground, slotPos, null, Color.White, 0f,
                Vector2.Zero, 1f, 0, 0f);
            
            if (ActiveOmegaSchematicID != 0)
            {
                Texture2D schematicIcon = GetOmegaSchematicTexture(ActiveOmegaSchematicID);
                if (schematicIcon != null)
                {
                    float iconX = slotPos.X + (cachedSlotBackground.Width * 0.5f - schematicIcon.Width * 0.5f);
                    float iconY = slotPos.Y + (cachedSlotBackground.Height * 0.5f - schematicIcon.Height * 0.5f);
                    Vector2 iconPos = new Vector2(iconX, iconY);
                    spriteBatch.Draw(schematicIcon, iconPos, null, Color.White, 0f,
                        Vector2.Zero, 1f, 0, 0f);
                }
            }
            
            HandleSlotInteraction(slotPos, cachedSlotBackground.Size());

            if (ActiveOmegaSchematicID != 0 && !IsDecrypting)
            {
                DrawCostBelowSlot(slotPos, cachedSlotBackground);
            }

            float buttonX = panelPos.X + (cachedBackground.Width * 0.5f - cachedDecryptIcon.Width * 0.5f) + 10f;
            float buttonY = slotPos.Y + cachedSlotBackground.Height + 15f;
            Vector2 buttonPos = new Vector2(buttonX, buttonY);

            if (IsDecrypting)
            {
                DrawProgressBarAtButton(buttonPos, cachedDecryptIcon);
            }
            else
            {
                DrawDecryptButton(buttonPos);
            }
        }
        
        private static Vector2 GetCodebreakerPosition()
        {
            Vector2 center = new Vector2(500f, Main.screenHeight * 0.5f + 115f);

            float scale = MathHelper.Lerp(1f, 0.7f, Utils.GetLerpValue(1325f, 750f, Main.screenWidth, true)) * Main.UIScale;

            float width = 400f;
            float height = 256f;

            Vector2 topLeft = new Vector2(
                center.X - (width * 0.5f * scale),
                center.Y - (height * 0.5f * scale)
            );
            
            return topLeft;
        }
        
        private static void DrawCostBelowSlot(Vector2 slotPos, Texture2D slotBackground)
        {
            int cost = GetRequiredCells(ActiveOmegaSchematicID);
            if (cost <= 0) return;
            
            float scale = 0.6f;

            string costText = Language.GetTextValue("Mods.RagnarokOfRedemptionAPI.UI.Cost");
            if (string.IsNullOrEmpty(costText) || costText.Contains("Cost"))
                costText = "Cost:";
            
            Vector2 textSize = FontAssets.MouseText.Value.MeasureString(costText) * scale;
            Vector2 cellTextureSize = cellTexture.Size() * scale * 0.8f;
            
            float totalWidth = textSize.X + 5f + cellTextureSize.X + 22f;
            float startX = slotPos.X + (slotBackground.Width * 0.5f - totalWidth * 0.5f);
            
            Vector2 costPos = new Vector2(startX, slotPos.Y + slotBackground.Height + 5f);
            
            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, costText,
                costPos.X, costPos.Y, Color.White, Color.Black, Vector2.Zero, scale);

            Vector2 cellPos = costPos + new Vector2(textSize.X + 5f, 0f);
            Main.spriteBatch.Draw(cellTexture, cellPos, null, Color.White, 0f, Vector2.Zero, scale * 0.8f, 0, 0f);

            Vector2 amountPos = cellPos + new Vector2(cellTextureSize.X + 2f, 2f);
            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.ItemStack.Value, cost.ToString(),
                amountPos.X, amountPos.Y, Color.White, Color.Black, Vector2.Zero, scale * 0.8f);
        }
        
        private static Texture2D GetOmegaSchematicTexture(int itemType)
        {
            try
            {
                if (itemType == ModContent.ItemType<Omega1DataTablet>())
                    return ModContent.Request<Texture2D>("RagnarokOfRedemptionAPI/Content/Items/Omega/Omega1DataTablet").Value;
                if (itemType == ModContent.ItemType<Omega2DataTablet>())
                    return ModContent.Request<Texture2D>("RagnarokOfRedemptionAPI/Content/Items/Omega/Omega2DataTablet").Value;
                if (itemType == ModContent.ItemType<Omega3DataTablet>())
                    return ModContent.Request<Texture2D>("RagnarokOfRedemptionAPI/Content/Items/Omega/Omega3DataTablet").Value;
            }
            catch { }
            return null;
        }
        
        private static void HandleSlotInteraction(Vector2 slotPos, Vector2 areaSize)
        {
            Rectangle clickArea = new Rectangle(
                (int)slotPos.X,
                (int)slotPos.Y,
                (int)areaSize.X,
                (int)areaSize.Y
            );
            
            if (!clickArea.Contains(Main.MouseScreen.ToPoint()))
                return;
                
            ref Item playerHandItem = ref Main.mouseItem;
            
            if (Main.mouseLeft && Main.mouseLeftRelease)
            {
                int omegaType = 0;
                if (playerHandItem.type == ModContent.ItemType<Omega1DataTablet>())
                    omegaType = playerHandItem.type;
                else if (playerHandItem.type == ModContent.ItemType<Omega2DataTablet>())
                    omegaType = playerHandItem.type;
                else if (playerHandItem.type == ModContent.ItemType<Omega3DataTablet>())
                    omegaType = playerHandItem.type;
                
                if (omegaType != 0 && ActiveOmegaSchematicID == 0)
                {
                    if (!IsDecrypting)
                    {
                        ActiveOmegaSchematicID = omegaType;
                        GirusDecoderCache.SetActiveOmegaSchematic(CurrentCodebreakerID, omegaType);
                        playerHandItem.TurnToAir();
                        SoundEngine.PlaySound(SoundID.Grab);
                    }
                }
                else if (playerHandItem.IsAir && ActiveOmegaSchematicID != 0)
                {
                    if (IsDecrypting)
                    {
                        SoundEngine.PlaySound(SoundID.MenuTick);
                        return;
                    }
                    
                    playerHandItem.SetDefaults(ActiveOmegaSchematicID);
                    ActiveOmegaSchematicID = 0;
                    GirusDecoderCache.SetActiveOmegaSchematic(CurrentCodebreakerID, 0);
                    SoundEngine.PlaySound(SoundID.Grab);
                }
            }
        }
        
        private static void DrawDecryptButton(Vector2 drawPosition)
        {
            if (cachedDecryptIcon == null) return;
            
            int requiredCells = GetRequiredCells(ActiveOmegaSchematicID);
            bool hasEnoughCells = HasEnoughPowerCells(requiredCells);
            bool canDecrypt = ActiveOmegaSchematicID != 0 && !IsDecrypting && hasEnoughCells;
            
            float buttonWidth = cachedDecryptIcon.Width * DecryptButtonScale;
            float buttonHeight = cachedDecryptIcon.Height * DecryptButtonScale;
            
            Rectangle clickArea = new Rectangle(
                (int)(drawPosition.X),
                (int)(drawPosition.Y),
                (int)buttonWidth,
                (int)buttonHeight
            );
            
            Color buttonColor = canDecrypt ? Color.White : Color.Gray;
            
            if (clickArea.Contains(Main.MouseScreen.ToPoint()) && canDecrypt)
            {
                DecryptButtonScale = MathHelper.Clamp(DecryptButtonScale + 0.035f, 1f, 1.35f);
                
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    StartDecryption();
                }
            }
            else
            {
                DecryptButtonScale = MathHelper.Clamp(DecryptButtonScale - 0.05f, 1f, 1.35f);
            }
            
            Main.spriteBatch.Draw(cachedDecryptIcon, drawPosition, null, buttonColor, 0f,
                Vector2.Zero, DecryptButtonScale, 0, 0f);
        }
        
        private static int GetRequiredCells(int schematicType)
        {
            if (DecryptionCosts.TryGetValue(schematicType, out int cost))
                return cost;
            return 0;
        }
        
        private static bool HasEnoughPowerCells(int required)
        {
            if (CurrentCodebreaker == null) return false;
            return CurrentCodebreaker.InputtedCellCount >= required;
        }
        
        private static void StartDecryption()
        {
            if (ActiveOmegaSchematicID == 0) return;
            
            int requiredCells = GetRequiredCells(ActiveOmegaSchematicID);
            if (!HasEnoughPowerCells(requiredCells)) return;
            
            SoundEngine.PlaySound(OmegaDecryptSound, Main.LocalPlayer.Center);

            int tier = 0;
            if (ActiveOmegaSchematicID == ModContent.ItemType<Omega1DataTablet>()) tier = 1;
            else if (ActiveOmegaSchematicID == ModContent.ItemType<Omega2DataTablet>()) tier = 2;
            else if (ActiveOmegaSchematicID == ModContent.ItemType<Omega3DataTablet>()) tier = 3;

            OmegaData.StartDecrypt(CurrentCodebreaker, tier, requiredCells);

            IsDecrypting = true;
            DecryptionTotalTime = 60 * 60;
            DecryptionCountdown = DecryptionTotalTime;
            DecryptionProgress = 0f;
            
            TotalCellsRequired = requiredCells;
            CellsConsumedSoFar = 0;
            CellsToConsumePerTick = (int)Math.Ceiling((float)requiredCells / DecryptionTotalTime);
        }
        
        private static void ResetDecryption()
        {
            if (TotalCellsRequired > 0 && CellsConsumedSoFar > 0 && CurrentCodebreaker != null)
            {
                CurrentCodebreaker.InputtedCellCount += CellsConsumedSoFar;
                CurrentCodebreaker.SyncContainedStuff();
            }
            
            IsDecrypting = false;
            DecryptionCountdown = 0;
            DecryptionProgress = 0f;
            TotalCellsRequired = 0;
            CellsConsumedSoFar = 0;
            CellsToConsumePerTick = 0;
        }
        
        private static void DrawProgressBarAtButton(Vector2 drawPosition, Texture2D buttonTexture)
        {
            if (cachedBarBorder == null || cachedBarFill == null) return;
            
            float realProgress = OmegaData.GetProgress(CurrentCodebreaker);

            float barX = drawPosition.X + (buttonTexture.Width * 0.5f - cachedBarBorder.Width * 0.5f);
            float barY = drawPosition.Y + (buttonTexture.Height * 0.5f - cachedBarBorder.Height * 0.5f);
            Vector2 barPos = new Vector2(barX, barY);

            Main.spriteBatch.Draw(cachedBarBorder, barPos, null, Color.White, 0f,
                Vector2.Zero, 1f, 0, 0);

            Vector2 fillPos = barPos + new Vector2(10f, 7f);
            Rectangle barRectangle = new Rectangle(0, 0, (int)(cachedBarFill.Width * realProgress), cachedBarFill.Height);
            Main.spriteBatch.Draw(cachedBarFill, fillPos, barRectangle, Color.White, 0f,
                Vector2.Zero, 1f, 0, 0);

            string percentText = $"{realProgress * 100f:F2}%";
            Vector2 textPos = barPos + new Vector2(
                (cachedBarBorder.Width * 0.5f - FontAssets.MouseText.Value.MeasureString(percentText).X * 0.5f) + 6f,
                cachedBarBorder.Height + 5f
            );
            
            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, percentText,
                textPos.X, textPos.Y, DecryptPercentColor, Color.Black, Vector2.Zero, 0.6f);
        }
        
        private static void CompleteDecryption()
        {
            IsDecrypting = false;
            
            TotalCellsRequired = 0;
            CellsConsumedSoFar = 0;
            CellsToConsumePerTick = 0;
            
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(CurrentCodebreaker.Center, 20, 20, DustID.GoldFlame, 0f, 0f, 0, default, 1f);
            }
        }
    }
}