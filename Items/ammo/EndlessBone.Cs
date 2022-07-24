using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.ammo
{
    public class EndlessBone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Bone Arrow Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois de flèche en os sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj de flechas de hueso eterno");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ProjectileID.BoneArrow;   
			Item.shootSpeed = 26f;                
			Item.ammo = AmmoID.Arrow;            
		}


        public override void AddRecipes()
		{
			CreateRecipe()
		     .AddIngredient(ItemID.BoneArrow,3996)
			.AddTile(TileID.CrystalBall)	
			.Register();
		}
	}
}