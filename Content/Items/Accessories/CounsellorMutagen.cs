using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace RagnarokOfRedemptionAPI.Content.Items.Accessories
{
    public class CounsellorMutagen : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.HasMod("Redemption") &&
                   ModLoader.HasMod("ContinentOfJourney") &&
                   ModLoader.HasMod("HomewardRagnarok");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 25);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 0.25f;

            player.whipRangeMultiplier *= 1.15f;

            var modPlayer = player.GetModPlayer<CounsellorMutagenPlayer>();
            modPlayer.minionCritChance = 0.07f;
        }

        public override void AddRecipes()
        {
            Mod redemption = ModLoader.GetMod("Redemption");
            Mod continent = ModLoader.GetMod("ContinentOfJourney");
            Mod calamity = ModLoader.GetMod("CalamityMod");

            int mutagenType = redemption.Find<ModItem>("MutagenSummon").Type;
            int badgeType = continent.Find<ModItem>("CounsellorBadge").Type;
            int crownType = continent.Find<ModItem>("WillToCrown").Type;
            int tileType = calamity.Find<ModTile>("CosmicAnvil").Type;

            CreateRecipe()
                .AddIngredient(mutagenType)
                .AddIngredient(badgeType)
                .AddIngredient(crownType, 5)
                .AddTile(tileType)
                .Register();
        }
    }
}

    public class CounsellorMutagenPlayer : ModPlayer
    {
        public float minionCritChance = 0f;

        public override void ResetEffects()
        {
            minionCritChance = 0f;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (proj.DamageType == DamageClass.Summon && minionCritChance > 0f)
            {
                if (Main.rand.NextFloat() < minionCritChance)
                {
                    modifiers.SetCrit();
                }
            }
        }
    }
