using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
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
			Item.shoot = ProjectileType<InfernalSpike_f>();
			Item.shootSpeed = 1;
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
    }
}

