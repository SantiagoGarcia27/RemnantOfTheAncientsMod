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
       
    }
}