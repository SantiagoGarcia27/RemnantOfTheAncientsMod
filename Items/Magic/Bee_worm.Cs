using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace opswordsII.Items.Magic
{
    public class Bee_worm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Worm");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Bee Worm");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Ver d'abeille");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Gusano de abejas");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 69;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 9;
			Item.rare = ItemRarityID.Yellow;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 9;
			Item.UseSound = SoundID.Item16;
			Item.useStyle = 5;
			Item.shootSpeed = 14f;
			Item.useAnimation = 9;
			Item.shoot = ProjectileID.Bee;
			Item.value = 550000;
			Item.autoReuse = true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 2) Projectile.NewProjectile(source, position, velocity, ProjectileID.Wasp, 100, 1, player.whoAmI);
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.BeeGun, 2)
			.AddIngredient(ItemID.WaspGun, 1)
			.AddIngredient(ItemID.SoulofMight, 3)
			.AddIngredient(ItemID.SoulofSight, 3)
			.AddIngredient(ItemID.SoulofFright, 3)
			.AddTile(TileID.HoneyDispenser)
			.Register();
		}
	}
}
