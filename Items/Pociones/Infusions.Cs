using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Buffs.Kits;

namespace RemnantOfTheAncientsMod.Items.Pociones
{
    #region comun
    public class Combat_Infusion : ModItem
    {
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/Combat_Infusion";
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
    public class Mining_Infusion : ModItem
    {
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/Mining_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffType<Mining_infusion>();
            Item.buffTime = 25400;
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
    public class Builder_Potion_Kit : ModItem
    {
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/Builder_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Builder Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffType<Builder_infusion>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BuilderPotion, 1)
            .AddIngredient(ItemID.CalmingPotion, 1)
            .AddIngredient(ItemID.ObsidianSkinPotion, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion
    #region Endless
    public class EndlessMInf : ModItem
    {  
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/Mining_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Mining Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria Eterno");         
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Mining_Infusion>());
            Item.consumable= false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Mining_Infusion>(),30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessCI : ModItem
    {
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/Combat_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Combat Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de combate Eterno");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Infusion>());
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Combat_Infusion>(), 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class EndlessBuilderInfusion : ModItem
    {
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/Builder_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Builder Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de construcción Eterno");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Builder_Potion_Kit>());
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Builder_Potion_Kit>(), 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion
}