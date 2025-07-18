using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles
{

    public class DamageHitbox : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 20;
            Projectile.light = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            AIType = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Dust dust = new Dust();
            switch (Projectile.ai[0])
            {
                case 1:
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                    dust.noGravity = true;
                    break;
                case 2:
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.EnchantedNightcrawler, 0f, 0f, 100, default, 2f);
                    dust.noGravity = true;
                    player.statDefense += 3;

                    Vector2 offset = new Vector2(player.width / 2, player.height / 2) + new Vector2(24, 24);
                    for (int i = 0; i <RemnantOfTheAncientsMod.ParticleMeter(20); i++)
                    {
                        Vector2 dustPos = player.position - new Vector2(24, 24) + offset + new Vector2(60, 0).RotatedBy(MathHelper.ToRadians(18 * i));
                        var d = Dust.NewDustPerfect(dustPos, 172, Vector2.Zero);
                        d.noGravity = true;
                    }
                    break;
                    case 3:
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Demonite, 0f, 0f, 100, default, 2f);
                    dust.noGravity = true;
                    break;
            }
            if (Projectile.ai[1] == 1)
            {  
                Projectile.Center = player.Center;
            }
        }
        NPC lastTarget = null;
        static Item baseItem = null;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (baseItem != null && baseItem.netID != 0)
            {
                baseItem.GetGlobalItem<SaberGlobalItem>().OnHitNPC(baseItem, Main.player[Projectile.owner],target, hit, damageDone);
            }
            if (lastTarget == target)
            {
                damageDone = 0;
                return;
            }

            if (Projectile.ai[0] == 1)
            {
                target.AddBuff(BuffID.OnFire, 100);
            }
            if (Projectile.ai[0] == 3)
            {
                target.AddBuff(BuffID.ShadowFlame, 100);
            }

            

           // if(Main.player[Projectile.owner].direction == -1)


            float KbX = target.direction /** target.velocity.X*/ * hit.Knockback * target.knockBackResist;
            float KbY = target.direction /** target.velocity.Y*/ * hit.Knockback / 2 * target.knockBackResist;
            Vector2 knockbackVelocity = new Vector2(KbX, KbY);
            target.velocity -= knockbackVelocity;
            hit.Knockback = 0;

            lastTarget = target;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnKill(int timeLeft)
        {
           
        }

        public static void SetItem(Item item)
        {
            baseItem = item;
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
    }
}