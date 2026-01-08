using FargowiltasSouls.Common.Utilities;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.DamageClasses;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;
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
        public List<int> MeleeProjFix =
        [
            ProjectileID.NightsEdge,
            ProjectileID.TrueNightsEdge,
            ProjectileID.Excalibur,
            ProjectileID.TrueExcalibur
        ];
        public static List<int> Spears =
        [
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
        ];
        public static List<int> MeleeWeaponEffectsProjectiles = [];
        public bool isHomming; 
        public Item ItemOwner;
        public NPC NpcOwner;
        public Player PlayerOwner;
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

            Item heldItem = Main.LocalPlayer.HeldItem;
            if (heldItem != null && !heldItem.IsAir)
            {
                float pierceStrikeChance = Main.LocalPlayer.GetModPlayer<StatPlayer>().GetProjectilePenetrationChance(DamageClass.Ranged);
                if (pierceStrikeChance > 0 && projectile.CountsAsClass(DamageClass.Ranged) && heldItem.useAmmo == AmmoID.Bullet && Main.rand.NextBool((int)(100/ pierceStrikeChance)))
                {
                    int bonus = (int)Main.LocalPlayer.GetModPlayer<StatPlayer>().GetProjectilePenetration(DamageClass.Ranged);
                    projectile.penetrate += bonus;
                }
            }
        }
        public static void FiilMeleeWeaponEffectsList()
        {
             ListUtils.AddSecure(ref MeleeWeaponEffectsProjectiles, CallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "ExampleSlash"));
             ListUtils.AddSecure(ref MeleeWeaponEffectsProjectiles, CallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "ExampleSlashingEffect"));
             ListUtils.AddSecure(ref MeleeWeaponEffectsProjectiles, CallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "HeroSlash2"));
             ListUtils.AddSecure(ref MeleeWeaponEffectsProjectiles, CallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "HeroSlashMethod"));
             ListUtils.AddSecure(ref MeleeWeaponEffectsProjectiles, CallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.MeleeWeaponEffects, "ExampleSlashWeapon"));
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
            if(projectile.bobber)
            {
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 10;
            }

            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.owner == Main.myPlayer)
            {
                PlayerOwner = Main.player[projectile.owner];
                ItemOwner = Main.player[projectile.owner].HeldItem;

                ReaperPlayer ReaperPlayer = PlayerOwner.GetModPlayer<ReaperPlayer>();
                RemnantPlayer RemnantPlayer = PlayerOwner.GetModPlayer<RemnantPlayer>();
                if (Reaper.ReaperMode && (projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.MeleeNoSpeed)) && projectile.friendly && ReaperPlayer.ChaliceOn)
                {
                    float newWidth = projectile.width * 1.5f;
                    float newHeight = projectile.height * 1.5f;

                    projectile.width = (int)newWidth;
                    projectile.height = (int)newHeight;
                    projectile.scale += 1.5f;

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
                if (projectile.bobber)
                {
                    if (projectile.damage <= 0)
                    {
                        projectile.damage = new Item(Main.LocalPlayer.HeldItem.type).damage;
                    }
                }
            }
        }
        int fireballTimmer = (int)Utils1.FormatTimeToTick(0, 0, 0, 10);


        public int AttackCounter = 0;
        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];

            if (projectile.type == 85 && projectile.ai[0] == 2)
            {
                GetAlpha(projectile, Color.Green);
            }
            else if(projectile.type == ProjectileID.CrystalPulse2)
            {
                HommingProjectile(projectile,Range:800f,speed: 5f);
            }
            else if (projectile.type == ProjectileID.ClingerStaff)
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
            else if(projectile.type == ProjectileID.TitaniumStormShard)
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
            else if (projectile.type == ProjectileID.FrostHydra)
            {
                if (projectile.ai[0] > 0)
                {
                    float maxTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
                    if (AttackCounter >= maxTimer)
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, new Vector2(0, -3), ProjectileID.ClusterSnowmanRocketI, projectile.damage, 1, projectile.owner);
                        AttackCounter = 0;
                    }
                    else
                    {
                        AttackCounter++;
                    }
                }
            }
            else if(projectile.type == ProjectileID.FlyingImp)
            {
                float maxTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);

                if (AttackCounter >= maxTimer)
                {

                    NPC target = projectile.FindTargetWithinRange(200, true);
                    
                    if (target != null)
                    {
                        Vector2 velocity = target.Center - projectile.Center;

                        for (int i = 0; i < 6; i++)
                        {
                            var p = Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, velocity.RotatedBy(MathHelper.ToRadians(-i *10)), ProjectileID.ImpFireball, projectile.damage / 2, 1, projectile.owner);
                            Main.projectile[p].usesLocalNPCImmunity = true;
                            Main.projectile[p].localNPCHitCooldown = 10;
                            Main.projectile[p].scale = 0.5f;
                        }
                        AttackCounter = 0;
                    }
                }
                else
                {
                    AttackCounter++;
                }
            }
            else if(projectile.type == ProjectileID.Excalibur)
            {
                if(projectile.localAI[2] >= 0 && Main.projectile[(int)projectile.localAI[2]] != null && Main.projectile[(int)projectile.localAI[2]].type == ModContent.ProjectileType<PlayerClone>())
                {
                    projectile.Center = Main.projectile[(int)projectile.localAI[2]].Center;
                }
            }
            if (isHomming)
            {

                HommingProjectile(projectile, Range: 800f, speed: ItemOwner.shootSpeed, rotationBonus: 90f);
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
            if (projectile.Opacity == 0)
            {
                lightColor = Color.Transparent;
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
        public void HommingProjectile(Projectile projectile, float Range = 100, float speed = 1,float rotationBonus = 0)
        {   
            NPC closestNPC = FindClosestNPC(Range,projectile);
            if (closestNPC == null)
                return;
            projectile.velocity = (closestNPC.Center - projectile.Center).SafeNormalize(Vector2.Zero) * speed;
            projectile.rotation = projectile.velocity.ToRotation() + rotationBonus;
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
        public override void PostAI(Projectile projectile)
        {
            if (projectile.type != ProjectileID.PurificationPowder || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == NPCID.Wraith || Main.npc[i].type == NPCID.Ghost) && projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
                {
                    Main.npc[i].Transform(ModContent.NPCType<MinerSoul>());
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