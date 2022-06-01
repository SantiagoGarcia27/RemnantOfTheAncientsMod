using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Magic
{
	public class Bloodtray : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Blood Ray");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Blood Ray");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rayon de sang");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rayo de sangre");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 9;
			Item.rare = 1;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 25;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 14f;
			Item.useAnimation = 49;
			Item.shoot = 125;
			Item.value = Item.sellPrice(silver: 30);
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CrimtaneBar, 10)
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
