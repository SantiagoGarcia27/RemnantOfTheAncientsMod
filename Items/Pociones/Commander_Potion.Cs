using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Buffs;


namespace RemnantOfTheAncientsMod.Items.Pociones
{
    public class Commander_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comander Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de comandante");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            Tooltip.SetDefault("Increases your max number of centries by 1");
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
        public override ModTranslation ItemName => GetInstance<Commander_Potion>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Commander_Potion>().Tooltip;
    }
}