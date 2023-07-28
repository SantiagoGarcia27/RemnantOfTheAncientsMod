using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Warrior_EmblemV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("Warrior Emblem V2");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème Guerrier V2");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Guerrero V2");
            //Tooltip.SetDefault(//Tooltip());

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        //public static string //Tooltip()
        //{
        //    return LocalizationHelper.IncreasedDamageBy//Tooltip(25, DamageClass.Melee);


        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 8, 0, 0);
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
            .AddIngredient(ItemID.WarriorEmblem, 1)
            .AddIngredient(ItemID.LunarBar, 10) //you need 10 Wood
            .AddTile(TileID.LunarCraftingStation)  //at work bench
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) *= 1.25f;

            /*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
            if (CalamityMod != null)

    player.GetDamage(DamageClass.Melee) += 0.20f; */
        }
    }
}