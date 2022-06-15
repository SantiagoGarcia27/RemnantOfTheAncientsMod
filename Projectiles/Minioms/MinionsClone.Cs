using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using opswordsII.Items.Fmode;
using opswordsII.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace opswordsII.Projectiles
{
    public class IceMistF : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CultistBossIceMist;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceMistF"); //projectile name
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			//ProjectileID.Sets.Homing[projectile.type] = true;
			AIType = ProjectileID.DeadlySphere;
        }
        public override void SetDefaults()   
        {   
            AIType = ProjectileID.DeadlySphere;
            //projectile.CloneDefaults(ProjectileID.CultistBossIceMist); 
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 5;      //how many npc will penetrate
            Projectile.timeLeft = 200;   //how many time this projectile has before disepire
            Projectile.light = 0.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.minion = true;
			Main.projFrames[Projectile.type] = 1;
            Projectile.ignoreWater = true;
            Projectile.CloneDefaults(ProjectileID.DeadlySphere);
            Projectile.minionSlots = 0f;


        }
        public override void AI()           //this make that the projectile will face the corect way
        {
            Player player = Main.player[Projectile.owner];
            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active) {
				//player.ClearBuff(BuffType<DesertMinionBuff>());
			}
			if (player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade && Reaper.ReaperMode && !player.dead) {
				Projectile.timeLeft = 2;
			}
			#endregion
		}
        
        public override bool MinionContactDamage() {
			return true;
		}
        public override void Kill(int timeLeft) {

		}
     }

    public class TempestClone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Tempest;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceMistF"); //projectile name
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			//ProjectileID.Sets.Homing[Projectile.type] = true;
			AIType = ProjectileID.Tempest;
        }
            public override void SetDefaults()   
        {   
            AIType = ProjectileID.Tempest;
            Projectile.CloneDefaults(ProjectileID.Tempest);
            Main.projFrames[Projectile.type] = 6;
            Projectile.minionSlots = 0f;

        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
			Player player = Main.player[Projectile.owner];
            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active) {
				//player.ClearBuff(BuffType<DesertMinionBuff>());
			}
			if (player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade && Reaper.ReaperMode && !player.dead) {
                Projectile.timeLeft = 2;
			}
			#endregion
		}
        
        public override bool MinionContactDamage() {
			return true;
		}
       public override void Kill(int timeLeft) {

		}
    }
}