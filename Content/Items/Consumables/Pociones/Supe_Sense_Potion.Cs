using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;


namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Supe_Sense_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Sense Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de super sens");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de super sentido");
            Tooltip.SetDefault("Show the locations of enemies and danger sources");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Afficher les emplacements des ennemis et des sources de danger");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Muestra la ubicación de los enemigos y las fuentes de peligro");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 1);
            Item.buffType = BuffType<SuperSenseBuff>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
             CreateRecipe(2)
             .AddIngredient(ItemID.Lens, 2)
             .AddIngredient(ItemID.SpiderFang, 1)
             .AddIngredient(ItemID.Daybloom, 1)
             .AddIngredient(ItemID.BottledWater, 1)
             .AddTile(TileID.Bottles)
             .Register();
        }
    }
    public class Endless_Supe_Sense_Potion : Endless
    {
        public override Item ItemBase => new(ItemType<Supe_Sense_Potion>());
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Super Sense Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de super sens sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de super sentido eterna");
            Tooltip.SetDefault("Show the locations of enemies and danger sources");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Afficher les emplacements des ennemis et des sources de danger");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Muestra la ubicación de los enemigos y las fuentes de peligro");
        }
    }
}