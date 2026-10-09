using Microsoft.Xna.Framework.Graphics;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
	public class GodClaws : ModProjectile
	{
		public int FrameCount = 0;
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 7;
		}
		public override void SetDefaults()
		{
			Projectile.width = 100;
			Projectile.height = 150;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.penetrate = 100;
			Projectile.timeLeft = 100000;
			Projectile.light = 1.75f;
			Projectile.scale = 1f;
			Projectile.extraUpdates = 0;
			Projectile.ignoreWater = true;
			AIType = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown= 1;
		}
		public override void AI()
		{
			Projectile.direction = Main.player[Projectile.owner].direction;
			Projectile.spriteDirection = Projectile.direction;

            AnimateTexture();

        }
        public override bool PreDraw(ref Microsoft.Xna.Framework.Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
        public void AnimateTexture()
        {
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
				{
					Projectile.frame = 0;
					Projectile.Kill();
				}
			}
		}
      
         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 0;

			if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
			{
				int buffType = CallUtils.TryGetBuffFromMod(CalamityMod, "GodSlayerInferno", DefaultValue: - 1);
				if (buffType == -1) buffType = BuffID.Daybreak;

				target.AddBuff(buffType, 400);
			}
			target.AddBuff(BuffID.OnFire, 1080);
			target.AddBuff(BuffID.BrokenArmor, 1080);
			target.AddBuff(BuffID.Slow, 1080);
			target.AddBuff(BuffID.CursedInferno, 1080);
			target.AddBuff(BuffID.Ichor, 1080);
		}
	}
}
