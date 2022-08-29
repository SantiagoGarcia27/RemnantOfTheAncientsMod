using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Dusts;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Projectiles
{

    public class InfernalBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalBall"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 16;       //projectile width
            Projectile.height = 16;  //projectile height
            Projectile.friendly = false;  
			Projectile.hostile =true;     //make that the projectile will not damage you
           Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 1.5f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.TerraBeam;  
        }
            public override void AI() {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);
        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            
         }
    }
    public class InfernalBallF : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalBallF"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = false;  
			Projectile.hostile =true;     //make that the projectile will not damage you
           Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.TerraBeam;  
        }
            public override void AI() {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);
        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            
         }
    }
     public class InfernalBall_f : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalBall"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 16;       //projectile width
            Projectile.height = 16;  //projectile height
            Projectile.friendly = true;  
			Projectile.hostile =false;     //make that the projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 1.5f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.TerraBeam;  
        }
            public override void AI() {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);
        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            
         }
    }
    public class InfernalBallF_f : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalBallF_F"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;  
			Projectile.hostile =false;
            Projectile.DamageType = DamageClass.Magic;      //make that the projectile will not damage you
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.TerraBeam;  
        }
            public override void AI() {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;  
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(10f);
        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            
         }
    }
} 