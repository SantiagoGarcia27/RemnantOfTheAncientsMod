using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class TrueTwilight : ModItem
	{
        int tagDamage = 13;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(tagDamage);
        public override void SetStaticDefaults() 
        {
           // //DisplayName.SetDefault("True Twilight");
            //Tooltip.SetDefault("13 summon tag damage" +
           //    "\nYour summons will focus struck enemies" +
           //    "\nPerforms better against multiple targets than most whips" +
           //    "\nStrike enemies to gain 7 of defense to all players");

           //// //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Véritable crépuscule");
           // //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "13 dégâts de balise d'invocation" +
           //      "\nVos invocations concentreront les ennemis frappés" +
           //      "\nFonctionne mieux contre plusieurs cibles que la plupart des fouets" +
           //      "\nFrappez les ennemis pour gagner 7 points de défense à tous les joueurs");

           //// //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Crepúsculo verdadero");
           // //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "13 daño de etiqueta de invocación" +
           //     "\nTu invocaciones se centrará en los enemigos golpeados." +
           //     "\nFunciona mejor contra múltiples objetivos que la mayoría de los látigos." +
           //     "\nAtacar un enemigo te otorga 7 de defensa a todos los jugadores");


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
		{
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 56;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<TrueTwilightProjectile>();
            Item.shootSpeed = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = new Item(ItemID.TrueNightsEdge).value;
            Item.useTime = 30;
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
                .AddIngredient<Twilight>(1)
                .AddIngredient(ItemID.SoulofMight,30)
                .AddIngredient(ItemID.SoulofFright, 30)
                .AddIngredient(ItemID.SoulofSight, 30)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
