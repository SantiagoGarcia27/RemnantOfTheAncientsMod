using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Projectiles;
using RemnantOfTheAncientsMod.VanillaChanges;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
	public class Night_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de la Nuit");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de la Noche");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item Base = new Item(ItemID.NightsEdge);
			Item.damage = Base.damage - 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = Base.useTime - 3;
			Item.useAnimation = Base.useAnimation;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Base.value;
			Item.rare = Base.rare;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.GetGlobalItem<GlobalItem1>().Saber = true;
        }

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Demonite);
			}
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame);
			}
			if (Main.rand.NextBool(7))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.WaterCandle,0f,0f,1,Color.MediumPurple);
			}
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water_Corruption);
			}
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water_Cavern);
			}
		}
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile == true)
                {
                    DashPlayer.JumpDash(player, 1f, 0.75f);
					var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, new Vector2(0, 0), ModContent.ProjectileType<DamageHitbox>(), Item.damage, 2f, Main.myPlayer, 3, 1);
                    Main.projectile[p].width = Item.width * 2;
                    Main.projectile[p].height = Item.height * 2;
                    Main.projectile[p].scale = Item.scale;
                    Main.projectile[p].timeLeft = 100;
                }
            }
            return true;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
					   .AddRecipeGroup("CorruptSaber")
			.AddIngredient(ItemType<Grass_saber>())
			.AddIngredient(ItemID.Muramasa, 1)
			.AddIngredient(ItemType<Fire_saber>())
			.AddTile(TileID.DemonAltar)
			.Register();

		}
	}
}

