using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using Redemption.Items.Materials.PostML; 
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using Redemption.Items.Materials.HM; 
using Redemption.Items.Materials.PreHM;
using CalamityMod.Items.Placeables.PlaceableTurrets;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.SummonItems;
using Redemption.Items.Usable.Summons;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Weapons.HM.Summon;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.Items.Armor.PostML.Xenium;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Accessories.HM;
using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using InfernumMode.Content.Items.Weapons.Magic;
using Redemption.Items.Quest.KingSlayer;
using CalamitySimpleWhipAddon.Content.Items.Accessories;
using CalamitySimpleWhipAddon.Content.Items.Weapons;
using ThoriumMod.Items.BossThePrimordials;

namespace RagnarokOfRedemptionAPI.Content.RecipeChanges
{
    public class CraftsChanges : ModSystem
    {
        public override void AddRecipes()
        {
            // SOTS
            
            if (ModLoader.HasMod("SOTS"))
            {
                AddIngredientToRecipeSOTS<BeelzebubConcoction>("VialofAcid");
                AddIngredientToRecipeSOTS<CyberTech>("SoulOfPlight", 5);
                AddIngredientToRecipeSOTS<SunshardGreatstaff>("SoulOfPlight", 5);
            }

            // CalamityBardHealer
            
            if (ModLoader.HasMod("CalamityBardHealer"))
            {
                AddIngredientToRecipeByName("ElementalBloom", ModContent.ItemType<XeniumAlloy>(), 7);
                AddIngredientToRecipeByName("OmniSpeaker", ModContent.ItemType<XeniumAlloy>(), 7);
                AddIngredientToRecipeByName("Exorectionist", ModContent.ItemType<LifeFragment>(), 5);
                AddIngredientToRecipeByName("ScuffedKeytar", ModContent.ItemType<Plating>(), 6);
                AddIngredientToRecipeByName("ElectricQuarterstaff", ModContent.ItemType<Plating>(), 6);
            }
            
            // HomewardRagnarok
            
            if (ModLoader.HasMod("HomewardRagnarok"))
            {
                AddIngredientToRecipeByName("RiftGenerator", ModContent.ItemType<XeniumAlloy>(), 7);
            }

            // InfernalEclipseWeaponsDLC 
            
            if (ModLoader.HasMod("InfernalEclipseWeaponsDLC"))
            {
                AddMultipleIngredientsToRecipeByName("Infrariff",
                    (ModContent.ItemType<Capacitor>(), 2),
                    (ModContent.ItemType<CarbonMyofibre>(), 5)
                );
                AddMultipleIngredientsToRecipeByName("NeonRipper",
                    (ModContent.ItemType<Capacitor>(), 2),
                    (ModContent.ItemType<CarbonMyofibre>(), 5)
                );
                AddIngredientToRecipeByName("PocketConcert", ModContent.ItemType<MoltenScrap>(), 3);
                AddIngredientToRecipeByName("GammaKnife", ModContent.ItemType<MoltenScrap>(), 3);
                AddIngredientToRecipeByName("PlasmaOcarina", ModContent.ItemType<LifeFragment>(), 5);
				AddMultipleIngredientsToRecipeByName("AbsoluteTVRemote",
                    (ModContent.ItemType<Capacitor>(), 5),
					(ModContent.ItemType<AIChip>(), 5),
					(ModContent.ItemType<CorruptedXenomite>(), 5),
					(ModContent.ItemType<CyberPlating>(), 5),
					(ModContent.ItemType<OmegaPowerCell>(), 5),
					(ModContent.ItemType<Plating>(), 5),
					(ModContent.ItemType<RoboBrain>(), 1)
				);
            }

            // RedemptionBardHealer
            
            if (ModLoader.HasMod("RedemptionBardHealer"))
            {
                AddMultipleIngredientsToRecipeByName("KillswitchEngineer",
                    (ModContent.ItemType<MysteriousCircuitry>(), 15),
                    (ModContent.ItemType<DubiousPlating>(), 25)
                );
                AddMultipleIngredientsToRecipeByName("Oboeterator",
                    (ModContent.ItemType<MysteriousCircuitry>(), 15),
                    (ModContent.ItemType<DubiousPlating>(), 25)
                );
                AddMultipleIngredientsToRecipeByName("OmegaDefibrillator",
                    (ModContent.ItemType<MysteriousCircuitry>(), 15),
                    (ModContent.ItemType<DubiousPlating>(), 25)
                );
                AddMultipleIngredientsToRecipeByName("ScorchingScalpel",
                    (ModContent.ItemType<MysteriousCircuitry>(), 15),
                    (ModContent.ItemType<DubiousPlating>(), 25)
                );
                AddMultipleIngredientsToRecipeByName("Shockriken",
                    (ModContent.ItemType<MysteriousCircuitry>(), 15),
                    (ModContent.ItemType<DubiousPlating>(), 25)
                );
                AddMultipleIngredientsToRecipeByName("VlitchSynthesizer",
                    (ModContent.ItemType<MysteriousCircuitry>(), 15),
                    (ModContent.ItemType<DubiousPlating>(), 25)
                );
	//		AddIngredientToRecipeLocal<HardlightMask>(ModContent.ItemType<DubiousPlating>(), 7);
	//		AddIngredientToRecipeLocal<HardlightReticle>(ModContent.ItemType<DubiousPlating>(), 7);
	//		AddIngredientToRecipeLocal<HardlightVisage>(ModContent.ItemType<DubiousPlating>(), 7);
            }
			
			if (ModLoader.HasMod("CalamitySimpleWhipAddon"))
            {
                AddIngredientToRecipeLocal<EmperorsGrip>(ModContent.ItemType<XeniumAlloy>(), 7);
                AddIngredientToRecipeLocal<ShieldConduitMkII>(ModContent.ItemType<Plating>(), 6);
				AddMultipleIngredientsToRecipeLocal<ShieldConduitMkIII>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );
			AddIngredientToRecipeLocal<ShieldConduitMkV>(ModContent.ItemType<LifeFragment>(), 5);
			AddIngredientToRecipeLocal<ShieldConduitMkIV>(ModContent.ItemType<MoltenScrap>(), 3);
            }

