using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class Intestine : ModItem
	{
		public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Intestine");
            Tooltip.SetDefault("5 summon tag damage" +
                "\nYour summons will focus struck enemies" +
                "\nHeal 1hp per hit");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Intestine");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "5 dégâts de balise d'invocation" +
                "\nVos invocations concentreront les ennemis frappés" +
                "\nSoignez 1 PV par coup");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Intestino");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "5 daño de etiqueta de invocación" +
                "\nTu invocaciones se centrará en los enemigos golpeados." +
                "\nCura 1 punto de vida por golpe");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 16;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<BloodyWhipProjectile>();
            Item.shootSpeed = 4;
            Item.value = new Item(ItemID.BloodButcherer).value;
            Item.useStyle = ItemUseStyleID.Swing;
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
                .AddIngredient(ItemID.CrimtaneBar, 15)
                .AddIngredient(ItemID.TissueSample, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
