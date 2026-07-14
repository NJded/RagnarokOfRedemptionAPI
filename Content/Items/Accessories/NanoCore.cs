using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using Redemption.BaseExtension;
using Redemption.Buffs.Debuffs;
using Redemption.Globals;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Usable.Potions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace RagnarokOfRedemptionAPI.Content.Items.Accessories
{
    public class NanoCore : ModItem
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Head}", EquipType.Head, this, null, new EquipTexture());
                EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Body}", EquipType.Body, this, null, new EquipTexture());
                EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this, null, new EquipTexture());
            }
        }

        private void SetupDrawing()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
                int equipSlotBody = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
                int equipSlotLegs = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);

                ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
                ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
                ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
                ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SetupDrawing();
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 36;
            Item.value = Item.buyPrice(2, 0, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.hasVanityEffects = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var nanoPlayer = player.GetModPlayer<NanoCorePlayer>();
            nanoPlayer.HideVanity = hideVisual;
            nanoPlayer.VanityOn = true;
            player.RedemptionRad().radiationLevel = 0;
            player.ClearBuff(ModContent.BuffType<HeadacheDebuff>());
            player.ClearBuff(ModContent.BuffType<NauseaDebuff>());
            player.ClearBuff(ModContent.BuffType<FatigueDebuff>());
            player.ClearBuff(ModContent.BuffType<FeverDebuff>());
            player.ClearBuff(ModContent.BuffType<HairLossDebuff>());
            player.ClearBuff(ModContent.BuffType<SkinBurnDebuff>());
            player.ClearBuff(ModContent.BuffType<RadiationDebuff>());
            player.buffImmune[ModContent.BuffType<HeadacheDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<NauseaDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<FatigueDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<FeverDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<HairLossDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<SkinBurnDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<RadiationDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<BileDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<GreenRashesDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<GlowingPustulesDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<FleshCrystalsDebuff>()] = true;
            player.buffImmune[ModContent.BuffType<ShockDebuff>()] = true;
            player.accDivingHelm = true;
            player.ignoreWater = true;
            player.arcticDivingGear = true;
            player.gills = true;
            player.RedemptionPlayerBuff().WastelandWaterImmune = true;
            if (player.wet)
            {
                player.moveSpeed += 0.15f;
                player.runAcceleration += 0.1f;
                player.maxRunSpeed += 1.5f;
                player.statDefense += 5;
            }
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ModContent.ItemType<HazmatSuit>() || 
                equippedItem.type == ModContent.ItemType<HazmatSuit2>() || 
                equippedItem.type == ModContent.ItemType<HazmatSuit3>() || 
                equippedItem.type == ModContent.ItemType<HazmatSuit4>())
                return false;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AbyssalDivingSuit>())
                .AddIngredient(ModContent.ItemType<HEVSuit>())
                .AddIngredient(ModContent.ItemType<ReaperTooth>(), 7)
                .AddIngredient(ModContent.ItemType<Plutonium>(), 15)
                .AddIngredient(ItemID.Nanites, 300)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }

    public class NanoCorePlayer : ModPlayer
    {
        public bool HideVanity;
        public bool ForceVanity;
        public bool VanityOn;

        public override void ResetEffects()
        {
            VanityOn = HideVanity = ForceVanity = false;
        }

        public override void UpdateVisibleVanityAccessories()
        {
            for (int n = 13; n < 18 + Player.GetAmountOfExtraAccessorySlotsToShow(); n++)
            {
                Item item = Player.armor[n];
                if (item.type == ModContent.ItemType<NanoCore>())
                {
                    HideVanity = false;
                    ForceVanity = true;
                }
            }
        }

        public override void FrameEffects()
        {
            if ((VanityOn || ForceVanity) && !HideVanity)
            {
                var nanoCore = ModContent.GetInstance<NanoCore>();
                Player.head = EquipLoader.GetEquipSlot(Mod, nanoCore.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, nanoCore.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, nanoCore.Name, EquipType.Legs);
            }
        }
    }
}
