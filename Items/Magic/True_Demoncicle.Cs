using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;
using Terraria.DataStructures;

namespace opswordsII.Items.Magic
{
    public class True_Demoncicle : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Demon Scythe");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Prawdziwa Kosa Demona");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Faux Démon Vrai");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Verdadera Guadaña Demoníaca");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; 
			Item.mana = 9;
			Item.rare = 5;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 19;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = 5;
			Item.shootSpeed = 2f;
			Item.useAnimation = 19;
			Item.shoot = ModContent.ProjectileType<DemonCycle>();
			Item.value = Item.sellPrice(gold: 5);
			Item.autoReuse = true;
			Item.reuseDelay = 14;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 6 + Main.rand.Next(3); 
			float rotation = MathHelper.ToRadians(45);

			position += Vector2.Normalize(velocity) * 45f;

			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; 
				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			}

			return false; 
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.SoulofMight, 3)
			.AddIngredient(ItemID.SoulofSight, 3)
			.AddIngredient(ItemID.SoulofFright, 3)
			.AddIngredient(ItemID.DemonScythe, 1)
			.AddTile(TileID.Bookcases)
			.Register();
		}
	}
}
