using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public class EndlesCursed : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CursedBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Cursed Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette incurvée sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de malditas eterna");
        }
    }
    public class Endles_High_Velocity_Bullet : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.HighVelocityBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless High Velocity Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette à haute vitesse sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Balas de Alta Velocidad");
        }
    }
    public class EndlesChlorophyte : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ChlorophyteBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Chlorophyte Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de Chlorophyte Sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Clorofita Eterna");
           
        }
    }
    public class EndlesCrystal : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CrystalBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Crystal Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette en cristal sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de cristal eterna");
        }
    }
    public class EndlesExplosive : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ExplodingBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Exploding Bullet Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de balles explosives sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de balas explosivas Eterna");
        }
    }
    public class EndlesGold : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.GoldenBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Gold Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sac à balles en or éternel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de oro eterna");        
        }
    }
    public class Endless_Ichor_Pouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.IchorBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Ichor Pouch ");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette Ichor sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de Ichor eterna");
           
        }
    }
    public class EndlesLuminite_Bullet : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.MoonlordBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/" + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Luminite Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de luminite sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de Luminita eterna");
           
        }
    }

        //public class EndelssAmmoGenerator : ModItem
        //{
        //       public class EndelssAmmoGeneratorLoader : ILoadable
        //       {
        //           public void Load(Mod mod)
        //           {
        //               //for (int i = 1; i < ItemLoader.ItemCount; i++)
        //               //{
        //               //    if (ContentSamples.ItemsByType[i].ammo > 0)
        //               //    {
        //               //        mod.AddContent(new EndelssAmmoGenerator(i));
        //               //    }
        //               //}
        //           }

        //           public void Unload()
        //           {
        //           }
        //       }

        //       protected override bool CloneNewInstances => true;
        //       private readonly int BaseID;

        //       public override string Name => GetInternalNameFromID(BaseID);

        //       public static string GetInternalNameFromID(int idBase)
        //       {
        //           Item item = ContentSamples.ItemsByType[idBase];

        //           if (item.ammo > 0)
        //           {
        //               return "Endelss "+ item.Name;
        //           }
        //           throw new Exception("Invalid style");
        //       }

        //       public EndelssAmmoGenerator(int ItemID)
        //       {
        //           this.BaseID = ItemID;
        //       }

        //       public override void SetDefaults()
        //       {
        //           Item.CloneDefaults(BaseID);
        //           Item.maxStack = 1;
        //           Item.consumable = false;
        //           Item.value += Item.sellPrice(0, 1, 0, 0);
        //           Item.rare += 1;
        //       }

        //       public override void AddRecipes()
        //       {
        //           CreateRecipe()
        //               .AddIngredient(BaseID)
        //               .Register();
        //       }
        //   }
    }
