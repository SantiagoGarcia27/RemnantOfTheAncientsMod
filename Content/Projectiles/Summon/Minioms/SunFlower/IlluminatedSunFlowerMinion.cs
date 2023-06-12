using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower
{
    public class IlluminatedSunFlowerMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Illuminated SunFlower Minion");
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }
        public sealed override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 60;
            Projectile.sentry = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 100;
            Projectile.manualDirectionChange = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 2000;

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public int RangeMax = 25;
        public int HealTimmer = Utils1.FormatTime(0, 0, 0, 7);
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CheckActive(player);
            Projectile.velocity = new Vector2(0, 7f);
            if(HealTimmer == 0)
            {
                HealTimmer = Utils1.FormatTime(0, 0, 0, 7);
            }
            else
            {
                HealTimmer--;
            }
            if (new RemnantOfTheAncientsMod().ParticleMeter(3) != 0)
            {
                AnimateTexture();
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                for (int p = 0; p < RemnantOfTheAncientsMod.MaxPlayers; p++)
                {
                    if (Projectile.Distance(Main.player[p].Center) <= RangeMax * 16)
                    {
                        Main.player[p].AddBuff(BuffID.Sunflower, Utils1.FormatTime(0, 0, 0, 2));
                        Main.player[p].statDefense += 5;
                        if (HealTimmer == 1)
                        {
                            Main.player[p].HealEffect(10);
                            SpawnParticles();
                        }
                    }
                }
            }
            else
            {
                if (Projectile.Distance(player.Center) <= RangeMax *16)
                {
                    player.AddBuff(BuffID.Sunflower, Utils1.FormatTime(0, 0, 0, 2));
                    player.statDefense += 5;
                    if (HealTimmer == 1)
                    {
                        player.HealEffect(10);
                        SpawnParticles();
                    }
                }
            } 
        }
        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 9)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        public void SpawnParticles()
        {
            Vector2 offset = new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(24, 24);//24
            for (int i = 0; i < new RemnantOfTheAncientsMod().ParticleMeter(90); i++)
            {
                Vector2 dustPos = Projectile.position - new Vector2(24, 24) + offset + new Vector2(RangeMax * 16, 0).RotatedBy(MathHelper.ToRadians(18 * i));//60
                var d = Dust.NewDustPerfect(dustPos, DustID.GrassBlades, Vector2.Zero);
                d.noLight = false;
                d.noGravity = true;
            }
        }
        public void CheckActive(Player player)
        {  
            if (player.HasBuff(BuffType<IlluminatedSunflowerMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
        public float fade = 2.6f;
        public override bool PreDraw(ref Color lightColor)
        {
            var texture = Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Summon/Minioms/AreaEffect");
            Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
            if (Main.myPlayer == Projectile.owner)
            {
                Color color = new Color(Color.Green.R, Color.Green.G, Color.Green.B, 20) * fade;
                Main.spriteBatch.Draw((Texture2D)texture, Projectile.Center - Main.screenPosition, null, color, 0f, origin, 3.2f, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 2;
        }
    }
}