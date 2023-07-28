using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class RazorpineV2: ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Pine Fury");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Star Tome");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pine Fury");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Furia de pino");
Item.staff[Item.type] = true;
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.CloneDefaults(ItemID.Razorpine);
Item.damage = 35;
Item.noMelee = true;
Item.DamageType = DamageClass.Magic;
Item.channel = true; 
Item.mana = 2;
Item.rare = ItemRarityID.Red;
Item.width = 28;
Item.height = 30;
Item.useTime = 10;
Item.useStyle = 5;
Item.shootSpeed = 14f;
Item.useAnimation = 10;
Item.shoot = ProjectileID.PineNeedleFriendly;
Item.value = 500000;
Item.autoReuse = true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
float numberProjectiles = 5 + Main.rand.Next(4); 
float rotation = MathHelper.ToRadians(4);
position += Vector2.Normalize(velocity) * 45f;
for (int i = 0; i < numberProjectiles; i++)
{
	Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; 
	Projectile.NewProjectile(source, position, perturbedSpeed * 20, ProjectileID.PineNeedleFriendly, Item.damage + 10, knockback, player.whoAmI);
	Projectile.NewProjectile(source, position, perturbedSpeed * 20, ModContent.ProjectileType<LeafFriendlyClone>(), Item.damage, knockback, player.whoAmI);
}

return false; 
		}


		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.Razorpine, 1)
.AddIngredient(ItemID.LeafBlower, 1)
.AddIngredient(ItemID.FragmentNebula, 10)
.AddIngredient(ItemID.LunarBar, 5)
.AddTile(TileID.LunarCraftingStation)
.Register();
		}
	}
}