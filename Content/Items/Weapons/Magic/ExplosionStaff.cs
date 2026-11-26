using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class ExplosionStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 55;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 10;
			Item.rare = ItemRarityID.Orange;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot; 
			Item.knockBack = 4;
			
			Item.shoot = ProjectileID.Volcano;
			Item.value = Item.sellPrice(0,1,0,0);
			Item.autoReuse = true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            Vector2 pos = Main.MouseWorld;
            int p = Projectile.NewProjectile(source, pos, Vector2.Zero, type, damage, knockback, player.whoAmI);
			Main.projectile[p].Size = new Vector2(48, 48);
            Main.projectile[p].Center = pos;
            return false;
		}
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
			velocity = Vector2.Zero;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.HellstoneBar, 10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}