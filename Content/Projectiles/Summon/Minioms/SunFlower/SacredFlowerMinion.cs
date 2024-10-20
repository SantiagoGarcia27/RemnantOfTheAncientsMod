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
    public class SacredFlowerMinion : ModProjectile
    {       
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Sacred Flowe Minion");
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
            Projectile.timeLeft = 36000;

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public int RangeMax = 30 * 16;
        public int HealTimmer = (int)Utils1.FormatTimeToTick(0, 0, 0, 7);
        public int Heal = 20;
        public int DefenseBonus = 10;
        public float DamageBonus = 1.05f;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CheckActive(player);
            Projectile.velocity = new Vector2(0, 7f);
            if(HealTimmer == 0)
            {
                HealTimmer = (int)(int)Utils1.FormatTimeToTick(0, 0, 0, 7);
            }
            else
            {
                HealTimmer--;
            }
            if (new RemnantOfTheAncientsMod().ParticleMeter(3) != 0)
            {
                AnimateTexture();
            }
            for (int p = 0; p < Main.maxPlayers; p++)
            {
                bool playerAvalible = Main.player[p].active && !Main.player[p].dead;
                bool playerOnRange = Projectile.WithinRange(Main.player[p].Center, RangeMax);

                if (playerAvalible && playerOnRange)
                {
                    Main.player[p].AddBuff(BuffID.Sunflower, (int)Utils1.FormatTimeToTick(0, 0, 0, 2));
                    if (HealTimmer == 1)
                    {
                        Main.player[p].HealEffect(Heal);
                        Main.player[p].statLife += Heal;     
                        SpawnParticles();
                    }
                    Main.player[p].statDefense += DefenseBonus;
                    Main.player[p].GetDamage(DamageClass.Generic) *= DamageBonus;
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
            if (player.HasBuff(BuffType<SacredFlowerMinionBuff>()))
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
                Main.spriteBatch.Draw((Texture2D)texture, Projectile.Center - Main.screenPosition, null, color, 0f, origin, 3.8f, SpriteEffects.None, 0f);
            }
            return true;
        }
         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}