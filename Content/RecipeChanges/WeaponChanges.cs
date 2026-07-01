using Terraria;
using Terraria.ModLoader;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Summon;

namespace RagnarokOfRedemptionAPI.Content.Changes
{
    public class WeaponChanges : GlobalItem
    {
        private static bool bardHealerLoaded = false;

        public override void Load()
        {
            if (ModLoader.TryGetMod("RedemptionBardHealer", out Mod bardHealerMod))
            {
                bardHealerLoaded = true;
            }
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<SunInThePalm>() || 
                   entity.type == ModContent.ItemType<SteampunkMinigun>() || 
                   entity.type == ModContent.ItemType<SwarmerCannon>() || 
                   entity.type == ModContent.ItemType<Constellations>() ||
                   entity.type == ModContent.ItemType<PoemOfIlmatar>() || 
                   entity.type == ModContent.ItemType<XeniumStaff>() || 
                   entity.type == ModContent.ItemType<OOFingergun>() || 
                   entity.type == ModContent.ItemType<Twinklestar>() || 
                   entity.type == ModContent.ItemType<XeniumDrone>() || 
                   entity.type == ModContent.ItemType<PortableHoloProjector>() || 
                   entity.type == ModContent.ItemType<UkonRuno>() || 
                   entity.type == ModContent.ItemType<Petridish>() ||
                   (bardHealerLoaded && IsBardHealerWeapon(entity.type));
        }

        private bool IsBardHealerWeapon(int itemType)
        {
            if (!ModLoader.TryGetMod("RedemptionBardHealer", out Mod bardHealerMod))
                return false;

            int halfLifeType = bardHealerMod.Find<ModItem>("HalfLife")?.Type ?? 0;
            int radiumGuitarType = bardHealerMod.Find<ModItem>("RadiumGuitar")?.Type ?? 0;
            int atomSplitterType = bardHealerMod.Find<ModItem>("AtomSplitter")?.Type ?? 0;
            int seventhSeraphType = bardHealerMod.Find<ModItem>("SeventhSeraph")?.Type ?? 0;
            int omegaDefibrillatorType = bardHealerMod.Find<ModItem>("OmegaDefibrillator")?.Type ?? 0;
            int scorchingScalpelType = bardHealerMod.Find<ModItem>("ScorchingScalpel")?.Type ?? 0;

            return itemType == halfLifeType ||
                   itemType == radiumGuitarType ||
                   itemType == atomSplitterType ||
                   itemType == seventhSeraphType ||
                   itemType == omegaDefibrillatorType ||
                   itemType == scorchingScalpelType;
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<SunInThePalm>())
            {
                item.damage = 400;
            }
            
            if (item.type == ModContent.ItemType<SteampunkMinigun>())
            {
                item.damage -= 15;
            }
            
            if (item.type == ModContent.ItemType<SwarmerCannon>())
            {
                item.damage = 240;
            }
            
            if (item.type == ModContent.ItemType<Twinklestar>())
            {
                item.damage = 500;
            }
            
            if (item.type == ModContent.ItemType<Constellations>())
            {
                item.damage = 580;
            }
            
            if (item.type == ModContent.ItemType<PoemOfIlmatar>())
            {
                item.damage = 300;
            }
            
            if (item.type == ModContent.ItemType<XeniumStaff>())
            {
                item.damage -= 55;
            }
            
            if (item.type == ModContent.ItemType<OOFingergun>())
            {
                item.damage = 240;
            }
            
            if (item.type == ModContent.ItemType<Petridish>())
            {
                item.damage = 300;
            }
            
            if (item.type == ModContent.ItemType<PortableHoloProjector>())
            {
                item.damage = 170;
            }
            
            if (item.type == ModContent.ItemType<XeniumDrone>())
            {
                item.damage = 90;
            }
            
            if (item.type == ModContent.ItemType<UkonRuno>())
            {
                item.damage = 280;
            }

            if (bardHealerLoaded)
            {
                if (ModLoader.TryGetMod("RedemptionBardHealer", out Mod bardHealerMod))
                {
                    int halfLifeType = bardHealerMod.Find<ModItem>("HalfLife")?.Type ?? 0;
                    int radiumGuitarType = bardHealerMod.Find<ModItem>("RadiumGuitar")?.Type ?? 0;
                    int atomSplitterType = bardHealerMod.Find<ModItem>("AtomSplitter")?.Type ?? 0;
                    int seventhSeraphType = bardHealerMod.Find<ModItem>("SeventhSeraph")?.Type ?? 0;
                    int omegaDefibrillatorType = bardHealerMod.Find<ModItem>("OmegaDefibrillator")?.Type ?? 0;
                    int scorchingScalpelType = bardHealerMod.Find<ModItem>("ScorchingScalpel")?.Type ?? 0;

                    if (item.type == halfLifeType)
                    {
                        item.damage = 354;
                    }
                    
                    if (item.type == radiumGuitarType)
                    {
                        item.damage = 301;
                    }
                    
                    if (item.type == atomSplitterType)
                    {
                        item.damage = 360;
                    }
                    
                    if (item.type == seventhSeraphType)
                    {
                        item.damage = 516;
                    }
                    
                    if (item.type == omegaDefibrillatorType)
                    {
                        item.damage = 277;
                    }
                    
                    if (item.type == scorchingScalpelType)
                    {
                        item.damage = 260;
                    }
                }
            }
        }
    }
}