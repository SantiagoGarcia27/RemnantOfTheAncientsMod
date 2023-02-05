using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.Buffs;

namespace RemnantOfTheAncientsMod.Projectiles.Minioms
{
    public class StardustMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("StardustMinion");
            Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;   
        }
        public sealed override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.StardustDragon1);
            Projectile.width = 10;
            Projectile.height = 20;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            AIType = ProjectileID.StardustDragon1;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Player1 modPlayer = player.GetModPlayer<Player1>();
            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<StardustMinionBuff>());
            }
            if (player.HasBuff(BuffType<StardustMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            if (modPlayer.StardustMinion)
            {
                Projectile.timeLeft = 2;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 2;
        }
    }
}