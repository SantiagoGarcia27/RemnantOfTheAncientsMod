using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
	public class Fire_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Feu");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable Ardiente");
			Tooltip.SetDefault("Inflict fire on enemies");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du feu aux ennemis");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego a los enemigos");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item Base = new Item(ItemID.FieryGreatsword);
			Item.damage = Base.damage - 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Base.value;
			Item.rare = Base.rare;
			Item.scale = 1.28f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {        
            if (player.altFunctionUse == 2)
            {
				if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile == true)
				{
					if (player.controlUseTile && Main.mouseRight)
					{
						DashPlayer.JumpDash(player, 2f, 0.25f);
						var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, new Vector2(0, 0), ModContent.ProjectileType<DamageHitbox>(), Item.damage * 2, 2, Main.myPlayer, 1, 1);
						Main.projectile[p].width = Item.width * 2;
						Main.projectile[p].height = Item.height * 2;
                        Main.projectile[p].scale = Item.scale;
                        Main.projectile[p].timeLeft = 100;

						for (int i = 0; i < 50; i++)
						{
							Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2f);
							dust.noGravity = true;
						}	
					}
				}
            }
			return true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 80);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(1)) Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pixie);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.HellstoneBar, 20)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
