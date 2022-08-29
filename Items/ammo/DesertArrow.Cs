using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.ammo
{
	public class DesertArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Arrow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Flèche du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Flecha Desertica");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;   
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 2;
			Item.shoot = ModContent.ProjectileType<DesertArrowP>();
			Item.shootSpeed = 26f;
			Item.ammo = AmmoID.Arrow;
		}

		

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.WoodenArrow, 200)
			.AddIngredient(ModContent.ItemType<Sand_escense>())
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}