using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Mele.Gem.Geode
{
    public class The_Geode_Red : ModItem
	{
		public override void SetStaticDefaults()
		{
			
	        DisplayName.SetDefault("The Geode");
			Tooltip.SetDefault("Inflict fire on enemies");
	
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger des tirs aux ennemis");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego a los enemigos");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.knockBack = 5;
			Item.value = 1200;
			Item.rare = 3;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;

		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 400);
		}	
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<The_Geode>())
			.AddIngredient(ItemID.LargeRuby,1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
