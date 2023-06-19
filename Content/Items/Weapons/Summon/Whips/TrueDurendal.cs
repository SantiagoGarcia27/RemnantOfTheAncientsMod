using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class TrueDurendal : ModItem
	{
		public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("True Durendal");
            Tooltip.SetDefault("14 summon tag damage" +
                "\nYour summons will focus struck enemies" +
                "\nStrike enemies to gain attack speed and summon a friendly Excalibur");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vrai Durendal");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "5 dégâts de balise d'invocation" +
                "\nVos invocations concentreront les ennemis frappés" +
                "\nFrappez les ennemis pour gagner en vitesse d'attaque et invoquer un Excalibur amical");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Durendal verdadero");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "5 daño de etiqueta de invocación" +
                "\nTu invocaciones se centrará en los enemigos golpeados." +
                "\nAl golpear un enemigo ganas velocidad de ataque e invoca a una Excalibur que lucha por ti.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }   
        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 60;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<TrueDurendalProjectile>();
            Item.shootSpeed = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = new Item(ItemID.TrueExcalibur).value;
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
                .AddIngredient(ItemID.SwordWhip, 1)
                .AddIngredient(ItemID.ChlorophyteBar, 24)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
