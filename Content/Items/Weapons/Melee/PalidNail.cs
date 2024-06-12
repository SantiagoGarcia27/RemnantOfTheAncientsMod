//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.GameContent.Creative;
//using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
//using Microsoft.Xna.Framework;
//using Terraria.DataStructures;
//using System;
//using CalamityMod;
//using RemnantOfTheAncientsMod.Common.UtilsTweaks;
//using System.Linq;

//namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee //arkimedeez' push test
//{
//	public class PalidNail : ModItem
//	{
//		public override void SetStaticDefaults()
//		{
//			//DisplayName.SetDefault("UltraBlade");
//			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
//		}
//		public override void SetDefaults()
//		{
//			Item.damage = 30;
//			Item.DamageType = DamageClass.Melee;
//			Item.width = 40;
//			Item.height = 40;
//			Item.useTime = 18;
//			Item.useAnimation = 18;
//			Item.useStyle = ItemUseStyleID.Swing;
//			Item.knockBack = 1;
//			Item.autoReuse = false;
//			Item.UseSound = SoundID.Item100;
//			Item.value = Item.sellPrice(silver: 100);
//			Item.rare = 1;
//			Item.scale = 1f;
//            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
//            {
//                if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ModContent.ProjectileType<NailSlash>();
//            }
//            else Item.shoot = ModContent.ProjectileType<NailSlash>();
//			Item.shootSpeed = 0f;
//			Item.noUseGraphic = true;
//		}

//		public override bool CanUseItem(Player player)
//		{
//			//Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;

//			//if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
//			//{
//			//	Projectile.NewProjectile(Terraria.Entity.GetSource_None(), player.position + new Vector2(3*16 * player.direction,0), velocity, ModContent.ProjectileType<NailSlash>(), Item.damage, 2f, Main.myPlayer);
//			//}
//			return base.CanUseItem(player);
//		}
//		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//		{
//			// Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;
//			Vector2 mousePos = Main.MouseWorld;
//			if (player.whoAmI == Main.myPlayer)
//			{
//				mousePos = Main.MouseWorld;

//			}
//			Vector2 distance = Main.MouseWorld - player.Center;
//			Vector2 sign = new Vector2(Math.Sign(distance.X), Math.Sign(distance.Y));
//			Vector2 pos = new Vector2(3 * 16 * sign.X, 0);

//			float angle = player.AngleTo(Main.MouseWorld);

//            Main.NewText(angle+"°");

//			//abajo: 2,69 _ 1
//			//arriva : -2,69 _ -1
//			//izquierda : 2,7 _ -2.7
//			//derecha : 0,9 _ -0.9

//			//         if (angle > 0 && angle < 90) Main.NewText("abajo");
//			//else if(angle > 90 && angle < 180) Main.NewText("arriva");
//			if (Utils1.NumberBetween(1, 2.69f, angle))
//			{
//                pos = new Vector2(0,3 * 16);
//                Main.NewText("abajo");
//            }
//            if (Utils1.NumberBetween(-2.69f, -1f, angle))
//            {
//                pos = new Vector2(0, -3 * 16);
//                Main.NewText("arriva");
//            }
//            if (Utils1.NumberBetween(-2.7f, 2.7f, angle))
//            {
//                pos = new Vector2(3 * 16,0);
//                Main.NewText("izquierda");
//            }
//            if (Utils1.NumberBetween(-0.9f, 0.9f, angle))
//            {
//                pos = new Vector2(-3 * 16,0);
//                Main.NewText("derecha");
//            }

//            //         if ( distance.X > 10)
//            //{
//            //             pos = new Vector2(3 * 16 * sign.X, 0);

//            //         }
//            //         if (distance.Y > 10)
//            //         {
//            //             pos = new Vector2(0,3 * 16 * sign.Y);

//            //         }
//            //Main.NewText(distance);
//            Projectile.NewProjectile(Projectile.GetSource_None(), player.position + pos, (Main.MouseWorld - player.position)* 0.00f, ModContent.ProjectileType<NailSlash>(), Item.damage, 2f, Main.myPlayer);

//			return false;//base.Shoot(player, source, position, velocity, type, damage, knockback);
//        }
//        public override void AddRecipes()
//		{
//			CreateRecipe()
//			.AddIngredient(ItemID.InfluxWaver, 1)
//			.AddIngredient(ItemID.Meowmere, 1)
//			.AddIngredient(ItemID.TerraBlade, 1)
//			.AddTile(TileID.LunarCraftingStation)
//			.Register();
//		}
//	}
//}
