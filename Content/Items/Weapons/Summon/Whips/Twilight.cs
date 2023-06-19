using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class Twilight : ModItem
	{
		public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Twilight");
            Tooltip.SetDefault("9 summon tag damage" +
                "\nYour summons will focus struck enemies" +
                "\nPerforms better against multiple targets than most whips" +
                "\nStrike enemies to gain 5 of defense");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Crépuscule");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "9 dégâts de balise d'invocation" +
                "\nVos invocations concentreront les ennemis frappés" +
                "\nFonctionne mieux contre plusieurs cibles que la plupart des fouets" +
                "\nFrappez les ennemis pour gagner 5 points de défens");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Crepúsculo");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "9 daño de etiqueta de invocación" +
                "\nTu invocaciones se centrará en los enemigos golpeados." +
                "\nFunciona mejor contra múltiples objetivos que la mayoría de los látigos."+
                "\nAtacar un enemigo te otorga 5 de defensa");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }  
        public override void SetDefaults()
		{
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 40;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<NightWhipProjectile>();
            Item.shootSpeed = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.value = new Item(ItemID.NightsEdge).value;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.channel = false; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = RemnantOfTheAncientsMod.CalamityMod != null;
        }
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
