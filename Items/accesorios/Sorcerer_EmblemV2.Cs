using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.accesorios
{
    public class Sorcerer_EmblemV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sorcerer Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Godło Czarnoksiężnika V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème du sorcier V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Hechicero V2");

            Tooltip.SetDefault("Increases magic damage by 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększ obrażenia magiczne o 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts magiques de 25%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño magico en un 25%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 350000;
            Item.rare = 10;
            Item.accessory = true;
        }
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.FragmentVortex, 10) 
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.SorcererEmblem, 1)
            .AddIngredient(ItemID.LunarBar, 10)  //you need 10 Wood
            .AddTile(TileID.LunarCraftingStation)  //at work bench
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) *= 0.25f;
        }
    }
}