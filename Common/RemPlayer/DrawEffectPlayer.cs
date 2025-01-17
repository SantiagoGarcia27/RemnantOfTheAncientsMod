using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.RemPlayer
{
    public class DrawEffectPlayer : ModPlayer
    {

        float rotation;
        int Timmer1;
        int Timmer2;
        int frameCounter = 0;
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Player.GetModPlayer<RemnantPlayer>().SummonerArea)
            {

                Asset<Texture2D> texture = ModContent.Request<Texture2D>(selectTexture());
                Vector2 origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5  
                Color color = selectColor();

                if (rotation >= 360)
                    rotation = 0;
                else
                    rotation += 0.003f;

                if(RemnantOfTheAncientsMod.CalamityMod != null)
                    animateEffects(Main.LocalPlayer, rotation, texture,color);
                else
                    Main.spriteBatch.Draw((Texture2D)texture, Main.LocalPlayer.Center - Main.screenPosition, null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
                int damage = setDamage(10);

                foreach (var npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.immortal)
                    {
                        float distance = Main.LocalPlayer.Distance(npc.Center);
                        if (distance < 194f)
                        {
                            setDebuff(npc);
                            if (Main.LocalPlayer.infernoCounter % 60 == 0)
                            {
                                Main.LocalPlayer.ApplyDamageToNPC(npc, damage, 5f, -npc.direction);
                            }
                        }
                    }
                }
            }
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);

            static string selectTexture()
            {
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                    return "RemnantOfTheAncientsMod/Content/Effects/Effect/RunicAuraTesla";
                return "RemnantOfTheAncientsMod/Content/Effects/Effect/RunicAura";
            }

            static Color selectColor()
            {
                float fade = 1.6f;
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                    return Utils1.ColorSwap(new Color(209, 102, 25, 102), new Color(69, 122, 191, 102), 12) * fade;
                return Utils1.ColorSwap(new Color(235, 106, 14, 20), new Color(235, 152, 94, 102), 10) * fade;
            }

            static int setDamage(int damage)
            {
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                    damage += 15;
                return damage;
            }
            static void setDebuff(NPC npc)
            {
                npc.AddBuff(BuffID.OnFire3, (int)Utils1.FormatTimeToTick(0, 0, 0, 10));
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    npc.AddBuff(SangarUtilities.Common.CallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "GalvanicCorrosion"), (int)Utils1.FormatTimeToTick(0, 0, 0, 1));
                    npc.AddBuff(BuffID.Electrified, (int)Utils1.FormatTimeToTick(0, 0, 0, 3));
                }
            }
            
            
            void animateEffects(Player player, float rotation, Asset<Texture2D> sprite,Color drawColour)
            {
                Vector2 frames = new(3, 6);
                Vector2 origin = new(sprite.Width() * 0.5f / frames.X, sprite.Height() * 0.5f / frames.Y);

                //if(Main.LocalPlayer.infernoCounter % 2 == 0)
                    frameCounter++;
                
                if (frameCounter > 3)
                {
                    Timmer1++;
                    frameCounter = 0;
                }
               
                if (Timmer1 >= frames.Y)
                {
                    Timmer1 = 0;
                    Timmer2++;
                }
                if (Timmer2 >= frames.X)
                    Timmer2 = 0;

                Rectangle sourceRect = sprite.Frame(3,6, Timmer2,Timmer1);
                float opacity = 1f;     
                Main.EntitySpriteDraw((Texture2D)sprite, player.Center - Main.screenPosition, sourceRect, drawColour * opacity,rotation, origin, 1f, SpriteEffects.None, 0);
            }
        }
    }
}
