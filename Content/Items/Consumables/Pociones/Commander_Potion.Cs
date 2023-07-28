using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Commander_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Comander Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de comandante");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de comandante");
            //Tooltip.SetDefault("Increases your max number of centries by 1");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el numero maximo de torretas en 1");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 1);
            Item.buffType = BuffType<CentryBuff>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
             CreateRecipe(2)
             .AddIngredient(ItemID.DefenderMedal, 1)
             .AddIngredient(ItemID.BottledWater, 1)
             .AddTile(TileID.Bottles)
             .Register();
        }
    }
    public class Endless_Commander_Porion : Endless
    {
        public override Item ItemBase => new(ItemType<Commander_Potion>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Comander Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de comandante sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de comandante eterna");
            //Tooltip.SetDefault("Augmente votre nombre maximum de centries de 1");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el numero maximo de torretas en 1");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DefenderMedal, 30)
            .AddIngredient(ItemID.BottledWater, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}