using CalamityMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant
{


    [AutoloadBossHead]
    public class InfernalTyrantHead : WormHead
    {
        public override int BodyType => ModContent.NPCType<InfernalTyrantBody>();

        public override int TailType => ModContent.NPCType<InfernalTyrantTail>();

        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/InfernalTyrantHead_Bestiary", 
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
   
        public bool head;
        public bool CanTp = false;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);      
            NPC.aiStyle = -1;
            NPC.Size = new(75, 75);
            
            NPC.boss = true;
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
            NPC.damage = 200;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
            
            Music = MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Infernal_Tyrant");
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {

            NPC.Calamity().canBreakPlayerDefense = true;
            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,0.02f,0.22f,0.29f,0.3f,0.5f);
            InfernalTyrantAuxiliaryClass.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }
      
       
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("A great and dreaded worm rules the underworld with an iron fist and his flames, powerful and majestic in equal parts, maintain the order and warmth of the underworld.")
            ]);
        }

        public override void Init()
        {
            MinSegmentLength = 15;
            MaxSegmentLength = MaxSegmentCount(MinSegmentLength);
            head = true;
            CommonWormInit(this);
        }
        internal static void CommonWormInit(Worm worm)
        {
            worm.MoveSpeed = 30f;
            worm.Acceleration = 0.245f;
        }

        private int attackCounter;
        private int attackCounterMaxValue = 800;
        private int movementTimer;
        private int movementPhase;
        private float mandibleAngle;

        private int orbitalPhase;
        private int orbitalTimer;
        private float orbitalAngle;
        private Vector2 dashDirection;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(movementTimer);
            writer.Write(movementPhase);
            writer.Write(orbitalPhase);
            writer.Write(orbitalTimer);
            writer.Write(orbitalAngle);
            writer.Write(dashDirection.X);
            writer.Write(dashDirection.Y);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
            movementTimer = reader.ReadInt32();
            movementPhase = reader.ReadInt32();
            orbitalPhase = reader.ReadInt32();
            orbitalTimer = reader.ReadInt32();
            orbitalAngle = reader.ReadSingle();
            dashDirection = new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }
        private int MaxSegmentCount(int MinSegmentLength) 
        {
            int scale = 0;
            if (DificultyUtils.MasochistMode) scale = 30;
            else if (DificultyUtils.EternityMode) scale = 25;
            else if (DificultyUtils.InfernumMode) scale = 25;
            else if (DificultyUtils.Death) scale = 20;
            else if (DificultyUtils.Revengeance) scale = 10;
            else if (Main.masterMode) scale = 5;
            else if (Main.expertMode) scale = 2;
            return MinSegmentLength + scale;
        }

        public bool SpawnClon = false;
        public bool IsEnraged => MathUtils.GetPorcentage(NPC.life, NPC.lifeMax) < 25f;
        public bool IsPhase2 => MathUtils.GetPorcentage(NPC.life, NPC.lifeMax) < 50f;
        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);

            if (!GenericVariables.SizeChanged[0])
            {
                try
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
                    }
                }
                catch 
                {
                    NPC.Size = new(75, 75);
                }
                GenericVariables.SizeChanged[0] = true;
            }

            Player target = Main.player[NPC.target];
            //UpdateMandibleAngle(target);

            UpdateCounters(target);
            DespawnSafeCheck(target, this);
            DoAttacks(target);
            if(IsPhase2) 
                SerpentMovementAi(target);
            //UpdateOrbitalAttack(target);


            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                    GenericVariables.IsSpawned = true;
                }
                else
                {
                    if (!GenericVariables.IsSpawned)
                    {
                        GenericVariables.SpawnCounter++;
                        RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC, 1f, 1f, 1f, 1f, 1f);
                    }
                }

                if (DificultyUtils.InfernumMode)
                {
                    if (attackCounter % 4 == 0)
                    {
                        for (int i = -3; i <= 3; i++)
                        {
                            Vector2 FlameVelocity = NPC.velocity * 1.25f;//1.25
                            FlameVelocity = FlameVelocity.RotatedBy(i * 20);
                            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, FlameVelocity, ProjectileID.Flames, 30, 0f, Main.myPlayer, Math.Sign(i));
                            Main.projectile[projectile].timeLeft = 30;
                            Main.projectile[projectile].tileCollide = false;
                            Main.projectile[projectile].friendly = false;
                            Main.projectile[projectile].hostile = true;
                            Main.projectile[projectile].usesLocalNPCImmunity = true;
                        }
                    }
                }

            }
            if (RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                if (DificultyUtils.EternityMode || DificultyUtils.MasochistMode)
                {
                    if (NPC.boss && !SpawnClon)
                    {
                        if (MathUtils.GetPorcentage(NPC.life, NPC.lifeMax) < (DificultyUtils.MasochistMode ? 70f : 50f))
                        {
                            var a = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<InfernalTyrantHead>());
                            Main.npc[a].boss = false;
                            Main.npc[a].lifeMax /= 8;
                            Main.npc[a].life /= 8;
                            Main.npc[a].scale = 0.5f;
                            Main.npc[a].damage /= 2;

                            SpawnClon = true;
                        }
                    }
                }
            }

            InfernalTyrantAuxiliaryClass.LifeSpeed(this);
            UpdateMovement();
        }

        public override void OnSpawn(IEntitySource source)
        {
            GenericVariables.IsSpawned = false;
            GenericVariables.SpawnCounter = 0;
        }
        private void DoAttacks(Player target)
        {
            switch (attackCounter)
            {
                case 200:
                    SpikeIa((int)(40 * RemnantGlobalNPC.DamageBonus), false, -3, -3);
                    break;
                case 250:
                    FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 4, 3, target, 0f);
                    FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "/", 4, 3, target, 0f);
                    break;
                case 300:
                    SummonIa(NPCID.Demon);
                    break;
                case 400:
                    SpikeIa((int)(90 * RemnantGlobalNPC.DamageBonus), true, 3, -3);
                    break;
                case 430:
                    SpikeIa((int)(40 * RemnantGlobalNPC.DamageBonus), false, 3, -3);
                    break;
                case 600:
                    for (int i = -2; i <= 3; i++)
                        FireBallIa(12f, (int)(30 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBall>(), "*", i - 1, i + 2, target, 0);
                    break;
                case 700:
                    if (RemnantOfTheAncientsMod.InfernumMod != null)
                    {
                        if (DificultyUtils.InfernumMode)
                        {
                            for (int i = 0; i <= 10; i++)
                            {
                                TornadoIa((int)(70 * RemnantGlobalNPC.DamageBonus), CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "Flarenado"), target, i);
                            }
                           // FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), CallUtils.Get<ModProjectile>(RemnantOfTheAncients.CalamityMod, "Flarenado"), "*", 4, 3, target, 0f);
                        }
                    }
                    SummonIa(NPCID.RedDevil);
                    break;
            }

            if (IsPhase2)
            {
                switch (attackCounter)
                {
                    case 650:
                        StartOrbitalAttack(target);
                        break;
                    case 550:
                        for (int i = -3; i <= 3; i++)
                            FireBallIa(12f, (int)(45 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 2, 2, target, i * 0.45f);
                        break;
                    case 450:
                        SpikeIa((int)(50 * RemnantGlobalNPC.DamageBonus), true, -3, -3);
                        SpikeIa((int)(50 * RemnantGlobalNPC.DamageBonus), true, 3, 3);
                        break;
                    case 320:
                        for (int i = -1; i <= 1; i++)
                            FireBallIa(12f, (int)(55 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBall>(), "*", 3, 3, target, i * 0.15f);
                        break;
                    case 180:
                        SpikeIa((int)(60 * RemnantGlobalNPC.DamageBonus), false, -2, 2);
                        SummonIa(NPCID.Demon);
                        break;
                }
            }

            if (IsEnraged)
            {
                switch (attackCounter)
                {
                    case 580:
                        for (int i = 0; i < 5; i++)
                            FireBallIa(12f, (int)(60 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 2, 2, target, i * 1.256f);
                        break;
                    case 500:
                        SpikeIa((int)(60 * RemnantGlobalNPC.DamageBonus), true, -3, 3);
                        SpikeIa((int)(60 * RemnantGlobalNPC.DamageBonus), false, 3, 3);
                        break;
                    case 350:
                        for (int i = -3; i <= 3; i++)
                            FireBallIa(12f, (int)(40 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBall>(), "*", i, i + 1, target, i * 0.2f);
                        break;
                    case 270:
                        SpikeIa((int)(80 * RemnantGlobalNPC.DamageBonus), true, -2, -2);
                        SpikeIa((int)(80 * RemnantGlobalNPC.DamageBonus), true, 2, -2);
                        SpikeIa((int)(80 * RemnantGlobalNPC.DamageBonus), true, 0, 3);
                        break;
                    case 150:
                        SpikeIa((int)(70 * RemnantGlobalNPC.DamageBonus), true, -3, -3);
                        SpikeIa((int)(70 * RemnantGlobalNPC.DamageBonus), true, 3, -3);
                        break;
                    case 120:
                        StartOrbitalAttack(target);
                        break;
                    case 50:
                        for (int i = -2; i <= 2; i++)
                            FireBallIa(12f, (int)(50 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 3, 2, target, i * 0.3f);
                        break;
                }
            }
        }
        public void UpdateCounters(Player target)
        {
            int distance = (int)Vector2.Distance(NPC.Center, target.Center);
            int distanceThreshold = IsEnraged ? 600 : IsPhase2 ? 400 : 200;

            if (distance < distanceThreshold && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
            {
                if (attackCounter <= 0)
                {
                    if (IsEnraged)
                        attackCounterMaxValue = !Main.expertMode ? 600 : 700;
                    else if (IsPhase2)
                        attackCounterMaxValue = !Main.expertMode ? 650 : 750;
                    else
                        attackCounterMaxValue = !Main.expertMode ? 700 : 800;

                    attackCounter = attackCounterMaxValue;
                    NPC.netUpdate = true;
                }       
            }
            if (attackCounter > 0)
            {
                attackCounter--;
            }
        }

        private void UpdateMovement()
        {
            if (!IsPhase2)
            {
                Acceleration = 0.245f;
                return;
            }

            movementTimer--;
            if (movementTimer <= 0)
            {
                int maxPhases = IsEnraged ? 3 : 2;
                movementPhase = (movementPhase + 1) % maxPhases;
                movementTimer = movementPhase switch
                {
                    0 => 180,
                    1 => IsEnraged ? 120 : 150,
                    2 => 100,
                    _ => 180
                };
                NPC.netUpdate = true;
            }

            switch (movementPhase)
            {
                case 1:
                    MoveSpeed = IsEnraged ? 42f : 36f;
                    Acceleration = 0.20f;
                    break;
                case 2:
                    MoveSpeed = 34f;
                    Acceleration = 0.15f;
                    break;
                default:
                    MoveSpeed = 30f;
                    Acceleration = IsEnraged ? 0.28f : 0.26f;
                    break;
            }
        }

        //No terminado (ignorar)
        /*private void UpdateMandibleAngle(Player target)
        {
            float distToPlayer = Vector2.Distance(NPC.Center, target.Center);
            float targetAngle;

            if (distToPlayer > 400f)
                targetAngle = 0f;
            else if (distToPlayer > 200f)
                targetAngle = 0.45f;
            else
                targetAngle = 0f;

            float lerpSpeed = targetAngle < mandibleAngle ? 0.2f : 0.08f;
            mandibleAngle = MathHelper.Lerp(mandibleAngle, targetAngle, lerpSpeed);
        }*/

        private void StartOrbitalAttack(Player target)
        {
            if (orbitalPhase != 0) return;
            orbitalPhase = 1;
            orbitalTimer = Utils1.FormatTimeToTick(Second:4);
            orbitalAngle = (NPC.Center - target.Center).ToRotation();
            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            NPC.netUpdate = true;
        }

        private float amplitud = 3.0f;          // grados máximos de desviación por frame
        private float frecuencia = 0.02f;       // velocidad de ondulación (ciclo completo cada ~50 ticks)

        private void SerpentMovementAi(Player target)
        {
            if (NPC.velocity.LengthSquared() < 0.01f) return;

            float tiempo = (float)Main.timeForVisualEffects;

            // Ángulo oscilante en radianes: rota la velocidad ±amplitud grados
            float angulo = MathHelper.ToRadians(amplitud)
                * (float)Math.Sin(tiempo * frecuencia * MathHelper.TwoPi + NPC.whoAmI * 1.5f);

            // Rotar la velocidad existente (no suma, no usa dirección externa)
            NPC.velocity = NPC.velocity.RotatedBy(angulo);
        }


        //No terminado
        /*private void UpdateOrbitalAttack(Player target)
        {
            if (orbitalPhase == 0 && target.Distance(NPC.Center) > (200 * 16f) ) return;

            const float orbitalRadius = 100f * 16f;

            switch (orbitalPhase)
            {
                case 1:
                {
                    float angularSpeed = MathHelper.TwoPi / 50f;
                    orbitalAngle += angularSpeed;

                    Vector2 desiredPos = target.Center + orbitalAngle.ToRotationVector2() * orbitalRadius;
                    Vector2 toDesired = desiredPos - NPC.Center;
                    float speed = MathHelper.Clamp(toDesired.Length() * 0.2f, 20f, 50f);
                    NPC.velocity = toDesired.SafeNormalize(Vector2.UnitX) * speed;

                    for (int d = 0; d < 2; d++)
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f);

                    orbitalTimer--;
                    if (orbitalTimer <= 0)
                    {
                        orbitalPhase = 2;
                        orbitalTimer = 40;
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
                        NPC.netUpdate = true;
                    }
                    break;
                }
                case 2:
                {
                    dashDirection = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    float t = orbitalTimer / 40f;
                    NPC.velocity = dashDirection * t * 5f;

                    for (int d = 0; d < 3; d++)
                    {
                        int dust = Dust.NewDust(NPC.Center - new Vector2(16f), 32, 32, DustID.InfernoFork, 0f, 0f, 100, default, 1.5f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 2f;
                    }

                    orbitalTimer--;
                    if (orbitalTimer <= 0)
                    {
                        orbitalPhase = 3;
                        orbitalTimer = 25;
                        dashDirection = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                        NPC.velocity = dashDirection * 45f;
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.netUpdate = true;
                    }
                    break;
                }
                case 3:
                {
                    NPC.velocity = dashDirection * 45f;

                    for (int d = 0; d < 3; d++)
                    {
                        int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.InfernoFork, -NPC.velocity.X * 0.3f, -NPC.velocity.Y * 0.3f, 100, default, 2f);
                        Main.dust[dust].noGravity = true;
                    }

                    orbitalTimer--;
                    if (orbitalTimer <= 0)
                    {
                        orbitalPhase = 0;
                        NPC.netUpdate = true;
                    }
                    break;
                }
            }
        }
        */
        public void FindTarget()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                if (InfernalTyrantAuxiliaryClass.AllPlayersDead())
                {
                    NPC.TargetClosest(true);
                    NPC.velocity *= 0.1f;
                }
            }
        } 
        public void DespawnSafeCheck(Player player, Worm worm)
        {
            float positionY = NPC.position.Y / 16;
            float limit = Main.maxTilesY - 50f;
            if ((!NPC.WithinRange(player.Center, 170 * 16f) || positionY >= limit) && !player.dead)
            {
                Vector2 directionToPlayer = Vector2.Normalize(player.Center - NPC.Center);
                NPC.velocity = directionToPlayer * worm.MoveSpeed/4;
                NPC.netUpdate = true;
            }
        }
        public void FireBallIa(float Speed, int damage, int type, string signo1, int cordx, int cordy, Player player, float grades)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * cordx), NPC.position.Y + (NPC.height * cordy));
            if (signo1 == "/")
                vector8 = new Vector2(NPC.position.X + (NPC.width / cordx), NPC.position.Y + (NPC.height / cordy));

           // int type = ProjectileType<InfernalBallF>();
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);

            Vector2 direction = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
            direction = direction.RotatedBy(grades);

            //float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction * 12, type, damage, 0f, 0);
            Main.projectile[projectile].timeLeft = 300;
        }
        public void TornadoIa(int damage, int type, Player player, int i)
        {
            Vector2 spawn = DistanceUtils.SearchLiquidCoordenates(LiquidID.Lava, 10, player);
            bool m = false;
            Vector2 fixedSpawn = new(spawn.X, DistanceUtils.CheckHigh(spawn));
          
            

            // player.position = spawn;
            Vector2 ppos = player.position;

            Vector2 position =  new(player.Center.X + (40 *16f), fixedSpawn.Y -i *16 *4);
           

            // int type = ProjectileType<InfernalBallF>();
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
            //float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), position, Vector2.Zero, type, damage, 0f, player.whoAmI,1);
            Main.projectile[projectile].timeLeft = 1300;
        }
        public void SpikeIa(int damage, bool isStrong, int cordx, int cordy)
        {
            int type = isStrong ? ModContent.ProjectileType<InfernalSpikeF>() : ModContent.ProjectileType<InfernalSpike>();
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
            Vector2 spikeDirection = Vector2.Normalize(new Vector2(cordx, cordy));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spikeDirection * 14f, type, damage, 0f, Main.myPlayer);
            Main.projectile[projectile].timeLeft = 1500;
        }
      

        public void SummonIa(int Npc) => NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, Npc);
       
     
       
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantHeadGore1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantHeadGore2").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood,hit.HitDirection, -1f);
                }
                RemnantDownedBossSystem.downedTyrant = true;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => head ? null : false;
        public override void BossLoot(ref string name, ref int potionType)
        {
            RemnantDownedBossSystem.downedTyrant = true;
            potionType = ItemID.GreaterHealingPotion;
        }
         public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return;

            //DrawMandibles(drawColor);
        }

        //No funciona (toca arreglar las posiciones de las mandíbulas)
        private void DrawMandibles(Color drawColor)
        {
            var texRequest = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/InfernalTyrantMandible");
            if (!texRequest.IsLoaded) return;
            Texture2D tex = texRequest.Value;

            Vector2 drawPos = NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);

            Vector2 forward = NPC.rotation.ToRotationVector2();

            // Pivote ajustado (mantén el que te dejó bien ubicado)
            float pivotOffset = 16f * NPC.scale;   // o el valor que te dejó el punto rojo en la boca
            Vector2 hingeCenter = drawPos + forward * pivotOffset;

            // Separación RELATIVA al frente del boss
            // Invertimos el signo del perp para que "superior" quede en el lado que querés (prueba + o -)
            Vector2 perp = forward.RotatedBy(MathHelper.PiOver2);  // +90° (derecha relativa al forward)
                                                                   // Si querés invertir (para que una quede "arriba" en vertical), usa:
                                                                   // Vector2 perp = forward.RotatedBy(-MathHelper.PiOver2);   // -90° (izquierda relativa)

            float halfSep = 3f * NPC.scale;  // ← prueba 2f a 5f hasta que la separación se vea bien (no muy pegadas ni muy lejos)
            Vector2 upperHinge = hingeCenter - perp * halfSep;   // "superior" = -perp
            Vector2 lowerHinge = hingeCenter + perp * halfSep;   // "inferior" = +perp

            Vector2 origin = new Vector2(0f, tex.Height * 0.5f);

            SpriteEffects upperEffects = NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            SpriteEffects lowerEffects = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;

            float mandibleBaseRot = NPC.rotation + MathHelper.Pi;  // el que te dejó la rotación correcta

            float angleMultiplier = NPC.spriteDirection == 1 ? 1f : -1f;

            // Mandíbula superior
            Main.EntitySpriteDraw(tex, upperHinge, null, drawColor, mandibleBaseRot - (mandibleAngle * angleMultiplier), origin, NPC.scale, upperEffects, 0);
            // Mandíbula inferior
            Main.EntitySpriteDraw(tex, lowerHinge, null, drawColor, mandibleBaseRot + (mandibleAngle * angleMultiplier), origin, NPC.scale, lowerEffects, 0);

            // Debug rojo (para confirmar pivote)
            var pixelTex = TextureAssets.MagicPixel.Value;
            if (pixelTex != null)
            {
                Main.spriteBatch.Draw(pixelTex, hingeCenter - Main.screenPosition, null, Color.Red * 0.9f, 0f, new Vector2(0.5f), 10f, SpriteEffects.None, 0f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpertOneFromOptions(1, 999999999,
            [
                ModContent.ItemType<SpikeSaber>(), 
                ModContent.ItemType<TyrantRepeater>(), 
                ModContent.ItemType<Tyran_Blast>(), 
                ModContent.ItemType<EvilEyeStaff>() 
            ]));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernalMask>(), 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernalTrophy>(), 10));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<infernalBag>()));
            if(DificultyUtils.InfernumMode) 
                npcLoot.Add(RemnantDropRules.InfernumModeCommonDrop(ModContent.ItemType<Tyrant_Relic>()));
            else 
                npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Tyrant_Relic>()));

            if (RemnantOfTheAncientsMod.CalamityMod != null) CalamityDrop(npcLoot);
        }
        [JITWhenModsEnabled("CalamityMod")]
        private static void CalamityDrop(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EssenceofChaos"), 1, 5, Utils1.ReaperDropScaler(15)));
        }
    }

    internal class InfernalTyrantBody : WormBody
    {
        [Obsolete]
        public override void SetStaticDefaults()
        { 
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }

        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            NPC.aiStyle = -1;
            NPC.Size = new(75, 75);

            NPC.boss = true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().canBreakPlayerDefense = true;
            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,0.2f, 0.42f, 0.59f, 0.6f,0.6f);
            InfernalTyrantAuxiliaryClass.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }
       
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            TyranStats.DrawGlow(NPC, "InfernalTyrantBody");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    for(int i = 1; i <= 3; i++) 
                    {
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantBodyGore"+i).Type, NPC.scale);
                    }      
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood,hit.HitDirection, -1f);
                }
                RemnantDownedBossSystem.downedTyrant = true;
            }
        }
        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            GenericVariables gv = new GenericVariables();
            if (!GenericVariables.SizeChanged[1])
            {
                try
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
                    }
                }
                catch (Exception)
                {
                    NPC.Size = new(75, 75);
                }
                GenericVariables.SizeChanged[1] = true;
            }
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                }
                else
                {
                    if (RemnantOfTheAncientsMod.CalamityMod != null) RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,1f, 1f, 1f, 1f, 1f);
                }
            }
        }
        public override void Init()
        {
            InfernalTyrantHead.CommonWormInit(this);
        }
    }

    internal class InfernalTyrantTail : WormTail
    {
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }

        [Obsolete]
        public override void SetStaticDefaults()
        {

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
            
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity(); 
            NPC.Size = new(75, 75);  
            NPC.defense = 25;
            NPC.aiStyle = -1;
            NPC.boss = true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().canBreakPlayerDefense = true;
            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,0.01f, 0.12f, 0.19f, 0.2f, 0.2f);
            InfernalTyrantAuxiliaryClass.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }

        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.defense = TyranStats.TyrantArmor(25, NPC);
            if (!GenericVariables.SizeChanged[2])
            {
                try
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
                    }
                }
                catch (Exception)
                {
                    NPC.Size = new(75, 75);
                }
                GenericVariables.SizeChanged[2] = true;
            }
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                }
                else
                {
                    RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC, 1f, 1f, 1f, 1f, 1f);
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantTailGore1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantTailGore2").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood,hit.HitDirection, -1f);
                }
                RemnantDownedBossSystem.downedTyrant = true;
            }
            base.HitEffect(hit);
        }
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 2;
            base.ModifyHitByItem(player, item, ref modifiers);
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 2;
            base.ModifyHitByProjectile(projectile, ref modifiers);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            TyranStats.DrawGlow(NPC, "InfernalTyrantTail");   
        }
        public override void Init()
        {
            InfernalTyrantHead.CommonWormInit(this);
        }
    }

    public static class TyranStats 
    {
        public static int TyrantArmor(int defense, NPC npc)
        {
            // 1. Calculamos el porcentaje de vida actual (0.0 a 1.0)
            float lifePercent = (float)npc.life / npc.lifeMax;

            // 2. Aplicamos una potencia para que la caída sea drástica al inicio.
            // Elevar el porcentaje a la potencia 5 hace que:
            // Al 100% de vida -> 1.0^5 = 1.0  (Defensa total: 999)
            // Al 75% de vida  -> 0.75^5 = 0.23 (Defensa: ~230)
            // Al 50% de vida  -> 0.50^5 = 0.03 (Defensa: ~30)
            float decayFactor = (float)Math.Pow(lifePercent, 5);

            // 3. Definimos el "Suelo" (Mínimo de defensa para que no sea papel al final)
            float minPercent = 0.05f; // Mantener al menos un 15% de la defensa

            if (Main.expertMode || Main.masterMode)
            {
                if (Reaper.ReaperMode) minPercent = 0.00f;
                minPercent = 0.02f;
            }

            // Mezclamos el factor de caída con el mínimo (Lerp manual)
            float finalFactor = MathHelper.Lerp(minPercent, 1f, decayFactor);

            int processedDefense = (int)(defense * finalFactor);

            // 4. Multiplicador de Calamity
            return RemnantOfTheAncientsMod.CalamityMod != null ? processedDefense * 2 : processedDefense;
        }


        public static void DrawGlow(NPC npc, string NpcName)
        {
            SpriteEffects effects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            var glowTexture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/" + NpcName + "_Glow");
            Vector2 origin = new(glowTexture.Width * 0.5f, glowTexture.Height / Main.npcFrameCount[npc.type] * 0.5f);
            Vector2 position = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
            Color color = Utils.MultiplyRGBA(new Color(127 - npc.alpha, 127 - npc.alpha, 127 - npc.alpha, 0), Color.LightYellow);

            Main.EntitySpriteDraw(glowTexture, position, npc.frame, color, npc.rotation, origin, npc.scale, effects, 0);
        }
    }
}