using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
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
            //AIType = ProjectileID.InfluxWaver;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            //GetAlpha(Color.Wheat);
        }
         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 1)
            {
                int numProjectiles = 6;
                float spacing = MathHelper.TwoPi / numProjectiles;
                float radius = 10 * 16f;

                for (int i = 0; i < numProjectiles; i++)
                {
                    Vector2 projectilePos = target.Center + radius * new Vector2((float)Math.Cos(spacing * i), (float)Math.Sin(spacing * i));
                    Vector2 projectileVel = 10f * (target.Center - projectilePos).SafeNormalize(Vector2.Zero);

                    int proj = Projectile.NewProjectile(Projectile.GetSource_None(), projectilePos, projectileVel, ModContent.ProjectileType<SpiritGemSword>(), damageDone / 3, hit.Knockback, Main.myPlayer, 2, 2);
                    float angle = (float)Math.Atan2(projectileVel.Y, projectileVel.X);
                    Main.projectile[proj].rotation = angle;
                }
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            if(Projectile.ai[0] == 2)
            {
                Projectile.scale *= DificultyUtils.ReaperMode ? 2.5f: 1f;
                Projectile.damage /= 4;
                Projectile.ai[1] = 2;
            }
        }
        public override void OnKill(int timeLeft)
        {
       
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        }
    
        private static readonly Color GeodeColorOne = GetRGeodeColor(1);

        private static readonly Color GeodeColorTwo = GetRGeodeColor(2);
        public override Color? GetAlpha(Color lightColor)
        {
            return base.GetAlpha(lightColor);
        }
        public override void PostDraw(Color lightColor)
        {
            lightColor = Color.Blue;
            base.PostDraw(lightColor);
        }
        public override bool PreDraw(ref Color lightColor)
        {        
            lightColor = Projectile.ai[1] switch
            {
                0 => Color.Yellow,
                1 => Color.Violet,
                2 => Color.Blue,
                3 => Color.Green,
                4 => Color.Red,
                5 => Color.White,
                6 => new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B),
                7 => Color.SandyBrown,
                _ => Color.Black,
            };
            return true;
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
