using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon
{
    public class CursedShotgunStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item baseItem = new Item(ItemID.Shotgun);
			Item.damage = baseItem.damage;
			Item.knockBack = 3f;
			Item.mana = 10;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<CursedShotgunMinionBuff>();
			Item.shoot = ProjectileType<CursedShotgunMinion>();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 160);
			velocity = Vector2.Zero;
			var projectile = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
			Main.projectile[projectile].originalDamage = damage;
			return false;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
		}
    }
}