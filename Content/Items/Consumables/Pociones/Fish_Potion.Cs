using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using CalamityMod.Items.Potions;
using System.Runtime.InteropServices;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Fish_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Fish Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de poisson");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de pez");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(silver: 1);
            Item.buffType = BuffType<FishBuff>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Calamity();
            }
            else
            {
                CreateRecipe()
                .AddIngredient(ItemID.WaterWalkingPotion, 1)
                .AddIngredient(ItemID.FlipperPotion, 1)
                .AddIngredient(ItemID.GillsPotion, 1)
                .AddTile(TileID.Bottles)
                .Register();
            }

            [JITWhenModsEnabled("CalamityMod")]
            void Calamity()
            {
                CreateRecipe()
                .AddIngredient(ItemID.WaterWalkingPotion, 1)
                .AddIngredient(ItemID.FlipperPotion, 1)
                .AddIngredient(ItemID.GillsPotion, 1)
                .AddIngredient(ItemType<EndlessAnechoicCoating>())
                .AddIngredient(ItemType<SulphurskinPotion>())
                .AddTile(TileID.Bottles)
                .Register();
            }
        }
    }
    public class Endless_Fish_Potion : ModelEndless
    {
        public override Item ItemBase => new Item(ItemType<Fish_Potion>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Fish Potions");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de poisson sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pocion de pez eterna");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();


            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Calamity();
            }
            else
            {
                CreateRecipe()
                .AddIngredient(ItemID.WaterWalkingPotion, 30)
                .AddIngredient(ItemID.FlipperPotion, 30)
                .AddIngredient(ItemID.GillsPotion, 30)
                .AddTile(TileID.Bottles)
                .Register();

                CreateRecipe()
                .AddIngredient(ItemType<EndlessGills>(), 1)
                .AddIngredient(ItemType<EndlessWaterWalking>(), 1)
                .AddIngredient(ItemType<EndlessFlipper>(), 1)
                .AddTile(TileID.Bottles)
                .Register();
            }

            [JITWhenModsEnabled("CalamityMod")]
            void Calamity()
            {
                CreateRecipe()
               .AddIngredient(ItemID.WaterWalkingPotion, 30)
               .AddIngredient(ItemID.FlipperPotion, 30)
               .AddIngredient(ItemID.GillsPotion, 30)
               .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AnechoicCoating"), 30)
               .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "SulphurskinPotion"), 30)
               .AddTile(TileID.Bottles)
               .Register();

                CreateRecipe()
               .AddIngredient(ItemType<EndlessGills>(), 1)
               .AddIngredient(ItemType<EndlessWaterWalking>(), 1)
               .AddIngredient(ItemType<EndlessFlipper>(), 1)
               .AddIngredient(ItemType<EndlessAnechoicCoating>(), 1)
               .AddIngredient(ItemType<EndlessSulphurskinPotion>(), 1)
               .AddTile(TileID.Bottles)
               .Register();
            }
        }
    }
}