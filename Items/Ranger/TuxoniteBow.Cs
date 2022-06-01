using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Ranger
{
    public class TuxoniteBow: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Bow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Łuk Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Arc Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arco de Tusonita");
			
		}
         public override void SetDefaults()
        {
          Item.damage = 14;
          Item.DamageType = DamageClass.Ranged;
          Item.width = 12;
          Item.height = 38;
          Item.maxStack = 1;
          Item.useTime = 18;
          Item.useAnimation = 18;
          Item.useStyle = 5;
          Item.knockBack = 0;
          Item.value = 12000;
          Item.rare = 0;
          Item.UseSound = SoundID.Item5;
          Item.noMelee = true;
          Item.shoot = 1;
          Item.useAmmo = AmmoID.Arrow;
          Item.shootSpeed = 10f;
          Item.autoReuse = false;
        }
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",7)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
