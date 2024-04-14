using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public enum WormSegmentType
    {
        /// <summary>
        /// The head segment for the worm.  Only one "head" is considered to be active for any given worm
        /// </summary>
        Head,
        /// <summary>
        /// The body segment.  Follows the segment in front of it
        /// </summary>
        Body,
        /// <summary>
        /// The tail segment.  Has the same AI as the body segments.  Only one "tail" is considered to be active for any given worm
        /// </summary>
        Tail
    }

    /// <summary>
    /// The base class for non-separating Worm enemies.
    /// </summary>
    public abstract class Worm : ModNPC
    {
        /*  ai[] usage:
		 *  
		 *  ai[0] = "follower" segment, the segment that's following this segment
		 *  ai[1] = "following" segment, the segment that this segment is following
		 *  
		 *  localAI[0] = used when syncing changes to collision detection
		 *  localAI[1] = checking if Init() was called
		 */

        /// <summary>
        /// Which type of segment this NPC is considered to be
        /// </summary>
        public abstract WormSegmentType SegmentType { get; }

        /// <summary>
        /// The maximum velocity for the NPC
        /// </summary>
        public float MoveSpeed { get; set; }

        /// <summary>
        /// The rate at which the NPC gains velocity
        /// </summary>
        public float Acceleration { get; set; }

        /// <summary>
        /// The NPC instance of the head segment for this worm.
        /// </summary>
        public NPC HeadSegment => Main.npc[NPC.realLife];

        /// <summary>
        /// The NPC instance of the segment that this segment is following (ai[1]).  For head segments, this property always returns <see langword="null"/>.
        /// </summary>
        public NPC FollowingNPC => SegmentType == WormSegmentType.Head ? null : Main.npc[(int)NPC.ai[1]];

        /// <summary>
        /// The NPC instance of the segment that is following this segment (ai[0]).  For tail segment, this property always returns <see langword="null"/>.
        /// </summary>
        public NPC FollowerNPC => SegmentType == WormSegmentType.Tail ? null : Main.npc[(int)NPC.ai[0]];

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return SegmentType == WormSegmentType.Head ? null : false;
        }

        private bool startDespawning;

        public sealed override bool PreAI()
        {
            if (NPC.localAI[1] == 0)
            {
                NPC.localAI[1] = 1f;
                Init();
            }

            if (SegmentType == WormSegmentType.Head)
            {
                HeadAI();

                if (!NPC.HasValidTarget)
                {
                    NPC.TargetClosest(true);

                    // If the NPC is a boss and it has no target, force it to fall to the underworld quickly
                    if (!NPC.HasValidTarget && NPC.boss)
                    {
                        NPC.velocity.Y += 8f;

                        MoveSpeed = 1000f;

                        if (!startDespawning)
                        {
                            startDespawning = true;

                            // Despawn after 90 ticks (1.5 seconds) if the NPC gets far enough away
                            NPC.timeLeft = 90;
                        }
                    }
                }
            }
            else
                BodyTailAI();

            return true;
        }

        // Not visible to public API, but is used to indicate what AI to run
        internal virtual void HeadAI() { }

        internal virtual void BodyTailAI() { }

        public abstract void Init();
    }

    public abstract class WormHead : Worm
    {
        public sealed override WormSegmentType SegmentType => WormSegmentType.Head;
        public abstract int BodyType { get; }

        public abstract int TailType { get; }

        public int MinSegmentLength { get; set; }

        public int MaxSegmentLength { get; set; }

        public bool CanFly { get; set; }

        public virtual int MaxDistanceForUsingTileCollision => 1000;

        public virtual bool HasCustomBodySegments => false;

        public Vector2? ForcedTargetPosition { get; set; }

        public virtual int SpawnBodySegments(int segmentCount)
        {
            return NPC.whoAmI;
        }


        protected int SpawnSegment(IEntitySource source, int type, int latestNPC)
        {
            int oldLatest = latestNPC;
            latestNPC = NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, type, NPC.whoAmI, 0, latestNPC);

            Main.npc[oldLatest].ai[0] = latestNPC;

            NPC latest = Main.npc[latestNPC];

            latest.realLife = NPC.whoAmI;

            return latestNPC;
        }

        internal sealed override void HeadAI()
        {
            HeadAI_SpawnSegments();

            bool collision = HeadAI_CheckCollisionForDustSpawns();

            HeadAI_CheckTargetDistance(ref collision);

            HeadAI_Movement(collision);
        }

        private void HeadAI_SpawnSegments()
        {

            bool hasFollower = NPC.ai[0] > 0;
            if (!hasFollower)
            {

                NPC.realLife = NPC.whoAmI;

                int latestNPC = NPC.whoAmI;


                int randomWormLength = Main.rand.Next(MinSegmentLength, MaxSegmentLength + 1);

                int distance = randomWormLength - 2;

                IEntitySource source = NPC.GetSource_FromAI();

                if (HasCustomBodySegments)
                {
                    latestNPC = SpawnBodySegments(distance);
                }
                else
                {

                    while (distance > 0)
                    {
                        latestNPC = SpawnSegment(source, BodyType, latestNPC);
                        distance--;
                    }
                }
                SpawnSegment(source, TailType, latestNPC);

                NPC.netUpdate = true;

                int count = 0;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC n = Main.npc[i];

                    if (n.active && (n.type == Type || n.type == BodyType || n.type == TailType) && n.realLife == NPC.whoAmI)
                    {
                        count++;
                    }
                }

                if (count != randomWormLength)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC n = Main.npc[i];

                        if (n.active && (n.type == Type || n.type == BodyType || n.type == TailType) && n.realLife == NPC.whoAmI)
                        {
                            n.active = false;
                            n.netUpdate = true;
                        }
                    }
                }
            }
            NPC.TargetClosest(true);
        }
    

        private bool HeadAI_CheckCollisionForDustSpawns()
        {
            int minTilePosX = (int)(NPC.Left.X / 16) - 1;
            int maxTilePosX = (int)(NPC.Right.X / 16) + 2;
            int minTilePosY = (int)(NPC.Top.Y / 16) - 1;
            int maxTilePosY = (int)(NPC.Bottom.Y / 16) + 2;

            // Ensure that the tile range is within the world bounds
            if (minTilePosX < 0)
            {
                minTilePosX = 0;
            }
            if (maxTilePosX > Main.maxTilesX)
            {
                maxTilePosX = Main.maxTilesX;
            }
            if (minTilePosY < 0)
            {
                minTilePosY = 0;
            }
            if (maxTilePosY > Main.maxTilesY)
            {
                maxTilePosY = Main.maxTilesY;
            }

            bool collision = false;

            // This is the initial check for collision with tiles.
            for (int i = minTilePosX; i < maxTilePosX; ++i)
            {
                for (int j = minTilePosY; j < maxTilePosY; ++j)
                {
                    Tile tile = Main.tile[i, j];

                    // If the tile is solid or is considered a platform, then there's valid collision
                    if (tile.HasUnactuatedTile && (Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType] && tile.TileFrameY == 0) || tile.LiquidAmount > 64)
                    {
                        Vector2 tileWorld = new Point16(i, j).ToWorldCoordinates(0, 0);

                        if (NPC.Right.X > tileWorld.X && NPC.Left.X < tileWorld.X + 16 && NPC.Bottom.Y > tileWorld.Y && NPC.Top.Y < tileWorld.Y + 16)
                        {
                            // Collision found
                            collision = true;

                            if (Main.rand.NextBool(100))
                            {
                                WorldGen.KillTile(i, j, fail: true, effectOnly: true, noItem: false);
                            }
                        }
                    }
                }
            }

            return collision;
        }

        private void HeadAI_CheckTargetDistance(ref bool collision)
        {
            // If there is no collision with tiles, we check if the distance between this NPC and its target is too large, so that we can still trigger "collision".
            if (!collision)
            {
                Rectangle hitbox = NPC.Hitbox;

                int maxDistance = MaxDistanceForUsingTileCollision;

                bool tooFar = true;

                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Rectangle areaCheck;

                    Player player = Main.player[i];

                    if (ForcedTargetPosition is Vector2 target)
                    {
                        areaCheck = new Rectangle((int)target.X - maxDistance, (int)target.Y - maxDistance, maxDistance * 2, maxDistance * 2);
                    }
                    else if (player.active && !player.dead && !player.ghost)
                    {
                        areaCheck = new Rectangle((int)player.position.X - maxDistance, (int)player.position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
                    }
                    else
                        continue;  // Not a valid player

                    if (hitbox.Intersects(areaCheck))
                    {
                        tooFar = false;
                        break;
                    }
                }

                if (tooFar)
                {
                    collision = true;
                }
            }
        }

        private void HeadAI_Movement(bool collision)
        {
            // MoveSpeed determines the max speed at which this NPC can move.
            // Higher value = faster speed.
            float speed = MoveSpeed;
            // acceleration is exactly what it sounds like. The speed at which this NPC accelerates.
            float acceleration = Acceleration;

            float targetXPos, targetYPos;

            Player playerTarget = Main.player[NPC.target];

            Vector2 forcedTarget = ForcedTargetPosition ?? playerTarget.Center;
            // Using a ValueTuple like this allows for easy assignment of multiple values
            (targetXPos, targetYPos) = (forcedTarget.X, forcedTarget.Y);

            // Copy the value, since it will be clobbered later
            Vector2 npcCenter = NPC.Center;

            float targetRoundedPosX = (int)(targetXPos / 16f) * 16;
            float targetRoundedPosY = (int)(targetYPos / 16f) * 16;
            npcCenter.X = (int)(npcCenter.X / 16f) * 16;
            npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            // If we do not have any type of collision, we want the NPC to fall down and de-accelerate along the X axis.
            if (!collision && !CanFly)
            {
                HeadAI_Movement_HandleFallingFromNoCollision(dirX, speed, acceleration);
            }
            else
            {
                // Else we want to play some audio (soundDelay) and move towards our target.
                HeadAI_Movement_PlayDigSounds(length);

                HeadAI_Movement_HandleMovement(dirX, dirY, length, speed, acceleration);
            }

            HeadAI_Movement_SetRotation(collision);
        }

        private void HeadAI_Movement_HandleFallingFromNoCollision(float dirX, float speed, float acceleration)
        {
            // Keep searching for a new target
            NPC.TargetClosest(true);

            // Constant gravity of 0.11 pixels/tick
            NPC.velocity.Y += 0.11f;

            // Ensure that the NPC does not fall too quickly
            if (NPC.velocity.Y > speed)
            {
                NPC.velocity.Y = speed;
            }

            // The following behaviour mimicks vanilla worm movement
            if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.4f)
            {
                // Velocity is sufficiently fast, but not too fast
                if (NPC.velocity.X < 0.0f)
                {
                    NPC.velocity.X -= acceleration * 1.1f;
                }
                else
                    NPC.velocity.X += acceleration * 1.1f;
            }
            else if (NPC.velocity.Y == speed)
            {
                // NPC has reached terminal velocity
                if (NPC.velocity.X < dirX)
                {
                    NPC.velocity.X += acceleration;
                }
                else if (NPC.velocity.X > dirX)
                {
                    NPC.velocity.X -= acceleration;
                }
            }
            else if (NPC.velocity.Y > 4)
            {
                if (NPC.velocity.X < 0)
                {
                    NPC.velocity.X += acceleration * 0.9f;
                }
                else
                    NPC.velocity.X -= acceleration * 0.9f;
            }
        }

        private void HeadAI_Movement_PlayDigSounds(float length)
        {
            if (NPC.soundDelay == 0)
            {
                // Play sounds quicker the closer the NPC is to the target location
                float num1 = length / 40f;

                if (num1 < 10)
                {
                    num1 = 10f;
                }

                if (num1 > 20)
                {
                    num1 = 20f;
                }

                NPC.soundDelay = (int)num1;

                SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
            }
        }

        private void HeadAI_Movement_HandleMovement(float dirX, float dirY, float length, float speed, float acceleration)
        {
            float absDirX = Math.Abs(dirX);
            float absDirY = Math.Abs(dirY);
            float newSpeed = speed / length;
            dirX *= newSpeed;
            dirY *= newSpeed;

            if ((NPC.velocity.X > 0 && dirX > 0) || (NPC.velocity.X < 0 && dirX < 0) || (NPC.velocity.Y > 0 && dirY > 0) || (NPC.velocity.Y < 0 && dirY < 0))
            {
                // The NPC is moving towards the target location
                if (NPC.velocity.X < dirX)
                {
                    NPC.velocity.X += acceleration;
                }
                else if (NPC.velocity.X > dirX)
                {
                    NPC.velocity.X -= acceleration;
                }

                if (NPC.velocity.Y < dirY)
                {
                    NPC.velocity.Y += acceleration;
                }
                else if (NPC.velocity.Y > dirY)
                {
                    NPC.velocity.Y -= acceleration;
                }

                // The intended Y-velocity is small AND the NPC is moving to the left and the target is to the right of the NPC or vice versa
                if (Math.Abs(dirY) < speed * 0.2 && ((NPC.velocity.X > 0 && dirX < 0) || (NPC.velocity.X < 0 && dirX > 0)))
                {
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.velocity.Y += acceleration * 2f;
                    }
                    else
                        NPC.velocity.Y -= acceleration * 2f;
                }

                // The intended X-velocity is small AND the NPC is moving up/down and the target is below/above the NPC
                if (Math.Abs(dirX) < speed * 0.2 && ((NPC.velocity.Y > 0 && dirY < 0) || (NPC.velocity.Y < 0 && dirY > 0)))
                {
                    if (NPC.velocity.X > 0)
                    {
                        NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
                    }
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
                }
            }
            else if (absDirX > absDirY)
            {
                // The X distance is larger than the Y distance.  Force movement along the X-axis to be stronger
                if (NPC.velocity.X < dirX)
                {
                    NPC.velocity.X += acceleration * 1.1f;
                }
                else if (NPC.velocity.X > dirX)
                {
                    NPC.velocity.X -= acceleration * 1.1f;
                }

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                {
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.velocity.Y += acceleration;
                    }
                    else
                        NPC.velocity.Y -= acceleration;
                }
            }
            else
            {
                // The X distance is larger than the Y distance.  Force movement along the X-axis to be stronger
                if (NPC.velocity.Y < dirY)
                {
                    NPC.velocity.Y += acceleration * 1.1f;
                }
                else if (NPC.velocity.Y > dirY)
                {
                    NPC.velocity.Y -= acceleration * 1.1f;
                }

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                {
                    if (NPC.velocity.X > 0)
                    {
                        NPC.velocity.X += acceleration;
                    }
                    else
                        NPC.velocity.X -= acceleration;
                }
            }
        }

        private void HeadAI_Movement_SetRotation(bool collision)
        {
            // Set the correct rotation for this NPC.
            // Assumes the sprite for the NPC points upward.  You might have to modify this line to properly account for your NPC's orientation
            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;

            // Some netupdate stuff (multiplayer compatibility).
            if (collision)
            {
                if (NPC.localAI[0] != 1)
                {
                    NPC.netUpdate = true;
                }

                NPC.localAI[0] = 1f;
            }
            else
            {
                if (NPC.localAI[0] != 0)
                {
                    NPC.netUpdate = true;
                }

                NPC.localAI[0] = 0f;
            }

            // Force a netupdate if the NPC's velocity changed sign and it was not "just hit" by a player
            if (((NPC.velocity.X > 0 && NPC.oldVelocity.X < 0) || (NPC.velocity.X < 0 && NPC.oldVelocity.X > 0) || (NPC.velocity.Y > 0 && NPC.oldVelocity.Y < 0) || (NPC.velocity.Y < 0 && NPC.oldVelocity.Y > 0)) && !NPC.justHit)
            {
                NPC.netUpdate = true;
            }
        }
    }

    public abstract class WormBody : Worm
    {
        public sealed override WormSegmentType SegmentType => WormSegmentType.Body;

        internal override void BodyTailAI()
        {
            CommonAI_BodyTail(this);
        }

        internal static void CommonAI_BodyTail(Worm worm)
        {
            if (!worm.NPC.HasValidTarget)
            {
                worm.NPC.TargetClosest(true);
            }

            if (Main.player[worm.NPC.target].dead && worm.NPC.timeLeft > 30000)
            {
                worm.NPC.timeLeft = 10;
            }

            NPC following = worm.NPC.ai[1] >= Main.maxNPCs ? null : worm.FollowingNPC;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // Some of these conditions are possble if the body/tail segment was spawned individually
                // Kill the segment if the segment NPC it's following is no longer valid
                if (following is null || !following.active || following.friendly || following.townNPC || following.lifeMax <= 5)
                {
                    worm.NPC.life = 0;
                    worm.NPC.HitEffect(0, 10);
                    worm.NPC.active = false;
                }
            }

            if (following is not null)
            {
                // Follow behind the segment "in front" of this NPC
                // Use the current NPC.Center to calculate the direction towards the "parent NPC" of this NPC.
                float dirX = following.Center.X - worm.NPC.Center.X;
                float dirY = following.Center.Y - worm.NPC.Center.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                // Assumes the sprite for the NPC points upward.  You might have to modify this line to properly account for your NPC's orientation
                worm.NPC.rotation = (float)Math.Atan2(dirY, dirX) + MathHelper.PiOver2;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - worm.NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                worm.NPC.velocity = Vector2.Zero;
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                worm.NPC.position.X += posX;
                worm.NPC.position.Y += posY;
            }
        }
    }

    // Since the body and tail segments share the same AI
    public abstract class WormTail : Worm
    {
        public sealed override WormSegmentType SegmentType => WormSegmentType.Tail;

        internal override void BodyTailAI()
        {
            WormBody.CommonAI_BodyTail(this);
        }
    }
}