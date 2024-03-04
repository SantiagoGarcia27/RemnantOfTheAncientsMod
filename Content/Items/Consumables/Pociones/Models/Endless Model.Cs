using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global.Items;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models
{

    public abstract class ModelEndless : ModItem
    {
        public abstract Item ItemBase { get; }
        public override string Texture => GetTextureFromItem(ItemBase);

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, Main.itemAnimations[ItemBase.type]);
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
           
                Item.CloneDefaults(ItemBase.type);
                Item.rare += 1;
                Item.maxStack = 1;
                Item.consumable = false;
                Item.buffTime = (int)Utils1.FormatTimeToTick(14, 0, 0, 0);
                Item.GetGlobalItem<RemnantGlobalItem>().ItemBase = ItemBase;
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