using Microsoft.Xna.Framework;
using Mono.Cecil;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Typeless
{
	public class WandOfCaverns : ModItem
	{
		public override string Texture => "RemnantOfTheAncientsMod/Content/Items/Weapons/Typeless/WandOfCaverns";
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
            Item.width = 64;
			Item.height = 64;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 0;
			Item.knockBack = 8;
			Item.mana = 10;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.scale = 1.5f;
		}
        public override void HoldItem(Player player)
        {
			if(player.ownedProjectileCounts[ProjectileType<WandOfCavernsHeldProjectile>()] < 1)
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center + new Vector2(16 * player.direction,0), Vector2.Zero, ProjectileType<WandOfCavernsHeldProjectile>(), 0, Item.knockBack , player.whoAmI);
            base.HoldItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			
            return false;
        }
	}

}
