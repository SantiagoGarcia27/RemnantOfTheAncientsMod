using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Pociones
{

    public abstract class Endless : ModItem
    {
        public abstract Item ItemBase { get; }
        public abstract ModTranslation ItemName { get; }
        public override string Texture => GetTextureFromItem(ItemBase);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault($"Endless {ItemName.GetDefault()}");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), $"{ItemName.GetTranslation(4)} sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), $"{ItemName.GetTranslation(5)} Eterno");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemBase.type);
            Item.rare += 1;
            Item.consumable = false;
        }
        public string GetTextureFromItem(Item item)
        {
            string Texture = item.type < ItemID.Count ? "Terraria/Images/Item_" + item : ItemLoader.GetItem(item.type).Texture;
            return Texture;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemBase.type, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
