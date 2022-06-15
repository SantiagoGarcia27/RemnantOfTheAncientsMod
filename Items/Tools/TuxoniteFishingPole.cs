using Microsoft.Xna.Framework;
using opswordsII.Projectiles.Bobbers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Tools
{
	public class TuxoniteFishingPole : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Fishing Pole");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wędka Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Canne à pêche en tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Caña de pescar de tusonita");
			Tooltip.SetDefault("");

			/* DisplayName.AddTranslation(GameCulture.Russian, "Туксонитовый топор");
			DisplayName.AddTranslation(GameCulture.Chinese, "毒x斧");*/
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.useAnimation = 8;
			Item.useTime = 8;
			Item.useStyle = 1;
			Item.UseSound = SoundID.Item1;
			Item.rare = 0;
			Item.fishingPole = 21;
			Item.shootSpeed = 15f;
			Item.shoot = ModContent.ProjectileType<TuxoniteBobber>();
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",7)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}