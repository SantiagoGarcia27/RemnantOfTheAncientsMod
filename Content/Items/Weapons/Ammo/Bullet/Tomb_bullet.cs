using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Bullet
{
	public class Tomb_bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 3;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 3.5f;
			Item.value = 0;
			Item.rare = ItemRarityID.Blue;
			Item.shoot = ModContent.ProjectileType<Tomb_BulletP>();
			Item.shootSpeed = 3f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			CreateRecipe(50)
			.AddRecipeGroup("Tumbas")
			.AddCondition(Condition.InGraveyard)
			.Register();
		}
	}
}