            AddIngredientToRecipeLocal<HardlightPlate>(ModContent.ItemType<DubiousPlating>(), 7);
			AddIngredientToRecipeLocal<HardlightVisor>(ModContent.ItemType<DubiousPlating>(), 7);
			AddIngredientToRecipeLocal<HardlightHood>(ModContent.ItemType<DubiousPlating>(), 7);
			AddIngredientToRecipeLocal<HardlightHelm>(ModContent.ItemType<DubiousPlating>(), 7);
			AddIngredientToRecipeLocal<HardlightCowl>(ModContent.ItemType<DubiousPlating>(), 7);
			AddIngredientToRecipeLocal<HardlightBoots>(ModContent.ItemType<DubiousPlating>(), 7);
			
            AddIngredientToRecipeLocal<Nanotech>(ModContent.ItemType<XeniumAlloy>(), 7);
            AddIngredientToRecipeLocal<Nucleogenesis>(ModContent.ItemType<XeniumAlloy>(), 7);
            AddIngredientToRecipeLocal<ElementalGauntlet>(ModContent.ItemType<XeniumAlloy>(), 7);
            AddIngredientToRecipeLocal<PlanebreakersPouch>(ModContent.ItemType<XeniumAlloy>(), 7);
            AddIngredientToRecipeLocal<TheSponge>(ModContent.ItemType<XeniumAlloy>(), 7);
            AddIngredientToRecipeLocal<InfectedJewel>(ModContent.ItemType<ToxicBile>(), 10);
            AddIngredientToRecipeLocal<AmbrosialAmpoule>(ModContent.ItemType<BeelzebubConcoction>());
            AddIngredientToRecipeLocal<StarTaintedGenerator>(ModContent.ItemType<ToxicBile>(), 10);
			AddIngredientToRecipeLocal<DraedonsForge>(ModContent.ItemType<LifeFragment>(), 25);
			AddIngredientToRecipeLocal<CosmicAnvilItem>(ModContent.ItemType<GildedStar>(), 15);

