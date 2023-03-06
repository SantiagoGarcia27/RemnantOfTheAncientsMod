using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Buffs;


namespace RemnantOfTheAncientsMod.Items.Pociones
{
    public class Gravitation_Control_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gravity Control Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de contrôle de la gravité");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de control gravitacional");
            Tooltip.SetDefault("Increases your max number of centries by 1");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 1);
            Item.buffType = BuffType<GravityControlBuff>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
             CreateRecipe(2)
             .AddIngredient(ItemID.GravitationPotion, 1)
             .AddIngredient(ItemID.FeatherfallPotion, 1)
             .AddTile(TileID.Bottles)
             .Register();
        }
    }
    public class Endless_Gravitation_Control_Potion : Endless
    {
        public override Item ItemBase => new(ItemType<Gravitation_Control_Potion>());
        public override ModTranslation ItemName => GetInstance<Gravitation_Control_Potion>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Gravitation_Control_Potion>().Tooltip;
    }
}