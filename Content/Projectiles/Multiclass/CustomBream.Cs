using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Multiclass
{
    internal class HealthBream : BaseBream
    {
        public override Color color => Color.LightGreen;
        public override int ProjId => ProjectileID.PinkLaser;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.hostile && !Projectile.friendly)
            {
                target.life += damage;
                target.HealEffect(damage);
            }
        }
        public override void AI()
        {
            base.AI();
            if (!Projectile.hostile && Projectile.friendly)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead)
                    {
                        if (Vector2.Distance(Projectile.Center, Main.player[i].Center) < 10)
                        {
                            Main.player[i].Heal((int)Utils1.GetValueFromPorcentage(Main.player[i].statLifeMax2, 5));
                            Projectile.Kill();
                        }
                    }
                }
            }
            else if(Projectile.hostile && !Projectile.friendly)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].friendly)
                    {
                        if (Main.npc[i].type != NPCID.Retinazer)
                        {
                            float Distance = Vector2.Distance(Projectile.Center, Main.npc[i].Center);
                            if (Distance <= 50)
                            {
                                if (Main.npc[i].life < Main.npc[i].lifeMax - Projectile.damage)
                                {
                                    Main.npc[i].life += Projectile.damage;
                                    Main.npc[i].HealEffect(Projectile.damage);
                                    Projectile.Kill();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    internal class InterceptionBream : BaseBream
    {
        public override Color color => Color.Blue;
        public override int ProjId => ProjectileID.PinkLaser;

        public override void AI()
        {
            base.AI();
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
               var f = Main.projectile[i].Name;
                if (Main.projectile[i].hostile != Projectile.hostile  && Main.projectile[i].friendly != Projectile.friendly)
                { 
                    if (Vector2.Distance(Projectile.Center, Main.projectile[i].Center) < 20)
                    {
                        Main.projectile[i].Kill();
                        Projectile.Kill();
                    }
                }
            }
        }
    }

    public abstract class BaseBream: ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Projectiles/Multiclass/CustomBream";
        public abstract int ProjId { get; }
        public abstract Color color { get; }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjId);
            Projectile.height = 50;
            Projectile.width = 50;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.knockBack = -5f;
            AIType = ProjId;
        }  
        public override Color? GetAlpha(Color lightColor) => color;
        public override void AI()
        {
           // Projectile.Size = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height);
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
            base.AI();
        }
    }
}
