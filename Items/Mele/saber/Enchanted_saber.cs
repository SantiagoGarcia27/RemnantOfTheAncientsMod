using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using RemnantOfTheAncientsMod.VanillaChanges;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
	public class Enchanted_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Enchanted Saber");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre enchanté");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable Encantado");
			//Tooltip.SetDefault("Shoot an enchanted sword beam");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tirez sur un rayon d'épée enchanté");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Dispara un rayo de espada encantada");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item Base = new Item(ItemID.EnchantedSword);
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Base.value;
			Item.rare = Base.rare;
			if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
			{
				if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ProjectileID.EnchantedBeam;
            }
			else Item.shoot = ProjectileID.EnchantedBeam;
			Item.shootSpeed = 10f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.GetGlobalItem<GlobalItem1>().Saber = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.damage = 16;
			}
		}
        [JITWhenModsEnabled("TerrariaOverhaul")]
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Projectile.NewProjectile(source,position,velocity,ProjectileID.EnchantedBeam,damage,knockback);
                return true;
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
						DashPlayer.JumpDash(player, 1.7f, 0.55f);
						var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, new Vector2(0, 0), ModContent.ProjectileType<DamageHitbox>(), Item.damage * 2, 2, Main.myPlayer, 2, 1);
						Main.projectile[p].width = Item.width * 2;
						Main.projectile[p].height = Item.height * 2;
						Main.projectile[p].scale = Item.scale;
						Main.projectile[p].timeLeft = 100;

						for (int i = 0; i < new RemnantOfTheAncientsMod().ParticleMeter(50); i++)
						{
							Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.EnchantedNightcrawler, 0f, 0f, 100, default, 2f);
							dust.noGravity = true;
						}
					}
				}
			}
			else
			{
				int i = 0;
				Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;

				if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
				{
					do
					{
						switch (i)
						{
							case 0:
							case 10:
								Projectile.NewProjectile(Projectile.GetSource_None(), player.position, velocity, ProjectileID.EnchantedBeam, Item.damage, Item.knockBack);
								break;
						}
						i++;
					} while (i <= 11);
				}
			}
			return true;
		}
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.EnchantedSword, 1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
