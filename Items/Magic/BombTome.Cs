using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Magic
{
	public class BombTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bomb Tome");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Tom Bomb");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tome de bombe");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tomo Explosivo");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 15;
			Item.rare = ItemRarityID.Pink;
			Item.width = 30;
			Item.height = 32;
			Item.useTime = 9;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 9;
			Item.value = 100000;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Grenade;
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.damage = 70;
				Item.useTime = 12;
				Item.useAnimation = 12;
			}
		}
	
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Grenade, 103)
			.AddIngredient(ItemID.SpellTome, 1)
			.AddTile(TileID.Bookcases)
			.Register();
		}
	}
}
