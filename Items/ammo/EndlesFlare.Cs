using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
    public class EndlesFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Flare");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Flare sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bengala Eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = ProjectileID.Flare;
			Item.shootSpeed = 16f;
			Item.ammo = AmmoID.Flare;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.Flare,3996)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
		}
	}
}