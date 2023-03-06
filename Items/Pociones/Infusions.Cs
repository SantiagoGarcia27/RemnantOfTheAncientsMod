using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Buffs.Kits;

namespace RemnantOfTheAncientsMod.Items.Pociones
{
    #region comun
    public class Combat_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combat Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de combate");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffType<Combat_infusion>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Melee_Potion_Kit>())
            .AddIngredient(ItemType<Tank_Potion_Kit>())
            .AddIngredient(ItemType<Ranger_Potion_Kit>())
            .AddIngredient(ItemType<Summon_Potion_Kit>())
            .AddIngredient(ItemType<Mage_Potion_Kit>())
            .AddIngredient(ItemType<Advanced_Potion_Kit>())
            .AddIngredient(ItemType<Melee_Potion_Kit>())
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Mining_Potion_Kit: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Mining_infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SpelunkerPotion, 1)
            .AddIngredient(ItemID.ShinePotion, 1)
            .AddIngredient(ItemID.NightOwlPotion, 1)
            .AddIngredient(ItemID.MiningPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Summon_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Summoner Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions d'invocateur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Summon_Infusion>(); 
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SummoningPotion, 1)
            .AddIngredient(ItemID.InfernoPotion, 1)
            .AddIngredient(ItemType<Commander_Potion>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Melee_Potion_Kit : ModItem
    {
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/Builder_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Melee Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de mêlée");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Melee_Infusion>(); 
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofIchor, 1)
            .AddIngredient(ItemID.Sake, 1)
            .AddIngredient(ItemID.TitanPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Mage_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions magiques");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Mage_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlaskofIchor, 1)
            .AddIngredient(ItemID.Sake, 1)
            .AddIngredient(ItemID.TitanPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Tank_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tank Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de réservoir");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Tank_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.EndurancePotion, 1)
            .AddIngredient(ItemID.LifeforcePotion, 1)
            .AddIngredient(ItemID.HeartreachPotion, 1)
            .AddIngredient(ItemID.WarmthPotion, 1)
            .AddIngredient(ItemID.ThornsPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Ranger_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ranger Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions Ranger");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Ranger_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.AmmoReservationPotion, 1)
            .AddIngredient(ItemID.ArcheryPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Basic_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de base");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones basico");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Basic_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.RegenerationPotion, 1)
            .AddIngredient(ItemID.SwiftnessPotion, 1)
            .AddIngredient(ItemID.IronskinPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Fisher_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fisher Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de pêche");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Fishing_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CalmingPotion, 1)
            .AddIngredient(ItemID.FishingPotion, 1)
            .AddIngredient(ItemID.SonarPotion, 1)
            .AddIngredient(ItemID.CratePotion, 1)
            .AddIngredient(ItemID.LuckPotionGreater, 1)
            .AddIngredient(ItemID.InvisibilityPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Advanced_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Advanced Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions avancées");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones avanzado");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Advanced_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Basic_Potion_Kit>(), 1)
            .AddIngredient(ItemID.WrathPotion, 1)
            .AddIngredient(ItemID.RagePotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Exploration_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exploration Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions d'exploration");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de exploración");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Exploration_infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Mining_Potion_Kit>(), 1)
            .AddIngredient(ItemType<Supe_Sense_Potion>(), 1)
            .AddIngredient(ItemType<Gravitation_Control_Potion>(), 1)
            .AddIngredient(ItemID.ObsidianSkinPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Poseidon_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poseidon Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions Poséidon");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de exploración");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Poseidon_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Fisher_Potion_Kit>(), 1)
            .AddIngredient(ItemType<Fish_Potion>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Ultimate_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions ultimes");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de definitivo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffType<Ultimate_Infusion>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Combat_Potion_Kit>())
            .AddIngredient(ItemType<Poseidon_Potion_Kit>())
            .AddIngredient(ItemType<Exploration_Potion_Kit>())         
            .AddIngredient(ItemID.BuilderPotion)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion



    #region Endless
    public class Endless_Mining_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Mining_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Mining_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Mining_Potion_Kit>().Tooltip;
    }
    public class Endless_Combat_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Combat_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Combat_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Combat_Potion_Kit>().Tooltip;
    }
    public class Endless_Melee_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Melee_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Melee_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Melee_Potion_Kit>().Tooltip;
    }
    public class Endless_Ranger_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Ranger_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Ranger_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Ranger_Potion_Kit>().Tooltip;
    }
    public class Endless_Summon_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Summon_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Summon_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Summon_Potion_Kit>().Tooltip;
    } 
   public class Endless_Fisher_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Fisher_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Fisher_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Fisher_Potion_Kit>().Tooltip;
    }
    public class Endless_Basic_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Basic_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Basic_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Basic_Potion_Kit>().Tooltip;
    }
    public class Endless_Magic_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Mage_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Mage_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Mage_Potion_Kit>().Tooltip;
    }
    public class Tank_Magic_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Tank_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Tank_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Tank_Potion_Kit>().Tooltip;
    }
    public class Endless_Advanced_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Advanced_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Advanced_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Advanced_Potion_Kit>().Tooltip;
    }
    public class Endless_Exploration_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Exploration_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Exploration_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Exploration_Potion_Kit>().Tooltip;
    }
    public class Endless_Poseidon_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Poseidon_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Poseidon_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Poseidon_Potion_Kit>().Tooltip;
    }
    public class Endless_Ultimate_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Ultimate_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Ultimate_Potion_Kit>().DisplayName;
        public override ModTranslation ItemDescription => GetInstance<Ultimate_Potion_Kit>().Tooltip;
    }
    #endregion
}