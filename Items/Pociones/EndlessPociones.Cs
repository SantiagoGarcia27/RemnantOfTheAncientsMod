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
    public class EndlessHea : BasePotions
    { 
        public override string Texture => "Terraria/Images/Item_" + ItemID.HeartreachPotion;
        public override int ItemId => ItemID.HeartreachPotion;
        public override int BuffId => BuffID.Heartreach;
        public override int TextureID => ItemID.HeartreachPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Heartreach Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de alcanzacorazón eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessInf : BasePotions
    {

        public override string Texture => "Terraria/Images/Item_" + ItemID.InfernoPotion;
        public override int ItemId => ItemID.InfernoPotion;
        public override int BuffId => BuffID.Inferno;
        public override int TextureID => ItemID.InfernoPotion;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Inferno Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción del infierno eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public class EndlessIron : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.IronskinPotion;
        public override int TextureID => ItemID.IronskinPotion;
        public override int ItemId => ItemID.IronskinPotion;
        public override int BuffId => BuffID.Ironskin;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ironskin Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de piel de hierro eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessAmmo : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.AmmoReservationPotion;
        public override int TextureID => ItemID.AmmoReservationPotion;
        public override int BuffId => BuffID.AmmoReservation;
        public override int ItemId => ItemID.AmmoReservationPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ammo Reservation Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de munición eterna");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
    public class EndlessArch : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ArcheryPotion;
        public override int TextureID => ItemID.ArcheryPotion;
        public override int BuffId => BuffID.Archery;
        public override int ItemId => ItemID.ArcheryPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Archery Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de arquero eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }



    public class EndlessBuil : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.BuilderPotion;
        public  override int TextureID => ItemID.BuilderPotion;
        public override int ItemId => ItemID.BuilderPotion;
        public override int BuffId => BuffID.Builder;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Builder Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción constructora eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessCalm : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.CalmingPotion;
        public override int TextureID => ItemID.CalmingPotion;
        public override int ItemId => ItemID.CalmingPotion;
        public override int BuffId => BuffID.Calm;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Calming Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción tranquilizadora eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessCra : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.CratePotion;
        public override int TextureID => ItemID.CratePotion;
        public override int BuffId => BuffID.Crate;
        public override int ItemId => ItemID.CratePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Crate Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de caja eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessDang : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.TrapsightPotion;
        public override int BuffId => BuffID.Dangersense;
        public override int ItemId => ItemID.TrapsightPotion;
        public override int TextureID => ItemID.TrapsightPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Dangersense Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de sentido del peligro eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }

    public class EndlessFish : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FishingPotion;
        public override int BuffId => BuffID.Fishing;
        public override int ItemId => ItemID.FishingPotion;
        public override int TextureID => ItemID.FishingPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Fishing Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de pesca eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }


    public class EndlessRe : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.RegenerationPotion;
        public override int BuffId => BuffID.Regeneration;
        public override int ItemId => ItemID.RegenerationPotion;
        public override int TextureID => ItemID.RegenerationPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Regeneration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de regeneración eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessSo : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SonarPotion;
        public override int BuffId => BuffID.Sonar;
        public override int ItemId => ItemID.SonarPotion;
        public override int TextureID => ItemID.SonarPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Sonar Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de sonar eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessSp : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SpelunkerPotion;
        public override int BuffId => BuffID.Spelunker;
        public override int ItemId => ItemID.SpelunkerPotion;
        public override int TextureID => ItemID.SpelunkerPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Spelunker Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de espeleógo eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessSu : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SummoningPotion;
        public override int BuffId => BuffID.Summoning;
        public override int ItemId => ItemID.SummoningPotion;
        public override int TextureID => ItemID.SummoningPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Summoning Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de invocación eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessSwif : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.SwiftnessPotion;
        public override int BuffId => BuffID.Swiftness;
        public override int ItemId => ItemID.SwiftnessPotion;
        public override int TextureID => ItemID.SwiftnessPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Swiftness Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de rapidez eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessTh : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ThornsPotion;
        public override int BuffId => BuffID.Thorns;
        public override int ItemId => ItemID.ThornsPotion;
        public override int TextureID => ItemID.ThornsPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Thorns Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de espinas eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessBat : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.BattlePotion;
        public override int BuffId => BuffID.Battle;
        public override int ItemId => ItemID.BattlePotion;
        public override int TextureID => ItemID.BattlePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Battle Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de batalla eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessEndu : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.EndurancePotion;
        public override int BuffId => BuffID.Endurance;
        public override int ItemId => ItemID.EndurancePotion;
        public override int TextureID => ItemID.EndurancePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Endurance Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de resistencia eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessFall : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.FeatherfallPotion;
        public override int BuffId => BuffID.Featherfall;
        public override int ItemId => ItemID.FeatherfallPotion;
        public override int TextureID => ItemID.FeatherfallPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Featherfall Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de caída de pluma eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessGills : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.GillsPotion;
        public override int BuffId => BuffID.Gills;
        public override int ItemId => ItemID.GillsPotion;
        public override int TextureID => ItemID.GillsPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gills Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de agallas eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessHunt : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.HunterPotion;
        public override int BuffId => BuffID.Hunter;
        public override int ItemId => ItemID.HunterPotion;
        public override int TextureID => ItemID.HunterPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Hunter Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de cazador eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessInv : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.InvisibilityPotion;
        public override int BuffId => BuffID.Invisibility;
        public override int ItemId => ItemID.InvisibilityPotion;
        public override int TextureID => ItemID.InvisibilityPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Invisibility Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de invisibilidad eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessLF : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.LifeforcePotion;
        public override int BuffId => BuffID.Lifeforce;
        public override int ItemId => ItemID.LifeforcePotion;
        public override int TextureID => ItemID.LifeforcePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Lifeforce Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de fuerza vital eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessMi : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MiningPotion;
        public override int BuffId => BuffID.Mining;
        public override int ItemId => ItemID.MiningPotion;
        public override int TextureID => ItemID.MiningPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Mining Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de minería eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessMP : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MagicPowerPotion;
        public override int BuffId => BuffID.MagicPower;
        public override int ItemId => ItemID.MagicPowerPotion;
        public override int TextureID => ItemID.MagicPowerPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Magic Power Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de poder mágico eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }

    public class EndlessMR : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ManaRegenerationPotion;
        public override int BuffId => BuffID.ManaRegeneration;
        public override int ItemId => ItemID.ManaRegenerationPotion;
        public override int TextureID => ItemID.ManaRegenerationPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Magic Regeneration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción reg. de maná eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessNig : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.NightOwlPotion;
        public override int BuffId => BuffID.NightOwl;
        public override int ItemId => ItemID.NightOwlPotion;
        public override int TextureID => ItemID.NightOwlPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Night Owl Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de noctámbulo eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessOb : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ObsidianSkinPotion;
        public override int BuffId => BuffID.ObsidianSkin;
        public override int ItemId => ItemID.ObsidianSkinPotion;
        public override int TextureID => ItemID.ObsidianSkinPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Obsidian Skin Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de piel de obsidiana eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessRa : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.RagePotion;
        public override int BuffId => BuffID.Rage;
        public override int ItemId => ItemID.RagePotion;
        public override int TextureID => ItemID.RagePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Rage Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de furia eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessSh : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ShinePotion;
        public override int BuffId => BuffID.Shine;
        public override int ItemId => ItemID.ShinePotion;
        public override int TextureID => ItemID.ShinePotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Shine Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de brillo eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessTi : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.TitanPotion;
        public override int BuffId => BuffID.Titan;
        public override int ItemId => ItemID.TitanPotion;
        public override int TextureID => ItemID.TitanPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Titan Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de titán eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessWa : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.WarmthPotion;
        public override int BuffId => BuffID.Warmth;
        public override int ItemId => ItemID.WarmthPotion;
        public override int TextureID => ItemID.WarmthPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Warmth Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción del calor eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
    public class EndlessWr : BasePotions
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.WrathPotion;
        public override int BuffId => BuffID.Wrath;
        public override int ItemId => ItemID.WrathPotion;
        public override int TextureID => ItemID.WrathPotion;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Wrath Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de ira eterna");
             CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

    }
}
