using Terraria.Localization;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public class EndlesCoperCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.CopperCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Copper Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce de Cuivre Sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda de Cobre Eterna");
        }
    }
    public class EndlesSilverCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.SilverCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Silver Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'argent sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda de plata infinita");
        }
    }
    public class EndlesGoldCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.GoldCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gold Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'or sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Monedas de oro infinitas");
        }
    }
    public class EndlesPlatinumCoin : EndlessBase
    {
        public override Item ItemBase => new(ItemID.PlatinumCoin);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gold Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'or sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Monedas de oro infinitas");
        }
    }
}
