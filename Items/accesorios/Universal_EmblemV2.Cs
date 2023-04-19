using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.accesorios
{
    public class Universal_EmblemV2 : ModItem
    {
        int multilier;

        public override void SetStaticDefaults()
        {
            multilier = (ModLoader.TryGetMod("CalamityMod", out Mod mod))? 20:25;
            DisplayName.SetDefault("Universal Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème Universel V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema Universal V2");
            Tooltip.SetDefault("Increases all damage by " + multilier + "%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente tous les dégâts de " + multilier + "%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta todo el daño en un" + multilier + "%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {     
            player.GetDamage(DamageClass.Generic) *= (ModLoader.TryGetMod("CalamityMod", out Mod mod)) ? 1.20f : 1.25f;
        }
        public override void AddRecipes() 
        {
            CreateRecipe()
            .AddIngredient(ItemType<Sorcerer_EmblemV2>())
            .AddIngredient(ItemType<Ranger_EmblemV2>())
            .AddIngredient(ItemType<Summoner_EmblemV2>())
            .AddIngredient(ItemType<TrowerEmblemV2>())
            .AddIngredient(ItemType<Warrior_EmblemV2>())
            .AddIngredient(ItemID.LunarBar, 10)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }  
    }
}