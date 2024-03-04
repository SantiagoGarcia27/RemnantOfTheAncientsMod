using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Endeless_Low_Food : ModelEndless
    {
        //  public override string Texture => "Terraria/Images/Item_" + ItemID.Marshmallow;

        public override Item ItemBase => new(ItemID.Marshmallow);
        public override void SetDefaults()
        {
            ItemID.Sets.IsFood[Item.type] = true;
            Item.DefaultToFood(22, 22, BuffID.WellFed, 28800);
            Item.consumable = false;
            Item.rare += 1;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.buffTime = (int)Utils1.FormatTimeToTick(14, 0, 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup("anyLowFood",30)
            .AddTile(TileID.CookingPots)
            .Register();
        }
    }
    public class Endeless_Medium_Food : ModelEndless
    {   
        public override Item ItemBase => new(ItemID.PumpkinPie);
        public override void SetDefaults()
        {
            ItemID.Sets.IsFood[Item.type] = true;
            Item.DefaultToFood(22, 22, BuffID.WellFed2, 28800);
            Item.consumable = false;
            Item.rare += 1;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.buffTime = (int)Utils1.FormatTimeToTick(14, 0, 0, 0);
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
    public class Endeless_Hight_Food : ModelEndless
    {
        public override Item ItemBase => new(ItemID.GoldenDelight);
        public override void SetDefaults()
        {
            ItemID.Sets.IsFood[Item.type] = true;
            Item.DefaultToFood(22, 22, BuffID.WellFed3, 28800);
            Item.consumable = false;
            Item.rare += 1;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.buffTime = (int)Utils1.FormatTimeToTick(14, 0, 0, 0);
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