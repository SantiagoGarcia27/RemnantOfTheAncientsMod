using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile.Desert;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class DesertTome : ModItem
	{
		public override void SetStaticDefaults()
		{	
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 18;
			Item.rare = ItemRarityID.Orange;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item84;
			Item.shoot = ModContent.ProjectileType<DesertTyphoonFriendly>();
			Item.value = Item.sellPrice(silver: 325);
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			 .AddIngredient(ItemID.Book, 1)
			 .AddIngredient(ModContent.ItemType<Sand_escense>(), 5)
			 .AddTile(TileID.Bookcases)
			 .Register();
		}
	}
}