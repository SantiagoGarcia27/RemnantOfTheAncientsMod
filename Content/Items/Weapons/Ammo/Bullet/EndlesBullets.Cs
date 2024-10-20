using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Bullet
{
    public class EndlesCursedPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CursedBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Cursed Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette incurvée sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de malditas eterna");
        }
    }
    public class EndlesHighVelocityPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.HighVelocityBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless High Velocity Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette à haute vitesse sans Fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Balas de Alta Velocidad");
        }
    }
    public class EndlesChlorophytePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ChlorophyteBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Chlorophyte Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de Chlorophyte Sans Fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Clorofita Eterna");
           
        }
    }
    public class EndlesCrystalPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CrystalBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Crystal Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette en cristal sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de cristal eterna");
        }
    }
    public class EndlesExplosivePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ExplodingBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Exploding Bullet Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de balles explosives sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de balas explosivas Eterna");
        }
    }
    public class EndlesGoldPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.GoldenBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Gold Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sac à balles en or éternel");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de oro eterna");        
        }
    }
    public class EndlessIchorPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.IchorBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Ichor Pouch ");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette Ichor sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de Ichor eterna");
           
        }
    }
    public class EndlesLuminitePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.MoonlordBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Luminite Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de luminite sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de Luminita eterna");
           
        }
    }
    public class EndlesMeteoritePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.MeteorShot);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Meteorite Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de météorite sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de meteorito eterna");
        }
    }
    public class EndlesNanoPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.NanoBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Nano Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette Nano Bullets sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Nanobalas eterna");
        }
    }
    public class EndlesPartyPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.PartyBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Party Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sac à balles Eternal Party");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas fiesteras eterna");
        }
    }
    public class EndlesVenomPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.VenomBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Venom Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sac éternel de balles empoisonnées");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas Envenenadas eterna");
        }
    }
    public class EndlesSilverPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.SilverBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Silver Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bourse d'argent sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de plata eterna");
        }
    }
    public class EndlessTombPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<Tomb_bullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Grave Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle tombale sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala de tumba eterna");
        }
    }
    public class EndlessIcePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<ice_bullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Frozen Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette gelée sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa congelada eterna");
        }
    }
    public class EndlessWrathSlimePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<WrathSlimeBullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Angry Slime Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de Slime en Colère Sans Fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Bala de Slime Furioso");
        }
    }
    public class EndlessFirePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<FireBullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Flaming Pouch");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle enflammée sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Bala llameante ");
        }
    }
}
