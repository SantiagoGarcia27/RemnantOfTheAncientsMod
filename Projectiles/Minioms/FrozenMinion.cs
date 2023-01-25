using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs;
using RemnantOfTheAncientsMod.Projectiles.BossProjectile;
using System;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.CodeAnalysis;
using System.IO;
using RemnantOfTheAncientsMod.NPCs;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod.World;
using static Humanizer.In;

namespace RemnantOfTheAncientsMod.Projectiles.Minioms
{
    public class FrozenMinion : HoverShooter
    {
        int currentPhase = 1;
        float i = 0;
        int idelay = 5;
        int TpDelay = 20;
        private int attackCounter;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Frozen Assaulter Minion");

            Main.projFrames[Projectile.type] = 6;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
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
            Projectile.usesLocalNPCImmunity = true;
            AIType = ProjectileID.Retanimini;
            inertia = 100f;//20
            shoot = ProjectileType<Frozenp_M>();
            shootSpeed = 12f; //12
            shootCool = 10f;

        }

        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            Player1 modPlayer = player.GetModPlayer<Player1>();
            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<FrozenMinionBuff>());
            }
            if (player.HasBuff(BuffType<FrozenMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            if (modPlayer.FrozenMinion)
            { // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
                Projectile.timeLeft = 2;

            }
        }
        public override void SelectFrame()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 4; //3
            }
        }

        /*  public override void SendExtraAI(BinaryWriter writer)
          {
              writer.Write(attackCounter);
          }
          public override void ReceiveExtraAI(BinaryReader reader)
          {
              attackCounter = reader.ReadInt32();
          }


          public override void AI()
          {
              Player player = Main.player[Projectile.owner];
              Player1 ModPlayer = player.RemnantOfTheAncientsMod();
              Vector2 Center = Projectile.Center;
              float distance = 0;

              if (player.HasMinionAttackTargetNPC)
              {
                  NPC target = Main.npc[player.MinionAttackTargetNPC];
                  if (target.CanBeChasedBy(Projectile, true))
                  {
                      float targetSize = target.width / 2 + target.height / 2;
                      distance = Vector2.Distance(target.Center, Projectile.Center);
                  }


                  if (distance >= 20)
                  {
                      Projectile.velocity = Projectile.Center - target.Center;
                  }
              }
              CheckExist(player, ModPlayer);
              SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
              GeneralBehavior(player, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
              Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);

          }

          public void attackController(NPC target)
          {
              switch (attackCounter)
              {
                  case 115:
                      shootIa(Projectile.damage * 2, ProjectileType<FrostBeamF>(), target, 20f, 0.5, 0.5);
                      break;
                  case >= 0:
                      shootIa(Projectile.damage, ProjectileType<Frozenp_M>(), target, 20f, 0.5, 0.5);
                      attackCounter--;
                      break;
              }
              if (attackCounter <= 0 && Vector2.Distance(Projectile.Center, target.Center) < 200 && Collision.CanHit(Projectile.Center, 1, 1, target.Center, 1, 1))
              {
                  attackCounter = 600;
                  Projectile.netUpdate = true;
              }
          }
          public void shootIa(int dammage, int type, NPC target, float Speed, double x, double y)
          {
              Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
              float rotation = (float)Math.Atan2(vector8.Y - (target.position.Y + (target.height * x)), vector8.X - (target.position.X + (target.width * y)));
              Vector2 direction;


              direction.X = (float)((Math.Cos(rotation) * Speed) * -1);
              direction.Y = (float)((Math.Sin(rotation) * Speed) * -1);


              Projectile.NewProjectile(Projectile.GetSource_FromAI(), vector8, direction, type, dammage, 0f, 0);
          }

          public void CheckExist(Player player, Player1 modPlayer)
          {
              bool minion = Projectile.type == ProjectileType<FrozenMinion>();
              player.AddBuff(BuffType<FrozenMinionBuff>(), 3600, true);
              if (minion)
              {
                  if (player.dead)
                      modPlayer.FrozenMinion = false;
                  if (modPlayer.FrozenMinion)
                      Projectile.timeLeft = 2;
              }
          }
          private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
          {
              Vector2 idlePosition = owner.Center;
              idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

              // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
              // The index is projectile.minionPos
              float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
              idlePosition.X += minionPositionOffsetX; // Go behind the player

              // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

              // Teleport to player if distance is too big
              vectorToIdlePosition = idlePosition - Projectile.Center;
              distanceToIdlePosition = vectorToIdlePosition.Length();

              if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
              {
                  // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                  // and then set netUpdate to true
                  Projectile.position = idlePosition;
                  Projectile.velocity *= 0.1f;
                  Projectile.netUpdate = true;
              }

              // If your minion is flying, you want to do this independently of any conditions
              float overlapVelocity = 0.04f;

              // Fix overlap with other minions
              for (int i = 0; i < Main.maxProjectiles; i++)
              {
                  Projectile other = Main.projectile[i];

                  if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                  {
                      if (Projectile.position.X < other.position.X)
                      {
                          Projectile.velocity.X -= overlapVelocity;
                      }
                      else
                      {
                          Projectile.velocity.X += overlapVelocity;
                      }

                      if (Projectile.position.Y < other.position.Y)
                      {
                          Projectile.velocity.Y -= overlapVelocity;
                      }
                      else
                      {
                          Projectile.velocity.Y += overlapVelocity;
                      }
                  }
              }
          }

          private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
          {
              // Starting search distance
              distanceFromTarget = 700f;
              targetCenter = Projectile.position;
              foundTarget = false;

              // This code is required if your minion weapon has the targeting feature
              if (owner.HasMinionAttackTargetNPC)
              {
                  NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                  float between = Vector2.Distance(npc.Center, Projectile.Center);

                  // Reasonable distance away so it doesn't target across multiple screens
                  if (between < 2000f)
                  {
                      distanceFromTarget = between;
                      targetCenter = npc.Center;
                      foundTarget = true;
                      attackController(npc);
                  }
              }

              if (!foundTarget)
              {
                  // This code is required either way, used for finding a target
                  for (int i = 0; i < Main.maxNPCs; i++)
                  {
                      NPC npc = Main.npc[i];

                      if (npc.CanBeChasedBy())
                      {
                          float between = Vector2.Distance(npc.Center, Projectile.Center);
                          bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                          bool inRange = between < distanceFromTarget;
                          bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                          // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                          // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                          bool closeThroughWall = between < 100f;

                          if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                          {
                              distanceFromTarget = between;
                              targetCenter = npc.Center;
                              foundTarget = true;
                          }
                      }
                  }
              }

              // friendly needs to be set to true so the minion can deal contact damage
              // friendly needs to be set to false so it doesn't damage things like target dummies while idling
              // Both things depend on if it has a target or not, so it's just one assignment here
              // You don't need this assignment if your minion is shooting things instead of dealing contact damage
              Projectile.friendly = foundTarget;
          }

          private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
          {
              // Default movement parameters (here for attacking)
              float speed = 8f;
              float inertia = 20f;

              if (foundTarget)
              {
                  // Minion has a target: attack (here, fly towards the enemy)
                  if (distanceFromTarget > 40f)
                  {
                      // The immediate range around the target (so it doesn't latch onto it when close)
                      Vector2 direction = targetCenter - Projectile.Center;
                      direction.Normalize();
                      direction *= speed;

                      Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                  }
              }
              else
              {
                  // Minion doesn't have a target: return to player and idle
                  if (distanceToIdlePosition > 600f)
                  {
                      // Speed up the minion if it's away from the player
                      speed = 12f;
                      inertia = 60f;
                  }
                  else
                  {
                      // Slow down the minion if closer to the player
                      speed = 4f;
                      inertia = 80f;
                  }

                  if (distanceToIdlePosition > 20f)
                  {
                      // The immediate range around the player (when it passively floats about)

                      // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                      vectorToIdlePosition.Normalize();
                      vectorToIdlePosition *= speed;
                      Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                  }
                  else if (Projectile.velocity == Vector2.Zero)
                  {
                      // If there is a case where it's not moving at all, give it a little "poke"
                      Projectile.velocity.X = -0.15f;
                      Projectile.velocity.Y = -0.05f;
                  }
              }
          }*/
    }
}



