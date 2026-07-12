using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs
{
    public class OverrideBuffsToNpc : GlobalNPC
    {
        int beetleLevel = 0;
        int solarShieldLevel = 0;

        Dictionary<int, List<int>> buffTiers = [];
        
        public override void AI(NPC npc)
        {
            #region beetle
            buffTiers.TryAdd(BuffID.BeetleEndurance1, [BuffID.BeetleEndurance1, BuffID.BeetleEndurance2, BuffID.BeetleEndurance3]);
            buffTiers.TryAdd(BuffID.SolarShield1, [BuffID.SolarShield1, BuffID.SolarShield2, BuffID.SolarShield3]);

            beetleLevel = CheckLevel(npc, BuffID.BeetleEndurance1);
            solarShieldLevel = CheckLevel(npc, BuffID.SolarShield1);

            Update(npc);
            #endregion

            base.AI(npc);
        }
        int CheckLevel(NPC npc, int buffId)
        {
            for (int i = 0; i < buffTiers[buffId].Count; i++)
            {
                if (npc.HasBuff(buffTiers[buffId][i]))
                    return i + 1;
            }
            return 0;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(BuffID.RapidHealing))
            {
                npc.lifeRegen += 4;
            }


            base.UpdateLifeRegen(npc, ref damage);
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (beetleLevel > 0)
            {
                int Tier1Id = 95;
                if (beetleLevel > 0)
                {
                    int index = -1;
                    index = npc.FindBuffIndex(Tier1Id + (beetleLevel - 1));
                    if (index > -1)
                    {
                        if (beetleLevel > 1)
                            npc.AddBuff(95 + (beetleLevel - 2), Utils1.FormatTimeToTick(Day: 1));
                        npc.DelBuff(index);
                    }
                    beetleLevel--;
                }
            }

            base.OnHitByProjectile(npc, projectile, hit, damageDone);
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (beetleLevel > 0)
            {
                int Tier1Id = 95;
                if (beetleLevel > 0)
                {
                    int index = -1;
                    index = npc.FindBuffIndex(Tier1Id + (beetleLevel - 1));
                    if (index > -1)
                    {
                        if (beetleLevel > 1)
                            npc.AddBuff(95 + beetleLevel - 1, Utils1.FormatTimeToTick(Day: 1));
                        npc.DelBuff(index);
                    }
                    beetleLevel--;
                }
            }

            base.OnHitByItem(npc, player, item, hit, damageDone);
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (solarShieldLevel > 0)
            {
                modifiers.SourceDamage.Base = target.statLifeMax2 / (5- solarShieldLevel);
            }
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (solarShieldLevel > 0)
            {
                hurtInfo.Damage = 100 * target.statDefense;
                int p = Projectile.NewProjectile(npc.GetSource_FromAI(), target.Center, Vector2.Zero,ProjectileID.SolarCounter, 100 * target.statDefense, 10, Main.myPlayer);
                Main.projectile[p].hostile = true;
                Main.projectile[p].friendly = false;
                Main.projectile[p].ArmorPenetration = 100;

                DecreaseCount(npc, BuffID.SolarShield1);
            }
            base.OnHitPlayer(npc, target, hurtInfo);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawBeetleEffect(npc, spriteBatch);
            DrawSolarShield(npc, spriteBatch);
            base.PostDraw(npc, spriteBatch, screenPos, drawColor);
        }
        void Update(NPC npc)
        {
            UpdateBeetle(npc);
            UpdateSolar(npc);

            UpdateMiscCounter();
        }

        int miscCounter = 0;
        public void UpdateMiscCounter()
        {
            miscCounter++;
            if (miscCounter >= 300)
            {
                miscCounter = 0;
            }
        }
        int UpdateCount(NPC npc,int buffId)
        {
            int index = -1;
            
            for (int i = 0; i < buffTiers[buffId].Count; i++)
            {
                if (npc.HasBuff(buffTiers[buffId][i]))
                {
                    //index = npc.FindBuffIndex(buffTiers[buffId][i]);
                    //if (index > -1 && i + 1 < buffTiers[buffId].Count)
                    //{
                    //    npc.DelBuff(index);
                    //    npc.AddBuff(buffTiers[buffId][i+1], Utils1.FormatTimeToTick(Day: 1));
                    //}
                    return i + 1;
                }
            }
            return 0;
        }
        void DecreaseCount(NPC npc, int buffId)
        {
            int index = -1;
            for (int i = 0; i < buffTiers[buffId].Count; i++)
            {
                if (npc.HasBuff(buffTiers[buffId][i]))
                {
                    index = npc.FindBuffIndex(buffTiers[buffId][i]);
                    if (index > -1)
                    {
                        if (i > 0)
                        {
                            npc.AddBuff(buffTiers[buffId][i - 1], Utils1.FormatTimeToTick(Day: 1));
                        }
                        npc.DelBuff(index);
                    }
                }
            }
        }

        List<DrawData> DrawDataCache = [];

        public override bool InstancePerEntity => true;

        #region Solar

        Vector2[] solarShieldVel = new Vector2[3];
        Vector2[] solarShieldPos = new Vector2[3];
        int solarCounter = 0;
        public void UpdateSolar(NPC npc)
        {
            solarShieldLevel = UpdateCount(npc, BuffID.SolarShield1);

            solarCounter++;
            int num9 = 180;
            if (solarCounter >= num9)
            {       
                if (solarShieldLevel < 3 && solarShieldLevel > 0)
                {
                    for (int num11 = 0; num11 < 16; num11++)
                    {
                        Dust obj = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 100)];
                        obj.noGravity = true;
                        obj.scale = 1.7f;
                        obj.fadeIn = 0.5f;
                        obj.velocity *= 5f;
                       // obj.shader = GameShaders.Armor.GetSecondaryShader(ArmorSetDye(), this);
                    }
                    solarCounter = 0;
                }
                else
                {
                    solarCounter = num9;
                }
            }
            for (int num12 = solarShieldLevel; num12 < 3; num12++)
            {
                solarShieldPos[num12] = Vector2.Zero;
            }
            for (int SolarLv = 0; SolarLv < solarShieldLevel; SolarLv++)
            {
                solarShieldPos[SolarLv] += solarShieldVel[SolarLv];
                Vector2 vector = (miscCounter / 100f * ((float)Math.PI * 2f) + SolarLv * ((float)Math.PI * 2f / solarShieldLevel)).ToRotationVector2() * 6f;
                vector.X = npc.direction * 20;
                solarShieldVel[SolarLv] = (vector - solarShieldPos[SolarLv]) * 0.2f;
            }
        }




        void DrawSolarShield(NPC npc, SpriteBatch spriteBatch)
        {
            SpriteEffects effect = npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (DrawDataCache == null) DrawDataCache = [];
            if (solarShieldLevel <= 0)
            {
                return;
            }
            if (solarShieldLevel > 0)
            {
                Texture2D value = TextureAssets.Extra[61 + solarShieldLevel - 1].Value;
                Color color = new Color(255, 255, 255, 127);
                float rotation = (solarShieldPos[0] * new Vector2(1f, 0.5f)).ToRotation();
                if (npc.direction == -1)
                {
                    rotation += (float)Math.PI;
                }
                rotation += (float)Math.PI / 50f * npc.direction;
                DrawData drawData = new(value, new Vector2((int)(npc.Center.X - Main.screenPosition.X + npc.width / 2), (int)(npc.Center.Y - Main.screenPosition.Y + npc.height / 2)) + solarShieldPos[0] - new Vector2(0,16f), null, color, rotation, value.Size() / 2f, npc.scale, effect, 1);

                spriteBatch.Draw(drawData.texture, drawData.position, drawData.sourceRect, drawData.color, drawData.rotation, drawData.origin, drawData.scale, drawData.effect, 1f);
            }
        }
        #endregion

        #region Beetle

        int beetleFrame = 0;
        int beetleFrameCounter = 0;
        Vector2[] beetleVel = new Vector2[3];
        Vector2[] beetlePos = new Vector2[3];

        public void UpdateBeetle(NPC npc)
        {
            beetleLevel = UpdateCount(npc,BuffID.BeetleEndurance1);

            beetleFrameCounter++;
            if (beetleFrameCounter >= 1)
            {
                beetleFrameCounter = 0;
                beetleFrame++;
                if (beetleFrame > 2)
                {
                    beetleFrame = 0;
                }
            }
            for (int l = beetleLevel; l < 3; l++)
            {
                beetlePos[l].X = 0f;
                beetlePos[l].Y = 0f;
            }
            for (int m = 0; m < beetleLevel; m++)
            {
                beetlePos[m] += beetleVel[m];
                beetleVel[m].X += Main.rand.Next(-100, 101) * 0.005f;
                beetleVel[m].Y += Main.rand.Next(-100, 101) * 0.005f;
                float x = beetlePos[m].X;
                float y = beetlePos[m].Y;
                float num6 = (float)Math.Sqrt(x * x + y * y);
                if (num6 > 100f)
                {
                    num6 = 20f / num6;
                    x *= 0f - num6;
                    y *= 0f - num6;
                    int num7 = 10;
                    beetleVel[m].X = (beetleVel[m].X * (num7 - 1) + x) / num7;
                    beetleVel[m].Y = (beetleVel[m].Y * (num7 - 1) + y) / num7;
                }
                else if (num6 > 30f)
                {
                    num6 = 10f / num6;
                    x *= 0f - num6;
                    y *= 0f - num6;
                    int num8 = 20;
                    beetleVel[m].X = (beetleVel[m].X * (num8 - 1) + x) / num8;
                    beetleVel[m].Y = (beetleVel[m].Y * (num8 - 1) + y) / num8;
                }
                x = beetleVel[m].X;
                y = beetleVel[m].Y;
                num6 = (float)Math.Sqrt(x * x + y * y);
                if (num6 > 2f)
                {
                    beetleVel[m] *= 0.9f;
                }
                beetlePos[m] -= npc.velocity * 0.25f;
            }

        }
        void DrawBeetleEffect(NPC npc, SpriteBatch spriteBatch)
        {
            SpriteEffects effect = npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if(DrawDataCache == null) DrawDataCache = [];
            if (beetleLevel <= 0)
            {
                return;
            }
            for (int i = 0; i < beetleLevel; i++)
            {
                DrawData item;
                for (int j = 0; j < 5; j++)
                {
                    Vector2 vector = -beetleVel[i] * j;
                    item = new DrawData(TextureAssets.Beetle.Value, new Vector2((int)(npc.Center.X - Main.screenPosition.X + npc.width / 2), (int)(npc.Center.Y - Main.screenPosition.Y + npc.height / 2)) + beetlePos[i] + vector, new Rectangle(0, TextureAssets.Beetle.Height() / 3 * beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2), Color.White, 0f, new Vector2(TextureAssets.Beetle.Width() / 2, TextureAssets.Beetle.Height() / 6), 1f, effect);
                    DrawDataCache.Add(item);
                }
                item = new DrawData(TextureAssets.Beetle.Value, new Vector2((int)(npc.Center.X - Main.screenPosition.X + npc.width / 2), (int)(npc.Center.Y - Main.screenPosition.Y + npc.height / 2)) + beetlePos[i], new Rectangle(0, TextureAssets.Beetle.Height() / 3 * beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2), Color.White, 0f, new Vector2(TextureAssets.Beetle.Width() / 2, TextureAssets.Beetle.Height() / 6), 1f, effect);
                DrawDataCache.Add(item);
            }
            foreach (DrawData drawData in DrawDataCache)
            {
                spriteBatch.Draw(drawData.texture, drawData.position, drawData.sourceRect, drawData.color, drawData.rotation, drawData.origin, drawData.scale, drawData.effect, 0f);
            }
            DrawDataCache.Clear();
        }
        #endregion
    }


    class BuffOverride :GlobalBuff
    {
        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            switch(type)
            {
                case BuffID.BeetleEndurance1:
                    npc.GetGlobalNPC<RemnantGlobalNPC>().DamageReduction += 15;
                    break;
                case BuffID.BeetleEndurance2:
                    npc.GetGlobalNPC<RemnantGlobalNPC>().DamageReduction += 30;
                    break;
                case BuffID.BeetleEndurance3:
                    npc.GetGlobalNPC<RemnantGlobalNPC>().DamageReduction += 45;
                    break;
                case BuffID.SolarShield1:
                    npc.GetGlobalNPC<RemnantGlobalNPC>().DamageReduction += 10;
                    break;
                case BuffID.SolarShield2:
                    npc.GetGlobalNPC<RemnantGlobalNPC>().DamageReduction += 15;
                    break;
                case BuffID.SolarShield3:
                    npc.GetGlobalNPC<RemnantGlobalNPC>().DamageReduction += 20;
                    break;
            }

            base.Update(type, npc, ref buffIndex);
        }
    }
}
