using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class ShinyPebbles : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 9999;
			Item.value = 100;
			Item.rare = ItemRarityID.Green;
		}

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            // Intensidad pulsante
            float pulse = (float)(0.5f + 0.5f * System.Math.Sin(Main.GlobalTimeWrappedHourly * 3f));

            // Color del contorno
            Color glowColor = Color.Cyan * (0.25f + pulse * 0.5f);

            // Grosor del contorno
            float radius = 2f + pulse * 2f;

            // Dibujar 8 copias alrededor del ítem
            for (int i = 0; i < 8; i++)
            {
                float angle = MathHelper.TwoPi * i / 8f;

                Vector2 offset = new Vector2(
                    (float)System.Math.Cos(angle),
                    (float)System.Math.Sin(angle)
                ) * radius;

                spriteBatch.Draw(texture, position + offset, frame, glowColor, 0f, origin, scale, SpriteEffects.None, 0f);
            }

            return true;
        }


        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
            {
                frame = Main.itemAnimations[Item.type].GetFrame(
                    texture,
                    Main.itemFrameCounter[whoAmI]
                );
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;

            Vector2 offset = new Vector2(
                Item.width / 2f - frameOrigin.X,
                Item.height - frame.Height
            );

            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            // Intensidad pulsante
            float pulse = 0.5f + 0.5f *
                (float)System.Math.Sin(
                    Main.GlobalTimeWrappedHourly * 3f
                );

            // Color del contorno
            Color glowColor = Color.Cyan *
                (0.25f + pulse * 0.5f);

            // Radio del contorno
            float radius = 2f + pulse * 2f;

            // Dibujar el contorno
            for (int i = 0; i < 16; i++)
            {
                float angle = MathHelper.TwoPi * i / 16f;

                Vector2 glowOffset = new Vector2(
                    (float)System.Math.Cos(angle),
                    (float)System.Math.Sin(angle)
                ) * radius;

                spriteBatch.Draw(texture, drawPos + glowOffset, frame, glowColor, rotation, frameOrigin, scale, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}