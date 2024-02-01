using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage.Lazer;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class JudgmentII : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Judgment II");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 80;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can hold the weapon [Important]
			Item.autoReuse = true;
			Item.mana = 5;
			Item.rare = ItemRarityID.Pink;
			Item.width = 30;
			Item.height = 34;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
            Item.GetGlobalItem<CustomTooltip>().CompletistItem = true;
            Item.shoot = ProjectileType<Judment_Lasser_Shooter>(); //Laser
			Item.value = Item.sellPrice(silver: 50);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            for (int i = 0; i < 1; i++)
            {
                position = target - new Vector2(Main.rand.Next(-8, 8) * 16, 600f);
                position.Y -= (100 * i);
                Vector2 heading;
                heading = new Vector2(0, 20);
                heading.Normalize();
                int p = Projectile.NewProjectile(source, position.X, position.Y, heading.X, heading.Y, type, damage, knockback, player.whoAmI, 0, ceilingLimit);
                Main.projectile[p].scale = 2;
            }
            return false;
        }
    
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<Judgment>(), 1)
			.AddIngredient(ItemID.ChlorophyteBar, 10)
			.AddIngredient(ItemID.Ectoplasm, 5)
			.AddTile(TileID.Bookcases)
			.Register();
		}

	}
}
