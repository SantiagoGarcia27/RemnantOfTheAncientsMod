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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
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
            /* CreateRecipe()
             .AddIngredient(ItemID.SummoningPotion, 1)
             .AddIngredient(ItemID.InfernoPotion, 1)
             .AddIngredient(ItemType<centr>(), 1)
             .AddTile(TileID.Bottles)
             .Register();*/
        }
    }
}