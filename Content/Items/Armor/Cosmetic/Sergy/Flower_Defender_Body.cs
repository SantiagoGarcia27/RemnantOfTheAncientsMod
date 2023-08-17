using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Sergy
{
	[AutoloadEquip(EquipType.Body)]
	public class Flower_Defender_Body : ModItem
	{
		public override void SetStaticDefaults()
		{
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public override void SetDefaults()
        {
            Item.Size = new(30, 30);
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Cyan;
        }
    }
    public class Flower_Defender_Body_EffectDraw : PlayerDrawLayer
    {
        public static Asset<Texture2D> BaseTexture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Items/Armor/Cosmetic/Sergy/Flower_Defender_Body_Effect"); 
        public override bool IsHeadLayer => true;
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return Main.LocalPlayer.armor[1].type == ModContent.ItemType<Flower_Defender_Body>();
        }
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Head);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            var position = drawInfo.Center + new Vector2(-13f * Main.LocalPlayer.direction, -5f) - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y);
            SpriteEffects direction = Main.LocalPlayer.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            drawInfo.DrawDataCache.Add(new DrawData(BaseTexture.Value, position, null, Color.White, 0f, BaseTexture.Size() * 0.5f, 1f, direction, 0));
        }
    }
}

