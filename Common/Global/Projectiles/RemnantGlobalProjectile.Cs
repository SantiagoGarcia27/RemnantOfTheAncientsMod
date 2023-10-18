using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class RemnantGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public int miscCounter;
        public float miscCounterNormalized => miscCounter / 300f;

        bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
        public override void SetDefaults(Projectile projectile)
        {  
            if (WeaponConf)
            {
                if (projectile.type == ProjectileID.BabySlime)
                {
                    projectile.minionSlots = 0.5f;
                }
            }
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
            if (WeaponConf)
            {
                if (Main.rand.NextBool(4) && projectile.type == ProjectileID.FlinxMinion)
                {
                    target.AddBuff(BuffID.Confused, Utils1.FormatTime(0, 0, 0, 5));
                }
            }
            if(projectile.type == 85 && projectile.ai[0] == 2)
            {
                target.AddBuff(BuffID.CursedInferno, 1200);
            }
            if(projectile.type == ProjectileID.ThunderStaffShot)
            {
                target.AddBuff(BuffID.Electrified, 1200);
            }
            if(projectile.type == ProjectileID.SpiritFlame)
            {
                target.AddBuff(BuffID.ShadowFlame, 1200);
            }
            if(projectile.type == ProjectileID.MedusaHeadRay && target.life <= 100)
            {
                target.AddBuff(BuffID.Stoned, 1200);
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Reaper.ReaperMode && projectile.CountsAsClass(DamageClass.Melee) && projectile.friendly)
            {
                projectile.width *= (int)1.5f;
                projectile.height *= (int)1.5f;
                projectile.scale *= 2.5f;
            }
        }
        int fireballTimmer = Utils1.FormatTime(0, 0, 0, 10);
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
                
                if(fireballTimmer++ >= Utils1.FormatTime(0, 0, 0, 2))
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
                    float f = (index / (float)totalIndexesInGroup + miscCounterNormalized * 6f) * ((float)Math.PI * 2f);
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
            base.OnKill(projectile, timeLeft);
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