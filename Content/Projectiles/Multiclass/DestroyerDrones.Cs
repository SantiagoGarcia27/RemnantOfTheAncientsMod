using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Multiclass
{
    public class HealingDrone : DroneModel
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HealingDrone");
        }
       
        public static int AttackCounter = 0;
        float distanceFromPlayer = 150f;
        float orbitSpeed = 0.02f;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Shoot(player);
            movement(player);
            checkActive(player);
            updateRotation(player);
            Projectile.Size = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height);
        }
        private void movement(Player player)
        {
            Vector2 orbitPosition = player.Center + new Vector2(distanceFromPlayer, 0).RotatedBy(Projectile.ai[0]);
            Projectile.position = orbitPosition - new Vector2(Projectile.width / 2, Projectile.height / 2);

            Projectile.ai[0] += orbitSpeed;
        }
        private void updateRotation(Player player)
        {
            Projectile.rotation = (Projectile.Center - player.Center).ToRotation();
        }

        private void checkActive(Player player)
        {
            if (player.dead || !player.active || !player.GetModPlayer<RemnantPlayer>().HealingDrone)
            {
                Projectile.timeLeft = 2;
                Projectile.Kill();
            }
            else
            {
                Projectile.timeLeft = 1000;
            }
        }
        private void Shoot(Player player)
        {
            if (AttackCounter == 0)
            {
                var p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, (player.position - Projectile.position), ModContent.ProjectileType<HealthBream>(), 0, 0f, Main.myPlayer);
                Main.projectile[p].hostile = false;
                Main.projectile[p].friendly = true;
                Main.projectile[p].damage = 1;
                AttackCounter = Utils1.FormatTime(0, 0, 0, 6);
            }
            else
            {
                AttackCounter--;
            }
        }
      
    }
    public class InterceptionDrone : DroneModel
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InterceptionDrone");
        }

        public static int AttackCounter = 0;
        float distanceFromPlayer = 150f;
        float orbitSpeed = 0.02f;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Shoot();
            movement(player);
            checkActive(player);
            updateRotation(player);
            Projectile.Size = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height);
        }
        public static Projectile target;
        private void movement(Player player)
        {
            Vector2 orbitPosition = player.Center + new Vector2(distanceFromPlayer, 0).RotatedBy(Projectile.ai[0]+3);
            Projectile.position = orbitPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);

            Projectile.ai[0] += orbitSpeed;
        }
        private void updateRotation(Player player)
        {
            if (SearchTarget() != null)
            {
                target = SearchTarget();
                Projectile.rotation = (Projectile.Center - target.Center).ToRotation();
            }
            else
            {
                Projectile.rotation = (Projectile.Center - player.Center).ToRotation();
            }     
        }

        private void checkActive(Player player)
        {
            if (player.dead || !player.active || !player.GetModPlayer<RemnantPlayer>().InterceptionDrone)
            {
                Projectile.timeLeft = 2;
                Projectile.Kill();
            }
            else
            {
                Projectile.timeLeft = 10000;
            }
        }

       
        private void Shoot()
        {
            if (AttackCounter == 0)
            {
                if (SearchTarget() != null)
                {
                    target = SearchTarget();
                    if (target.active)
                    {
                        Vector2 velocity = target.position - Projectile.position;
                        var p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, velocity, ModContent.ProjectileType<InterceptionBream>(), 0, 0, Main.myPlayer);
                        Main.projectile[p].tileCollide = false;
                        Main.projectile[p].damage = 0;
                        Main.projectile[p].velocity += new Vector2(1, 0);
                        AttackCounter = (int)Utils1.FormatTime(0f, 0f, 0f, 5f);
                    }
                }
            }
            else
            {
                AttackCounter--;
            }
        }
        public float Range = 40f * 16f;
        private Projectile SearchTarget()
        {
            for(int i = 0;i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].hostile && !Main.projectile[i].friendly && Main.projectile[i].active)
                {
                    float distance = Vector2.Distance(Projectile.position, Main.projectile[i].position);
                    if (distance < Range)
                    {
                       // var a = Main.projectile[i].Name;
                        return Main.projectile[i];
                    }
                }
            }
            return null;
        }
    }
    public abstract class DroneModel : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2000;
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = -1;
        }
       
    }
}