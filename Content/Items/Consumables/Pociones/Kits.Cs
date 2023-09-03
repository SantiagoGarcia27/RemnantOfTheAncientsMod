using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using static RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Endless_Basic_Potion_Kit;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Kits;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    #region combat
    public class Combat_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Combat Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de combate");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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

    public class Endless_Combat_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Combat_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Combat Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de combat sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de combate eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemType<Tank_Potion_Kit>(),30)
            .AddIngredient(ItemType<Ranger_Potion_Kit>(),30)
            .AddIngredient(ItemType<Summon_Potion_Kit>(),30)
            .AddIngredient(ItemType<Mage_Potion_Kit>(),30)
            .AddIngredient(ItemType<Advanced_Potion_Kit>(),30)
            .AddIngredient(ItemType<Melee_Potion_Kit>(),30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<Endless_Melee_Potion_Kit>())
            .AddIngredient(ItemType<Endless_Tank_Potion_Kit>())
            .AddIngredient(ItemType<Endless_Ranger_Potion_Kit>())
            .AddIngredient(ItemType<Endless_Summon_Potion_Kit>())
            .AddIngredient(ItemType<Endless_Magic_Potion_Kit>())
            .AddIngredient(ItemType<Endless_Advanced_Potion_Kit>())
            .AddIngredient(ItemType<Endless_Melee_Potion_Kit>())
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region mining
    public class Mining_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Mining Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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

    public class Endless_Mining_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Mining_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Mining Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions minières sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mineria eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.SpelunkerPotion, 30)
            .AddIngredient(ItemID.ShinePotion, 30)
            .AddIngredient(ItemID.NightOwlPotion, 30)
            .AddIngredient(ItemID.MiningPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<EndlessSpelunker>(), 1)
            .AddIngredient(ItemType<EndlessShine>(), 1)
            .AddIngredient(ItemType<EndlessNightOwl>(), 1)
            .AddIngredient(ItemType<EndlessMining>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region summon
    public class Summon_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Summoner Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions d'invocateur");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de invocador");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Summon_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Summon_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Summoner Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions d'invocateur sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de invocador eterno");
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.SummoningPotion, 30)
            .AddIngredient(ItemID.InfernoPotion, 30)
            .AddIngredient(ItemType<Commander_Potion>(), 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
           .AddIngredient(ItemType<EndlessSummoning>(), 1)
           .AddIngredient(ItemType<EndlessInferno>(), 1)
           .AddIngredient(ItemType<Endless_Commander_Porion>(), 1)
           .AddTile(TileID.Bottles)
           .Register();
        }
    }
    #endregion

    #region melee
    public class Melee_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Melee Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de mêlée");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de guerrero");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
             .AddRecipeGroup("anyBeer", 1)
             .AddIngredient(ItemID.TitanPotion, 1)
             .AddTile(TileID.Bottles)
             .Register();
        }
    }

    public class Endless_Melee_Potion_Kit : ModelEndless
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Unlimited Melee Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de mêlée sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de guerrero eterno");
        }
        public override Item ItemBase => new(ItemType<Melee_Potion_Kit>());
        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.FlaskofIchor, 30)
            .AddRecipeGroup("anyBeer", 30)
            .AddIngredient(ItemID.TitanPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<EndlessIchorFlask>(), 1)
            .AddIngredient(ItemType<EndlessSake>(), 1)
            .AddIngredient(ItemType<EndlessTitan>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region mage
    public class Mage_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Magic Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions magiques");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mago");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<Combat_Potion_Kit>());
            Item.buffType = BuffType<Mage_Infusion>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MagicPowerPotion, 1)
            .AddIngredient(ItemID.ManaRegenerationPotion, 1)    
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Endless_Magic_Potion_Kit : ModelEndless
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Magic Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions magiques sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de mago eterno");
        }
        public override Item ItemBase => new(ItemType<Mage_Potion_Kit>());
        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.MagicPowerPotion, 30)
            .AddIngredient(ItemID.ManaRegenerationPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<EndlessManaRegeneration>(), 1)
            .AddIngredient(ItemType<EndlessManaPower>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region tank
    public class Tank_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Tank Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de réservoir");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de paladin");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Tank_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Tank_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Tank Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de réservoir sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de paladin eterno");
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.EndurancePotion, 30)
            .AddIngredient(ItemID.LifeforcePotion, 30)
            .AddIngredient(ItemID.HeartreachPotion, 30)
            .AddIngredient(ItemID.WarmthPotion, 30)
            .AddIngredient(ItemID.ThornsPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<EndlessEndurance>(), 1)
            .AddIngredient(ItemType<EndlessLifeForce>(), 1)
            .AddIngredient(ItemType<EndlessWarmth>(), 1)
            .AddIngredient(ItemType<EndlessThorns>(), 1)
            .AddIngredient(ItemType<EndlessHeaRtreach>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region ranger
    public class Ranger_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Ranger Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions Ranger");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de tirador");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Ranger_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Ranger_Potion_Kit>());

        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Unlimited Ranger Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions Ranger sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de tirador eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.AmmoReservationPotion, 30)
            .AddIngredient(ItemID.ArcheryPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<EndlessArchery>(), 1)
            .AddIngredient(ItemType<EndlessAmmoReservation>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region basic
    public class Basic_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Basic Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de base");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones basico");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Basic_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Basic_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Basic Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de base sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones basico eterno");

        }
        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.RegenerationPotion, 30)
            .AddIngredient(ItemID.SwiftnessPotion, 30)
            .AddIngredient(ItemID.IronskinPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
           .AddIngredient(ItemType<EndlessRegeneration>(), 1)
           .AddIngredient(ItemType<EndlessSwiftness>(), 1)
           .AddIngredient(ItemType<EndlessIronSkin>(), 1)
           .AddTile(TileID.Bottles)
           .Register();
        }
        #endregion

    #region fisher
    public class Fisher_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Fisher Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de pêche");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de pescador");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Fisher_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Fisher_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Fisher Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions de pêche sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de pescador eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();

            CreateRecipe()
            .AddIngredient(ItemID.CalmingPotion, 30)
            .AddIngredient(ItemID.FishingPotion, 30)
            .AddIngredient(ItemID.SonarPotion, 30)
            .AddIngredient(ItemID.CratePotion, 30)
            .AddIngredient(ItemID.LuckPotionGreater, 30)
            .AddIngredient(ItemID.InvisibilityPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<EndlessCalming>(), 1)
            .AddIngredient(ItemType<EndlessFishing>(), 1)
            .AddIngredient(ItemType<EndlessSonar>(), 1)
            .AddIngredient(ItemType<EndlessCrate>(), 1)
            .AddIngredient(ItemType<EndlessInvisibility>(), 1)
            .AddIngredient(ItemType<EndlessGreaterLuck>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region advanced
    public class Advanced_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Advanced Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions avancées");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones avanzado");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Advanced_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Advanced_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Advanced Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions avancées sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones avanzado eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemType<Basic_Potion_Kit>(), 30)
            .AddIngredient(ItemID.WrathPotion, 30)
            .AddIngredient(ItemID.RagePotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<Endless_Basic_Potion_Kit>(), 1)
            .AddIngredient(ItemType<EndlessRage>(), 1)
            .AddIngredient(ItemType<EndlessWrath>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region exploration
    public class Exploration_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Exploration Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions d'exploration");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de exploración");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Exploration_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Exploration_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Exploration Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions d'exploration sasn fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones de exploración eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemType<Mining_Potion_Kit>(), 30)
            .AddIngredient(ItemType<Supe_Sense_Potion>(), 30)
            .AddIngredient(ItemType<Gravitation_Control_Potion>(), 30)
            .AddIngredient(ItemID.ObsidianSkinPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<Endless_Mining_Potion_Kit>(), 1)
            .AddIngredient(ItemType<EndlessObsidian>(), 1)
            .AddIngredient(ItemType<Endless_Supe_Sense_Potion>(), 1)
            .AddIngredient(ItemType<Endless_Gravitation_Control_Potion>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region poseidon
    public class Poseidon_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Poseidon Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions Poséidon");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit Poseídón");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
    public class Endless_Poseidon_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Poseidon_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Poseidon Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions Poséidon sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit Poseídón eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemType<Fisher_Potion_Kit>(), 30)
            .AddIngredient(ItemType<Fish_Potion>(), 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<Endless_Fisher_Potion_Kit>(), 1)
            .AddIngredient(ItemType<Endless_Fish_Potion>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion

    #region ultimate
    public class Ultimate_Potion_Kit : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Ultimate Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions ultimes");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones definitivo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = 8;
            Item.buffType = BuffType<Ultimate_Infusion>();
            Item.buffTime = 25400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Combat_Potion_Kit>())
            .AddIngredient(ItemType<Poseidon_Potion_Kit>())
            .AddIngredient(ItemType<Exploration_Potion_Kit>())
            .AddIngredient(ItemType<Money_Collector_Potion>())
            .AddIngredient(ItemID.BiomeSightPotion)
            .AddIngredient(ItemID.BuilderPotion)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public class Endless_Ultimate_Potion_Kit : ModelEndless
    {
        public override Item ItemBase => new(ItemType<Ultimate_Potion_Kit>());
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Ultimate Potions Kit");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Kit de potions ultimes sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Kit de pociones definitivo eterno");
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
            .AddIngredient(ItemType<Combat_Potion_Kit>(),30)
            .AddIngredient(ItemType<Poseidon_Potion_Kit>(), 30)
            .AddIngredient(ItemType<Exploration_Potion_Kit>(), 30)
            .AddIngredient(ItemType<Money_Collector_Potion>(), 30)
            .AddIngredient(ItemID.BuilderPotion,30)
            .AddIngredient(ItemID.BiomeSightPotion, 30)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemType<Endless_Combat_Potion_Kit>(), 1)
            .AddIngredient(ItemType<Endless_Poseidon_Potion_Kit>(), 1)
            .AddIngredient(ItemType<Endless_Exploration_Potion_Kit>(), 1)
            .AddIngredient(ItemType<Money_Collector_Potion>(), 1)
            .AddIngredient(ItemType<EndlessBuilder>(), 1)
            .AddIngredient(ItemType<EndlessBiomeSightPotion>(), 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    #endregion
    }
}