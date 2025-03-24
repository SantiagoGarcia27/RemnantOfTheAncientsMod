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
	public class Emperor : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Vampire Gun");
			//Tooltip.SetDefault("healing gun.");

			// //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vampire Gun");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "pistolet guérisseur");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pistola Vampírica");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "una pistola curativa");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.Size = new Vector2(8, 8);	
			Item.useTime = 4;
			Item.useAnimation = 4;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 8,0, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 12f;
			Item.useAmmo = AmmoID.Bullet;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			return true;
		}
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(20));
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return !(player.itemAnimation < Item.useAnimation - 2);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Uzi, 1)
            .AddIngredient(ItemID.Gatligator, 1)
            .AddIngredient(ItemID.IllegalGunParts, 1)
            .AddIngredient(ItemID.CobaltBar, 10)

			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
 