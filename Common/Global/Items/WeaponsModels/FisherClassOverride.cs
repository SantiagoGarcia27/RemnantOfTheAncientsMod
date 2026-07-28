using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels
{
    public class FisherClassOverride : GlobalItem
    {
        readonly Mod Calamity = RemnantOfTheAncientsMod.CalamityMod;
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item item)
        {
            /*if (item.type == ItemID.PurpleClubberfish)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.ReaverShark)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.Rockfish)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.SawtoothShark)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.FrostDaggerfish)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.Swordfish)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.Toxikarp)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.Bladetongue)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.CrystalSerpent)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
            if (item.type == ItemID.ObsidianSwordfish)
            {
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }*/
            SetFishClass(item);
        }
        public static void SetFishClass(Item item)
        {
            if (item.fishingPole > 0)
            {
                //item.damage = item.fishingPole - 1;
                //item.DamageType = ModContent.GetInstance<FisherDamageClass>();
                item.autoReuse = false;
                item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && item.fishingPole > 0)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<BoberHook>()] <= 0)
                {
                    var p = Projectile.NewProjectile(source, position, velocity * 1.5f, ModContent.ProjectileType<BoberHook>(), 1, 1, player.whoAmI, ai2: item.shoot);
                }
                return false;
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }


        /* public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
         {
             if (Calamity != null)
             {
                 if (item.type == CallUtils.TryGetItemFromMod(Calamity, "TheDevourerofCods"))
                 {
                     FixModRodsBobers(source, position, velocity, type, damage, knockback, player.whoAmI, 10);
                     return false;
                 }
                 else if (item.type == CallUtils.TryGetItemFromMod(Calamity, "RiftReeler"))
                 {
                     FixModRodsBobers(source, position, velocity, type, damage, knockback, player.whoAmI, new Vector2(3, 6));
                     return false;
                 }
                 else if (item.type == CallUtils.TryGetItemFromMod(Calamity, "FeralDoubleRod"))
                 {
                     FixModRodsBobers(source, position, velocity, type, damage, knockback, player.whoAmI, 2);
                     return false;
                 }
                 else if (item.type == CallUtils.TryGetItemFromMod(Calamity, "EarlyBloomRod"))
                 {
                     FixModRodsBobers(source, position, velocity, type, damage, knockback, player.whoAmI, 6);
                     return false;
                 }
             }
             return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
         }
         public static void FixModRodsBobers(IEntitySource source, Vector2 velocity, Vector2 position, int type, int damage, float knockback, int player, int BoberAmount)
         {
             for (int index = 0; index < BoberAmount; index++)
             {
                 Vector2 Velocity = velocity.RotatedByRandom((double)MathHelper.ToRadians(18f));
                 Projectile.NewProjectile(source, position, Velocity, type, damage, knockback, player);
             }
         }
         public static void FixModRodsBobers(IEntitySource source, Vector2 velocity, Vector2 position, int type, int damage, float knockback, int player, Vector2 BoberAmount)
         {
             for (int index = 0; index < Main.rand.Next((int)BoberAmount.X, (int)BoberAmount.Y); index++)
             {
                 Vector2 Velocity = velocity.RotatedByRandom((double)MathHelper.ToRadians(18f));
                 Projectile.NewProjectile(source, position,Velocity, type, damage, knockback, player);
             }
         }
         // Aca terminaba el comentario original
         public override bool InstancePerEntity => true;
     }*/
    }
    public class BobersOverride : GlobalProjectile
    {
        NPC Target = null;
        public override void AI(Projectile projectile)
        {
            Player owner = Main.player[projectile.owner];
            if (projectile.bobber)
            {
                if (Target != null)
                {
                    int MaxLife = projectile.damage * 20;
                    if (Target.knockBackResist > 0 && !Target.boss && Target.lifeMax < MaxLife)
                    {
                        if (projectile.Distance(owner.Center) > 200)
                        {
                            Target.Center = projectile.position - new Vector2(0, 1) * 16f;
                            Target.velocity = Vector2.Zero;
                            NetMessage.SendData(MessageID.SyncNPC, number: Target.whoAmI);
                        }
                    }

                    if (Target.life <= 0 || !Target.active)
                        projectile.timeLeft = 1;
                }
            }
            base.AI(projectile);
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {

            if (projectile.bobber)
            {
                projectile.velocity = Vector2.Zero;
                projectile.penetrate = 100;
                projectile.timeLeft = 1000;
                Target = target;


            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override bool InstancePerEntity => true;
    }
}

