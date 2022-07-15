using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Ranger
{
	public class flaremachinegun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flare Machine Gun");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Karabin maszynowy pochodni");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fusée mitrailleuse");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ametralladora de bengala");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item.damage = 59;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 60; 
            Item.height = 40; 
            Item.useTime = 6; 
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(silver:400);
            Item.rare = ItemRarityID.Lime;
   		    Item.UseSound = SoundID.Item45 ;
            Item.autoReuse = true;
			Item.shoot = ProjectileID.Flare; 
            Item.shootSpeed = 1f;
            Item.useAmmo = AmmoID.Flare;
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X,velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
			velocity.X = perturbedSpeed.X;
			velocity.Y = perturbedSpeed.Y;
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FlareGun, 1)
			.AddIngredient(ItemID.ChainGun, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
