using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;

namespace RemnantOfTheAncientsMod.Common.Graphics
{
    public class PrimitiveTrail
    {
        public Func<float, float> WidthFunction;

        public Func<float, Color> ColorFunction;

        public Texture2D Texture;

        private VertexPositionColorTexture[] _vertices = Array.Empty<VertexPositionColorTexture>();
        private readonly BasicEffect _effect;
        public bool DebugMode = false;
        public PrimitiveTrail(Texture2D texture, Func<float, float> widthFunction, Func<float, Color> colorFunction)
        {
            Texture = texture;
            WidthFunction = widthFunction;
            ColorFunction = colorFunction;
        }


        public void Draw(Vector2[] points)
        {

            BuildVertices(points);

            if (DebugMode) DrawDebug();
            else DrawVertices();
        }

        private void BuildVertices(Vector2[] points)
        {
            if (points.Length < 2)
                return;

            _vertices = new VertexPositionColorTexture[points.Length * 2];

            int vertexIndex = 0;

            for (int i = 0; i < points.Length; i++)
            {
                Vector2 current = points[i];

                // Ignorar posiciones vacías
                if (current == Vector2.Zero)
                    continue;

                if (i == 0 && points[1] == Vector2.Zero)
                    continue;

                Vector2 direction;

                if (i == points.Length - 1) direction = current - points[i - 1];
                else if (i == 0) direction = points[1] - points[0];
                else if (i == points.Length - 1) direction = points[i] - points[i - 1];
                else direction = points[i + 1] - points[i - 1];

                if (direction == Vector2.Zero) direction = Vector2.UnitY;

                direction.Normalize();

                Vector2 normal = new Vector2(-direction.Y, direction.X);

                float progress = i / (float)(points.Length - 1);

                float width = WidthFunction(progress);

                Color color = ColorFunction(progress);

                Vector2 left = current - normal * width;

                Vector2 right = current + normal * width;

                float u = progress;

                _vertices[vertexIndex++] = new VertexPositionColorTexture(new Vector3(left, 0), color, new Vector2(u, 0));
                _vertices[vertexIndex++] = new VertexPositionColorTexture(new Vector3(right, 0), color, new Vector2(u, 1));
            }
            Array.Resize(ref _vertices, vertexIndex);
        }

        private void DrawVertices()
        {
            if (_vertices.Length < 3)
                return;

            GraphicsDevice device = Main.instance.GraphicsDevice;

            Main.spriteBatch.End();

            Matrix world = Matrix.Identity;

            Matrix view = Matrix.CreateTranslation(
                -Main.screenPosition.X,
                -Main.screenPosition.Y,
                0f);

            Matrix projection = Matrix.CreateOrthographicOffCenter(
                0,
                Main.screenWidth,
                Main.screenHeight,
                0,
                0,
                1);

            _effect.World = world;
            _effect.View = view;
            _effect.Projection = projection;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawUserPrimitives(
                    PrimitiveType.TriangleStrip,
                    _vertices,
                    0,
                    _vertices.Length - 2);
            }

            Main.spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                Main.DefaultSamplerState,
                DepthStencilState.None,
                Main.Rasterizer,
                null,
                Main.GameViewMatrix.TransformationMatrix);
        }

        public void DrawDebug()
        {
            Texture2D pixel = TextureAssets.MagicPixel.Value;

            for (int i = 0; i < _vertices.Length; i += 2)
            {
                Vector2 left = new(
                    _vertices[i].Position.X,
                    _vertices[i].Position.Y);

                Main.EntitySpriteDraw(pixel, left - Main.screenPosition, null, Color.Red, 0, Vector2.Zero, 5, SpriteEffects.None, 0);

                if (i + 1 >= _vertices.Length)
                    break;

                Vector2 right = new(
                    _vertices[i + 1].Position.X,
                    _vertices[i + 1].Position.Y);

                Main.EntitySpriteDraw(pixel, right - Main.screenPosition, null, Color.Blue, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
            }

        }
    }
}