using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Armor.Daylight;
using RemnantOfTheAncientsMod.Common.Global.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Strawberry
{
    [AutoloadEquip(EquipType.Head)]
    public class Strawberry_Hairpin : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.Size = new(30, 30);
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 6;
            if (Main.netMode != 2)
            {
                ArmorIDs.Head.Sets.DrawFullHair[base.Item.headSlot] = true;
            }
        }
        //public override void AddRecipes()
        //{
        //    CreateRecipe()
        //    .AddIngredient(ItemID.Sunflower, 1)
        //    .AddIngredient(ModContent.ItemType<DaylightHat>())
        //    .AddTile(TileID.Loom)
        //    .Register();
        //}
    }

    //public class Flower_Defender_Helmet_EffectDraw : PlayerDrawLayer
    //{
    //    public static Asset<Texture2D> BaseTexture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Items/Armor/Cosmetic/Sergy/Flower_Defender_Helme_Effect");
    //    public float floatingCounter = 0;
    //    public static Item ItemSource;
    //    public override bool IsHeadLayer => true;
    //    //public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    //    //{
    //    //    Player drawPlayer = drawInfo.drawPlayer;
    //    //    if (drawInfo.shadow != 0f || drawPlayer.dead)
    //    //    {
    //    //        return false;
    //    //    }
    //    //    return drawPlayer.head == EquipLoader.GetEquipSlot(Mod, nameof(Flower_Defender_Helmet), EquipType.Head);
    //    //    //  return Main.LocalPlayer.armor[0].type == ModContent.ItemType<Flower_Defender_Helmet>()|| Main.LocalPlayer.armor[10].type == ModContent.ItemType<Flower_Defender_Helmet>();
    //    //}
    //    public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Head);     
    //    protected override void Draw(ref PlayerDrawSet drawInfo)
    //    {    
    //        var position = drawInfo.Center + new Vector2(-20f * Main.LocalPlayer.direction, -25f - floatingCounter) - Main.screenPosition;
    //        position = new Vector2((int)position.X, (int)position.Y); // You'll sometimes want to do this, to avoid quivering.
    //        UpdateCounters();
    //       // var ñ = drawInfo.shadow;
    //        drawInfo.DrawDataCache.Add(new DrawData(BaseTexture.Value,position,null,Color.White,0f,BaseTexture.Size() * 0.5f, 1f, SpriteEffects.None,0));
    //    }
    //public void UpdateCounters()
    //{
    //    floatingCounter = floatingCounter >= 5? 0 : floatingCounter + 0.1f;
    //}
}

