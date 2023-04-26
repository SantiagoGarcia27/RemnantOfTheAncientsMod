using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Pociones
{
    public class EndlessGoldFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofGold);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Gold Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de oro eterno");
        }
    }
    public class EndlessIchorFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofIchor);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Ichor Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de icor eterno");
        }
    }
    public class EndlessFireFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofFire);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Fire Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de fuego eterno");
        }
    }
    public class EndlessCurcedFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofCursedFlames);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Cursed Flames Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de las llamas malditas eterno");
        }
    }
    public class EndlessVenomFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofVenom);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Venom Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de ponzoña eterno");
        }
    }
    public class EndlessPartyFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofParty);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Party Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco fiestero eterno");
        }
    }
    public class EndlessPoisonFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofPoison);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Poison Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de veneno eterno");  
        }
    }
    public class EndlessNaniteFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofNanites);
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Endless Nanites Flask");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de nanitos eterno");
        }
    }

}