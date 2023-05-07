using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles
{
    public class IceMistF : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CultistBossIceMist;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceMistF");
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            AIType = ProjectileID.DeadlySphere;
        }
        public override void SetDefaults()
        {
            AIType = ProjectileID.DeadlySphere;
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 200;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.minion = true;
            Main.projFrames[Projectile.type] = 1;
            Projectile.ignoreWater = true;
            Projectile.CloneDefaults(ProjectileID.DeadlySphere);
            Projectile.minionSlots = 0f;
        }
        public override void AI()           
        {
            if (Main.player[Projectile.owner].GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade && Reaper.ReaperMode && !Main.player[Projectile.owner].dead) Projectile.timeLeft = 2;
            else Projectile.Kill();
        }

        public override bool MinionContactDamage() => true;
        public override void Kill(int timeLeft)
        {
        }
    }

    public class TempestClone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Tempest;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceMistF"); 
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            AIType = ProjectileID.Tempest;
        }
        public override void SetDefaults()
        {
            AIType = ProjectileID.Tempest;
            Projectile.CloneDefaults(ProjectileID.Tempest);
            Main.projFrames[Projectile.type] = 6;
            Projectile.minionSlots = 0f;
        }
        public override void AI()         
        {
            if (Main.player[Projectile.owner].GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade && Reaper.ReaperMode && !Main.player[Projectile.owner].dead) Projectile.timeLeft = 2;
            else Projectile.Kill();
        }
        public override bool MinionContactDamage() => true;
        public override void Kill(int timeLeft)
        {

        }
    }
}

