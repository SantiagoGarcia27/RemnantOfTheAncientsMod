using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.accesorios
{
    public class Universal_EmblemV2 : ModItem
    { 
        int multilier = 25;

        public override void SetStaticDefaults()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod mod)) multilier = 20;
            AccesorieDamageClass a = new AccesorieDamageClass();
            DisplayName.SetDefault("Universal Emblem V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème Universel V2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema Universal V2");
            Tooltip.SetDefault("Increases all damage by "+ multilier + "%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente tous les dégâts de " + multilier + "%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta todo el daño en un"+ multilier + "%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 450000;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
           // AccesorieDamageClass a = new AccesorieDamageClass();
            if (ModLoader.TryGetMod("CalamityMod", out Mod mod)) player.GetDamage(DamageClass.Generic) *= 1.20f;
            else player.GetDamage(DamageClass.Generic) *= 1.25f;
            //a.accesorieDamageCalculator(multilier);
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