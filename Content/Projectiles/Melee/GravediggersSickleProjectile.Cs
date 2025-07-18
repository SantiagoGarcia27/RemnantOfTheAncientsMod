using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{

    public class GravediggersSickleProjectile : ModProjectile
    {
        public override string Texture => ItemLoader.GetItem(ModContent.ItemType<GravediggersSickle>()).Texture;
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            //AIType = ProjectileID.WoodenBoomerang;
            Projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
            Projectile.scale = 1.5f;
            Projectile.tileCollide = Projectile.scale * ReaperGlobalItem.currentScale > 1.5f ? false : true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1500;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
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
        int totalTimeLeft = 0;
        bool canBeKilled = false;
        bool canReturn = false;
        public override void AI()
        {
            Projectile.rotation += Projectile.direction * 0.8f;
           
            Player owner = Main.player[Projectile.owner];
            float distance = owner.Center.Distance(Projectile.Center);
            if (Projectile.timeLeft  <= totalTimeLeft / 2 || distance > totalTimeLeft/4)
            {
                canReturn = true;
            }


            if (canReturn)
            { 
                Projectile.velocity = owner.Center - Projectile.Center;
                Projectile.velocity.Normalize();
                Projectile.velocity *=  owner.HeldItem.shootSpeed == 0? 10f : owner.HeldItem.shootSpeed * 2;
                canBeKilled = true;
            }

            if (distance <= 20 && canBeKilled)
                Projectile.Kill();
        }
    }
}