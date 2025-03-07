using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms
{
    public class FlinxMinionClone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.FlinxMinion;

        public float dust;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Main.projFrames[ProjectileID.FlinxMinion];

            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FlinxMinion);
            Projectile.minionSlots = 0;
            Projectile.minion = true;
            AIType = ProjectileID.FlinxMinion;
            Projectile.scale = 1.5f;
    
        }


        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            RemnantPlayer Modplayer = player.RemnantOfTheAncientsMod();   
            if (!CheckActive(player)) return;
        }

        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.GetModPlayer<RemnantPlayer>().FlinxArmorSetBonus = false;
                return false;
            }
            if (owner.GetModPlayer<RemnantPlayer>().FlinxArmorSetBonus)
            {
                Projectile.timeLeft = 2;
            }
            else
            {
                Projectile.Kill();
            }

            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Confused, 300);
            }
        }
    }
}