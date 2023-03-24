using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    [CloneByReference]
    public class EndlessJest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Jester's Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois de bouffon sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj del bufón eterno");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Lime;
			Item.shoot = ProjectileID.JestersArrow;
			Item.shootSpeed = 1f;                
			Item.ammo = AmmoID.Arrow;            
		}
        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.JestersArrow,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}