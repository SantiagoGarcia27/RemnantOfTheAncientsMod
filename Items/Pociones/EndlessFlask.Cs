using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace opswordsII.Items.Pociones
{
    public class EndlessGoldF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofGold;
        public override int ItemId => ItemID.FlaskofGold;
        public override int BuffId => BuffID.WeaponImbueGold;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gold Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de oro eterno");
            base.SetStaticDefaults();
        }
    }
    public class EndlessIchorF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofIchor;
        public override int ItemId => ItemID.FlaskofIchor;
        public override int BuffId => BuffID.WeaponImbueIchor;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ichor Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de icor eterno");
            base.SetStaticDefaults();
        }
    }
    public class EndlessFireF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofFire;
        public override int ItemId => ItemID.FlaskofFire;
        public override int BuffId => BuffID.WeaponImbueFire;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Fire Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de fuego eterno");
            base.SetStaticDefaults();
        }
    }
    public class EndlessCurcedF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofCursedFlames;
        public override int ItemId => ItemID.FlaskofCursedFlames;
        public override int BuffId => BuffID.WeaponImbueCursedFlames;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Cursed Flames Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de las llamas malditas eterno");
            base.SetStaticDefaults();
        }
    }
    public class EndlessPonF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofVenom;
        public override int ItemId => ItemID.FlaskofVenom;
        public override int BuffId => BuffID.WeaponImbueVenom;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Venom Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de ponzoña eterno");
            base.SetStaticDefaults();
        }
    }
    public class EndlessPartyF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofParty;
        public override int ItemId => ItemID.FlaskofParty;
        public override int BuffId => BuffID.WeaponImbueConfetti;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Party Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco fiestero eterno");
            base.SetStaticDefaults();
        }
    }
    public class EndlessVenomF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofPoison;
        public override int ItemId => ItemID.FlaskofPoison;
        public override int BuffId => BuffID.WeaponImbuePoison;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Poison Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de veneno eterno");
            base.SetStaticDefaults();
        }
    }
    public class EndlessNaniteF : BasePotions
    {
        public override int TextureID => ItemID.FlaskofNanites;
        public override int ItemId => ItemID.FlaskofNanites;
        public override int BuffId => BuffID.WeaponImbueNanites;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Nanites Flask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de nanitos eterno");
            base.SetStaticDefaults();
        }


    }
}