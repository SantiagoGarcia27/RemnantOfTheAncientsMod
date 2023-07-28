using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;


namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Money_Collector_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Money Collector Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de super sens");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de recolectora de dinero");
            //Tooltip.SetDefault("Transport the money from your inventory to the piggy bank");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Transportez l'argent de votre inventaire à la tirelire");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Transporta el dinero de tu inventario a la hucha");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Money Collector Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de super sens sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de recolectora de dinero eterna");
            //Tooltip.SetDefault("Transport the money from your inventory to the piggy bank");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Transportez l'argent de votre inventaire à la tirelire");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Transporta el dinero de tu inventario a la hucha");
        }
    }
}