using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Endeless_Low_Food : ModItem
    {
      //  public override string Texture => "Terraria/Images/Item_" + ItemID.Marshmallow;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 1);
            Item.buffType = BuffID.WellFed;
            Item.consumable = false;
            Item.buffTime = Utils1.FormatTime(6,0,0,0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup("anyLowFood",30)
            .AddTile(TileID.CookingPots)
            .Register();
        }
    }
    public class Endeless_Medium_Food : ModItem
    {
       // public override string Texture => "Terraria/Images/Item_" + ItemID.SeafoodDinner;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 50);
            Item.buffType = BuffID.WellFed2;
            Item.consumable = false;
            Item.buffTime = Utils1.FormatTime(6, 0, 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Endeless_Low_Food>())
            .AddRecipeGroup("anyMediumFood",30)
            .AddTile(TileID.CookingPots)
            .Register();
        }
    }
    public class Endeless_Hight_Food : ModItem
    {
      //  public override string Texture => "Terraria/Images/Item_" + ItemID.GoldenDelight;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(gold: 3);
            Item.buffType = BuffID.WellFed3;
            Item.consumable = false;
            Item.buffTime = Utils1.FormatTime(6, 0, 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Endeless_Medium_Food>())
            .AddRecipeGroup("anyHightFood", 30)
            .AddTile(TileID.CookingPots)
            .Register();
        }
    }
}