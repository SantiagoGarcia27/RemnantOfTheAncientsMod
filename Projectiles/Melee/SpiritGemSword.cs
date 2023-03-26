using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Projectiles.Melee
{
    public class SpiritGemSword : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 300;   //how many time this projectile has before disepire
            Projectile.light = 1.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.7f;
            Projectile.timeLeft = 1000;
            AIType = ProjectileID.InfluxWaver;
        }

        public override void AI()
        {
            //Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            //Projectile.rotation = Projectile.velocity.ToRotation(); //+ MathHelper.ToRadians(0f);
            //Projectile.rotation += Projectile.direction * 0.8f;

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            /* if (Projectile.alpha > 70)
             {
                 Projectile.alpha -= 15;
                 if (Projectile.alpha < 70) Projectile.alpha = 70;
             }
             if (Projectile.localAI[0] == 0f)
             {
                 AdjustMagnitude(ref Projectile.velocity);
                 Projectile.localAI[0] = 10f;
             }
             Vector2 move = Vector2.Zero;
             float distance = 235f;
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
             }*/


        }
        public override void OnSpawn(IEntitySource source)
        {
        }
        public override void Kill(int timeLeft)
        {
        }
        private static readonly Color GeodeColorOne = GetRGeodeColor(1);

        private static readonly Color GeodeColorTwo = GetRGeodeColor(2);
        public override Color? GetAlpha(Color lightColor)
        {
            switch (Projectile.ai[1])
            {
                case 0:
                    return Color.Yellow;  
                case 1:
                    return Color.Violet;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Green;
                case 4:
                    return Color.Red;
                case 5:
                    return Color.White;
                case 6:
                    return new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B);                 
                default: 
                  return Color.Black;
                    
            }
        }
        public static Color GetRGeodeColor(int x)
        {
            Color color = new Color(0, 0, 0);
            if (x == 1) color = new Color(255, 255, 255);
            else if (x == 2) color = new Color(0, 0, 0);
            return color;
        }
    }
}
