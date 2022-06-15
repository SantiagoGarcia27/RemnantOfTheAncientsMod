using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace opswordsII.Projectiles
{

    public class Frozenp : ModProjectile
    {
        public override string Texture => "opswordsII/Projectiles/Frozenp";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozenp"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = false;      //make that the projectile will not damage you
            Projectile.hostile = true;
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 2;      //how many NPC will penetrate
            Projectile.timeLeft = 20000;   //how many time this projectile has before disepire
            Projectile.light = 1.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.FrostWave;  

           /* Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null){
            Projectile.tileCollide = false;
            Projectile.timeLeft = 900;*/
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
       public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position; // Position to use for dusts
			
			// Please note the usage of MathHelper, please use this!
			// We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
			usePos += rotVector * 16f;

			// Declaring a constant in-line is fine as it will be optimized by the compiler
			// It is however recommended to define it outside method scope if used elswhere as well
			// They are useful to make numbers that don't change more descriptive
			const int NUM_DUSTS = 20;

			// Spawn some dusts upon javelin death
			for (int i = 0; i < NUM_DUSTS; i++) {
				// Create a new dust
				Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, 81);
				dust.position = (dust.position + Projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
            
        }
          public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
              
              target.immune[Projectile.owner] = 0;
        }
    }



    public class frozen_p_f : ModProjectile
    {
        public override string Texture => "opswordsII/Projectiles/Frozenp";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("frozen_p_f"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 2;      //how many NPC will penetrate
            Projectile.timeLeft = 20000;   //how many time this projectile has before disepire
            Projectile.light = 0.15f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
             Projectile.DamageType = DamageClass.Magic; 
            AIType = ProjectileID.FrostWave;  
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
       public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position; // Position to use for dusts
			
			// Please note the usage of MathHelper, please use this!
			// We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
			usePos += rotVector * 16f;

			// Declaring a constant in-line is fine as it will be optimized by the compiler
			// It is however recommended to define it outside method scope if used elswhere as well
			// They are useful to make numbers that don't change more descriptive
			const int NUM_DUSTS = 20;

			// Spawn some dusts upon javelin death
			for (int i = 0; i < NUM_DUSTS; i++) {
				// Create a new dust
				Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, 81);
				dust.position = (dust.position + Projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
       }
              public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
              
              target.immune[Projectile.owner] = 0;
        }
    }


    public class Frozenp_M : ModProjectile
    {
        public override string Texture => "opswordsII/Projectiles/Frozenp";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("frozenp_M"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 2;      //how many NPC will penetrate
            Projectile.timeLeft = 20000;   //how many time this projectile has before disepire
            Projectile.light = 0.15f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.FrostWave;

           /* Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null)
			Projectile.tileCollide = false; */
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
       public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position; // Position to use for dusts
			
			// Please note the usage of MathHelper, please use this!
			// We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
			usePos += rotVector * 16f;

			// Declaring a constant in-line is fine as it will be optimized by the compiler
			// It is however recommended to define it outside method scope if used elswhere as well
			// They are useful to make numbers that don't change more descriptive
			const int NUM_DUSTS = 20;

			// Spawn some dusts upon javelin death
			for (int i = 0; i < NUM_DUSTS; i++) {
				// Create a new dust
				Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, 81);
				dust.position = (dust.position + Projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
        }
          public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
              
             target.immune[Projectile.owner] = 0;


        }
    }
}