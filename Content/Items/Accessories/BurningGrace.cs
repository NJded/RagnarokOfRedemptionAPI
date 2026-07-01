using Microsoft.Xna.Framework;
using Redemption.BaseExtension;
using Redemption.Buffs.Debuffs;
using Redemption.Globals;
using Redemption.Globals.Players;
using Redemption.Items.Accessories.HM;
using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RagnarokOfRedemptionAPI.Content.Items.Accessories
{
	[JITWhenModsEnabled("ThoriumMod")]
	[ExtendsFromMod("ThoriumMod")]
	public class BurningGrace : ModItem
	{
		static int _divineGeodeType, _savingGraceType;
		int _timer, _healTimer;
		
		readonly int _radius = 320 * 320;
		readonly float _pulseMult = 0.8f;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ElementID.HolyS, ElementID.FireS);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
			ElementID.ItemHoly[Type] = true;
			ElementID.ItemArcane[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 52;
			Item.value = Item.sellPrice(0, 15, 0, 0);
			Item.rare = ItemRarityID.Red;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			var rPlayer = player.RedemptionPlayerBuff();
			
			rPlayer.ElementalDamage[ElementID.Holy] += 0.12f;
			rPlayer.ElementalResistance[ElementID.Holy] += 0.12f;
			player.GetCritChance(DamageClass.Generic) += 6;
			player.longInvince = true;
			rPlayer.gracesGuidance = true;
			
			if (player.lifeRegen != 0)
				player.lifeRegen += (int)(player.lifeRegen * 0.08f);
			
			player.GetModPlayer<BurningGraceThoriumPlayer>()._equipped = true;
			DoAura(player);
		}
		
		void DoAura(Player player)
		{
			if (player.whoAmI != Main.myPlayer || player.dead) return;
			
			var held = player.HeldItem;
			if (!held.HasElementItem(ElementID.Fire) && !held.HasElementItem(ElementID.Holy)) return;
			
			bool isHealer = ModLoader.HasMod("ThoriumMod") && (held.healLife > 0 || held.healMana > 0);
			
			if (++_timer == 30)
				RedeDraw.SpawnCirclePulse(player.Center, Color.DarkOrange * _pulseMult, 0.7f, player);
			else if (_timer >= 40)
			{
				RedeDraw.SpawnCirclePulse(player.Center, Color.Goldenrod * _pulseMult, 0.8f, player);
				_timer = 0;
			}
			
			if (isHealer)
			{
				if (++_healTimer == 25)
					RedeDraw.SpawnCirclePulse(player.Center, Color.OrangeRed * 0.9f, 0.9f, player);
				else if (_healTimer >= 35)
				{
					RedeDraw.SpawnCirclePulse(player.Center, Color.HotPink * _pulseMult, 1f, player);
					_healTimer = 0;
				}
			}
			
			int debuff = ModContent.BuffType<HolyFireDebuff>();
			var center = player.Center;
			
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				var npc = Main.npc[i];
				if (npc.active && !npc.friendly && npc.CanBeChasedBy() && center.DistanceSQ(npc.Center) <= _radius)
					npc.AddBuff(debuff, 4);
			}
		}
		
		bool GetThoriumItem(string name, ref int cache)
		{
			if (cache != 0) return true;
			if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return false;
			var item = thorium.Find<ModItem>(name);
			if (item != null) cache = item.Type;
			return item != null;
		}
		
		bool GetCalamityItem(string name, ref int cache)
		{
			if (cache != 0) return true;
			if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return false;
			var item = calamity.Find<ModItem>(name);
			if (item != null) cache = item.Type;
			return item != null;
		}
		
		public override void AddRecipes()
		{
			if (!ModLoader.HasMod("ThoriumMod")) return;
			
			GetThoriumItem("SavingGrace", ref _savingGraceType);
			GetCalamityItem("DivineGeode", ref _divineGeodeType);
			
			if (_savingGraceType == 0 || _divineGeodeType == 0) return;
				
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<GracesGuidance>())
				.AddIngredient(_savingGraceType)
				.AddIngredient(_divineGeodeType, 10)
				.AddIngredient(ModContent.ItemType<Necroplasm>(), 15)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
	
	[JITWhenModsEnabled("ThoriumMod")]
	[ExtendsFromMod("ThoriumMod")]
	public class BurningGraceThoriumPlayer : ModPlayer
	{
		public bool _equipped;
		
		public override void ResetEffects() => _equipped = false;
		
		public override void PostUpdate()
		{
			if (!_equipped) return;
			
			var center = Player.Center;
			int regen = ModContent.BuffType<BurningGraceRegenBuff>();
			int defense = ModContent.BuffType<BurningGraceDefenseBuff>();
			
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				var other = Main.player[i];
				if (other == Player || !other.active || other.dead || other.statLife >= other.statLifeMax2)
					continue;
					
				if (center.DistanceSQ(other.Center) <= 40000)
				{
					other.AddBuff(regen, 900);
					other.AddBuff(defense, 1200);
				}
			}
		}
		
		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int dmg)
		{
			if (!_equipped) return;
			var owner = Main.player[proj.owner];
			if (owner?.HeldItem.HasElementItem(ElementID.Holy) == true && Main.rand.NextBool(5))
				Projectile.NewProjectile(null, target.Center, Vector2.Zero, ProjectileID.HolyWater, 50, 2f);
		}
		
		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int dmg)
		{
			if (_equipped && item.HasElementItem(ElementID.Holy) && Main.rand.NextBool(5))
				Projectile.NewProjectile(null, target.Center, Vector2.Zero, ProjectileID.HolyWater, 50, 2f);
		}
	}
	
	public class BurningGraceRegenBuff : ModBuff
	{
		public override void SetStaticDefaults() => Main.buffNoSave[Type] = true;
		public override void Update(Player player, ref int idx) => player.lifeRegen += 5;
	}
	
	public class BurningGraceDefenseBuff : ModBuff
	{
		public override void SetStaticDefaults() => Main.buffNoSave[Type] = true;
		public override void Update(Player player, ref int idx) => player.statDefense += 14;
	}
}