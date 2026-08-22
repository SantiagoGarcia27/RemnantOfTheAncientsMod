using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{
	public class StoneBlunderbusHeldProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 2.ToCoordinatePosition();
			Projectile.height = 1.ToCoordinatePosition();
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 999100;
			Projectile.alpha = 0;
			Projectile.light = 0f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			AIType = -1;

		}
        float recoilRotation = MathHelper.ToRadians(20f);
        float maxRecoil = MathHelper.ToRadians(20f);

        bool isAnimation;
        int animationTimer;
        int animationTime = 30;

        int couldown = 0;
        int couldownMax = Utils1.FormatTimeToTick(Second: 1);
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.HeldItem.type != ModContent.ItemType<StoneBlunderbuss>())
            {
                Projectile.Kill();
                return;
            }
            DrawRotation(owner);
            if (couldown > 0) couldown--;
            

            bool clicked = owner.controlUseItem && !isAnimation;

            if (clicked && couldown <= 0)
            {
                isAnimation = true;
                animationTimer = 0;
                couldown = couldownMax;
                shoot();
            }

            if (isAnimation)
            {
                animationTimer++;

                float progress = animationTimer / (float)animationTime;

                if (progress < 0.5f)
                {
                    recoilRotation = MathHelper.Lerp(
                        0f,
                        maxRecoil,
                        progress * 2f
                    );
                }
                else
                {
                    recoilRotation = MathHelper.Lerp(maxRecoil,0f, (progress - 0.5f) * 2f);
                }

                if (animationTimer >= animationTime)
                {
                    isAnimation = false;
                    recoilRotation = 0f;
                }
            }
        }
        public void DrawRotation(Player owner)
        {
            Vector2 direccion = (Main.MouseWorld - owner.MountedCenter).SafeNormalize(Vector2.UnitX);

            float distanciaDeLaMano = 20f;

            Projectile.Center = owner.MountedCenter + direccion * distanciaDeLaMano;

            Projectile.rotation = direccion.ToRotation() + recoilRotation;

            if (direccion.X < 0)
            {
                Projectile.rotation += MathHelper.Pi;

            }
            owner.direction = direccion.X < 0 ? -1 : 1;
        }

        public void shoot()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.HeldItem.type == ModContent.ItemType<StoneBlunderbuss>())
            {
                Vector2 direccion = (Main.MouseWorld - owner.Center).SafeNormalize(Vector2.UnitX);
                Vector2 velocity = direccion * 20f;
                SoundEngine.PlaySound(
                    SoundID.Item40 with
                    {
                        Pitch = -0.4f,
                        Volume = 0.8f
                    },
                    owner.Center
                );
                SoundEngine.PlaySound(
                    SoundID.Item36 with
                    {
                        Pitch = -0.3f,
                        Volume = 0.7f
                    },
                    owner.Center
                );
                int damage = owner.HeldItem.damage;
                float knockBack = owner.HeldItem.knockBack;
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), owner.Center + new Vector2(16 * owner.direction, 0), velocity, ModContent.ProjectileType<GemstoneCrusherProj_Emerald>(), damage, knockBack, owner.whoAmI);
                Main.projectile[p].friendly = true;
                Main.projectile[p].hostile = false;
                Main.projectile[p].timeLeft = Utils1.FormatTimeToTick(Second: 10);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;
            Vector2 position = (Projectile.position - Main.screenPosition) + new Vector2(Projectile.width * 0.5f, Projectile.height - texture.Height * 0.5f + 2f);
            Player owner = Main.player[Projectile.owner];
            Vector2 direccion = (Main.MouseWorld - owner.MountedCenter).SafeNormalize(Vector2.UnitX);
            SpriteEffects effect =  SpriteEffects.None;
            if (direccion.X < 0) {
                effect = SpriteEffects.FlipHorizontally; 
            }
            Main.spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), lightColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, effect, 0f);
            return false;
        }
	}
}
