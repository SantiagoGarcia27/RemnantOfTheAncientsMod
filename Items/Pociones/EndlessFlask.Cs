using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Pociones
{
    public class EndlessGoldF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofGold;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gold Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de oro eterno");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbueGold;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofGold, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessIchorF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofIchor;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ichor Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de icor eterno");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbueIchor;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofIchor, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessFireF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofFire;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Fire Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de fuego eterno");
            Tooltip.SetDefault("");
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbueFire;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofFire, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessCurcedF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofCursedFlames;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Cursed Flames Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de las llamas malditas eterno");
            Tooltip.SetDefault("");
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbueCursedFlames;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofCursedFlames, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessPonF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofVenom;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Venom Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de ponzoña eterno");
            Tooltip.SetDefault("");
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbueVenom;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofVenom, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessPartyF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofParty;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Party Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco fiestero eterno");
            Tooltip.SetDefault("");
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbueConfetti; //Specify an existing buff to be applied when used.
            Item.buffTime = 75400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofParty, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessVenomF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofPoison;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Poison Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de veneno eterno");
            Tooltip.SetDefault("");
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbuePoison;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofPoison, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessNaniteF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FlaskofNanites;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Nanites Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de nanitos eterno");
            Tooltip.SetDefault("");
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
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffID.WeaponImbueNanites;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofNanites, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}