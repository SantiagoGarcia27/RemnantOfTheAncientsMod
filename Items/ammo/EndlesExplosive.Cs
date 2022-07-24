using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
	public class EndlesExplosive : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Exploding Bullet Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette de balles explosives sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de balas explosivas Eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = ProjectileID.ExplosiveBullet;
			Item.shootSpeed = 16f;                 
			Item.ammo = AmmoID.Bullet;           
		}

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.ExplodingBullet,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}