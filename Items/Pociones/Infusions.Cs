using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Pociones
{
    public class EndlessCI : BasePotions
    {
        public override int TextureID => ItemType<Combat_Infusion>();
        public override int ItemId => ItemType<Combat_Infusion>();
        public override int BuffId => BuffType<Combat_infusion>();
        public override string Texture => "opswordsII/Items/Pociones/Combat_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Combat Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de combate Eterno");
            base.SetStaticDefaults();
        }
    }
    public class Combat_Infusion : ModItem
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
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
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
    public class EndlessMInf : BasePotions
    {
        public override int TextureID => ItemType<Mining_Infusion>();
        public override int ItemId => ItemType<Mining_Infusion>();
        public override int BuffId => BuffType<Combat_infusion>();
        public override string Texture => "opswordsII/Items/Pociones/Mining_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Mining Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria Eterno");
            base.SetStaticDefaults();
        }
    }
    public class Mining_Infusion : ModItem
    {
        public override string Texture => "opswordsII/Items/Pociones/Combat_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
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
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
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
}