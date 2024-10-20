using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon
{
	public class IluminatedSunflowerStaff: ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.knockBack = 3f;
			Item.mana = 15;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.sentry = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 80, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<IlluminatedSunflowerMinionBuff>();
			Item.shoot = ProjectileType<IlluminatedSunFlowerMinion>();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			player.AddBuff(Item.buffType, 2);
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
            player.GetModPlayer<RemnantPlayer>().UpdateMaxTurrets();
            return false;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
		}
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<SunflowerStaff>())
            .AddIngredient(ItemID.FallenStar, 6)
            .AddIngredient(ItemID.HealingPotion, 5)
             .AddIngredient(ItemID.Bone, 5)
            .AddTile(TileID.SkyMill)
            .Register();
        }
    }
}