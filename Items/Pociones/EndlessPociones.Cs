using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Pociones
{ 
    public class EndlessGrav : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gravitation Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de gravedad eterna");
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
            Item.buffType = BuffID.Gravitation; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.GravitationPotion, 30)
			.AddTile(TileID.Bottles)
			
			.Register();
        }
    }
    public class EndlessHea : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Heartreach Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de alcanzacorazón eterna");
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
            Item.buffType = BuffID.Heartreach; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Inferno Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción del infierno eterna");
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
            Item.buffType = BuffID.Inferno; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.InfernoPotion, 30)
			.AddTile(TileID.Bottles)
			
			.Register();
        }
    }
    public class EndlessIron: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ironskin Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de piel de hierro eterna");
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
            Item.buffType = BuffID.Ironskin; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.IronskinPotion, 30)
			.AddTile(TileID.Bottles)			
			.Register();
        }
    }
    public class EndlessAmmo: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ammo Reservation Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de munición eterna");
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
            Item.buffType = BuffID.AmmoReservation; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.AmmoReservationPotion, 30)
			.AddTile(TileID.Bottles)
			
			.Register();
        }
    }
    public class EndlessArch: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Archery Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de arquero eterna");
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
            Item.buffType = BuffID.Archery; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.ArcheryPotion, 30)
			.AddTile(TileID.Bottles)
			.Register();
        }  
    }
    
    

    public class EndlessBuil: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Builder Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción constructora eterna");
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
            Item.buffType = BuffID.Builder; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Calming Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción tranquilizadora eterna");
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
            Item.buffType = BuffID.Calm; //Specify an existing buff to be applied when used.
            Item.buffTime = 16400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CalmingPotion, 30)
			.AddTile(TileID.Bottles)
			.Register();
        }
    }
    public class EndlessCra: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Crate Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de caja eterna");
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
            Item.buffType = BuffID.Crate; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CratePotion, 30)
			.AddTile(TileID.Bottles)
			.Register();
        }
    }
    
   
    public class EndlessDang: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Dangersense Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de sentido del peligro eterna");
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
            Item.buffType = BuffID.Dangersense; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TrapsightPotion, 30)
			.AddTile(TileID.Bottles)
			.Register();
        }
    }

    public class EndlessFish: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Fishing Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de pesca eterna");
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
            Item.buffType = BuffID.Fishing;//Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Regeneration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de regeneración eterna");
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
            Item.buffType = BuffID.Regeneration; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Sonar Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de sonar eterna");
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
            Item.buffType = BuffID.Sonar; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Spelunker Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de espeleógo eterna");
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
            Item.buffType = BuffID.Spelunker; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Summoning Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de invocación eterna");
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
            Item.buffType = BuffID.Summoning; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Swiftness Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de rapidez eterna");
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
            Item.buffType = BuffID.Swiftness; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Thorns Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de espinas eterna");
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
            Item.buffType = BuffID.Thorns; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.ThornsPotion, 30)
			.AddTile(TileID.Bottles)	
			.Register();
        }
    }
    public class EndlessBat: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Battle Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de batalla eterna");
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
            Item.buffType = BuffID.Battle; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.BattlePotion, 30)
			.AddTile(TileID.Bottles)
			.Register();
        }
    }
    public class EndlessEndu: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Endurance Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de resistencia eterna");
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
            Item.buffType = BuffID.Endurance; //Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.EndurancePotion, 30)
			.AddTile(TileID.Bottles)
			
			.Register();
        }
    }

    public class EndlessFall: ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Featherfall Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de caída de pluma eterna");
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
            Item.buffType = BuffID.Featherfall;//Specify an existing buff to be applied when used.
            Item.buffTime = 25400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gills Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de agallas eterna");
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
            Item.buffType = BuffID.Gills; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Hunter Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de cazador eterna");
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
            Item.buffType = BuffID.Hunter; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Invisibility Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de invisibilidad eterna");
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
            Item.buffType = BuffID.Invisibility; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Lifeforce Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de fuerza vital eterna");
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
            Item.buffType = BuffID.Lifeforce; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Mining Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de minería eterna");
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
            Item.buffType = BuffID.Mining; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Magic Power Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de poder mágico eterna");
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
            Item.buffType = BuffID.MagicPower; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Magic Regeneration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción reg. de maná eterna");
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
            Item.buffType = BuffID.ManaRegeneration; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Night Owl Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de noctámbulo eterna");
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
            Item.buffType = BuffID.NightOwl; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Obsidian Skin Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de piel de obsidiana eterna");
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
            Item.buffType = BuffID.ObsidianSkin; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Rage Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de furia eterna");
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
            Item.buffType = BuffID.Rage; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Shine Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de brillo eterna");
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
            Item.buffType = BuffID.Shine; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Titan Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de titán eterna");
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
            Item.buffType = BuffID.Titan; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Warmth Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción del calor eterna");
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
            Item.buffType = BuffID.Warmth; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Wrath Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de ira eterna");
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
            Item.buffType = BuffID.Wrath; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
