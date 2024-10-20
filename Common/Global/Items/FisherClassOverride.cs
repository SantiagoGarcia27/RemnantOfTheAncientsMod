using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.DamageClasses;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Bobbers;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class FisherClassOverride : GlobalItem
    {
        readonly Mod Calamity = RemnantOfTheAncientsMod.CalamityMod;

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.PurpleClubberfish)
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
            }
            SetFishClass(item);
        }
        public static void SetFishClass(Item item)
        {
            if (item.fishingPole > 0)
            {
                item.damage = item.fishingPole - 1;
                item.DamageType = ModContent.GetInstance<FisherDamageClass>();
            }
        }
        /*
         public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Calamity != null)
            {
                if (item.type == ExternalModCallUtils.GetItemFromMod(Calamity, "TheDevourerofCods"))
                {
                    FixModRodsBobers(source, position, velocity, type, damage, knockback, player.whoAmI, 10);
                    return false;
                }
                else if (item.type == ExternalModCallUtils.GetItemFromMod(Calamity, "RiftReeler"))
                {
                    FixModRodsBobers(source, position, velocity, type, damage, knockback, player.whoAmI, new Vector2(3, 6));
                    return false;
                }
                else if (item.type == ExternalModCallUtils.GetItemFromMod(Calamity, "FeralDoubleRod"))
                {
                    FixModRodsBobers(source, position, velocity, type, damage, knockback, player.whoAmI, 2);
                    return false;
                }
                else if (item.type == ExternalModCallUtils.GetItemFromMod(Calamity, "EarlyBloomRod"))
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
        */
        public override bool InstancePerEntity => true; 
    }
}
