using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class SpikeSaber : ModItem
	{
		public override void SetStaticDefaults()
		{
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
            Item.scale = 1;
            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
			{
				if (GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ProjectileType<InfernalSpike_f>();
			}
			else Item.shoot = ProjectileType<InfernalSpike_f>();
            Item.shootSpeed = 1;
            Item.GetGlobalItem<SaberGlobalItem>().isSaber = true;
            Item.GetGlobalItem<SaberGlobalItem>().DashStrength = new(1f, 0.75f);

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
			if(player.altFunctionUse == 0)
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

