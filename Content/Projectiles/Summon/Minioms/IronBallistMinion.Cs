using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using Terraria.GameContent;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

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
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 2f;
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
            fallThrough = true;
            return true;
        }

        Vector2 targetPos;
        float targetDist;
        NPC target = null;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!CheckActive(player)) return;

            FixSize();
            FindFrame();
            if (target == null || !target.active || !FoundTarget || player.HasMinionAttackTargetNPC) target = FindTarget(player);
            float playerDistance = Projectile.Distance(player.position);
            Projectile.tileCollide = (playerDistance > 1000 && !FoundTarget) ? false : true;

            if (target != null)
            {
                Vector2 TargetDistance = Utils1.FormatToPositive(DistanceHelper.GetDistance(target, Projectile));
                if (FoundTarget)
                {
                    Projectile.direction = DistanceHelper.GetDistance(target, Projectile).X > 0 ? 1 : -1;
                    Projectile.spriteDirection = Projectile.direction;
                    if (TargetDistance.X < 400 && TargetDistance.X > 0)
                    {
                        Shoot(target);
                    }
                }
            }
            Movment(player, target);
        }
        public NPC FindTarget(Player player)
        {
            int Range = 10;
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
                    return npc;
                }
            }
            else
            {
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    npc = Main.npc[k];
                    if (npc.CanBeChasedBy(this, false) && npc.active)
                    {
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);
                        if ((distance < targetDist && distance > 0 || !FoundTarget) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            FoundTarget = true;
                            return npc;
                        }
                    }
                }
            }
            FoundTarget = false;
            return null;
        }
        int attackCounter = 30;
        bool shootAnimation = false;
        public void Shoot(NPC target)
        {
            int CouldownMax = 30;
            int damage = Projectile.damage;
            int Speed = 2;
            float KnockBack = 3;
            if (attackCounter >= CouldownMax)
            {
                Vector2 Velocity = target.position - Projectile.position;
                int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, Velocity, ProjectileID.DD2BallistraProj, damage, KnockBack, Projectile.owner);
                Main.projectile[p].velocity *= Speed;
                attackCounter = 0;
                shootAnimation = true;
            }
            else
            {
                attackCounter++;

            }
        }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public void Movment(Player player, NPC? target)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {

            float speed = 5f;
            float Range = 500f;
            if (!FoundTarget || target == null)
            {

                MovmentToPlayer(player, speed);
            }
            else
            {
                MovmentToTarget(target, speed * 2, Range);
            }

        }
        int jumpCounter = 0;
        public void MovmentToPlayer(Player player, float speed)
        {
            Point ProjectileFoor = Utils.ToTileCoordinates(Projectile.Center);
            float playerDistanceFloat = Projectile.Distance(player.position);
            Vector2 playerDistance = DistanceHelper.GetDistance(Projectile, player);
            if (playerDistanceFloat < 2200 && !FoundTarget)
            {
                if (Collision.SolidCollision(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), Projectile.width + 2, Projectile.height))
                {
                    Projectile.velocity.X = Projectile.position.DirectionTo(player.position).X * speed;
                    Projectile.velocity -= new Vector2(0, 2);
                }
                else
                {
                    List<Tile> positionsFloor = new List<Tile>
                    {
                        Main.tile[ProjectileFoor.X, (ProjectileFoor.Y + 1)],
                        Main.tile[ProjectileFoor.X , (ProjectileFoor.Y + 2)]
                    };

                    if (!positionsFloor[0].HasTile && !positionsFloor[1].HasTile)
                    {
                        Projectile.velocity += new Vector2(0, 8);
                    }
                    if (playerDistance.X / 16 > 10 && jumpCounter >= 20)
                    {
                        if (jumpCounter >= 20)
                        {
                            Projectile.velocity -= new Vector2(16, 0);
                            Projectile.tileCollide = false;
                        }
                        if (jumpCounter == 10)
                        {
                            Projectile.velocity -= new Vector2(0, 18);
                            Projectile.tileCollide = false;
                        }
                        jumpCounter = 0;
                    }
                    else
                    {
                        jumpCounter++;
                        Projectile.velocity.X = Projectile.position.DirectionTo(player.position).X * speed;
                        Projectile.tileCollide = true;
                    }
                }
            }
            else
            {
                Projectile.position = Main.player[Projectile.owner].position;
            }
        }
        public void MovmentToTarget(NPC target, float speed, float range)
        {
            Point ProjectileFoor = Utils.ToTileCoordinates(Projectile.Center);
            float targetDistanceTotal = Projectile.Distance(target.position);
            Vector2 targetDistance = Utils1.FormatToPositive(DistanceHelper.GetDistance(Projectile, target));
            if (targetDistance.X < 200)
            {
                Projectile.velocity = new Vector2(0, 18);
            }
            else if (targetDistance.X < range)
            {

                if (Projectile.velocity.Y > 0)
                {
                    Projectile.velocity.Y = 0;
                }
                else
                {
                    Projectile.velocity.X = Projectile.position.DirectionTo(target.position).X * speed;
                }
                if (Collision.SolidCollision(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), Projectile.width, Projectile.height))
                {
                    Projectile.velocity -= new Vector2(0, 4);
                }
                else if (!Collision.SolidCollision(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), Projectile.width, Projectile.height))
                {
                    List<Tile> positionsFloor = new List<Tile>
                    {
                    Main.tile[ProjectileFoor.X, (ProjectileFoor.Y + 1)],
                    Main.tile[ProjectileFoor.X , (ProjectileFoor.Y + 2)]
                    };

                    if (!positionsFloor[0].HasTile && !positionsFloor[1].HasTile)
                    {
                        Projectile.velocity += new Vector2(0, 8);
                    }
                }
            }
            else
            {
                FoundTarget = false;
            }
        }


        private void FixSize()
        {
            Projectile.Size = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width - 35, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) /*+ 125*/);
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
        int ShootFrameCounter = 0;
        public void FindFrame()
        {
            Projectile.spriteDirection = Projectile.direction;
            if (!shootAnimation)
            {
                if (Projectile.velocity.X > 0.01f || Projectile.velocity.X < -0.01f)
                {
                    if (Projectile.frameCounter < 3)
                    {
                        Projectile.frame = MovementFrames[0];
                        Projectile.frameCounter++;
                    }
                    else if (Projectile.frameCounter < 6)
                    {
                        Projectile.frame = MovementFrames[1];
                        Projectile.frameCounter++;
                    }
                    else
                    {
                        Projectile.frame = MovementFrames[2];
                        Projectile.frameCounter++;
                    }
                    if (Projectile.frameCounter > 8)
                    {
                        Projectile.frameCounter = 0;
                    }
                }
                else
                {
                    Projectile.frame = MovementFrames[2];
                }
            }
            else
            {
                switch (ShootFrameCounter)
                {
                    case 0:
                    case 1:
                        Projectile.frame = ShootFrames[0];
                        ShootFrameCounter++;
                        break;
                    case 2:
                    case 3:
                        Projectile.frame = ShootFrames[1];
                        ShootFrameCounter++;
                        break;
                    case 4:
                    case 5:
                        Projectile.frame = ShootFrames[2];
                        ShootFrameCounter++;
                        break;
                    case 6:
                    case 7:
                        Projectile.frame = ShootFrames[3];
                        ShootFrameCounter++;
                        break;
                    case > 6:
                        ShootFrameCounter = 0;
                        shootAnimation = false;
                        break;
                }

                //if (ShootFrameCounter == 0 ||)
                //{
                //    Projectile.frame = ShootFrames[0];
                //    ShootFrameCounter++;
                //}
                //if (ShootFrameCounter == 2)
                //{
                //    Projectile.frame = ShootFrames[1];
                //    ShootFrameCounter++;
                //}

                //if (ShootFrameCounter == 4)
                //{
                //    Projectile.frame = ShootFrames[2];
                //    ShootFrameCounter++;
                //}
                //if (ShootFrameCounter == 6)
                //{
                //    Projectile.frame = ShootFrames[3];
                //    ShootFrameCounter++;
                //}
                //if (ShootFrameCounter > 6)
                //{
                //    ShootFrameCounter = 0;
                //    shootAnimation = false;
                //}
            }
        }
    }
}