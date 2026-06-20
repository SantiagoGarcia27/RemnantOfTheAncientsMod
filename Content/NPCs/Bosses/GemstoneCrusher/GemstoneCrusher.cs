using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Trower;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher
{
    [AutoloadBossHead]

    public class GemstoneCrusher : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f,
                Frame = 0,
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 500;
            NPC.damage = 20;
            NPC.width = 60;
            NPC.height = 60;
            NPC.defense = 10;
            NPC.value = Item.buyPrice(0, 1, 0, 0);
            NPC.npcSlots = 30f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
            base.SetDefaults();
        }

        Player target = null;
        float speed = 5f;

        enum BossState
        {
            Sleep,
            Iddle,
            Stomp,
            Falling
        }

        BossState currentState = BossState.Sleep;

        int stompTimer = (int)Utils1.FormatTimeToTick(Second: 2);
        int stompColdownTimer = (int)Utils1.FormatTimeToTick(Second: 6);

        int stompActivationTimmerTrigger = (int)Utils1.FormatTimeToTick(Second: 20);
        int shootTimmerTrigger = (int)Utils1.FormatTimeToTick(Second: 5);
        int spawnTimmerTrigger = (int)Utils1.FormatTimeToTick(Second: 15);
        bool touchFloor => DistanceUtils.ExistTileSolid(new Vector2(NPC.Center.X, NPC.Center.Y + NPC.height / 2));
        bool spawnWorms = false;
        public override void AI()
        {
            UpdateTarget();
           
            if (target == null) return;
            NPC.ai[2]++;

            if (NPC.ai[2] >= stompActivationTimmerTrigger)
            {
                currentState = BossState.Stomp;
                NPC.ai[2] = 0;
            }
            if (currentState == BossState.Stomp) 
                MovementAi();
            else
            {
                NPC.ai[0]++;
                NPC.ai[1]++;
                if (NPC.ai[0] >= shootTimmerTrigger - (int)Utils1.FormatTimeToTick(Second: 1))
                {

                    if (NPC.ai[0] >= shootTimmerTrigger)
                    {
                        ShootAi();
                        NPC.ai[0] = 0;
                    }
                    else
                    {
                        Dust.NewDust(NPC.Center, 10, 10, DustID.GemRuby);
                    }
                }
                if (NPC.ai[1] == spawnTimmerTrigger)
                {
                    SpawnAi();
                    NPC.ai[1] = 0;
                }
            }

            base.AI();
        }

        internal void UpdateTarget()
        {
            if (target != null && !target.dead && target.active) return;

            NPC.TargetClosest(true);
            target = Main.player[NPC.target];
        }


        internal void MovementAi()
        {
            if (touchFloor)
            {
                /*if (currentState == BossState.Stomp)
                    currentState = BossState.Falling;*/
                if (touchFloor && currentState == BossState.Falling && NPC.noTileCollide)
                {
                    NPC.noTileCollide = false;
                    currentState = BossState.Iddle;
                }
            }

            StompMovmentAi();
        }
        internal void StompMovmentAi()
        {
           // if (stompColdownTimer == 0)
                FollowPlayerCeilingAi();
            //else stompColdownTimer--;
        }
        internal void FollowPlayerCeilingAi()
        {
            if (stompTimer == 0)
            {
                StompAi();
                return;
            }
            else stompTimer--;

            //NPC.noTileCollide = !(NPC.Center.Y + 10 >= target.Center.Y && touchFloor);

            float Offset = 300f;
            Vector2 CelinPosition = new(target.Center.X, target.Center.Y - Offset);
            NPC.Center = CelinPosition;
            //NPC.position = CelinPosition;
        }
        internal void StompAi()
        {
            if (NPC.Bottom.Y + 5 >= target.Center.Y)
            {
                NPC.noTileCollide = false;
                stompTimer = (int)Utils1.FormatTimeToTick(Second: 2);
                stompColdownTimer = (int)Utils1.FormatTimeToTick(Second: 6);
                return;
            }

            if (NPC.velocity.Y == 0) NPC.velocity = new(0, 9);
            NPC.velocity.Y += 0.3f;
            currentState = BossState.Falling;

        }
        List<int> gemId = [
            ModContent.ProjectileType<GemstoneCrusherProj_Sapphire>(),
            ModContent.ProjectileType<GemstoneCrusherProj_Emerald>(),
            ModContent.ProjectileType<GemstoneCrusherProj_Ruby>(),
            ModContent.ProjectileType<GemstoneCrusherProj_Diamond>(),
        ];
        internal void ShootAi()
        {
            Vector2 shootVelocityRuby = Vector2.Normalize(target.Center - NPC.Center) * Main.rand.Next(3,10);
            int randomGemChoice = Main.rand.Next(gemId.Count);
            int p = -1;
            //randomGemChoice = 2;
            if (randomGemChoice == 0)
            {
                p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootVelocityRuby, gemId[randomGemChoice], 10, 0f, Main.myPlayer, NPC.whoAmI);
            }
            else if(randomGemChoice == 1)
            {
                shootVelocityRuby /= 2; 
                p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootVelocityRuby, gemId[randomGemChoice], 10, 0f, Main.myPlayer);
            }
            else if(randomGemChoice == 2)
            {
                shootVelocityRuby = Vector2.Normalize(target.Center - NPC.Center) * Main.rand.Next(6, 8);
                shootVelocityRuby.Y = -7f;
                p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootVelocityRuby, gemId[randomGemChoice], 10, 0f, Main.myPlayer);
            }
            else if(randomGemChoice == 3)
            {
                shootVelocityRuby.X *= 1.5f;
                p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootVelocityRuby, gemId[randomGemChoice], 10, 0f, Main.myPlayer);
            }

            if (p == -1) return;

            Main.projectile[p].penetrate = -1;
            Main.projectile[p].hostile = true;
            Main.projectile[p].friendly = false;
        }
        internal void SpawnAi()
        {
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.CaveBat);
            if(!spawnWorms && Main.expertMode && NPC.life < NPC.lifeMax / 2)
            {
                int i = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + (10 * 16), NPCID.GiantWormHead);
                Main.npc[i].lifeMax = 50;
                Main.npc[i].life = 50;
                i = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 30, (int)NPC.Center.Y + (10 * 16), NPCID.GiantWormHead);
                Main.npc[i].lifeMax = 50;
                Main.npc[i].life = 50;
                i = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 30, (int)NPC.Center.Y + (10 * 16), NPCID.GiantWormHead);
                Main.npc[i].lifeMax = 50;
                Main.npc[i].life = 50;
                spawnWorms = true;
            }
        }    
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (item.pick > 0)
            {
                modifiers.FinalDamage *= 5;
            }
            base.ModifyHitByItem(player, item, ref modifiers);
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (currentState == BossState.Sleep)
            {
                currentState = BossState.Iddle;
            }
            base.OnHitByItem(player, item, hit, damageDone);
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (currentState == BossState.Sleep)
            {
                currentState = BossState.Iddle;
            }
            base.OnHitByProjectile(projectile, hit, damageDone);

        }
        public override void OnSpawn(IEntitySource source)
        {
            currentState = BossState.Sleep;
            NPC.ai[0] = 0; // Disparo
            NPC.ai[1] = 0; // Murcielago
            NPC.ai[2] = 0; // Stomp
            base.OnSpawn(source);
        }
    }
}
