using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{

    public class ElectricRailGunProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{ 
             ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 3000;
		}

		public override void SetDefaults()
		{  
            Projectile.width = 5;      
			Projectile.height = 5;  
			Projectile.friendly = true;     
			Projectile.tileCollide = true; 
			Projectile.penetrate = -1;     
			Projectile.timeLeft = 80;
            Projectile.DamageType = DamageClass.Magic;
			Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            AIType = -1;
		}
        
	
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            state = HookState.Attached;      
            return false;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            state = HookState.Attached;
            host = target;
            damageDone = 1;
            hit.Damage = 1;

            base.OnHitNPC(target, hit, damageDone);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetMaxDamage(1);
           
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override bool? CanDamage()
        {
            return host == null;
        }
        public override bool MinionContactDamage()
        {
            
            return false;
        }
        enum HookState {Throw, Pull, Attached };

		HookState state = HookState.Throw;
        NPC host = null;
        int timmer = 0;
        int timmerMax = 0;
		public override void AI()
		{         
			Player player = Main.player[Projectile.owner];
            timmerMax = Utils1.FormatTimeToTick(Second: 0.15f);
			if (state == HookState.Attached)
			{
                if (host == null)
                {
                    Projectile.timeLeft = 40;
                    Projectile.velocity = Vector2.Zero;
                }
                else
                {
                    Projectile.timeLeft = 40;
                    Projectile.velocity = Vector2.Zero;
                    Projectile.position = host.Center;
                }
			
                if(timmer++ >= timmerMax)
                {
                    Vector2 velocity = Projectile.Center - player.Center;
                    velocity.Normalize();
                    velocity *= 20;
                    var p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, velocity,ProjectileID.MartianTurretBolt, Projectile.damage,10f,player.whoAmI);
                    Main.projectile[p].hostile = false;
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].scale = 2f;
                    timmer = 0;
                }
                if(host != null && !host.active)
                {

                    state = HookState.Pull;
                }
            }
			else if (state == HookState.Pull)
			{
                Projectile.velocity = (player.Center - Projectile.Center);
                Projectile.velocity.Normalize();
                Projectile.velocity *= 20;
				if (player.Distance(Projectile.Center) < 10)
				{
					Projectile.Kill();
				}
			}
            else
            {
                Projectile.GetGlobalProjectile<RemnantGlobalProjectile>().HommingProjectile(Projectile, speed: 10);
            }
			if ((player.ownedProjectileCounts[Type] >= 1 && Main.mouseLeft && state == HookState.Attached) || (state != HookState.Attached && Projectile.timeLeft <= 40) || player.dead || player.Distance(Projectile.Center) > 1000 || player.HeldItem.shoot != Projectile.type)
			{
				state = HookState.Pull;
            }
			base.AI();
		}
        Color[] lineColor = [Color.White, Color.White];
        Texture2D texture = null;
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 origin;
            SpriteEffects effects;
            Vector2 Offset = Vector2.Zero;

            if (Projectile.velocity.X != 0 || Projectile.velocity.Y != 0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(270f);
            }
            if (Projectile.spriteDirection > 0)
            {
                origin = new Vector2(0, Projectile.height);
               // rotationOffset = MathHelper.ToRadians(180f);
                effects = SpriteEffects.None;
                Offset += new Vector2(6, 0);
            }
            else
            {
                origin = new Vector2(Projectile.width, Projectile.height);
               // rotationOffset = MathHelper.ToRadians(135f);
                effects = SpriteEffects.FlipHorizontally;
                Offset += new Vector2(-6, 0);
            } 
            if(texture == null)
             texture = ModContent.Request<Texture2D>(Texture).Value;

            Main.spriteBatch.Draw(texture, (Projectile.position - Main.screenPosition) - Offset, default, lightColor * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, effects, 0);


            lineColor = [Color.White, Color.Cyan];
            Utils.DrawLine(Main.spriteBatch, Projectile.Center, Main.player[Projectile.owner].Center, lineColor[0], lineColor[1], 3f);
            // Since we are doing a custom draw, prevent it from normally drawing
            return false;
        }    
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
    }
}
