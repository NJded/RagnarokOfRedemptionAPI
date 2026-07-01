using Redemption.BaseExtension;
using Redemption.Buffs.Debuffs;
using Redemption.Globals.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamityMod.Items.Accessories;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RagnarokOfRedemptionAPI.Content.Items
{
    public class AmbrosialAmpouleExtraEffects : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<AmbrosialAmpoule>() || 
                   entity.type == ModContent.ItemType<Radiance>();
        }
        
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<DevilScentedDebuff>(), 30);
            
            BuffPlayer redemptionBuffPlayer = player.RedemptionPlayerBuff();
            redemptionBuffPlayer.erleasFlower = true;
            
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[ModContent.BuffType<SpiderSwarmedDebuff>()] = true;
            
            redemptionBuffPlayer.spiderFriendly = true;
            redemptionBuffPlayer.beelzebub = true;
        }
        
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            bool shouldAddTooltip = item.type == ModContent.ItemType<AmbrosialAmpoule>() || 
                                    item.type == ModContent.ItemType<Radiance>();
            
            if (shouldAddTooltip)
            {
                string additionalTooltip = Language.GetTextValue("Mods.RagnarokOfRedemptionAPI.Items.AmbrosialAmpoule.ExtraTooltip");
                
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
                    lastTooltipLine.Text += additionalTooltip;
                }
            }
        }
    }
}