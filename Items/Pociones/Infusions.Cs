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
            .AddIngredient(ItemID.ManaRegenerationPotion, 1)
            .AddIngredient(ItemID.RagePotion, 1)
            .AddIngredient(ItemID.RegenerationPotion, 1)
            .AddIngredient(ItemID.SummoningPotion, 1)
            .AddIngredient(ItemID.SwiftnessPotion, 1)
            .AddIngredient(ItemID.WrathPotion, 1)
            .AddIngredient(ItemID.EndurancePotion, 1)
            .AddIngredient(ItemID.IronskinPotion, 1)
            .AddIngredient(ItemID.LifeforcePotion, 1)
            .AddIngredient(ItemID.MagicPowerPotion, 1)
            .AddIngredient(ItemID.InfernoPotion, 1)
            .AddIngredient(ItemID.ArcheryPotion, 1)
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
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
            DisplayName.SetDefault("Range Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
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

    #endregion



    #region Endless
    public class Endless_Mining_Potion_Kit : Endless
    {
        public override Item ItemBase => new Item(ItemType<Mining_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Mining_Potion_Kit>().DisplayName;
    }
    public class Endless_Combat_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Combat_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Combat_Potion_Kit>().DisplayName;
    }
    public class Endless_Melee_Potion_Kit : Endless
    {
        public override Item ItemBase => new(ItemType<Melee_Potion_Kit>());
        public override ModTranslation ItemName => GetInstance<Melee_Potion_Kit>().DisplayName;
    }
    #endregion
}