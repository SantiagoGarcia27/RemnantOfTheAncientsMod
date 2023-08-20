using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Sergy
{
    [AutoloadEquip(EquipType.Legs)]
    public class Flower_Defender_Legs : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Sangar´s head");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tête de Sangar");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cabeza de Sangar");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
        }
        public override void SetDefaults()
        {
            Item.Size = new(30,30);
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.GetGlobalItem<CustomTooltip>().CompletistItem = true;
        }
        
        public override void UpdateVanity(Player player)
        {   
            base.UpdateVanity(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup("anyStrangePlants")
            .AddIngredient(ItemID.DirtBlock, 10)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Loom)
            .Register();
        }
    }
}