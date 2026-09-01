using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.star
{
	public class StarCannonV2 : ModItem
	{
        public int NotConsumeAmmoChance = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NotConsumeAmmoChance);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 105;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 40;
			Item.useTime = 9;
			Item.useAnimation = 9;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
			Item.value = 100000;
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item10;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.SuperStar;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.FallenStar;
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.damage = 70;
				Item.useTime = 17;
				Item.useAnimation = 17;
			}
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 5)
			{
				Projectile.NewProjectile(source, position, velocity, ProjectileID.Starfury, damage * 2, 1, player.whoAmI);
				return false;
			}
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= NotConsumeAmmoChance / 100f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.SuperStarCannon)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddIngredient(ItemID.SoulofMight, 5)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
