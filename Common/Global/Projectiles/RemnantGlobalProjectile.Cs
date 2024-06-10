using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using RemnantOfTheAncientsMod.Common.Global.DamageClasses;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class RemnantGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public int miscCounter;
        public static DamageClass ProjdamageClass = null;
        public float miscCounterNormalized => miscCounter / 300f;

        bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
        public List<int> MeleeProjFix = new List<int>()
        {
            ProjectileID.NightsEdge,
            ProjectileID.TrueNightsEdge,
            ProjectileID.Excalibur,
            ProjectileID.TrueExcalibur
        };
        public static List<int> Spears = new List<int>()
        {
            ProjectileID.Spear,
            ProjectileID.AdamantiteGlaive,
            ProjectileID.CobaltNaginata,
            ProjectileID.MythrilHalberd,
            ProjectileID.OrichalcumHalberd,
            ProjectileID.PalladiumPike,
            ProjectileID.TitaniumTrident,
            ProjectileID.Trident,
            ProjectileID.ObsidianSwordfish,
            ProjectileID.Swordfish,
            ProjectileID.ChlorophytePartisan
        };
        public static List<int> MeleeWeaponEffectsProjectiles = new List<int>()
        {
            
        };
        public override void SetDefaults(Projectile projectile)
        {  
            if (WeaponConf)
            {
                if (projectile.type == ProjectileID.BabySlime)
                {
                    projectile.minionSlots = 0.5f;
                }
          
            }
            if (projectile.bobber)
            {
                projectile.penetrate = (Main.LocalPlayer.HeldItem.rare + 2);
                projectile.DamageType = ModContent.GetInstance<FisherDamageClass>();
                projectile.damage = Main.LocalPlayer.HeldItem.damage + Main.LocalPlayer.fishingSkill;
                projectile.friendly = true;
                projectile.hostile = !projectile.friendly;
            }

            if (RemnantOfTheAncientsMod.MeleeWeaponEffects != null)
            {
                FiilMeleeWeaponEffectsList();
                if (MeleeWeaponEffectsProjectiles.Contains(projectile.type))
                {
                    //projectile.DamageType = DamageClass.Melee;
                    projectile.friendly = true;
                }
            }
            
        }
        public static void FiilMeleeWeaponEffectsList()
        {
            Utils1.AddSecure(MeleeWeaponEffectsProjectiles, ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "ExampleSlash"));
            Utils1.AddSecure(MeleeWeaponEffectsProjectiles, ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "ExampleSlashingEffect"));
            Utils1.AddSecure(MeleeWeaponEffectsProjectiles, ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "HeroSlash2"));
            Utils1.AddSecure(MeleeWeaponEffectsProjectiles, ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "HeroSlashMethod"));
            Utils1.AddSecure(MeleeWeaponEffectsProjectiles, ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "ExampleSlashWeapon"));
        }
        public void UpdateMiscCounter()
        {
            miscCounter++;
            if (miscCounter >= 300)
            {
                miscCounter = 0;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];
            if (WeaponConf)
            {
                if (Main.rand.NextBool(4) && projectile.type == ProjectileID.FlinxMinion)
                {
                    target.AddBuff(BuffID.Confused, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));
                }
                if (projectile.type == ProjectileID.ThunderStaffShot)
                {
                    target.AddBuff(BuffID.Electrified, 1200);
                }
                if (projectile.type == ProjectileID.SpiritFlame)
                {
                    target.AddBuff(BuffID.ShadowFlame, 1200);
                }
                if (projectile.type == ProjectileID.MedusaHeadRay && target.life <= 100)
                {
                    target.AddBuff(BuffID.Stoned, 1200);
                }
                if (projectile.type == ProjectileID.SoulDrain)
                {
                    if (player.statLife <= player.statLifeMax2)
                    {
                        player.statLife++;
                        if (target.life >= 0)
                        {
                            target.value *= 2;
                        }
                    }
                }
                if (projectile.type == 85 && projectile.ai[0] == 2)
                {
                    target.AddBuff(BuffID.CursedInferno, 1200);
                }
                if (projectile.type == ProjectileID.SpiderEgg)
                {
                    target.AddBuff(BuffID.Venom, 1200);
                }
                if (projectile.type == ProjectileID.BoulderStaffOfEarth)
                {
                    if (target.type != NPCID.TargetDummy)
                    {
                        projectile.knockBack = target.knockBackResist + 3;
                        target.velocity.X -= target.velocity.X > 0 ? 1 * 16 : -1 * 16;
                    }
                }
                if (projectile.type == ProjectileID.OneEyedPirate
                || projectile.type == ProjectileID.SoulscourgePirate
                || projectile.type == ProjectileID.PirateCaptain)
                {
                    if (target.life >= 0)
                    {
                        target.value *= 2;
                    }
                }


                if (projectile.minion && projectile.owner == Main.myPlayer)
                {
                    List<int> list = player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict;

                    if (list.Count > 0)
                    {
                        foreach (int debuff in list)
                        {
                            target.AddBuff(debuff, (int)Utils1.FormatTimeToTick(0, 0, 0, 10));
                        }
                    }
                }
                if (projectile.type == ProjectileID.Seed)
                {
                    if (player.HeldItem.type == ItemID.Blowgun)
                    {
                        target.AddBuff(BuffID.Poisoned, (int)Utils1.FormatTimeToTick(0, 0, 0, 3));
                    }
                }
                if(projectile.type == ProjectileID.NanoBullet)
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<NanoDrone>()] <= 2)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_None(), player.position, new Vector2(0, 2), ModContent.ProjectileType<NanoDrone>(), projectile.damage, 1, projectile.owner);
                        projectile.Kill();
                    }
                }
                if (projectile.type == ProjectileID.PewMaticHornShot)
                {
                    int MaxProj = 5;
                    projectile.penetrate = 1;
                    if (projectile.ai[1] == 2)
                    {
                        for (int i = 0; i < 360; i += 360/ MaxProj)
                        {
                            double angle = MathHelper.ToRadians(i);
                            Vector2 position = target.Center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 50;
                            Vector2 direction = -projectile.velocity.RotatedByRandom(angle);
                            Projectile a = Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, direction, ProjectileID.CopperCoin, projectile.damage / MaxProj, 1);
                            a.timeLeft = 10;
                        }
                    }
                    else if(projectile.ai[1] == 3)
                    {
                        target.AddBuff(BuffID.Slow, (int)Utils1.FormatTimeToTick(0, 0, 0, 3));
                    }
                    else if (projectile.ai[1] == 6)
                    {
                        player.Heal(1);        
                    }
                    else if (projectile.ai[1] == 17)
                    {
                        target.AddBuff(BuffID.Confused, (int)Utils1.FormatTimeToTick(0, 0, 0, 4));
                    }
                    else if (projectile.ai[1] == 18)
                    {
                        target.AddBuff(BuffID.Ichor, (int)Utils1.FormatTimeToTick(0, 0, 0, 1));
                    }
                    else if (projectile.ai[1] == 19)
                    {
                        player.Heal(3);
                    }
                    else if (projectile.ai[1] == 20)
                    {
                        target.AddBuff(BuffID.OnFire, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));
                    }
                    else if (projectile.ai[1] == 21)
                    {
                        target.AddBuff(BuffID.Slow, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));
                    }
                    else if (projectile.ai[1] == 22)
                    {
                        projectile.damage *= 2; 
                    }
                    else if (projectile.ai[1] == 23)
                    {
                        for (int i = 0; i < 360; i += 360 / MaxProj)
                        {
                            double angle = MathHelper.ToRadians(i);
                            Vector2 position = target.Center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 50;
                            Vector2 direction = -projectile.velocity.RotatedByRandom(angle);
                            Projectile a = Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, direction, ProjectileID.AmberBolt, projectile.damage / (MaxProj /2), 1);
                            a.timeLeft = 10;
                        }
                    }
                    if (projectile.ai[1] > 0) 
                    projectile.ai[1] = 0;
                }
            }

            if (projectile.minion || projectile.CountsAsClass(DamageClass.Summon))
            {
                if (projectile.owner == Main.myPlayer)
                {
                    int critChance = 100 - player.GetModPlayer<RemnantPlayer>().MinionCritChance;
                    if(critChance <= 0) critChance = 1;
                    if (critChance > 0 && critChance < 100)
                    {    
                        if (Main.rand.NextBool(critChance))
                        {     
                            hit.Crit = true;
                            damageDone *= 2;        
                        }
                    }
                }
            }
            if (projectile.bobber)
            {
                UpdateBobers(projectile);
            }

            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public void UpdateBobers(Projectile projectile)
        {
           // projectile.penetrate = (Main.LocalPlayer.HeldItem.rare + 2);
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            ReaperPlayer ReaperPlayer = Main.player[Main.myPlayer].GetModPlayer<ReaperPlayer>();
            RemnantPlayer RemnantPlayer = Main.player[Main.myPlayer].GetModPlayer<RemnantPlayer>();
            if (DificultyUtils.ReaperMode && (projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.MeleeNoSpeed)) && projectile.friendly && ReaperPlayer.ChaliceOn )
            {
                projectile.width *= (int)1.5f;
                projectile.height *= (int)1.5f;
                projectile.scale *= 2.5f;
                if (Spears.Contains(projectile.type))
                {
                    projectile.velocity *= 3;
                }
                if (RemnantOfTheAncientsMod.MeleeWeaponEffects != null)
                {
                    if (MeleeWeaponEffectsProjectiles.Contains(projectile.type))
                    {
                        projectile.localAI[0] = Main.player[projectile.owner].GetWeaponDamage(Main.player[projectile.owner].HeldItem);
                        projectile.localAI[1] = Main.player[projectile.owner].HeldItem.scale;
                    }
                }
                //if (MeleeProjFix.Contains(projectile.type))
                //{
                //    projectile.ai[2] *= 2.5f;
                //}
            }
            if (projectile.hostile)
            {
                if (RemnantPlayer.EnemyProjectilesScaleBouns != 1)
                {
                    if (RemnantPlayer.EnemyProjectilesScaleBouns <= 0) RemnantPlayer.EnemyProjectilesScaleBouns = 0.1f;
                    projectile.scale *= RemnantPlayer.EnemyProjectilesScaleBouns;
                }
                if (RemnantPlayer.EnemyProjectilesSpeedScaleBouns != 1)
                {
                    if (RemnantPlayer.EnemyProjectilesSpeedScaleBouns < 0) RemnantPlayer.EnemyProjectilesSpeedScaleBouns = 0.01f;
                    projectile.velocity *= RemnantPlayer.EnemyProjectilesSpeedScaleBouns;
                }
               
            }          
        }
        int fireballTimmer = (int)Utils1.FormatTimeToTick(0, 0, 0, 10);


        public int HydraMissileCounter = 0;
        public override void AI(Projectile projectile)
        {
           
            if (projectile.type == 85 && projectile.ai[0] == 2)
            {
                GetAlpha(projectile, Color.Green);
            }
            if(projectile.type == ProjectileID.CrystalPulse2)
            {
                HommingProjectile(800f, 5f, projectile);
            }
            if (projectile.type == ProjectileID.ClingerStaff)
            {
                
                if(fireballTimmer++ >= (int)Utils1.FormatTimeToTick(0, 0, 0, 2))
                {
                    fireballTimmer = 0;
                }
                if (fireballTimmer == 0)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, new Vector2(-1f, 0.5f), ProjectileID.CursedFlameFriendly, (int)(projectile.damage * 0.7f), 1f, projectile.owner);
                    Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, new Vector2(1f, 0.5f), ProjectileID.CursedFlameFriendly, (int)(projectile.damage * 0.7f), 1f, projectile.owner);
                }
            }
            if(projectile.type == ProjectileID.TitaniumStormShard)
            {
                if (projectile.hostile)
                {
                    NPC npc = Main.npc[projectile.owner];
                    if (!npc.active || npc.life <= 0 || !npc.HasBuff(BuffID.TitaniumStorm))
                    {
                        projectile.Kill();
                        return;
                    }
                    if (projectile.frameCounter == 0)
                    {
                        projectile.frameCounter = 1;
                        projectile.frame = Main.rand.Next(12);
                        projectile.rotation = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                    }
                    projectile.rotation += (float)Math.PI / 200f;
                    AI_GetMyGroupIndexAndFillBlackList(null, out var index, out var totalIndexesInGroup);
                    float f = (index / totalIndexesInGroup + miscCounterNormalized * 6f) * ((float)Math.PI * 2f);
                    float num = 24f + totalIndexesInGroup * 6f;
                    Vector2 vector = npc.position - npc.oldPosition;
                    projectile.Center += vector;
                    Vector2 vector2 = f.ToRotationVector2();
                    projectile.localAI[0] = vector2.Y;
                    Vector2 value = npc.Center + vector2 * new Vector2(1f, 0.05f) * num;
                    projectile.Center = Vector2.Lerp(projectile.Center, value, 1f);//0.3
                }
                else
                {
                    base.AI(projectile);
                }

            }
            if (projectile.type == ProjectileID.FrostHydra)
            {
                if (projectile.ai[0] > 0)
                {
                    float maxTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
                    if (HydraMissileCounter >= maxTimer)
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, new Vector2(0, -3), ProjectileID.ClusterSnowmanRocketI, projectile.damage, 1, projectile.owner);
                        HydraMissileCounter = 0;
                    }
                    else
                    {
                        HydraMissileCounter++;
                    }
                }
            }
            if (projectile.scale > 1)
            {
                if (projectile.type == ProjectileID.AmethystBolt)
                {
                    OverrideParticles(projectile, DustID.GemAmethyst);
                }
                else if (projectile.type == ProjectileID.TopazBolt)
                {
                    OverrideParticles(projectile, DustID.GemTopaz);
                }
                else if (projectile.type == ProjectileID.EmeraldBolt)
                {
                    OverrideParticles(projectile, DustID.GemEmerald);
                }
                else if (projectile.type == ProjectileID.SapphireBolt)
                {
                    OverrideParticles(projectile, DustID.GemSapphire);
                }
                else if (projectile.type == ProjectileID.RubyBolt)
                {
                    OverrideParticles(projectile, DustID.GemRuby);
                }
                else if (projectile.type == ProjectileID.DiamondBolt)
                {
                    OverrideParticles(projectile, DustID.GemDiamond);
                }
                else if (projectile.type == ProjectileID.AmberBolt)
                {
                    OverrideParticles(projectile, DustID.GemAmber);
                }
            } 
            
            Update();
            base.AI(projectile);
        }
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (projectile.type == 85 && projectile.ai[0] == 2)
            {
                return Color.Green;
            }
            return lightColor;
        }
        public bool DeleteVanilla = false;
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if(projectile.type == ProjectileID.CrystalStorm)
            {
                for(int i = 0; i < 5; i++) 
                {
                    Vector2 velocity = new Vector2(new Random().Next(-2, 2),new Random().Next(-2, 2));
                    Projectile.NewProjectileDirect(Projectile.GetSource_None(), projectile.Center + (velocity/2)*16, velocity, ProjectileID.CrystalShard, projectile.damage, 0,projectile.owner);
                }
            }
            if(projectile.type == ProjectileID.Spark)
            {
                if (projectile.localAI[2] == 27)
                {
                    float numberProjectiles = 5;
                    float rotation = MathHelper.ToRadians(4);
                    projectile.position += Vector2.Normalize(projectile.velocity) * 45f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = projectile.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                       var p = Projectile.NewProjectileDirect(projectile.GetSource_Death(), projectile.position, perturbedSpeed * 5, ModContent.ProjectileType<LeafFriendlyClone>(), projectile.damage + 10, projectile.knockBack, projectile.owner);
                        p.tileCollide = false;
                        p.timeLeft = 300;
                    }
                }
            }
            if(projectile.type == ProjectileID.SnowBallFriendly)
            {
                if (projectile.ai[2] > 0)
                {
                    var a = Projectile.NewProjectile(projectile.GetSource_Death(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<FrozenNovaExplosion>(), projectile.damage / 4, 40, projectile.owner);
                    Main.projectile[a].scale = projectile.ai[2] -1;
                    Main.projectile[a].width *= (int)projectile.ai[2];
                    Main.projectile[a].height *= (int)projectile.ai[2];
                    Main.projectile[a].ai[2] = (int)projectile.ai[1];
                }
            }
            if (!DeleteVanilla)
            {
                base.OnKill(projectile, timeLeft);
            }
        }
        public void HommingProjectile(float Range,float speed, Projectile projectile)
        {   
            NPC closestNPC = FindClosestNPC(Range,projectile);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            projectile.velocity = (closestNPC.Center - projectile.Center).SafeNormalize(Vector2.Zero) * speed;
            projectile.rotation = projectile.velocity.ToRotation();
        }
        public NPC FindClosestNPC(float maxDetectDistance, Projectile projectile)
        {
            NPC closestNPC = null;
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                
                if (target.CanBeChasedBy())
                {
                  
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
        private void AI_GetMyGroupIndexAndFillBlackList(List<int> blackListedTargets, out int index, out int totalIndexesInGroup)
        {
            index = 0;
            totalIndexesInGroup = 0;
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && (projectile.type != 759 || projectile.frame == Main.projFrames[projectile.type] - 1))
                {
                    if (projectile.whoAmI > i)
                    {
                        index++;
                    }
                    totalIndexesInGroup++;
                }
            }
        }
        public void OverrideParticles(Projectile porjectile,int DustId) 
        {
            Dust.NewDust(porjectile.position, porjectile.width, porjectile.height, DustId, porjectile.velocity.X * -1/6, porjectile.velocity.Y / 6, 0, default, porjectile.scale);
        }
        public void Update()
        {
            UpdateMiscCounter();
        }
        
    }
}