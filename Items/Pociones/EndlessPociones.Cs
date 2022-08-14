using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Pociones
{
    public class EndlessGrav : BasePotions
    {
        public override int TextureID => ItemID.GravitationPotion;
        public override int ItemId => ItemID.GravitationPotion;
        public override int BuffId => BuffID.Gravitation;
        public override string Texture => "Terraria/Images/Item_" + ItemID.GravitationPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gravitation Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de gravedad eterna");
            base.SetStaticDefaults();
        }
    }
    public class EndlessHea : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.HeartreachPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Heartreach Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de alcanzacorazón eterna");
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
            Item.buffType = BuffID.Heartreach;
            Item.buffTime = 15400;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HeartreachPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessInf : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.InfernoPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Inferno Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción del infierno eterna");
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
            Item.buffType = BuffID.Inferno;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.InfernoPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessIron : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.IronskinPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ironskin Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de piel de hierro eterna");
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
            Item.buffType = BuffID.Ironskin;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IronskinPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessAmmo : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.AmmoReservationPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ammo Reservation Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de munición eterna");
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
            Item.buffType = BuffID.AmmoReservation;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.AmmoReservationPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessArch : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ArcheryPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Archery Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de arquero eterna");
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
            Item.buffType = BuffID.Archery;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ArcheryPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }



    public class EndlessBuil : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.BuilderPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Builder Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción constructora eterna");
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
            Item.buffType = BuffID.Builder;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BuilderPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessCalm : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.CalmingPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Calming Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción tranquilizadora eterna");
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
            Item.buffType = BuffID.Calm;
            Item.buffTime = 16400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CalmingPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessCra : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.CratePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Crate Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de caja eterna");
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
            Item.buffType = BuffID.Crate;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CratePotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessDang : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.TrapsightPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Dangersense Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de sentido del peligro eterna");
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
            Item.buffType = BuffID.Dangersense;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.TrapsightPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessFish : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FishingPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Fishing Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de pesca eterna");
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
            Item.buffType = BuffID.Fishing;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FishingPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }


    public class EndlessRe : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.RegenerationPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Regeneration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de regeneración eterna");
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
            Item.buffType = BuffID.Regeneration;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.RegenerationPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessSo : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SonarPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Sonar Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de sonar eterna");
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
            Item.buffType = BuffID.Sonar;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SonarPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessSp : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SpelunkerPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Spelunker Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de espeleógo eterna");
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
            Item.buffType = BuffID.Spelunker;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SpelunkerPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessSu : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SummoningPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Summoning Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de invocación eterna");
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
            Item.buffType = BuffID.Summoning;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SummoningPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessSwif : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SwiftnessPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Swiftness Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de rapidez eterna");
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
            Item.buffType = BuffID.Swiftness;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SwiftnessPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessTh : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ThornsPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Thorns Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de espinas eterna");
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
            Item.buffType = BuffID.Thorns;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ThornsPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessBat : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.BattlePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Battle Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de batalla eterna");
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
            Item.buffType = BuffID.Battle;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BattlePotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessEndu : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.EndurancePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Endurance Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de resistencia eterna");
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
            Item.buffType = BuffID.Endurance;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.EndurancePotion, 30)
            .AddTile(TileID.Bottles)

            .Register();
        }
    }

    public class EndlessFall : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FeatherfallPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Featherfall Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de caída de pluma eterna");
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
            Item.buffType = BuffID.Featherfall;
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FeatherfallPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessGills : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.GillsPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gills Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de agallas eterna");
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
            Item.buffType = BuffID.Gills;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.GillsPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessHunt : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.HunterPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Hunter Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de cazador eterna");
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
            Item.buffType = BuffID.Hunter;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HunterPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessInv : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.InvisibilityPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Invisibility Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de invisibilidad eterna");
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
            Item.buffType = BuffID.Invisibility;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.InvisibilityPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessLF : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.LifeforcePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Lifeforce Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de fuerza vital eterna");
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
            Item.buffType = BuffID.Lifeforce;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LifeforcePotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessMi : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MiningPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Mining Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de minería eterna");
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
            Item.buffType = BuffID.Mining;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MiningPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessMP : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MagicPowerPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Magic Power Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de poder mágico eterna");
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
            Item.buffType = BuffID.MagicPower;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MagicPowerPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public class EndlessMR : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ManaRegenerationPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Magic Regeneration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción reg. de maná eterna");
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
            Item.buffType = BuffID.ManaRegeneration;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ManaRegenerationPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessNig : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.NightOwlPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Night Owl Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de noctámbulo eterna");
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
            Item.buffType = BuffID.NightOwl;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.NightOwlPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessOb : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ObsidianSkinPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Obsidian Skin Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de piel de obsidiana eterna");
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
            Item.buffType = BuffID.ObsidianSkin;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ObsidianSkinPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessRa : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.RagePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Rage Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de furia eterna");
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
            Item.buffType = BuffID.Rage;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.RagePotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessSh : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ShinePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Shine Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de brillo eterna");
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
            Item.buffType = BuffID.Shine;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ShinePotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessTi : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.TitanPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Titan Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de titán eterna");
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
            Item.buffType = BuffID.Titan;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.TitanPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessWa : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.WarmthPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Warmth Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción del calor eterna");
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
            Item.buffType = BuffID.Warmth;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WarmthPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessWr : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.WrathPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Wrath Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de ira eterna");
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
            Item.buffType = BuffID.Wrath;
            Item.buffTime = 15400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WrathPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