            AddMultipleIngredientsToRecipeLocal<DaawnlightSpiritOrigin>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );
            AddMultipleIngredientsToRecipeLocal<ElectriciansGlove>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );
            AddIngredientToRecipeLocal<RampartofDeities>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<SeraphTracers>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<Radiance>(ModContent.ItemType<LifeFragment>(), 5);

            AddIngredientToRecipeLocal<HydraulicVoltCrasher>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<MountedScanner>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<HolofibreImmolator>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<LongRangedSensorArray>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<LaserTurret>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<HostileLaserTurret>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<IceTurret>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<HostileIceTurret>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<PulseGrenade>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<Vulcan>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<FireTurret>(ModContent.ItemType<Plating>(), 6);
            AddIngredientToRecipeLocal<HostileFireTurret>(ModContent.ItemType<Plating>(), 6);
			
			AddIngredientToRecipeLocal<SlayerHullPlating>(ModContent.ItemType<DubiousPlating>(), 8);
			AddIngredientToRecipeLocal<SlayerWiringKit>(ModContent.ItemType<MysteriousCircuitry>(), 10);
			AddMultipleIngredientsToRecipeLocal<SlayerShipEngine>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );
			
			AddMultipleIngredientsToRecipeLocal<SlayerFist>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );
			
			AddMultipleIngredientsToRecipeLocal<Nanoswarmer>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );
			
			AddMultipleIngredientsToRecipeLocal<UraniumRaygun>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );
			
			AddMultipleIngredientsToRecipeLocal<SlayerGun>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );
			
			AddMultipleIngredientsToRecipeLocal<HydrasMaw>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );
			
			AddMultipleIngredientsToRecipeLocal<PlutoniumRailgun>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );
			
			AddMultipleIngredientsToRecipeLocal<PlutoniumNukeRadio>(
                (ModContent.ItemType<DubiousPlating>(), 12),
                (ModContent.ItemType<MysteriousCircuitry>(), 15)
            );

            AddMultipleIngredientsToRecipeLocal<GalvanizingGlaive>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );
            AddMultipleIngredientsToRecipeLocal<AdvancedDisplay>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5),
                (ModContent.ItemType<OmegaPowerCell>(), 3)
            );
            AddMultipleIngredientsToRecipeLocal<Abombination>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5),
                (ModContent.ItemType<OmegaPowerCell>(), 1)
            );
            AddMultipleIngredientsToRecipeLocal<Kevin>(
                (ModContent.ItemType<Capacitor>(), 50),
                (ModContent.ItemType<CarbonMyofibre>(), 50),
                (ModContent.ItemType<AIChip>(), 10),
				(ModContent.ItemType<Plating>(), 50)
            );
            AddMultipleIngredientsToRecipeLocal<CountermeasureMitt>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );
            AddMultipleIngredientsToRecipeLocal<Nidhogg>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );

            AddMultipleIngredientsToRecipeLocal<PlagueTurret>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );
            AddMultipleIngredientsToRecipeLocal<HostilePlagueTurret>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );
            AddMultipleIngredientsToRecipeLocal<PulseTurretRemote>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );

            AddMultipleIngredientsToRecipeLocal<SystemBane>(
                (ModContent.ItemType<Capacitor>(), 2),
                (ModContent.ItemType<CarbonMyofibre>(), 5)
            );

            AddMultipleIngredientsToRecipeLocal<BlastBattery>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<CorruptedDAN>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<CorruptedDoubleRifle>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<DrillRevolver>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<GigapeiliContactor>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<HydrasMaw>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );

            AddMultipleIngredientsToRecipeLocal<OversizedScrewdriver>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<PulseBlade>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );

            AddMultipleIngredientsToRecipeLocal<TinyCleaver>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<OOFingergun>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );
            AddMultipleIngredientsToRecipeLocal<SunInThePalm>(
                (ModContent.ItemType<MysteriousCircuitry>(), 15),
                (ModContent.ItemType<DubiousPlating>(), 25)
            );

            AddIngredientToRecipeLocal<XeniumDrone>(ModContent.ItemType<MysteriousCircuitry>(), 15);
            AddIngredientToRecipeLocal<XeniumElectrolaser>(ModContent.ItemType<MysteriousCircuitry>(), 15);
            AddIngredientToRecipeLocal<XeniumLance>(ModContent.ItemType<MysteriousCircuitry>(), 15);
            AddIngredientToRecipeLocal<XeniumStaff>(ModContent.ItemType<MysteriousCircuitry>(), 15);
            
            AddIngredientToRecipeLocal<XeniumBreastplate>(ModContent.ItemType<DubiousPlating>(), 13);
            AddIngredientToRecipeLocal<XeniumLeggings>(ModContent.ItemType<DubiousPlating>(), 7);
            AddIngredientToRecipeLocal<XeniumVisor>(ModContent.ItemType<DubiousPlating>(), 5);
            
            AddIngredientToRecipeLocal<PulseDragon>(ModContent.ItemType<MoltenScrap>(), 3);
            AddIngredientToRecipeLocal<PhalanxSurge>(ModContent.ItemType<MoltenScrap>(), 3);
            AddIngredientToRecipeLocal<PlasmaCaster>(ModContent.ItemType<MoltenScrap>(), 3);
            AddIngredientToRecipeLocal<SnakeEyes>(ModContent.ItemType<MoltenScrap>(), 3);
            AddIngredientToRecipeLocal<WavePounder>(ModContent.ItemType<MoltenScrap>(), 3);
			AddMultipleIngredientsToRecipeLocal<VoltageRegulationSystem>(
			    (ModContent.ItemType<MoltenScrap>(), 3),
				(ModContent.ItemType<RoboBrain>(), 1)
			);

            AddIngredientToRecipeLocal<TheAnomalysNanogun>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<Phaseslayer>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<PlasmaGrenade>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<PoleWarper>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<PulseRifle>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<TeslaCannon>(ModContent.ItemType<LifeFragment>(), 5);
            
            AddIngredientToRecipeLocal<AscendantSpiritEssence>(ModContent.ItemType<LostSoul>(), 3);
            
            AddIngredientToRecipeLocal<MiracleMatter>(ModContent.ItemType<XeniumAlloy>(), 2);

            AddIngredientToRecipeLocal<YharonEgg>(ModContent.ItemType<LifeFragment>(), 5);
            AddIngredientToRecipeLocal<CosmicWorm>(ModContent.ItemType<GildedStar>(), 5);
            AddIngredientToRecipeLocal<NebSummon>(ModContent.ItemType<CosmiliteBar>(), 15);

            AddIngredientToAllEtherealTalismanRecipes();
        }
        private void AddIngredientToAllEtherealTalismanRecipes()
        {
            int etherealTalismanType = ModContent.ItemType<EtherealTalisman>();

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.type == etherealTalismanType)
                {
                    recipe.AddIngredient(ModContent.ItemType<XeniumAlloy>(), 7);
                }
            }
        }

        private void AddIngredientToRecipeLocal<TItem>(int ingredientType, int amount = 1) where TItem : ModItem
        {
            int itemType = ModContent.ItemType<TItem>();
            
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.type == itemType)
                {
                    recipe.AddIngredient(ingredientType, amount);
                    break;
                }
            }
        }

        private void AddMultipleIngredientsToRecipeLocal<TItem>(params (int type, int amount)[] ingredients) where TItem : ModItem
        {
            int itemType = ModContent.ItemType<TItem>();
            
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.type == itemType)
                {
                    foreach (var ingredient in ingredients)
                    {
                        recipe.AddIngredient(ingredient.type, ingredient.amount);
                    }
                    break;
                }
            }
        }

        private void AddIngredientToRecipeSOTS<TItem>(string itemName, int amount = 1) where TItem : ModItem
        {
            if (!ModLoader.TryGetMod("SOTS", out Mod sotsMod))
                return;

            int itemType = ModContent.ItemType<TItem>();
            int ingredientType = sotsMod.Find<ModItem>(itemName).Type;
            
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.type == itemType)
                {
                    recipe.AddIngredient(ingredientType, amount);
                    break;
                }
            }
        }
        
        private void AddIngredientToRecipeThorium(string targetItemName, string ingredientName, int amount = 1)
        {
            if (!ModLoader.TryGetMod("Thorium", out Mod thoriumMod))
                return;

            ModItem targetItem = thoriumMod.Find<ModItem>(targetItemName);
            if (targetItem == null) return;
            
            ModItem ingredientItem = thoriumMod.Find<ModItem>(ingredientName);
            if (ingredientItem == null) return;
            
            int targetType = targetItem.Type;
            int ingredientType = ingredientItem.Type;
            
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.type == targetType)
                {
                    recipe.AddIngredient(ingredientType, amount);
                    break;
                }
            }
        }

        private void AddIngredientToRecipeByName(string targetItemName, int ingredientType, int amount = 1)
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.ModItem != null && recipe.createItem.ModItem.Name == targetItemName)
                {
                    recipe.AddIngredient(ingredientType, amount);
                    break;
                }
            }
        }

        private void AddMultipleIngredientsToRecipeByName(string targetItemName, params (int type, int amount)[] ingredients)
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.ModItem != null && recipe.createItem.ModItem.Name == targetItemName)
                {
                    foreach (var ingredient in ingredients)
                    {
                        recipe.AddIngredient(ingredient.type, ingredient.amount);
                    }
                    break;
                }
            }
        }
    }
}