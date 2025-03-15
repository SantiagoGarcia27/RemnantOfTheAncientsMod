using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Bullet
{
	public class ReinforcedIronBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
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
			Item.knockBack = 7.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<ReinforcedBulletP>();
			Item.shootSpeed = 0.5f;
			Item.ammo = AmmoID.Bullet;
		}
	}
}