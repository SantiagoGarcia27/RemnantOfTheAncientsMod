using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    public class EndlesParty_Bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Party Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sac à balles Eternal Party");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas fiesteras eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = ProjectileID.PartyBullet;
			Item.shootSpeed = 16f;
			Item.ammo = AmmoID.Bullet;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.PartyBullet,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}