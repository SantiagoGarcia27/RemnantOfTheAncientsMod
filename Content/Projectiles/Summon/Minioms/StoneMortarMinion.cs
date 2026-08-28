using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static FargowiltasSouls.Content.Projectiles.EffectVisual;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms
{
    public class StoneMortarMinion : ModProjectile
    {


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 36;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1;
            Projectile.alpha = 0;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 18000;
            Projectile.sentry = true;
            AIType = -1;
        }


        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }

        NPC target = null;
        int timer = 0;
        int timmerMax = Utils1.FormatTimeToTick(Second: 2);
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            RemnantPlayer Modplayer = player.RemnantOfTheAncients();   
            if (!CheckActive(player)) return;

            Projectile.velocity.Y = 10;

            target = Projectile.FindTargetWithinRange(600f, true);

            if(target  == null) return;
            float angle = Projectile.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver2;
            if(angle > 0.7f) angle = 0.7f;
            if(angle < -0.7f) angle = -0.7f;
            Projectile.rotation = angle;

            if (timer++ >= timmerMax) 
            { 
                Shoot();
                timer = 0;
            }

            
        }

        private void Shoot()
        {
            Vector2 velocity = CalculateMortarVelocity(Projectile.Center, target, 12f);
            int i = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, velocity, ModContent.ProjectileType<GemstoneCrusherProj_Ruby>(), Projectile.damage, Projectile.knockBack);
            Main.projectile[i].hostile = false;
            Main.projectile[i].friendly = true;
            Main.projectile[i].penetrate = -1;
            Main.projectile[i].usesLocalNPCImmunity = true;
        }
        private Vector2 CalculateMortarVelocity(Vector2 start,NPC target,float speed)
        {
            float gravity = 0.3f;
            float distance = Vector2.Distance(start, target.Center);
            float predictionTime = distance / speed;

            Vector2 targetEstimatedPos = target.Center + target.velocity * predictionTime;
            Vector2 difference = targetEstimatedPos - start;

            float x = difference.X;
            float y = difference.Y;

            float speedSquared = speed * speed;

            float discriminant =
                speedSquared * speedSquared -
                gravity * (gravity * x * x + 2f * y * speedSquared);

            // El objetivo está fuera del alcance
            if (discriminant < 0f)
            {
                return difference.SafeNormalize(Vector2.Zero) * speed;
            }

            float sqrt = MathF.Sqrt(discriminant);

            // Arco alto, ideal para un mortero
            float angle = MathF.Atan2(speedSquared + sqrt,gravity * MathF.Abs(x));

            return new Vector2(
                MathF.Cos(angle) * speed * MathF.Sign(x),
                -MathF.Sin(angle) * speed
            );
        }
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(BuffType<StoneMortarBuff>());
                return false;
            }
            if (owner.HasBuff(BuffType<StoneMortarBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            Type type = GetType();
            String ruta = type.FullName.Replace('.', '/');
            string Texture = $"{ruta}_Base";

            Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;

            Vector2 pos = new(
                Projectile.position.X - Main.screenPosition.X + Projectile.width * 0.5f,
                Projectile.position.Y - Main.screenPosition.Y + Projectile.height - texture.Height * 0.5f
            );

            Rectangle source = new(0, 0, texture.Width, texture.Height);

            Main.spriteBatch.Draw(texture, pos, source, Color.White, 0, texture.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 1f);
            base.PostDraw(lightColor);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
    }
}