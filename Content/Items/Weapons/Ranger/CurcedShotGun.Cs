using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
	public class CurcedShotGun : ModItem
	{
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(5);
        public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Cursed Shotgun");
//Tooltip.SetDefault("5% chance of not spending ammo");

//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Escopeta Maldita");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Probabilidad del 5% de no gastar munición ");

//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fusil de chasse maudit");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "5% de chances de ne pas dépenser de munitions ");

CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
Item.damage = 30;
Item.DamageType = DamageClass.Ranged;
Item.width = 80;
Item.height = 40;
Item.useTime = 46;
Item.useAnimation = 46;
Item.useStyle = ItemUseStyleID.Shoot;
Item.noMelee = true;
Item.knockBack = 5;
Item.value = 100000;
Item.rare = ItemRarityID.LightRed;
Item.UseSound = SoundID.Item36;
Item.autoReuse = true;
Item.shoot = ProjectileID.Starfury;
Item.shootSpeed = 100f;
Item.useAmmo = AmmoID.Bullet;

		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
if (type == ProjectileID.Bullet) type = ProjectileID.CursedBullet;

const int NumProjectiles = 8;

for (int i = 0; i < NumProjectiles; i++)
{
	Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));
	newVelocity *= 1f - Main.rand.NextFloat(0.3f);
	Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
}

return false;
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
return Main.rand.NextFloat() >= 0.05f;
		}
		public override Vector2? HoldoutOffset()
		{
return new Vector2(-5, 0);
		}
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.CursedFlame, 40)
.AddIngredient(ItemID.Shotgun, 1)
.AddTile(TileID.MythrilAnvil)

.Register();
		}
	}
}
