using Terraria.Localization;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public class EndlesFlare : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Flare);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Flare");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Flare sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bengala Eterna");
        }
    }
    public class EndlesBlueFlare : EndlessBase
    {
        public override Item ItemBase => new(ItemID.BlueFlare);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Blue Flare");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fusée bleue sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bengala Azul Eterna");
        }
    }
    public class EndlesStar : EndlessBase
    {
        public override Item ItemBase => new(ItemID.FallenStar);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Star");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Étoile sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Estrella Eterna");
        }
    }
    public class EndlesNail : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Nail);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Nail");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Clou sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Clavo Infinito");
        }
    }
    public class EndlesGel : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Gel);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Gel sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Gel infinito");
        }
    }
    public class EndlesStake : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Stake);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Stake");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jeu sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Estaca eterna");
        }
    }
    public class EndlesSnowBall : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Snowball);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Snow Ball");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Boule de neige sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bola de nieve eterna");
        }
    }
}
