using System;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class NebulaTyphoon : BaseNebulaTyphoon
    {
        public override string texture => "NebulaTyphoon";
        public override int Penetrate => 10;
        public override int NumProjectileKill => 10;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
    }
    public class NebulaTyphoon_Strong : BaseNebulaTyphoon
    {
        public override string texture => "NebulaTyphoon_Strong";
        public override int Penetrate => 15;
        public override int NumProjectileKill => 15;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
    }


    public abstract class BaseNebulaTyphoon : ModProjectile
    {
        public abstract string texture { get; }
        public abstract int Penetrate { get; }
        public abstract int NumProjectileKill { get; }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override string Texture => $"RemnantOfTheAncientsMod/Content/Projectiles/Mage/{texture}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Typhoon);
            Projectile.aiStyle = 71;
            Projectile.timeLeft = 800;
            Projectile.penetrate = Penetrate;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.penetrate--;

            target.AddBuff(BuffID.Ichor, 500);
            target.AddBuff(BuffID.CursedInferno, 500);
            base.OnHitNPC(target, hit, damageDone);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0) Projectile.Kill();
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) Projectile.velocity.X = -oldVelocity.X;
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.penetrate < 5)
            {
                spawnDeath(Projectile);
            }

            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Gastropod, 0f, 0f, 100, default(Color), 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        }

        public void spawnDeath(Projectile p)
        {
            float radus = 15 * 16f;
            int numProj = NumProjectileKill;
            for (int i = 0; i < numProj; i++)
            {
                float angulo = MathHelper.ToRadians(360f / numProj * i);

                Vector2 pos = new Vector2(p.Center.X + radus * (float)Math.Cos(angulo), p.Center.Y + radus * (float)Math.Sin(angulo));


                int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), pos.X, pos.Y, 0f, 0f, ProjectileID.Spark, p.damage /(numProj /2), 1f, Main.myPlayer);
                Main.projectile[proj].localAI[2] = 27;
                Main.projectile[proj].velocity = Vector2.Zero;

                int proj2 = Projectile.NewProjectile(Projectile.GetSource_Death(), pos.X, pos.Y, 0f, 0f, ProjectileID.PineNeedleFriendly, p.damage, 1f, Main.myPlayer);
                Main.projectile[proj2].velocity = (p.Center - Main.projectile[proj2].Center);

                Main.projectile[proj2].velocity.Normalize();
                Main.projectile[proj2].velocity *= 12;
                Main.projectile[proj2].tileCollide = false;
            }
        }
    }
}
