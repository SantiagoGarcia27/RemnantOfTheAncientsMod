using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.DrawLayer
{
    public class CoreDrawLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Torso);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            List<string> cores = player.GetModPlayer<DrawEffectPlayer>().activeCores;

            int count = cores.Count;
            if (count == 0) return;

            Vector2 center = player.Center
                - Main.screenPosition
                + new Vector2(-24f * player.direction, player.gfxOffY);

            float radius = 10f + (MathUtils.GetValueFromPorcentage(10,50) * cores.Count);
            float tilt = MathHelper.ToRadians(85f);
            float time = Main.GlobalTimeWrappedHourly * 1.5f;
            float distanceToPlayerInBlocks = 1.2f;

            // Ordenar los Cores por profundidad
            var orderedCores = new List<(string path,Vector2 position, float scale, float depth)>();
       

            for (int i = 0; i < count; i++)
            {
                float angle = MathHelper.TwoPi * i / count + time;

                // Coordenadas del anillo con perspectiva
                float depth = (float)Math.Cos(angle);

                float x = depth * radius * (float)Math.Cos(tilt);
                float y = (float)Math.Sin(angle) * radius;

                Vector2 offset = new(x * player.direction, y);
                offset.X += distanceToPlayerInBlocks.ToCoordinatePosition() * -player.direction;

                // Escala según la profundidad
                float scale = 0.5f + (depth + 1f) * 0.22f;

                orderedCores.Add((cores[i],center + offset,scale,depth));
            }

            // Los más lejanos se dibujan primero
            orderedCores.Sort((a, b) => a.depth.CompareTo(b.depth));

            foreach (var core in orderedCores)
            {
                // Convertir profundidad de -1..1 a 0..1
                float depthFactor = (core.depth + 1f) * 0.5f;

                // Los Cores lejanos son más oscuros
                float brightness = MathHelper.Lerp(0.35f, 1f, depthFactor);

                Vector2 worldPosition = core.position + Main.screenPosition;

                Color lightColor = Lighting.GetColor(
                    worldPosition.ToTileCoordinates()
                );

                Color color = Color.White * brightness;
                color = color.MultiplyRGB(lightColor);

                Texture2D texture = ModContent.Request<Texture2D>(core.path).Value;
                drawInfo.DrawDataCache.Add(new DrawData(texture, core.position, null, color, 0f, texture.Size() * 0.5f, core.scale, SpriteEffects.None, 0));
            }
        }
    }
}