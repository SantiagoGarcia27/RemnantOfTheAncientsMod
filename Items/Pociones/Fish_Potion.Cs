using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Buffs;


namespace RemnantOfTheAncientsMod.Items.Pociones
{
    public class Fish_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de poisson");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de pez");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 1);
            Item.buffType = BuffType<FishBuff>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WaterWalkingPotion, 1)
            .AddIngredient(ItemID.FlipperPotion, 1)
            .AddIngredient(ItemID.GillsPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Endless_Fish_Potion : Endless
    {
        public override Item ItemBase => new Item(ItemType<Fish_Potion>());
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Fish Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de poisson sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de pez eterna");
        }
    }
}