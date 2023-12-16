using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using Terraria.GameContent;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms
{
    public class IronBallistMinion : ModProjectile
    {
        bool FoundTarget = false;
        float GravityForce = 2f;
        float ViewDistance = 500;

        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Assault Ballista Minion");
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

            fallThrough = false;

            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            //Vector2 t = Projectile.Center + new Vector2(10 * 16, 10 * 16);
            //Rectangle rec = new Rectangle((int)Projectile.Center.X, (int)Projectile.Center.Y, (int)t.X, (int)t.Y );
            //var origin = Request<Texture2D>(Texture).Size() / 2;
            //Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, Projectile.position - Main.screenPosition, rec, Color.Orange * 0.75f, 0f, origin, 1, 0);
            return true;
        }



        Vector2 targetPos;
        float targetDist;
        NPC target = null;
        Vector2 UnfocusDistance = new(1,1);
        public override void AI()
        {
            //Main.NewText(Projectile.position);   
            Player player = Main.player[Projectile.owner];
            UnfocusDistance = new Vector2(Main.screenWidth, Main.screenHeight);

            //Dust.NewDust(Projectile.Center - UnfocusDistance/2, (int)UnfocusDistance.X, (int)UnfocusDistance.Y, DustID.Torch, 0, 0, 1, default, 1);
            //Rectangle rec = new Rectangle((int)Projectile.Center.X, (int)Projectile.Center.Y, (int)UnfocusDistance.X, (int)UnfocusDistance.Y);
 
            //if (!rec.Contains(Utils1.ToPoint(player.Center))) Main.NewText("fuera de rango");
            //else Main.NewText("dentro de rango");
           

            if (!CheckActive(player)) return;
            FixSize();
            FindFrame();
            if (target == null || !target.active || !FoundTarget || player.HasMinionAttackTargetNPC) 
                target = FindTarget(player);
            float playerDistance = Projectile.Distance(player.position);
            Projectile.tileCollide = (playerDistance > UnfocusDistance.X && !FoundTarget) ? false : true;

            if (target != null)
            {
                Vector2 TargetDistance = Utils1.FormatToPositive(DistanceUtils.GetDistance(target, Projectile));

                if (FoundTarget)
                {
                    if (playerDistance >= UnfocusDistance.X)
                    {
                        target = null;
                        Projectile.velocity = Projectile.Center - player.Center;
                        return;
                    }
                    Projectile.direction = DistanceUtils.GetDistance(target, Projectile).X > 0 ? 1 : -1;
                    Projectile.spriteDirection = Projectile.direction;
                    if (TargetDistance.X < 400 && TargetDistance.X > 0)
                    {
                        Shoot(target);
                    }
                    if (playerDistance >= UnfocusDistance.X)
                    {
                        Projectile.Center = player.Center;
                    }
                }
            }
            else if(playerDistance >= UnfocusDistance.X)
            {
                Projectile.Center = player.Center;
            }
            Movment(player, target);
        }


        public NPC FindTarget(Player player)
        {
            targetPos = Projectile.position;
            targetDist = ViewDistance;
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
                        if ((distance < targetDist /*|| !FoundTarget*/) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
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

        public void Movment(Player player, NPC? target)
        {

            float speed = 5f;
            float Range = 300f * 16;
            bool TouchBlock = Collision.SolidCollision((Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f)), Projectile.width + 2, Projectile.height);
            Tile tile = Main.tile[(int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16)];
            bool MinionStuck = tile.HasTile && Main.tileSolid[tile.TileType];
         // int a =  Dust.NewDust(DistanceUtils.ToCoordenatePosition(new Vector2(Projectile.Center.X / 16, Projectile.Center.Y / 16)), 1, 1, DustID.AmberBolt,0,0,1,default,1);
          //  Main.dust[a].noGravity = true;
            if (TouchBlock)
            {
                Jump(2f);
            }
            if (MinionStuck)
            {
                Projectile.tileCollide = false;
                Jump(4f);
            }
            if (DistanceUtils.ProjectileTouchFlourLiquid(Projectile) && player.waterWalk)
            {
                Jump(1f);
            }
            else
            {
                if (!FoundTarget || target == null)
                {

                    MovmentToPlayer(player, speed * (player.moveSpeed * 1.5f));
                }
                else
                {
                    MovmentToTarget(target, speed * 2, Range);
                }
            }

        }
        public void MovmentToPlayer(Player player, float speed)
        { 
            float playerDistanceFloat = Projectile.Distance(player.position);
           
         
            if (playerDistanceFloat < 2200 && !FoundTarget)
            {
                Vector2 playerDistance = player.Center - Projectile.Center;
                if (Utils1.FormatToPositive(playerDistance).Y < 20 * 16)
                {
                    Projectile.tileCollide = -playerDistance.Y < 5 * 16;
                    Projectile.velocity.X = (Projectile.position.DirectionTo(player.position).X * (speed / 2));
                    ApplyGravity();
                }
                else
                {
                    if (DistanceUtils.PlayerTouchFlour(player))
                    {
                        Projectile.velocity = Projectile.position.DirectionTo(player.position) * speed;
                        Projectile.tileCollide = false;
                        Projectile.Center = player.Center;
                    }
                    else
                    {
                        Projectile.velocity.X = (Projectile.position.DirectionTo(player.position).X * (speed / 2));
                        ApplyGravity();
                    }
                }
               


            }
            //    if (TouchBlock)
            //    {
            //        Projectile.velocity.X = Projectile.position.DirectionTo(player.position).X * speed;
            //        Projectile.velocity -= new Vector2(0, 2);
            //        Projectile.tileCollide = Projectile.Center.DistanceSQ(player.Center) > UnfocusDistance.X ? false : true;
            //    }
            //    else
            //    {
     
            //        List<Tile> positionsFloor = new List<Tile>
            //        {
            //            Main.tile[ProjectileFoor.X, (ProjectileFoor.Y + 1)],
            //            Main.tile[ProjectileFoor.X , (ProjectileFoor.Y + 2)]
            //        };

            //        if (!positionsFloor[0].HasTile && !positionsFloor[1].HasTile)
            //        {
            //            if ((Projectile.Center.Y- player.Center.Y) < 10*16)
            //            {
            //                Projectile.velocity += new Vector2(0, 8);
            //            }
            //            else
            //            {
            //                if (DistanceUtils.PlayerTouchFlour(player) || DistanceUtils.PlayerTouchFlourLiquid(player))
            //                {
            //                    Projectile.velocity = (player.Center - Projectile.Center);// * speed;
            //                    Projectile.tileCollide = false;
            //                }
            //            }

            //        }
            //        if (playerDistance.X / 16 > 10 && jumpCounter >= 20)
            //        {
            //            if (jumpCounter >= 20)
            //            {
            //                Projectile.velocity -= new Vector2(16, 0);
            //                //Projectile.tileCollide = false;
            //            }
            //            if (jumpCounter == 10)
            //            {
            //                Projectile.velocity -= new Vector2(0, 2);
            //               // Projectile.tileCollide = false;
            //            }
            //            jumpCounter = 0;
            //        }
            //        else
            //        {
            //            jumpCounter++;
            //            Projectile.velocity.X = Projectile.Center.DirectionTo(player.Center).X * speed;
            //            Projectile.tileCollide = true;
            //        }
            //    }
            //}
            //else
            //{
            //    Projectile.Center = player.Center;
            //}
        }
        public void MovmentToTarget(NPC target, float speed, float range)
        {
            //Vector2 distance =  Projectile.Center -target.Center;
            //if (distance.X <= range && distance.Y <= range)
            //{
            int dist = 10 * 16;
            bool touchFlour = DistanceUtils.ProjectileTouchFlour(Projectile);


            if (Projectile.DistanceSQ(target.Center) > dist * dist && touchFlour)
            {
                Projectile.velocity = Projectile.DirectionTo(target.Center) * speed;
                
                Projectile.velocity.Y -= 1f;
            }
            else
            {
                //Jump(1);
            }


            //Projectile.velocity.X = Projectile.position.DirectionTo(target.position + new Vector2(10 * 16) *Projectile.direction).X  * speed;
                ApplyGravity();
                Projectile.tileCollide = true;
          
            //}
            //else
            //{
            //    FindTarget(Main.player[Projectile.owner]);
            //}
            //if (targetDistance.X < 200)
            //{
            //    Projectile.velocity += new Vector2(0, 18);
            //}
            //else if (targetDistance.X < range)
            //{

            //    if (Projectile.velocity.Y > 0)
            //    {
            //        Projectile.velocity.Y = 0;
            //    }
            //    else
            //    {
            //        Projectile.velocity.X = Projectile.position.DirectionTo(target.position).X * speed;
            //    }
            //    if (Collision.SolidCollision(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), Projectile.width, Projectile.height))
            //    {
            //        Projectile.velocity -= new Vector2(0, 4);
            //    }
            //    else if (!Collision.SolidCollision(Projectile.position + new Vector2(Projectile.width / 2f * Projectile.direction, 0f), Projectile.width, Projectile.height))
            //    {
            //        List<Tile> positionsFloor = new List<Tile>
            //        {
            //        Main.tile[ProjectileFoor.X, (ProjectileFoor.Y + 1)],
            //        Main.tile[ProjectileFoor.X , (ProjectileFoor.Y + 2)]
            //        };

            //        if (!positionsFloor[0].HasTile && !positionsFloor[1].HasTile)
            //        {
            //            Projectile.velocity += new Vector2(0, 8);
            //        }
            //    }
            //}
            //else
            //{
            //    FoundTarget = false;
            //}
        }

        public void ApplyGravity()
        {
            Projectile.velocity.Y += GravityForce;
        }
        public void Jump(float force)
        {
            Projectile.velocity.Y -= GravityForce + force;
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
            if (owner.HasBuff(BuffType<IronBallistaBuff>()) || owner.GetModPlayer<RemnantPlayer>().IronBallistaEnchantment)
            {
                Projectile.timeLeft = 4;
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