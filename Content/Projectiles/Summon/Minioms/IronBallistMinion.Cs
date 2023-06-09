using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Dusts;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using Terraria.GameContent;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms
{
    public class IronBallistMinion : ModProjectile
    {
        bool FoundTarget = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Assault Ballista Minion");
            Main.projFrames[Projectile.type] = 8;

            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            //ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
            //AIType = ProjectileID.BabySlime;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1f;
            Projectile.alpha = 1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = -1;
            Projectile.tileCollide = true;
        }

        public override bool? CanCutTiles() => false;
        public override bool MinionContactDamage() => false;

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
        public float speed = 145f;
        public float Range = 400f;
        Vector2 targetPos;
        float targetDist;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            RemnantPlayer Modplayer = player.RemnantOfTheAncientsMod();
            if (!CheckActive(player)) return;
            FizSize();
            NPC target = FindTarget(player);
            FindFrame();
            float playerDistance = Projectile.Distance(player.position);
            if (playerDistance > 500 && !FoundTarget)
            {
                Projectile.tileCollide = false;
            }
            else
            {
                Projectile.tileCollide = true;
            }
            Movment(player);
        }
        public NPC FindTarget(Player player)
        {
            targetPos = Projectile.position;
            targetDist = Range;
            NPC npc = Main.npc[0];
            if (player.HasMinionAttackTargetNPC)
            {
                npc = Main.npc[player.MinionAttackTargetNPC];
                if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                {
                    targetDist = Vector2.Distance(Projectile.Center, targetPos);
                    targetPos = npc.Center;
                    FoundTarget = true;
                }
            }
            else
            {
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    npc = Main.npc[k];
                    if (npc.CanBeChasedBy(this, false))
                    {
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);
                        if ((distance < targetDist || !FoundTarget) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            FoundTarget = true;
                        }
                    }
                }
            }
            return npc;
        }
        public void Movment(Player player)
        {
              float speed = 5f;
              float Range = 400f;
        // Main.player[Projectile.owner].position = Projectile.position;

            Projectile.velocity = Projectile.position.DirectionTo(player.position) * speed;
          
            float i = (float)Main.worldSurface - Utils.ToTileCoordinates(Projectile.position).Y; 
            Point ProjectileFoor = Utils.ToTileCoordinates(Projectile.position);
            if(Projectile.velocity.Y > 0)
            {
                Projectile.velocity.Y = 0;
            }
            if (Collision.SolidCollision(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), Projectile.width, Projectile.height))
            {
                Dust.NewDust(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), 1, 1, DustID.GemRuby);
                Projectile.velocity -= new Vector2(0, 4);
            }
            else if (!Collision.SolidCollision(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), Projectile.width, Projectile.height))
            {
                Dust.NewDust(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f),1, 1, DustID.GemEmerald);
                Projectile.velocity += new Vector2(0, 4);
                // Main.player[Projectile.owner].position = new((ProjectileFoor.X + 2) * 16, (ProjectileFoor.Y + 5)*16);

            }
        }
        private void FizSize()
        {
            Projectile.Size = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width -20, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) -25);
        }
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(BuffType<IronBallistaBuff>());
                return false;
            }
            if (owner.HasBuff(BuffType<IronBallistaBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //if (Main.rand.NextBool())
            //{
            //    target.AddBuff(BuffType<Burning_Sand>(), 300);
            //}
        }
        private int[] MovementFrames = new int[3]
        {
            0,
            1,
            2
        };
        private int[] AimingFrames = new int[1]
        {
            3
        };
        private int[] ShootFrames = new int[4]
        {
            4,
            5,
            6,
            7
        };

        public void FindFrame()
        {
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.velocity.X > 0.01f || Projectile.velocity.X < -0.01f)
            {
                if(Projectile.frameCounter < 3)
                {
                    Projectile.frame = MovementFrames[0];
                    Projectile.frameCounter++;
                }
                else if(Projectile.frameCounter < 6) 
                {
                    Projectile.frame = MovementFrames[1];
                    Projectile.frameCounter++;
                }
                else
                {
                    Projectile.frame = MovementFrames[2];
                    Projectile.frameCounter++;
                }
                if(Projectile.frameCounter > 8)
                {
                    Projectile.frameCounter = 0;
                }
            }
            else
            {
                Projectile.frame = MovementFrames[2];
            }

        }
    }
}