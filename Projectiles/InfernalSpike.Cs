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

    public class InfernalSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalSpike"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 16;       //projectile width
            Projectile.height = 16;  //projectile height
            Projectile.friendly = false;  
			Projectile.hostile = true;     //make that the projectile will not damage you
           Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 2.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.BlueMoon;  
        }
            public override void AI() {
        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            
         }
    }
    public class InfernalSpikeF : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalSpikeF"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 16;       //projectile width
            Projectile.height = 16;  //projectile height
            Projectile.friendly = false;  
			Projectile.hostile = true;     //make that the projectile will not damage you
           Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 2.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.BlueMoon;  
        }
            public override void AI() {
        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            
         }
    }



    public class InfernalSpike_f : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalSpike"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 16;       //projectile width
            Projectile.height = 16;  //projectile height
            Projectile.friendly = true; 
           Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 1.5f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.BlueMoon;  
        }
            public override void AI() {
        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            
         }
    }
    public class InfernalSpikeF_f : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfernalSpikeF"); //projectile name
        }
            public override void SetDefaults()   
        {    
            Projectile.width = 16;       //projectile width
            Projectile.height = 16;  //projectile height
            Projectile.friendly = true;  
           Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            Projectile.light = 1.20f;    // projectile light
			Projectile.scale = 1.5f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.BlueMoon;  
        }
            public override void AI() {

        }
        
         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.defense = target.defense/2;
         }
    }
} 