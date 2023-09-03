using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models
{

    public abstract class ModelEndless : ModItem
    {
        public abstract Item ItemBase { get; }
       // public abstract ModTranslation ItemName { get; }
      // public abstract ModTranslation ItemDescription { get; }
        public override string Texture => GetTextureFromItem(ItemBase);
       // public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ItemBase.ToolTip.GetLine(0));
        public override void SetStaticDefaults()
        {
           /*// //DisplayName.SetDefault($"ModelEndless {ItemName.GetDefault()}");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), $"{ItemName.GetTranslation(4)} sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), $"{ItemName.GetTranslation(5)} Eterno");
            //Tooltip.SetDefault(ItemDescription.GetDefault());
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ItemDescription.GetTranslation(4));
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ItemDescription.GetTranslation(5));*/
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemBase.type);
            Item.rare += 1;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.buffTime = Utils1.FormatTime(7, 0, 0, 0);
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
