using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.GameContent.Bestiary;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Frozen;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter
{
    [AutoloadBossHead]
    public class FrozenAssaulter : ModNPC
    {
        public int invincibilityTimer = 0;
        public bool healAnimation = false;
        public int currentPhase = 1;
        public int MaxPlayers = RemnantOfTheAncientsMod.MaxPlayerOnline() / 2;
        public int TpDelay = 20;
        public override void SetStaticDefaults()
        {        
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        public override void SetDefaults()
        {
            NPC.Size = new Vector2(150, 150);
            NPC.aiStyle = 5;
            NPC.lifeMax = 18500;
            NPC.damage = 90;
            NPC.defense = 15;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 5, 75, 45);
            NPC.npcSlots = 10f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Frozen_Assaulter_p1");
            NPC.netAlways = true;  
        }
        public int attackCounter;
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                 BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
                //new FlavorTextBestiaryInfoElement("A great and dreaded worm rules the underworld with an iron fist and his flames, powerful and majestic in equal parts, maintain the order and warmth of the underworld.")
            ]);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }
        float grades = 0;
        float delay = 2;
        public override void AI()
        {
            Player target = Main.player[NPC.target];
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            PhaseChanger();
            setAttackCounter(target);

            if (target.dead)
            {
                NPC.EncourageDespawn(7);
                return;
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }


            if (RemnantOfTheAncientsMod.InfernumMod != null && DificultyUtils.InfernumMode)
            {
                FrozenAssaulterInfernum.InfernumAi(NPC, currentPhase, attackCounter);
                CheckPhase();
                CheckDistance(distance, NPC);
                if (RemnantOfTheAncientsMod.FargosSoulMod != null)
                {
                    EternityIA(target);
                }
            }
            else
            {
                BaseAi(target, distance);
            }
        }
        
        public void BaseAi(Player target, float distance)
        {
            ShootAi(target, DificultyUtils.ReaperMode);
            SummonAi(DificultyUtils.ReaperMode);
            CheckDistance(distance, NPC);
            DogdeAi(target);


            if (Main.expertMode)
            {
                if (attackCounter < 600 && attackCounter > 500 && currentPhase != 3)
                {
                    ShootIa((int)NpcChanges1.ExpertDamageScale(10), ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                }
            }
            if (Main.expertMode )
            {
                CheckPhase();
            }
        }
        private void DogdeAi(Player target)
        {
            float DistancePlayer;
            List<Projectile> projectiles;
            if (DogdeCouldown == 0)
            {
                DistancePlayer = Vector2.Distance(NPC.Center, target.Center);
                if (DistancePlayer <= 250)
                {
                    Dogde(target);
                }

                projectiles = Utils1.SearchProjectiles(true, NPC, 250);
                if (projectiles.Count > 0)
                {
                    Dogde(projectiles[0]);
                }
            } 
            else
            {
                DogdeCouldown = Math.Max(DogdeCouldown - 1, 0); //actualiza el contador
            }
        }
        private void SummonAi(bool isReaper)
        {  
            int conter1 = isReaper? 150 : 300;
            if(attackCounter == conter1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IceElemental);
            if (attackCounter == 600 && currentPhase >= 2 && (Main.expertMode || Main.masterMode))
            {
                int a = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IceGolem);
                Main.npc[a].damage *= 2;
                Main.npc[a].lifeMax /= 2;
                Main.npc[a].value = 0;
            }
        }
        float DogdeCouldown =0;
        public void Dogde(Player player)
        {
            NPC.velocity = -Vector2.Normalize(player.Center - NPC.Center) * 12;
            NPC.velocity.Y +=  5 * (Main.rand.NextBool(2)? 1:-1);
            DogdeCouldown = 360;
        }
        public void Dogde(Projectile proj)
        {
            NPC.velocity = -Vector2.Normalize(proj.Center - NPC.Center) * 12;
            NPC.velocity.Y += 3 * (Main.rand.NextBool(2) ? 1 : -1);
            DogdeCouldown = 360;
        }
        private Vector2 ExplosionPosition;
        private void ShootAi(Player target, bool isReaper)
        {
            switch (attackCounter)
            {
                case 500:
                case 200:
                    if (currentPhase == 3 )
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ShootSwordIa(target, 50, ModContent.ProjectileType<FrozenPermafrostRain>(), 4f);
                        }
                    }
                    break;
                case 400:
                    if (isReaper)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            ShootIa(10, ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                        }
                    }
                    break;
                case 115:
                    if (isReaper)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            ShootIa(10, ProjectileID.FrostBeam, target, 30f, 0.5, 0.5);
                        }
                    }
                    else
                    {
                        ShootIa(50, ProjectileID.FrostBeam, target, 20f, 0.5, 0.5);
                    }
                    break;
                case >= 0:
                    if (currentPhase != 3 && !DificultyUtils.EternityMode && !DificultyUtils.MasochistMode)
                    {
                        if (!isReaper)
                        {

                            if (currentPhase > 2) ShootIa(10, ProjectileType<Frozenp>(), target, -20f, 0.5, 0.5);
                            for (int i = -1; i < MaxPlayers; i++)
                            {
                                int a = i == -1 ? Main.myPlayer : i > 1 ? i - 1 : i;
                                ShootIa(10, ProjectileType<Frozenp>(), Main.player[a], 20f, 0.5, 0.5);
                            }
                        }
                        else
                        {
                            int framerate;
                            if (Main.frameRate >= 60)
                            {
                                framerate = 60;
                            }
                            else
                            {
                                framerate = Main.frameRate;
                            }
                            float diferencia = Utils1.FormatToPositive(55 - framerate);
                            if (++delay >= diferencia)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    ShootIa(10, ProjectileType<Frozenp>(), 7f, 360f + grades, 0);//70
                                    ShootIa(10, ProjectileType<Frozenp>(), 7f, -120f + grades, 0);
                                    ShootIa(10, ProjectileType<Frozenp>(), 7f, 120f + grades, 0);
                                    ShootIa(10, ProjectileType<Frozenp>(), 7f, -360f + grades, 0);
                                }
                                grades = (grades <= 360) ? (grades + 0.01f) : 0;
                                delay = 0;
                            }
                        }
                    }
                    break;
            }
            if (RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                EternityIA(target);
            }
        }
        #region Eternity
        public void EternityIA(Player target)
        {
            if (RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                if (DificultyUtils.EternityMode || DificultyUtils.MasochistMode)
                {
                    int MainShootRate = ((DificultyUtils.MasochistMode ? 4 : 8) * (!DificultyUtils.InfernumMode ? 1 : 2));
                    if (attackCounter % MainShootRate == 0 && currentPhase != 2)
                    {
                        EthernityCommonShoot(target);
                    }
                    if (currentPhase > 2)
                    {
                        for (int i = DificultyUtils.MasochistMode ? 5 : 4; i > (DificultyUtils.MasochistMode ? 0 : 3); i--)
                        {
                            EthernityExplosionIA(i * 10);
                        }
                        if (currentPhase == 3)
                        {
                            for (int i = 8; i > 0; i--)
                            {
                                EthernityExplosionIA(i * 10);
                            }
                        }
                        if (currentPhase >= 4)
                        {
                            if (!DificultyUtils.MasochistMode)
                            {
                                EthernityExplosionIA(currentPhase == 4 ? 200 : 500);
                            }
                            else
                            {
                                if (currentPhase == 4)
                                {
                                    for (int i = 100; i > 0; i -= 5)
                                    {
                                        EthernityExplosionIA(i);
                                    }
                                }
                                else if (currentPhase > 4)
                                {
                                    for (int i = 300; i >= 500; i += 100)
                                    {
                                        EthernityExplosionIA(i);
                                    }
                                }
                            }
                        }
                    }
                }  
            }
        }

        public void EthernityExplosionIA(int count)
        {
            int X;
            int Y;
            if (attackCounter == count + 120 && attackCounter > 0)
            {
                X = Main.rand.Next((int)(Main.player[NPC.target].position.X - 50 * 16f), (int)(Main.player[NPC.target].position.X + 50 * 16f));
                Y = Main.rand.Next((int)(Main.player[NPC.target].position.Y - 50 * 16f), (int)(Main.player[NPC.target].position.Y + 50 * 16f));
                ExplosionPosition = new Vector2(X, Y);
                var p = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(ExplosionPosition.X, ExplosionPosition.Y), Vector2.Zero, ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "WOFReticle"), 0, 0f, Main.myPlayer);
            }
            if (attackCounter == count && attackCounter > 0)
            {
                int numProjectiles = 5;

                for (int i = 0; i < numProjectiles; i++)
                {
                    Vector2 Velocity = new Vector2(2, 2).RotatedBy(i * 10);

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(ExplosionPosition.X, ExplosionPosition.Y), Velocity, ProjectileID.FrostWave, 30, 1, Main.myPlayer);
                }
            }
        }
        public void EthernityCommonShoot(Player target)
        {
            if (!DificultyUtils.ReaperMode)
            {

                if (currentPhase > 2)
                {
                    ShootIa(10, ProjectileID.FrostBeam, target, -10f, 0.5, 1.5);
                    ShootIa(10, ProjectileID.FrostBeam, target, 10f, 0.5,-1.5);

                    if (DificultyUtils.MasochistMode)
                    {
                        if (!DificultyUtils.InfernumMode)
                        {
                            ShootIa(10, ProjectileID.FrostBeam, target, -30f, -1.5, 1.5);
                            ShootIa(10, ProjectileID.FrostBeam, target, 30f, 1.5, -1.5);
                        }
                        ShootIa(10, ProjectileID.FrostBeam, target, 40, 0, 1.5);
                        ShootIa(10, ProjectileID.FrostBeam, target, 10, -1.5, -1.5);
                    }
                }
                for (int i = -1; i < MaxPlayers; i++)
                {
                    int a = i == -1 ? Main.myPlayer : i > 1 ? i - 1 : i;
                    ShootIa(10, ProjectileID.FrostBeam, Main.player[a], 20f, 0.5, 0.5);
                    ShootIa(10, ProjectileID.FrostBeam, target, -20f, 0.5, 0.5);
                }
            }
            else
            {
                int framerate;
                if (Main.frameRate >= 60)
                {
                    framerate = 60;
                }
                else
                {
                    framerate = Main.frameRate;
                }
                float diferencia = Utils1.FormatToPositive(55 - framerate);
                if (++delay >= diferencia)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (!DificultyUtils.InfernumMode)
                        {
                            ShootIa(10, ProjectileID.FrostBeam, 7f, 360f + grades, 0);//70
                            ShootIa(10, ProjectileID.FrostBeam, 7f, -120f + grades, 0);
                        }
                        ShootIa(10, ProjectileID.FrostBeam, 7f, 120f + grades, 0);
                        ShootIa(10, ProjectileID.FrostBeam, 7f, -360f + grades, 0);
                    }
                    grades = (grades <= 360) ? (grades + 0.01f) : 0;
                    delay = 0;
                }
            }
        }
        #endregion

        private void setAttackCounter(Player target)
        {
            if (attackCounter > 0)
            {
                attackCounter--;
            }
            float Distance = Vector2.Distance(NPC.Center, target.Center);
            bool IsOnDistance = Distance < 600;
            bool CanHit = Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1);
            if (attackCounter <= 0 && IsOnDistance && CanHit)
            {

                attackCounter = 900;
                NPC.netUpdate = true;
            }

            if (DificultyUtils.InfernumMode)
            {
                if (attackCounter <= 0)
                {
                    attackCounter = 900;
                    NPC.netUpdate = true;
                }
            }
        }
        public void CheckPhase()
        {
            if (currentPhase == 3)
            {
               FrozenPhase3();
            }
            if (currentPhase == 4 && (attackCounter == 700 || attackCounter == 200))
            {
                FrozenTp();
            }
        }

        public static void CheckDistance(float distance, NPC NPC)
        {
            if(distance <= 60)
            {
                NPC.directionY = -NPC.oldDirectionY - 310;
            }
        }
        public void ShootIa(int damage, int type, Player player, float speed, double x, double y)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * x)), vector8.X - (player.position.X + (player.width * y)));
            Vector2 direction;
            direction.X = (float)((Math.Cos(rotation) * speed) * -1);
            direction.Y = (float)((Math.Sin(rotation) * speed) * -1);
            int i = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction, type, damage, 0f, 0);
            Main.projectile[i].timeLeft = 200;
            Main.projectile[i].hostile = true;
            Main.projectile[i].friendly = false;
        }
        public void ShootIa(int damage, int type, float speed, float grades, float Orotation)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            Vector2 direction = Vector2.UnitX * speed;
            direction = direction.RotatedBy(grades);
            int i = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction, type, damage, 0f, Main.myPlayer);
            Main.projectile[i].timeLeft = 200;
            Main.projectile[i].hostile = true;
            Main.projectile[i].friendly = false;
            Main.projectile[i].rotation += Orotation;
        }
        public void ShootSwordIa(Player target,int Damage,int type,float speed)
        {
            // Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            Vector2 position = target.position;
            float ceilingLimit = position.Y;
            if (ceilingLimit > target.Center.Y - 200f)
            {
                ceilingLimit = target.Center.Y - 200f;
            }
            for (int i = 0; i < 3; i++)
            {
                position = target.Center - new Vector2(Main.rand.NextFloat(401) * NPC.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = position- NPC.position /*- position*/;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }
                Vector2 velocity = Vector2.UnitX * speed;
                velocity = velocity.RotatedBy(grades);
                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                var p = Projectile.NewProjectile(Projectile.GetSource_None(), position, heading, type, Damage, 1, target.whoAmI, 0f, ceilingLimit);
                Main.projectile[p].stepSpeed = speed;
            }
        }
       
        public void FrozenPhase3()
        {
            Music = MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Frozen_Assaulter_p2");


            switch (invincibilityTimer)
            {
                case 0:
                    NPC.defDefense = 9999;
                    invincibilityTimer = 1304;   
                    break;
                case 4:
                    NPC.defDefense = 15;
                    invincibilityTimer--;
                    break;
                case 3 :
                    if (!healAnimation)
                    {
                        if (DificultyUtils.ReaperMode)
                        {
                            NPC.life = NPC.lifeMax / 2;
                        }
                        else
                        {
                            NPC.life = NPC.lifeMax / 3;
                        }
                        healAnimation = true;
                        invincibilityTimer--;
                    }
                    break;
                case > 1:
                    invincibilityTimer--;
                        break;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (currentPhase == 3 && NPC.defDefense == 9999) modifiers.SetMaxDamage(1);
            base.ModifyHitByProjectile(projectile, ref modifiers);
        }
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (currentPhase == 3 && NPC.defDefense == 9999) modifiers.SetMaxDamage(1);
            base.ModifyHitByItem(player, item, ref modifiers);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment2").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment3").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment4").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("frozen_assaulterFragment5").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood,hit.HitDirection, -1f);
                }

                if(RemnantOfTheAncientsMod.InfernumMod != null)
                {
                    if(DificultyUtils.InfernumMode) 
                    {
                        int numberOfPoints = 6;
                        float radius = 9 *16;
                        for (int i = 0; i < numberOfPoints; i++)
                        {
                            float angle = (float)(2 * Math.PI * i / numberOfPoints);
                            float x = (float)(NPC.Center.X + radius * Math.Cos(angle));
                            float y = (float)(NPC.Center.Y + radius * Math.Sin(angle));

                            FrozenAssaulterInfernum.ShootIceCubeIa(10, ProjectileType<FrozenHommingIceBlock>(), 30f, i * 10, 0, NPC.target,NPC,x,y);
                        }

                        numberOfPoints = 8;
                        radius = 15 * 16;
                        for (int i = 0; i < numberOfPoints; i++)
                        {
                            float angle = (float)(2 * Math.PI * i / numberOfPoints);
                            float x = (float)(NPC.Center.X + radius * Math.Cos(angle));
                            float y = (float)(NPC.Center.Y + radius * Math.Sin(angle));

                            FrozenAssaulterInfernum.ShootIceCubeIa(10, ProjectileType<FrozenHommingIceBlock>(), 30f, i * 10, 0, NPC.target,NPC, x, y);
                        }
                    }
                }
            }
            PhaseChanger();
        }
        public void FrozenTp()
        {

            Vector2 position = Main.player[NPC.target].Center + new Vector2(Main.rand.Next(-250 * 2, 150 * 2), Main.rand.Next(-250 * 2, 150 * 2));
            Projectile.NewProjectile(NPC.GetSource_FromAI(), position, new Vector2(0, 0), ProjectileType<BossMark>(), 0, 0f, 0);
            new BossMark().texture = "RemnantOfTheAncientsMod/Items/Core/Frost_core";

            if (TpDelay == 0)
            {
                NPC.position = position;
                TpDelay = 20;
            }
            else
            {
                TpDelay--;
            }
        }
        public void PhaseChanger()
        {
            if (NPC.life >= (NPC.lifeMax / 2))
            {
                currentPhase = 1;
                NPC.localAI[2] = 0;
            }
            else if (NPC.life <= (NPC.lifeMax / 4))
            {
                if (healAnimation)
                {
                    currentPhase = 4;
                }
                else
                {
                    currentPhase = 3;
                    NPC.localAI[2] = 1;
                }
            }
            else if (NPC.life <= (NPC.lifeMax / 2) && currentPhase < 3) currentPhase = 2;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            RemnantOfTheAncientsMod.MaxPlayers = 0;
            RemnantDownedBossSystem.downedFrozen = true;
            potionType = ItemID.GreaterHealingPotion;
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Ice_escense>(), 10);
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.IceBlock, 50);

        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<FrostShark>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<Permafrost>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<FrozenStafff>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<frozen_staff>(), 4, 999999999));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<FrozenMask>(), 7, 999999999));
            npcLoot.Add(ItemDropRule.Common(ItemType<Ice_escense>(), 1, 5, 20));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.FrostCore, 5, 3));
            npcLoot.Add(ItemDropRule.BossBag(ItemType<frostBag>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<FrostTrophy>(), 10));
            if (RemnantOfTheAncientsMod.InfernumMod != null) npcLoot.Add(RemnantDropRules.InfernumModeCommonDrop(ModContent.ItemType<Frozen_Relic>()));
            else npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Frozen_Relic>()));

            if (RemnantOfTheAncientsMod.CalamityMod != null) CalamityDrop(npcLoot);
        }

        [JITWhenModsEnabled("CalamityMod")]
        private static void CalamityDrop(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EssenceofEleum"), 1,2,Utils1.ReaperDropScaler(5)));
        } 
        public override void OnSpawn(IEntitySource source)
        {
        }

        private const int Frame_static = 0;
        private const int Frame_1 = 1;
        private const int Frame_2 = 2;
        private const int Frame_3 = 3;
        private const int Frame_Breack_0 = 4;
        private const int Frame_Breack_1 = 5;
        private const int Frame_Breack_2 = 6;
        private const int Frame_Breack_3 = 7;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            if (NPC.life > NPC.lifeMax / 4)
            {
                switch (NPC.frameCounter)
                {
                    case < 3:
                        ChoiseFrame(Frame_static, frameHeight);
                        break;
                    case < 6:
                        ChoiseFrame(Frame_1, frameHeight);
                        break;
                    case < 9:
                        ChoiseFrame(Frame_2, frameHeight);
                        break;
                    case < 12:
                        ChoiseFrame(Frame_3, frameHeight);
                        break;
                    default:
                        NPC.frameCounter = 0;
                        break;
                }
            }

            else if (NPC.life < NPC.lifeMax / 4)
            {
                switch (NPC.frameCounter)
                {
                    case < 3:
                        ChoiseFrame(Frame_Breack_0, frameHeight);
                        break;
                    case < 5:
                        ChoiseFrame(Frame_Breack_1, frameHeight);
                        break;
                    case < 7:
                        ChoiseFrame(Frame_Breack_2, frameHeight);
                        break;
                    case < 9:
                        ChoiseFrame(Frame_Breack_3, frameHeight);
                        break;
                    default:
                        NPC.frameCounter = 0;
                        break;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, (NPC.position - Main.screenPosition) + new Vector2((0 * 16), NPC.gfxOffY - (0 * 16)), NPC.frame, drawColor, 0, new Vector2(Texture.Width * 0f, Texture.Height * 0f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.defDefense == 9999)
            {
                SpriteEffects effects = SpriteEffects.None;
                if (NPC.spriteDirection == 1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                Vector2 vectorFrame = new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2);
                Vector2 position = new Vector2(NPC.Center.X, NPC.Center.Y) - (Main.screenPosition - new Vector2(0, -65));
                var a = Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/FrozenAssaulter/FrozenAssaulter_Shield");
                position -= new Vector2(a.Width(), a.Height() / Main.npcFrameCount[NPC.type]) * 1f / 2f;
                position += vectorFrame + new Vector2(0f, 4f + NPC.gfxOffY);
                Color color = new Color(147, 219, 252,150);
                Main.spriteBatch.Draw((Texture2D)a, position, null, color, 0, vectorFrame, 1f, effects, 0f);    
            }
        }
        public void ChoiseFrame(int frame, int frameHeight)
        {
            NPC.frame.Y = frame * frameHeight;
            NPC.frameCounter++;
        } 
    }
}
	

    
