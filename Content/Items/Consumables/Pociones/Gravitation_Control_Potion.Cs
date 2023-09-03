using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Gravitation_Control_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Gravity Control Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de contrôle de la gravité");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de control gravitacional");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Gravitation_Control_Potion : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Gravitation_Control_Potion>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Gravity Control Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de contrôle de la gravité sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de control gravitacional eterna");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemID.GravitationPotion, 30)
            .AddIngredient(ItemID.FeatherfallPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<EndlessGravitation>(), 1)
            .AddIngredient(ItemType<EndlessFeather>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}