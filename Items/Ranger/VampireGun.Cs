using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Ranger
{
	public class VampireGun: ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Vampire Gun");
//Tooltip.SetDefault("healing gun.");

            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vampire Gun");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "pistolet guérisseur");

//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pistola Vampírica");
           // //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "una pistola curativa");

CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
Item.damage = 50;
Item.DamageType = DamageClass.Ranged;
Item.width = 8;
Item.height = 2;
Item.useTime = 4;
Item.useAnimation = 12;
Item.useStyle = ItemUseStyleID.Shoot;
Item.noMelee = true;
Item.knockBack = 5;
Item.value = 10000;
Item.rare = ItemRarityID.Yellow;
Item.UseSound = SoundID.Item38;
Item.autoReuse = true;
Item.shoot = ProjectileID.PurificationPowder;
Item.shootSpeed = 2f;
Item.useAmmo = AmmoID.Bullet;
Item.reuseDelay = 14;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
if (Main.rand.NextFloat() <= (float)1 / 2) Projectile.NewProjectile(source, position, velocity*4, ProjectileID.VampireKnife, damage, 1, player.whoAmI);

return true;
		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
return !(player.itemAnimation < Item.useAnimation - 2);
		}

		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.TheUndertaker, 1)
            .AddIngredient(ItemID.VampireKnives, 1)
.AddTile(TileID.MythrilAnvil)
.Register();
		}
	}
}
 