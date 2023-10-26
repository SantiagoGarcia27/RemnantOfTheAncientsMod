using System;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class SharknadoClone : BaseTornado
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Sharknado);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            AIType = -1;
        }
    }
    public class SharknadoShootClone : BaseTornado
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Sharknado);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            AIType = -1;
        }
    }
    public abstract class BaseTornado : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Sharknado}";
        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 6;
        public override void AI()
        {
            int num535 = 10;//10  Altura tornado
            int num536 = 15; //15
            float scale = 2f; //1 escala
            int TornadoMovmentStreght = 150; //150 
            int SegmentHeight = 8; //42 altura entre segmentos
            if (Projectile.type == ModContent.ProjectileType<SharknadoShootClone>())
            {
                num535 = 16;
                num536 = 16;
                scale = 2.5f;
            }
            if (Projectile.velocity.X != 0f)
            {
                Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 6)
            {
                Projectile.frame = 0;
            }
            if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
            {
                Projectile.localAI[0] = 1f;
                Projectile.position.X += Projectile.width / 2;
                Projectile.position.Y += Projectile.height / 2;
                Projectile.scale = (num535 + num536 - Projectile.ai[1]) * scale / (num536 + num535);
                Projectile.width = (int)(TornadoMovmentStreght * Projectile.scale);
                Projectile.height = (int)(SegmentHeight * Projectile.scale);
                Projectile.position.X -= Projectile.width / 2;
                Projectile.position.Y -= Projectile.height / 2;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] != -1f)
            {
                Projectile.scale = (num535 + num536 - Projectile.ai[1]) * scale / (num536 + num535);
                Projectile.width = (int)(TornadoMovmentStreght * Projectile.scale);
                Projectile.height = (int)(SegmentHeight * Projectile.scale);
            }
            if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 60)
                {
                    Projectile.alpha = 60;
                }
                if (Projectile.type == ModContent.ProjectileType<SharknadoShootClone>() && Projectile.alpha < 100)
                {
                    Projectile.alpha = 100;
                }
            }
            else
            {
                Projectile.alpha += 30;
                if (Projectile.alpha > 150)
                {
                    Projectile.alpha = 150;
                }
            }
            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0]--;
            }
            if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
                Vector2 center4 = Projectile.Center;
                center4.Y -= SegmentHeight * Projectile.scale / 2f;
                float num540 = (num535 + num536 - Projectile.ai[1] + 1f) * scale / num536 + num535;
                center4.Y -= SegmentHeight * num540 / 2f;
                center4.Y += 2f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), center4.X, center4.Y, Projectile.velocity.X, Projectile.velocity.Y, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
                int num541 = 4;
                if (Projectile.type == ModContent.ProjectileType<SharknadoShootClone>())
                {
                    num541 = 2;
                }
                if ((int)Projectile.ai[1] % num541 == 0 && Projectile.ai[1] != 0f)
                {
                    Vector2 velocity = new Vector2(10, 0);
                    
                    for (int i = 0; i < 2; i++)
                    {
                        int dir = (i == 0) ? 1 : -1;
                        velocity.X = velocity.X * 15 * dir;
                        int shark = Projectile.NewProjectile(Projectile.GetSource_FromThis(), center4, velocity, ModContent.ProjectileType<LittleShark>(), Projectile.damage, 0, Projectile.owner);
                        Main.projectile[shark].velocity = Projectile.velocity;
                        Main.projectile[shark].netUpdate = true;
                        Main.projectile[shark].scale = 2;
                        Main.projectile[shark].rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0);
                        Main.projectile[shark].spriteDirection = Main.projectile[shark].velocity.X > 0 ? -1 : 1;
                        if (Projectile.type == ModContent.ProjectileType<SharknadoShootClone>())
                        {
                            Main.projectile[shark].ai[2] = Projectile.width;
                            Main.projectile[shark].ai[3] = -1.5f;
                        }
                    }
                }
            }
            if (Projectile.ai[0] <= 0f)
            {
                float num544 = (float)Math.PI / 30f;
                float num545 = (float)Projectile.width / 5f;
                if (Projectile.type == ModContent.ProjectileType<SharknadoShootClone>())
                {
                    num545 *= 2f;
                }
                float num546 = (float)(Math.Cos(num544 * (0f - Projectile.ai[0])) - 0.5) * num545;
                Projectile.position.X -= num546 * -Projectile.direction;
                Projectile.ai[0]--;
                num546 = (float)(Math.Cos(num544 * (0f - Projectile.ai[0])) - 0.5) * num545;
                Projectile.position.X += num546 * -Projectile.direction;
            }
        }
    }

    public class SharknadoBoltClone : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.SharknadoBolt}";
        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 3;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SharknadoBolt);
            Projectile.penetrate = 1;
            AIType = -1;
        }
        public override void OnKill(int timeLeft)
        {

            int ParticleAmmount = 36;
            for (int i = 0; i < ParticleAmmount; i++)
            {
                Vector2 spinningpoint = Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width / 2f, Projectile.height) * 0.75f;
                spinningpoint = spinningpoint.RotatedBy((i - (ParticleAmmount / 2 - 1)) * (Math.PI * 2f) / ParticleAmmount) + Projectile.Center;
                Vector2 vector46 = spinningpoint - Projectile.Center;
                int dust = Dust.NewDust(spinningpoint + vector46, 0, 0, DustID.DungeonWater, vector46.X * 2f, vector46.Y * 2f, 100, default, 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = vector46;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                if (Projectile.ai[1] < 1f)
                {
                    int damage = Projectile.damage;
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X - Projectile.direction * 30, Projectile.Center.Y - 4f), new Vector2(Projectile.direction * 1f, 0f), ModContent.ProjectileType<SharknadoClone>(), damage, 4f, Projectile.owner, 16f, 15f);
                    Main.projectile[proj].netUpdate = true;
                    Main.projectile[proj].friendly = Projectile.friendly;
                    Main.projectile[proj].hostile = Projectile.hostile;
                }
                else
                {
                    Vector2 projectileCenterTile = new Vector2(Projectile.Center.X, Projectile.Center.Y);
                    int num419 = 100;
                    if (projectileCenterTile.X < 10)
                    {
                        projectileCenterTile.X = 10;
                    }
                    if (projectileCenterTile.X > Main.maxTilesX - 10)
                    {
                        projectileCenterTile.X = Main.maxTilesX - 10;
                    }
                    if (projectileCenterTile.Y < 10)
                    {
                        projectileCenterTile.Y = 10;
                    }
                    if (projectileCenterTile.Y > Main.maxTilesY - num419 - 10)
                    {
                        projectileCenterTile.Y = Main.maxTilesY - num419 - 10;
                    }
                    int num420 = (int)projectileCenterTile.Y + num419;
                    int num421 = (int)projectileCenterTile.Y + 15;
                    for (int i = (int)projectileCenterTile.Y; i < num420; i++)
                    {
                        Tile tile = Main.tile[(int)projectileCenterTile.X, i];
                        if (Main.tileSolid[tile.TileType] || tile.LiquidAmount > 0)
                        {
                            num421 = i;
                            break;
                        }
                    }
                    projectileCenterTile.Y = num421;
                    int damage = Projectile.damage;
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), projectileCenterTile.Y * 16 + 8, projectileCenterTile.X * 16 - 24, 0f, 0f, ModContent.ProjectileType<SharknadoShootClone>(), damage, 4f, Main.myPlayer, 16f, 24f);
                    Main.projectile[proj].netUpdate = true;
                    Main.projectile[proj].friendly = Projectile.friendly;
                    Main.projectile[proj].hostile = Projectile.hostile;
                }
            }
        }
    }
}
