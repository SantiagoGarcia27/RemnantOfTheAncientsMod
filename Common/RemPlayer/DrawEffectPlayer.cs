using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.RemPlayer
{
    public class DrawEffectPlayer : ModPlayer
    {
        public bool FogOfVoidEffect;



        public override void ResetEffects()
        {
            FogOfVoidEffect = false;
        }


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
                    animateEffects(Main.LocalPlayer,texture, 3,6,rotation,color,1f);
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

            if (FogOfVoidEffect)
            {
                string texturePath = "RemnantOfTheAncientsMod/Content/Effects/VoidFogBackground";
                Color currentColor = new Color(0, 0, 53, 230);

                //Background
                Color BackgroundColorOne = new(Color.White.R - 255, Color.White.G - 255, Color.White.B - 203, 255 - 10);
                Color BackgroundColorTwo = new(Color.White.R - 255, Color.White.G - 255, Color.White.B - 213, 255 - 15);
                Color BackgroundColor = Utils1.ColorSwap(BackgroundColorOne, BackgroundColorTwo, 2f);
                Asset<Texture2D> texture = ModContent.Request<Texture2D>(texturePath + "_Background");
                Vector2 origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);
                Main.spriteBatch.Draw((Texture2D)texture, Main.LocalPlayer.Center - Main.screenPosition, null, BackgroundColor, 0f, origin, 18f, SpriteEffects.None, 0f);

                //Center
                if (rotation >= 360)
                    rotation = 0;
                else
                    rotation +=  0.003f;
                texture = ModContent.Request<Texture2D>(texturePath + "_Center");     

                animateEffects(Main.LocalPlayer, texture, 1, 7, rotation, new Color(255, 255, 255, 230),13f,4);
                
                //Extras
                Vector2 positions;
                if (Player.infernoCounter % RemnantOfTheAncientsMod.ParticleMeter(1, 2, 4, 35) == 0)
                {
                    positions = GenerateRandomPoints(Main.screenWidth, Main.screenHeight, 50, Main.LocalPlayer);
                    var du = Dust.NewDust(positions, Main.rand.Next(5, 30), Main.rand.Next(5, 30), DustID.Stone, 0f, 0f, 0, currentColor, Main.rand.Next(1, 10));
                    Main.dust[du].noGravity = true;
                }
                for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(10,5,2,0); i++)
                {
                    positions = GenerateRandomPoints(Main.screenWidth, Main.screenHeight, 50, Main.LocalPlayer);
                    Dust.NewDust(positions, Main.rand.Next(5, 30), Main.rand.Next(5, 30), DustID.Shadowflame, 0f, 0f, 0, default, Main.rand.Next(1, 2));
                }
                //Lighting.AddLight(Player.RotatedRelativePoint(new Vector2(Main.LocalPlayer.Center.X - 16f + Main.LocalPlayer.velocity.X, Main.LocalPlayer.Center.Y - 14f)), currentColor.R, currentColor.G, currentColor.B);
                /* texture = ModContent.Request<Texture2D>(texturePath + "_Extras");
                origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);
                Main.spriteBatch.Draw((Texture2D)texture, Main.LocalPlayer.Center - Main.screenPosition, null, new Color(255, 255, 255, 230), 0f, origin, 12f, SpriteEffects.None, 0f);*/
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
                npc.AddBuff(BuffID.OnFire3, Utils1.FormatTimeToTick(0, 0, 0, 10));
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    npc.AddBuff(CallUtils.TryGetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "GalvanicCorrosion"), Utils1.FormatTimeToTick(0, 0, 0, 1));
                    npc.AddBuff(BuffID.Electrified, Utils1.FormatTimeToTick(0, 0, 0, 3));
                }
            }
            
            
            void animateEffects(Player player, Asset<Texture2D> sprite,int horizontalFrames,int verticalFrames, float rotation, Color drawColour,float scale = 1f,int speed = 1)
            {
                Vector2 frames = new(horizontalFrames, verticalFrames);
                Vector2 origin = new(sprite.Width() * 0.5f / frames.X, sprite.Height() * 0.5f / frames.Y);

                if(Main.LocalPlayer.infernoCounter % speed == 0)
                    frameCounter++;
                
                if (frameCounter > horizontalFrames)
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

                Rectangle sourceRect = sprite.Frame(horizontalFrames, verticalFrames, Timmer2,Timmer1);
                float opacity = 1f;     
                Main.EntitySpriteDraw((Texture2D)sprite, player.Center - Main.screenPosition, sourceRect, drawColour * opacity,rotation, origin, scale, SpriteEffects.None, 0);
            }  
        }
        public Vector2 GenerateRandomPoints(float X,float Y, int radius, Player player)
        {
            float px, py;

            do
            {
                px = player.Center.X + (Main.rand.Next(radius, (int)X) * (int)Math.Pow(-1, Main.rand.Next(2))); // Genera un número entre 0 y x
                py = player.Center.Y + (Main.rand.Next(radius, (int)Y) * (int)Math.Pow(-1, Main.rand.Next(2)));
            }
            while (player.Center.Distance(new Vector2(px, py)) < radius + 3); // Verifica si está dentro del círculo

            return new(px, py);
        }
    }
}
