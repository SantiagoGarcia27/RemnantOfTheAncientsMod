using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles.Melee
{
    public class MoltenGrassSwordLeaft : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BladeOfGrass;
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Sharp Leaf");
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BladeOfGrass);
            AIType = ProjectileID.BladeOfGrass;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (new RemnantOfTheAncientsMod().ParticleMeter(4) != 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), target.position, new Vector2(0f, 0f), ProjectileID.Volcano, (int)(Projectile.damage * 0.5f), 0);
            }
            target.AddBuff(BuffID.OnFire, 120);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            lightColor= Color.OrangeRed;
            return lightColor;
        }
    }
}