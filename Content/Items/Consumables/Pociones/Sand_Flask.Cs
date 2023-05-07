using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Sand_Flask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Flacon de sable");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de Arena Ardiente");
            Tooltip.SetDefault("Melee attacks set enemies burning sand");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Les attaques de mêlée font brûler le sable par les ennemis");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Los ataques cuerpo a cuerpo hacen que los enemigos se quemen con arena ardiendo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(0, 0, 20, 0);
            Item.buffType = ModContent.BuffType<Sand_Weapons>();
            Item.buffTime = 72000;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ModContent.ItemType<Sand_escense>(), 2)
            .AddTile(TileID.ImbuingStation)
            .Register();
        }
    }
    public class Endless_Sand_Flask : Endless
    {
        public override Item ItemBase => new(ModContent.ItemType<Sand_Flask>());
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Sand Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de Arena Ardiente eterno");
            Tooltip.SetDefault("Melee attacks set enemies burning sand");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Los ataques cuerpo a cuerpo hacen que los enemigos se quemen con arena ardiendo");
        }

    }
}

