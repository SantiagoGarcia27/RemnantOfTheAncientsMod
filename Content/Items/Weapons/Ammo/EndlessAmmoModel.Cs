using RemnantOfTheAncientsMod.Common.Global.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public abstract class EndlessAmmo : ModItem
    {
        public abstract Item ItemBase { get; }
        public abstract string? TexturePath { get; }
        public override string Texture => GetTextureFromItem(ItemBase);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemBase.type);
            Item.maxStack = 1;
            Item.consumable = false;
            Item.value += Item.sellPrice(0, 1, 0, 0);
            Item.rare += 1;
            Item.GetGlobalItem<RemnantGlobalItem>().ItemBase = ItemBase;
        }
        public string GetTextureFromItem(Item item)
        {
            string Texture;
            if (TexturePath != null)
            {
                Texture = TexturePath;
            }
            else Texture = item.type < ItemID.Count ? "Terraria/Images/Item_" + item.type : ItemLoader.GetItem(item.type).Texture;
            return Texture;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemBase.type, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}
