using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.accesorios
{
    public class TrowerEmblemV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thrower Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème du Lanceur V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Lanzador V2");
            Tooltip.SetDefault("Increases throwing damage by 25% ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts de lancer de 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño lanzable en un 25%  ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.FragmentVortex, 10) 
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ModContent.ItemType<Neutrum_Fragment>(),10)
            .AddIngredient(ModContent.ItemType<TrowerEmblem>(),1)
            .AddIngredient(ItemID.LunarBar, 10) 
            .AddTile(TileID.LunarCraftingStation)   
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) *= 1.25f;

        }
    }
}