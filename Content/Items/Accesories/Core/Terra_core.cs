using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    public class Terra_core : ModItem
    {
        int miningSpeedBonus = 15;
        int damageReduction = 5;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(miningSpeedBonus, damageReduction);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Utils1.FormatMoney(Gold:1);
            Item.rare = ItemRarityID.Red;
            Item.expert = true;
            Item.accessory = true;
        }


        int timmer = 0;
        int timmerMax = Utils1.FormatTimeToTick(Second: 5);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= miningSpeedBonus/100f;
            player.endurance += damageReduction/100f;

            float playerPos = player.Center.Y.ToTilePosition();
            if (playerPos > Main.rockLayer) 
            {
                Lighting.AddLight(player.Center, TorchID.Jungle);
                if (timmer++ > timmerMax)
                {
                    timmer = 0;
                    player.AddBuff(BuffID.Dangersense, Utils1.FormatTimeToTick(Second: 1));
                    player.AddBuff(BuffID.Hunter, Utils1.FormatTimeToTick(Second: 1));

                }       
            }

            DrawCore(player);
        }

        public void DrawCore(Player player)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            SpriteEffects effect = player.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Vector2 offset = new Vector2(-16,0) * player.direction;
            Vector2 position = player.Center - Main.screenPosition + new Vector2(0f, player.gfxOffY) + offset;
            Main.spriteBatch.Begin();
            Main.spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(5, 7), 1f, effect, 0f);
            Main.spriteBatch.End();
        }
    }
}