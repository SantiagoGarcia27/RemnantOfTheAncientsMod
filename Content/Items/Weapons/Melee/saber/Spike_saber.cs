using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
	public class Spike_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spike saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre à pointes");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de púas");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 80;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(gold: 6);
			Item.rare = ItemRarityID.Lime;
			Item.scale = 1.60f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
			{
				if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ProjectileType<InfernalSpike_f>();
			}
			else Item.shoot = ProjectileType<InfernalSpike_f>();
            Item.shootSpeed = 1;
             Item.GetGlobalItem<CustomTooltip>().Saber = true;
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(90));
			velocity.X = perturbedSpeed.X;
			velocity.Y = perturbedSpeed.Y;
			Projectile.NewProjectile(source, position, velocity, ProjectileType<InfernalSpike_f>(), damage, 1, player.whoAmI);
			return true;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(90));
		}
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
			bool PlayerTouchFlour = Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile;
            int projectileCount = 32;
            float projectileDistance = 10f;

            if (player.altFunctionUse == 2)
            {
                if (PlayerTouchFlour)
                {
                    DashPlayer.JumpDash(player, 1f, 0.75f);
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float angle = MathHelper.ToRadians(360f / projectileCount * i);
                        Vector2 velocity = angle.ToRotationVector2() * projectileDistance;
                        Vector2 position = player.Center + velocity;

                        var p = Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ProjectileType<InfernalSpike_f>(), Item.damage, 2f, Main.myPlayer);
                        Main.projectile[p].timeLeft = 100;
                    }
                }
            }
			else
			{
                if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
				{
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(90));
                    velocity.X = perturbedSpeed.X;
					velocity.Y = perturbedSpeed.Y;
					Projectile.NewProjectile(Projectile.GetSource_None(), player.position, velocity, ProjectileType<InfernalSpike_f>(), Item.damage, 1, player.whoAmI);
				}
			}
			return true;
        }

    }
}

