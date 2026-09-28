using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Spear;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee.Flail;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Flail
{
	internal class RockAndRoll : ModItem
	{
		public override void SetStaticDefaults() {
			ItemID.Sets.ToolTipDamageMultiplier[Type] = 1f;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<StoneImpaler>();
        }

		public override void SetDefaults() {
            Item.damage = 12;
            Item.crit = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.knockBack = 6.75f;
			Item.width = 30;
			Item.height = 10;
			Item.scale = 1.1f;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<RockAndRollProjectile>();
			Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(silver: 50);
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.channel = true;
			Item.noMelee = true;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}
	}
}
