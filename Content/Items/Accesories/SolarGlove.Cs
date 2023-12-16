using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class SolarGlove : ModItem
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

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(25);
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
            .AddIngredient(ItemID.FireGauntlet, 1)
            .AddIngredient(ItemID.SunStone, 1)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.LunarBar, 5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) *= 1.14f;
            player.kbGlove = true;
            player.autoReuseGlove = true;
            player.meleeScaleGlove = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.14f;
            player.magmaStone = true;
            player.skyStoneEffects = true;
            ModContent.GetInstance<RemnantPlayer>().MeleeBuffInflict.Add(BuffID.Daybreak);

            /*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
            if (CalamityMod != null)

    player.GetDamage(DamageClass.Melee) += 0.20f; */
        }
    }
}