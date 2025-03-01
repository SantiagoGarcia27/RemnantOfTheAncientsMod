using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Boots
{
    [AutoloadEquip(EquipType.Shoes)]
    public class Boot : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 0, 5, 72);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.defense = 3;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxFallSpeed *= 2f;   
        }
        public override void AddRecipes() 
        {
            CreateRecipe()
            .AddIngredient(ItemID.Leather, 3) 
            .AddTile(TileID.WorkBenches) 
            .Register();
        }
    }
}