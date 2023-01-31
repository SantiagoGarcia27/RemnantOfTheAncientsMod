using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem.Geode
{
	public class The_Geode : ModItem
	{
		public override void SetStaticDefaults()
		{
	        DisplayName.SetDefault("The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1200;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;

		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Vector2 perturbedSpeed = getPerturbatedSpeed(player, target, 0, "-");
			Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 6) * 16), target.position.Y), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<DiamondSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
			perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * -target.direction), "-");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 7 ) * 16), target.position.Y + 10 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<SaphireSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * target.direction), "-");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 7) * 16), target.position.Y - 10 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<EmeraldSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, 0, "+");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X + ((target.direction * 6) * 16), target.position.Y +1 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<RubySpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * target.direction), "+");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * -4) * 16), target.position.Y + 6 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<TopazSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
            perturbedSpeed = getPerturbatedSpeed(player, target, MathHelper.ToRadians(50 * -target.direction), "+");
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * -7) * 16), target.position.Y - 6 * 16), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<AmathystSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
        }

		public Vector2 getPerturbatedSpeed(Player player, NPC target,float rotation, string signo)
		{
			if (signo == "-")
			{
				return (player.position - target.position).RotatedBy(MathHelper.Lerp(-rotation, rotation, 1)) * .2f;
			}
			else  return (target.position - player.position).RotatedBy(MathHelper.Lerp(-rotation, rotation, 1)) * .2f;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Amethyst_Sword>())
            .AddIngredient(ModContent.ItemType<Topaz_Sword>())
			.AddIngredient(ModContent.ItemType<Sapphire_Sword>())
			.AddIngredient(ModContent.ItemType<Ruby_Sword>())
			.AddIngredient(ModContent.ItemType<Emerald_Sword>())
			.AddIngredient(ModContent.ItemType<Diamond_Sword>())
			.AddIngredient(ModContent.ItemType<BoneBar>(),2)
			.AddIngredient(ItemID.StoneBlock,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
