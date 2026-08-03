using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Redemption.Items.Accessories.PreHM;
using Redemption.BaseExtension;
using Redemption.Globals;
using System.Collections.Generic;
using Terraria.ID;
using CalamityMod.CalPlayer;

namespace RagnarokOfRedemptionAPI.Content.CrossMod
{
    public class GaiaBootsRework : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<GaiaBoots>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (modPlayer != null)
            {
                modPlayer.fairyBoots = true;
            }

            player.accRunSpeed = 6.75f;

            if (!player.mount.Active)
                player.maxFallSpeed *= 1.2f;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<GaiaBoots>())
            {
                string extraTooltip = Language.GetTextValue("Mods.RagnarokOfRedemptionAPI.Items.GaiaBoots.ExtraTooltip");
                
                if (!string.IsNullOrEmpty(extraTooltip))
                {
                    TooltipLine lastTooltipLine = null;
                    foreach (TooltipLine line in tooltips)
                    {
                        if (line.Name.StartsWith("Tooltip"))
                        {
                            lastTooltipLine = line;
                        }
                    }

                    if (lastTooltipLine != null)
                    {
                        lastTooltipLine.Text += "\n" + extraTooltip;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == ModContent.ItemType<GaiaBoots>())
                {
                    recipe.AddRecipeGroup("AnyAdamantiteBar", 5);
                    break;
                }
            }
        }
    }
}