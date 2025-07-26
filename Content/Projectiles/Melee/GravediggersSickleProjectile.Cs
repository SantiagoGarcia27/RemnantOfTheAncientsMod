using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public abstract class SickleThrowModel : ModProjectile
    {
        public abstract int itemBaseType { get; }

        int totalTimeLeft = 0;
        bool canBeKilled = false;
        bool canReturn = false;
        public override string Texture => GetTexturePath();

        string GetTexturePath()
        {
            string Texture = itemBaseType > 0 ? (ItemLoader.GetItem(itemBaseType).Texture) : "Terraria/Images/Item_0";
            return Texture;
        }
        public override void SetDefaults()
        {
            //AIType = ProjectileID.WoodenBoomerang;
            Projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
            Projectile.Size = new Vector2(80, 80);
            Projectile.scale = 1f;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1500;
        }
        public override bool PreKill(int timeLeft)
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.Center.Distance(Projectile.Center) <= 50)
                return true;
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            canReturn = true;
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            totalTimeLeft = Projectile.timeLeft;
            base.OnSpawn(source);
        }
        float ghostTimmer = Utils1.FormatTimeToTick(0, 0, 0,0.5f);
        public override void AI()
        {

            if (DistanceUtils.ExistTile(Projectile.Center) || DistanceUtils.TouchFlour(Projectile))
            {
                Projectile.velocity.Y -= 0.2f;
                Projectile.position.Y -= 2f;
            }
            Projectile.rotation += Projectile.direction * 0.8f;
            if (ghostTimmer-- <= 0)
            {
                Projectile.tileCollide = canReturn? false : true;
            }
            Player owner = Main.player[Projectile.owner];
            float distance = owner.Center.Distance(Projectile.Center);
            if (Projectile.timeLeft <= totalTimeLeft / 2 || distance > totalTimeLeft / 4)
            {
                canReturn = true;
            }


            if (canReturn)
            {
                Projectile.velocity = owner.Center - Projectile.Center;
                Projectile.velocity.Normalize();
                Projectile.velocity *= owner.HeldItem.shootSpeed == 0 ? 10f : owner.HeldItem.shootSpeed * 2;
                canBeKilled = true;
            }

            if (distance <= 30 * Projectile.scale && canBeKilled)
                Projectile.Kill();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = ModContent.Request<Texture2D>(GetTexturePath()).Value;

            // Calculate texture origin (center of texture)
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            // Calculate scale to match hitbox size
            Vector2 textureScale = new Vector2(
                Projectile.width / (float)texture.Width,
                Projectile.height / (float)texture.Height
            ) * Projectile.scale;

            // Draw texture centered on the projectile's position
            spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                textureScale,
                SpriteEffects.None,
                0f
            );

            return false; // Skip base.PreDraw since we're handling drawing completely
        }
    };

    public class GravediggersSickleProjectile : SickleThrowModel
    {
        public override int itemBaseType => ModContent.ItemType<GravediggersSickle>();
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
    }
    public class NightLotusSickleProjectile : SickleThrowModel
    {
        public override int itemBaseType => ModContent.ItemType<NightLotus>();
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            int proj = Projectile.NewProjectile(Projectile.GetSource_None(), target.Center, (new Vector2(target.direction, 0f) * new Vector2(0.5f, 0.5f)).RotatedByRandom(90), ProjectileID.LightsBane, 10, 0, owner.whoAmI, Main.rand.NextFloat(-90.6f, 90.6f), owner.itemAnimationMax, 1f);
            Main.projectile[proj].ai[0] = Main.rand.NextFloat(0.5f, 1.6f);
            Main.projectile[proj].rotation = Main.rand.NextFloat(-90.6f, 90.6f);
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player owner = Main.player[Projectile.owner];
            int proj = Projectile.NewProjectile(Projectile.GetSource_None(), target.MountedCenter, (new Vector2(target.direction, 0f) * new Vector2(0.5f, 0.5f)).RotatedByRandom(90), ProjectileID.LightsBane, 10, 0, target.whoAmI, Main.rand.NextFloat(-90.6f, 90.6f), target.itemAnimationMax, 1f);
            Main.projectile[proj].ai[0] = Main.rand.NextFloat(0.5f, 1.6f);
            Main.projectile[proj].rotation = Main.rand.NextFloat(-90.6f, 90.6f);
            base.OnHitPlayer(target, info);
        }
    }
}