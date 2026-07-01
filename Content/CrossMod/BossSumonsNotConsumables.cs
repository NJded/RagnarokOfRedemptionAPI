using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable.Summons;

namespace RagnarokOfRedemptionAPI.Content.CrossMod
{
    public class CoJSummonPatch : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<EaglecrestSpelltome>() ||
                item.type == ModContent.ItemType<EggCrown>())
            {
                item.consumable = false;
                item.maxStack = 1;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<EaglecrestSpelltome>() ||
                item.type == ModContent.ItemType<EggCrown>())
            {
                tooltips.RemoveAll(t => t.Mod == "Terraria" && t.Name == "Consumable");

                tooltips.Add(new TooltipLine(Mod, "NonConsumableInfo", "Not consumable"));
            }
        }
    }
}