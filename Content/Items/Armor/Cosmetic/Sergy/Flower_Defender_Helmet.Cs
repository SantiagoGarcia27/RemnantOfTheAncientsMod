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
    [AutoloadEquip(EquipType.Head)]
    public class Flower_Defender_Helmet : ModItem
    {
        public override void SetStaticDefaults()
        {         
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;         
        }
        public override void SetDefaults()
        {
            Item.Size = new(30,30);
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Cyan;
        }
    }

    public class Flower_Defender_Helmet_EffectDraw : PlayerDrawLayer
    {
        public static Asset<Texture2D> BaseTexture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Items/Armor/Cosmetic/Sergy/Flower_Defender_Helme_Effect");
        public float floatingCounter = 0;
        public static Item ItemSource;
        public override bool IsHeadLayer => true;
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return Main.LocalPlayer.armor[0].type == ModContent.ItemType<Flower_Defender_Helmet>();
        }
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Head);     
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {    
            var position = drawInfo.Center + new Vector2(-20f * Main.LocalPlayer.direction, -25f - floatingCounter) - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y); // You'll sometimes want to do this, to avoid quivering.
            UpdateCounters();

            drawInfo.DrawDataCache.Add(new DrawData(BaseTexture.Value,position,null,Color.White,0f,BaseTexture.Size() * 0.5f, 1f, SpriteEffects.None,0));
        }
        public void UpdateCounters()
        {
            floatingCounter = floatingCounter >= 5? 0 : floatingCounter + 0.1f;
        }
    }
}