using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie.Override.Fargos
{
    [JITWhenModsEnabled("FargowiltasSouls")]
    public class OverrideFargosProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public int miscCounter;
        public static DamageClass ProjdamageClass = null;
        public float miscCounterNormalized => miscCounter / 300f;



        public override void SetDefaults(Projectile projectile)
        {


        }

        public void UpdateMiscCounter()
        {

        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {

            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {



        }
        Color color;
        Dictionary<int, Color> GlowRingList = new Dictionary<int, Color>()
                {
                    { -12, new Color(0, 0, 255, 0)},
                    { -11, new Color(0, 255, 0, 0)},
                    { -10, new Color(0, 255, 255, 0)},
                    { -9, new Color(255, 255, 0, 0)},
                    { -8, new Color(255, 127, 40, 0)},
                    { -7, new Color(255, 0, 0, 0)},
                    { 4, new Color(51, 255, 191, 0)}
                    //{222,new Color(255, 255, 100, 0)},
                    //{DesertID,Color.SandyBrown },
                    //{FrozenID,Color.White },
                    //{InfernalTyrantID,Color.Red }
                };
        public override void AI(Projectile projectile)
        {
            if (projectile.type == ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "GlowRing"))
            {
                NPC npc = FargoSoulsUtil.NPCExists(projectile.ai[0]);
                if (npc != null)
                {
                    projectile.Center = npc.Center;
                }
                float scale = 12f;
                int maxTime = 30;
                bool customScaleAlpha = false;


                if ((int)projectile.ai[1] == -25)
                {
                    Player player = Main.player[(int)projectile.ai[2]];
                    if (player != null && player.active && !player.dead && !player.ghost)
                    {
                        customScaleAlpha = true;
                        projectile.Center = player.Center;
                        maxTime = 40;
                        color = Color.LightGoldenrodYellow;
                        projectile.scale = 875f / (float)TextureAssets.Projectile[projectile.type].Value.Height;
                        projectile.alpha = 100 + (int)(155f * projectile.localAI[0] / (float)maxTime);
                        color.A = 0;
                    }
                    else
                    {
                        projectile.Kill();
                    }
                }
                else if ((int)projectile.ai[1] == -24)
                {
                    maxTime = 60;
                    float modifier = projectile.localAI[0] / (float)maxTime;
                    scale = 1f + 50f * modifier;
                    color = Color.Blue;
                    projectile.alpha = (int)(255f * (1f - modifier));
                    color.A = 0;
                }
                else if ((int)projectile.ai[1] == -23)
                {
                    customScaleAlpha = true;
                    maxTime = 90;
                    float modifier2 = projectile.localAI[0] / (float)maxTime;
                    color = new Color(51, 255, 191) * modifier2;
                    projectile.alpha = (int)(255f * (1f - modifier2));
                    projectile.scale = 27f * (1f - modifier2);
                }
                else if ((int)projectile.ai[1] == -22)
                {
                    customScaleAlpha = true;
                    maxTime = 645;
                    if (npc != null && npc.type == 114 && (npc.GetGlobalNPC<WallofFleshEye>().HasTelegraphedNormalLasers || Main.netMode == 1))
                    {
                        projectile.rotation = npc.rotation + ((npc.direction > 0) ? 0f : ((float)Math.PI));
                        projectile.velocity = projectile.rotation.ToRotationVector2();
                        projectile.Center = npc.Center + (float)(npc.width - 52) * Vector2.UnitX.RotatedBy(projectile.rotation);
                        if (projectile.localAI[0] < npc.localAI[1])
                        {
                            projectile.localAI[0] = (int)npc.localAI[1];
                        }
                        float modifier3 = (float)Math.Cos(Math.PI / 2.0 / (double)maxTime * (double)projectile.localAI[0]);
                        color = new Color(255, 0, 255, 100) * (1f - modifier3);
                        projectile.alpha = (int)(255f * modifier3);
                        projectile.scale = 18f * modifier3;
                    }
                    projectile.Kill();
                }
                else if ((int)projectile.ai[1] == -21)
                {
                    scale = 4f;
                    maxTime = 60;
                }
                else if ((int)projectile.ai[1] == -20)
                {
                    customScaleAlpha = true;
                    maxTime = 200;
                    float modifier4 = projectile.localAI[0] / (float)maxTime;
                    color = new Color(51, 255, 191) * modifier4;
                    projectile.alpha = (int)(255f * (1f - modifier4));
                    projectile.scale = 18f * (1f - modifier4);
                }
                else if ((int)projectile.ai[1] == -19)
                {
                    color = Color.Yellow;
                    color.A = 0;
                    scale = 18f;
                }
                else if ((int)projectile.ai[1] == -18)
                {
                    scale = 36f;
                    maxTime = 120;
                }
                else if ((int)projectile.ai[1] == -17 || (int)projectile.ai[1] == -15 || (int)projectile.ai[1] == -14)
                {
                    if ((int)projectile.ai[1] == -17) scale = 6f;
                    if ((int)projectile.ai[1] == -15) scale = 18f;
                    if ((int)projectile.ai[1] == -14) scale = 24f;
                    color = new Color(255, 51, 153, 0);
                }
                else if ((int)projectile.ai[1] == -13)
                {
                    color = new Color(93, 255, 241, 0);
                    scale = 6f;
                    maxTime = 15;
                }
                else if ((int)projectile.ai[1] == -6)
                {
                    color = new Color(255, 255, 0, 0);
                    scale = 18f;
                }
                else if ((int)projectile.ai[1] == -5)
                {
                    color = new Color(200, 0, 255, 0);
                    scale = 18f;
                }
                else if ((int)projectile.ai[1] == -4)
                {
                    color = new Color(255, 255, 0, 0);
                    scale = 18f;
                    maxTime = 60;
                }
                else if ((int)projectile.ai[1] == -3)
                {
                    color = new Color(255, 100, 0, 0);
                    scale = 18f;
                    maxTime = 60;
                }
                else if ((int)projectile.ai[1] == -2)
                {
                    color = new Color(51, 255, 191, 0);
                    scale = 18f;
                }
                else if ((int)projectile.ai[1] == -1)
                {
                    color = new Color(200, 0, 200, 0);
                    maxTime = 60;
                }

                else if ((int)projectile.ai[1] == 114)
                {
                    color = new Color(93, 255, 241, 0);
                    scale = 12f;
                    maxTime = 30;
                }
                else if ((int)projectile.ai[1] == 125)
                {
                    color = new Color(255, 0, 0, 0);
                    scale = 24f;
                    maxTime = 60;
                }
                else if ((int)projectile.ai[1] == 128
                    || (int)projectile.ai[1] == 128
                    || (int)projectile.ai[1] == 128
                    || (int)projectile.ai[1] == 128)
                {
                    color = new Color(51, 255, 191, 0);
                    scale = 12f;
                    maxTime = 30;
                }
                else if ((int)projectile.ai[1] == 439)
                {
                    color = new Color(255, 127, 40, 0);
                }
                else if ((int)projectile.ai[1] == 657)
                {
                    color = Color.HotPink;
                    color.A = 200;
                    scale = 6f;
                    maxTime = 60;
                    if (projectile.localAI[0] > maxTime * 0.25f && NPC.AnyNPCs(ModContent.NPCType<GelatinSubject>()))
                    {
                        projectile.localAI[0] = maxTime * 0.25f;
                    }
                    if (npc != null)
                    {
                        projectile.Center = npc.Bottom + npc.height / 2 * -Vector2.UnitY.RotatedBy(npc.rotation);
                    }
                }
                else if ((int)projectile.ai[1] == 396
                || (int)projectile.ai[1] == 397
                || (int)projectile.ai[1] == 398)
                {
                    color = new Color(51, 255, 191, 0);
                    scale = 12f;
                    maxTime = 60;
                }
                else if ((int)projectile.ai[1] == 657)
                {
                    color = Color.LightSkyBlue;
                    color.A = 0;
                    scale = 9f;
                    maxTime = 30;
                    if (npc != null)
                    {
                        projectile.Center = ((npc.direction < 0) ? npc.TopLeft : npc.TopRight);
                    }
                }
                foreach (var element in GlowRingList)
                {
                    if ((int)projectile.ai[1] == element.Key)
                    {
                        color = element.Value;
                        maxTime = 45;
                    };
                }

                if ((projectile.localAI[0] += 1f) > maxTime)
                {
                    projectile.Kill();
                    return;
                }
                if (!customScaleAlpha)
                {
                    projectile.scale = scale * (float)Math.Sin(Math.PI / 2.0 * projectile.localAI[0] / maxTime);
                    projectile.alpha = (int)(255f * projectile.localAI[0] / maxTime);
                }
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                }
            }



            else base.AI(projectile);
        }
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
         
            return lightColor;
        }
        public bool DeleteVanilla = false;
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            
        }
    }
}