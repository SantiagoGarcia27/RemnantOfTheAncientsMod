using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class RemnantGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
        public override void SetDefaults(Projectile projectile)
        {  
            if (WeaponConf)
            {
                if (projectile.type == ProjectileID.BabySlime)
                {
                    projectile.minionSlots = 0.5f;
                }
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (WeaponConf)
            {
                if (Main.rand.NextBool(4) && projectile.type == ProjectileID.FlinxMinion)
                {
                    target.AddBuff(BuffID.Confused, Utils1.FormatTime(0, 0, 0, 5));
                }
            }
            if(projectile.type == 85 && projectile.ai[0] == 2)
            {
                target.AddBuff(BuffID.CursedInferno, 1200);
            }
            if(projectile.type == ProjectileID.ThunderStaffShot)
            {
                target.AddBuff(BuffID.Electrified, 1200);
            }
            if(projectile.type == ProjectileID.SpiritFlame)
            {
                target.AddBuff(BuffID.ShadowFlame, 1200);
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Reaper.ReaperMode && projectile.CountsAsClass(DamageClass.Melee) && projectile.friendly)
            {
                projectile.width *= (int)1.5f;
                projectile.height *= (int)1.5f;
                projectile.scale *= 2.5f;
            }
        }
        public override void AI(Projectile projectile)
        {
            if (projectile.type == 85 && projectile.ai[0] == 2)
            {
                GetAlpha(projectile, Color.Green);
            }
            base.AI(projectile);
        }
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (projectile.type == 85 && projectile.ai[0] == 2)
            {
                return Color.Green;
            }
            return lightColor;
        }

    }
}