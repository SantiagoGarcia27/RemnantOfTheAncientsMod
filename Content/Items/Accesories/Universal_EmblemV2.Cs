using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Universal_EmblemV2 : ModItem
    {
        int multilier;

        public override void SetStaticDefaults()
        {
           // multilier = (ModLoader.TryGetMod("CalamityMod", out Mod mod)) ? 20 : 25;
           // //DisplayName.SetDefault("Universal Emblem V2");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème universel V2");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema Universal V2");
            //Tooltip.SetDefault(//Tooltip());    
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        //private string //Tooltip()
        //{
        //    return LocalizationHelper.IncreasedDamageBy//Tooltip(25, DamageClass.Generic);
        //}
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) *= (ModLoader.TryGetMod("CalamityMod", out Mod mod)) ? 1.25f : 1.25f;
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