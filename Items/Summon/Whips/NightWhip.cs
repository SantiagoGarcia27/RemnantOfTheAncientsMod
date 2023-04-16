using RemnantOfTheAncientsMod.Items.Items;
using RemnantOfTheAncientsMod.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Items.Summon.Whips
{
    public class NightWhip : ModItem
	{
		public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Twilight");
            Tooltip.SetDefault("9 summon tag damage" +
                "\nYour summons will focus struck enemies" +
                "\nPerforms better against multiple targets than most whips");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Crépuscule");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "9 dégâts de balise d'invocation" +
                "\nVos invocations concentreront les ennemis frappés" +
                "\nFonctionne mieux contre plusieurs cibles que la plupart des fouets");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Crepúsculo");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "9 daño de etiqueta de invocación" +
                "\nTu convocatoria se centrará en los enemigos golpeados." +
                "\nFunciona mejor contra múltiples objetivos que la mayoría de los látigos.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public static readonly int Damage = 20;
        public static readonly int StrongDamage = Damage * 2;

        public override void SetDefaults()
		{
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 45;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<NightWhipProjectile>();
            Item.shootSpeed = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.channel = false; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
        }
        //  public override bool AltFunctionUse(Player player) => true;
        //public override bool CanShoot(Player player)
        //{
        //  /*  if (player.altFunctionUse == 2)
        //    {
        //        Item.channel = false;
        //        Item.damage = Damage;
        //        return true;
        //    }
        //    else
        //    {
        //        Item.channel = true;
        //        Item.damage = StrongDamage;
        //        return true;
        //    }*/
        //}
        //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        //{
        //    if (player.altFunctionUse != 2)
        //    {
        //        Item.channel = false;
        //        Item.damage = Damage;
        //    }
        //    else
        //    {
        //        Item.channel = true;
        //        Item.damage = StrongDamage;
        //    }
        //    return base.Shoot(player, source, position, velocity, type, damage, knockback);
        //}

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<NightBar>(5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
