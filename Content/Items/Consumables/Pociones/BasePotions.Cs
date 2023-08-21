using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public abstract class BasePotions : ModItem
    {      
        public abstract Item ItemBase { get; }
        public override string Texture => GetTextureFromItem(ItemBase);
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ItemBase.ToolTip.GetLine(0));
        public override void SetStaticDefaults() 
        {       
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
            string Texture = item.type < ItemID.Count ? "Terraria/Images/Item_" + item.type : ItemLoader.GetItem(item.type).Texture;
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