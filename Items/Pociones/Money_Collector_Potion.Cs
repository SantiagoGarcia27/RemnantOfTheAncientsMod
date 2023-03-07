using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Buffs;


namespace RemnantOfTheAncientsMod.Items.Pociones
{
    public class Money_Collector_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Money Collector Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de super sens");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de recolectora de dinero");
            Tooltip.SetDefault("Show the locations of enemies and danger sources");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Muestra la ubicación de los enemigos y las fuentes de peligro");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 1);
            Item.buffType = BuffType<MoneyColectorBuff>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
             CreateRecipe(1)
             .AddIngredient(ItemID.Moonglow, 1)
             .AddIngredient(ItemID.GoldCoin, 1)
             .AddIngredient(ItemID.BottledWater, 1)
             .AddTile(TileID.Bottles)
             .Register();
        }
    }
    public class Endless_Money_Collector_Potion : Endless
    {
        public override Item ItemBase => new(ItemType<Money_Collector_Potion>());
        public override ModTranslation ItemName => GetInstance<Money_Collector_Potion>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Money_Collector_Potion>().Tooltip;
    }
}