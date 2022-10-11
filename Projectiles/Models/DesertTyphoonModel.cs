using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Dusts;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent;

namespace RemnantOfTheAncientsMod.Projectiles
{

	public abstract class DesertTyphoonModel : ModProjectile
	{
		public override string Texture => "RemnantOfTheAncientsMod/Projectiles/Textures/DesertTyphoon";
		public abstract bool Friendly { get; }

		public abstract bool Hostile { get; }
		public abstract int PenetrateKill { get; }
		
		public override void SetStaticDefaults()
		{ 
			Main.projFrames[Projectile.type] = 3; 
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.DemonScythe);
			Projectile.aiStyle = ProjectileID.Typhoon;
			Projectile.width = 36;
			Projectile.height = 36;
			Projectile.friendly = Friendly;
			Projectile.hostile = Hostile;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = true;
			Projectile.penetrate = 5;
			Projectile.timeLeft = 200;
			Projectile.light = 0.75f;
			Projectile.extraUpdates = 1;
			Main.projFrames[Projectile.type] = 3;
			Projectile.ignoreWater = true;
				
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= PenetrateKill) Projectile.Kill();
			else
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -oldVelocity.X;
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextBool()) target.AddBuff(BuffType<Buffs.Burning_Sand>(), 300);

		}
	}
}
