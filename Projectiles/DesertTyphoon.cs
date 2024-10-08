using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Dusts;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent;

namespace opswordsII.Projectiles
{

    public class DesertTyphoon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DesertTyphoon"); //projectile name
            
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
           Projectile.DamageType = DamageClass.Magic;          // 
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 5;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time this projectile has before disepire
            Projectile.light = 0.75f;    // projectile light
            Projectile.extraUpdates = 1;
			Main.projFrames[Projectile.type] = 3;
            Projectile.ignoreWater = true; 
            Projectile.aiStyle = ProjectileID.Typhoon;
			Projectile.CloneDefaults(ProjectileID.DemonScythe);

        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
		Projectile.penetrate--;
			if (Projectile.penetrate <= 30)
			{
				Projectile.Kill();
			}
			else
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				// If the projectile hits the left or right side of the tile, reverse the X velocity
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}

				// If the projectile hits the top or bottom side of the tile, reverse the Y velocity
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}

			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}
		public override void AI()
		{
			 Projectile.rotation += (float)Projectile.direction * 0.8f;
			 
			if (Projectile.alpha > 70)
			{
				Projectile.alpha -= 15;
				if (Projectile.alpha < 70)
				{
					Projectile.alpha = 70;
				}
			}
			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 10f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[k].Center - Projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
				if (target)
				{
					AdjustMagnitude(ref move);
					Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
					AdjustMagnitude(ref Projectile.velocity);
				}
				if (Projectile.alpha <= 100)
				{
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<QuemaduraA>());
					Main.dust[dust].velocity /= 0.5f;
				}
			}
		}
            private void AdjustMagnitude(ref Vector2 vector)
            {
				float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
				if (magnitude > 106f) {
					vector *= 26f / magnitude;
				}
			}
		
                   public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			if (Main.rand.NextBool()) {
				target.AddBuff(BuffType<Buffs.QuemaduraArenosa>(), 300);
			}
        }
    }
	
	 public class DesertTyphoonE : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DesertTyphoonE"); //projectile name
            Main.projFrames[Projectile.type] = 3;
        }
            public override void SetDefaults()   
        {   
			
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = false;      //make that the projectile will not damage you
           Projectile.DamageType = DamageClass.Melee;   
			Projectile.hostile =true;       // 
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 5;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time this projectile has before disepire
            Projectile.light = 0.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true; 
            
			
			

        }
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}
		public override void AI()
		{

			 Projectile.rotation += (float)Projectile.direction * 0.8f;
			if (Projectile.alpha > 70)
			{
				Projectile.alpha -= 15;
				if (Projectile.alpha < 70)
				{
					Projectile.alpha = 70;
				}
			}
			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 10f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[k].Center - Projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
				if (target)
				{
					AdjustMagnitude(ref move);
					Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
					AdjustMagnitude(ref Projectile.velocity);
				}
				if (Projectile.alpha <= 100)
				{
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<QuemaduraA>());
					Main.dust[dust].velocity /= 0.5f;
				}
			}
		}
            private void AdjustMagnitude(ref Vector2 vector)
            {
				float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
				if (magnitude > 106f) {
					vector *= 26f / magnitude;
				}
			}
		
                   public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			if (Main.rand.NextBool()) {
				target.AddBuff(BuffType<Buffs.QuemaduraArenosa>(), 300);
			}
        }
  }
  }
