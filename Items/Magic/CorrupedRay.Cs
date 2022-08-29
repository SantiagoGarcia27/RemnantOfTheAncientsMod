using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Magic
{
	public class CorrupedRay : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Shadow Ray");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Promień cienia");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rayon d'ombre");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rayo de sombra");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 9;
			Item.rare = ItemRarityID.Blue;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 23;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 49;
			Item.shoot = ProjectileID.AmethystBolt;
			Item.value = Item.sellPrice(silver: 30);
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.DemoniteBar, 10)
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
