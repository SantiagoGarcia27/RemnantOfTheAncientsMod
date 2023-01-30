using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.acorns;

namespace RemnantOfTheAncientsMod.Projectiles.Minioms.Saplings
{
	public class SaplingGoldMinion : HoverShooter
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Animated Golden Sapling Minion");
			Main.projFrames[Projectile.type] = 6;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.scale = 2f;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;
			inertia = 100f;
			Projectile.scale = 1f;
			shoot = ProjectileType<Stick_f_P>();
			shootSpeed = 9f;
			shootCool = 100f;

            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                shootCool = 70f;
				Projectile.tileCollide = false;
            }
        }
		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			Player1 modPlayer = player.GetModPlayer<Player1>();
			if (player.dead || !player.active) player.ClearBuff(BuffType<GoldenAcornBuff>());
			if (player.HasBuff(BuffType<GoldenAcornBuff>())) Projectile.timeLeft = 2;
		}
	}
}
