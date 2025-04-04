using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class EnchantedSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.EnchantedSword);
        public override float[] DashStrength => [0.9f, 0.65f];
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.EnchantedSword);
			Item.damage = (int)(Item.damage * 0.60f);
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 25;
			Item.knockBack += 5;
			Item.shootSpeed = 10f;
			Item.GetGlobalItem<SaberGlobalItem>().isSaber = true;
            Item.GetGlobalItem<SaberGlobalItem>().DashStrength = new(0.9f, 0.65f);
			Item.noMelee = true;

        }

        public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player)
		{

			if (player.altFunctionUse == 1)	
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
			return base.CanUseItem(player);
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
