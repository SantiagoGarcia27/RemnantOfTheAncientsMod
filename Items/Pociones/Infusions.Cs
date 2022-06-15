using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Pociones
{
public class EndlessCI: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Combat Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zestaw Endless Combat Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de combate Eterno");
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
            Item.buffType = BuffType<Buffs.Combat_infusion>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(null,"Combat_Infusion",30)
			.AddTile(TileID.Bottles)
			
			.Register();
    
        }
    }	
    public class Combat_Infusion: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combat Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zestaw mikstur bojowych");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de combate");
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
            Item.buffType = BuffType<Buffs.Combat_infusion>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
    public class EndlessMInf: ModItem
	{
        public override string Texture => "opswordsII/Items/Pociones/Mining_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Mining Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zestaw Endless Mining Potions");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria Eterno");
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
            Item.buffType = BuffType<Buffs.Mining_infusion>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(null,"Mining_Infusion",30)
			.AddTile(TileID.Bottles)
			.Register();
    
        }
    }
    public class Mining_Infusion: ModItem
	{
        public override string Texture => "opswordsII/Items/Pociones/Combat_Infusion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Potions Kit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zestaw mikstur górniczych");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
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
            Item.buffType = BuffType<Buffs.Mining_infusion>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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