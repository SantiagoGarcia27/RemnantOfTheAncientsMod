using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class EndlessGoldFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofGold);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Gold Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de oro eterno");
        }
    }
    public class EndlessIchorFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofIchor);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Ichor Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de icor eterno");
        }
    }
    public class EndlessFireFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofFire);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Fire Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de fuego eterno");
        }
    }
    public class EndlessCurcedFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofCursedFlames);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Cursed Flames Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de las llamas malditas eterno");
        }
    }
    public class EndlessVenomFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofVenom);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Venom Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de ponzoña eterno");
        }
    }
    public class EndlessPartyFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofParty);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Party Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco fiestero eterno");
        }
    }
    public class EndlessPoisonFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofPoison);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Poison Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de veneno eterno");  
        }
    }
    public class EndlessNaniteFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofNanites);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Nanites Flask");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Frasco de nanitos eterno");
        }
    }

}