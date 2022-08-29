using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Fmode;
using opswordsII.Projectiles.Minioms;
using opswordsII.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;

namespace opswordsII.Projectiles
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
        public override void AI()           //this make that the projectile will face the corect way
        {
            Player player = Main.player[Projectile.owner];
            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active)
            {
                //player.ClearBuff(BuffType<DesertMinionBuff>());
            }
            if (player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade && world1.ReaperMode && !player.dead)
            {
                Projectile.timeLeft = 2;
            }
            #endregion
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
            DisplayName.SetDefault("IceMistF"); //projectile name
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
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Player player = Main.player[Projectile.owner];
            #region Active check
            if (player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade && world1.ReaperMode && !player.dead)
            {
                Projectile.timeLeft = 2;
            }
            #endregion
        }

        public override bool MinionContactDamage() => true;
        public override void Kill(int timeLeft)
        {

        }
    }
    public class CloroCrystalClone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CrystalLeaf;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CrystalLeaf"); //projectile name
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            AIType = ProjectileID.CrystalLeaf;
        }
        public override void SetDefaults()
        {
        
            Main.projFrames[Projectile.type] = 6;
            Projectile.minionSlots = 0f;

        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Player player = Main.player[Projectile.owner];
            if (Main.player[Projectile.owner].GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade && world1.ReaperMode && !Main.player[Projectile.owner].dead) Projectile.timeLeft = 2;

           Projectile.netUpdate = true;   
            float num2 = Main.mouseTextColor / 200f - 0.35f;
            num2 *= 0.2f;
            Projectile.scale = num2 + 0.95f;
            Lighting.AddLight(Projectile.Center, 0.1f, 0.4f, 0.2f);
            Projectile.position = player.Center + Utils.RotatedBy(new Vector2(75f, 0f), Projectile.ai[1], default(Vector2));
            Projectile.position.X -= Projectile.width / 2;
            Projectile.position.Y -= Projectile.height / 2;
            Projectile.ai[1] -= 0.03f;
            if (Projectile.ai[1] > (float)Math.PI)
            {
               Projectile.ai[1] -= (float)Math.PI * 2f;
               Projectile.netUpdate = true;
            }
            Projectile.rotation =Projectile.ai[1] + (float)Math.PI / 2f;
            if (Projectile.ai[0] != 0f)
            {
               Projectile.ai[0] -= 1f;
                return;
            }
            
            float num5 = Projectile.position.Y;
            float num6 = 700f;
            bool flag = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(Projectile, true))
                {
                    float num9 = Math.Abs(Projectile.position.X + Projectile.width / 2 - Main.npc[i].position.X + Main.npc[i].width / 2) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - Main.npc[i].position.Y + Main.npc[i].height / 2);
                    if (num9 < num6 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                    {
                        num6 = num9;
                        Projectile.position.X = Main.npc[i].position.X + Main.npc[i].width / 2;
                        num5 = Main.npc[i].position.Y + Main.npc[i].height / 2;
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                Vector2 vector = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float num10 = Projectile.position.X - vector.X;
                float num11 = num5 - vector.Y;
                float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
                num12 = 10f / num12;
                num10 *= num12;
                num11 *= num12;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X - 4f, Projectile.Center.Y, num10, num11, ProjectileID.CrystalLeafShot,Projectile.damage,Projectile.knockBack,Projectile.owner, 0f, 0f);
               Projectile.ai[0] = 50f;
            }
            if (Main.netMode == NetmodeID.Server)  Projectile.netUpdate = true;
            
        }

        public override bool MinionContactDamage() => true;
        public override void Kill(int timeLeft)
        {

        }
    }

    /*public class PlanteraReaper : PlayerHoverShooter
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Plantera;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plant");

            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; 
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.scale *= 0.25f;
            inertia = 100f;
            shoot = ProjectileType<Frozenp_M>();
            shootSpeed = 12f; 
            shootCool = 10f;
         //   AIType = NPCID.IceElemental;
        }

        public override void CheckActive()
        {
            if (Main.player[Projectile.owner].GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade && world1.ReaperMode && !Main.player[Projectile.owner].dead) Projectile.timeLeft = 2;
        }
       
        public override void SelectFrame() => Projectile.frameCounter = 0;
    }*/

}

