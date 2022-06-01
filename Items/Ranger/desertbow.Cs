using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.Ranger
{
    public class desertbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Bow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Desert Bow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Arc du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arco Desertico");
			Tooltip.SetDefault(""
			+ "\nTransform normal arrows in desert arrows");
           Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), ""
			+ "\nPrzekształć zwykłe strzały w strzały pustyni");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
			+ "\nTransformez les flèches normales en flèches du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
			+ "\nTransforma flechas normales en flechas del deserticas");
		}
     public override void SetDefaults()
    {
    Item.damage = 25;
    Item.DamageType = DamageClass.Ranged;
    Item.width = 12;
    Item.height = 38;
    Item.maxStack = 1;
    Item.useTime = 28;
    Item.useAnimation = 28;
    Item.useStyle = 5;
    Item.knockBack = 2;
    Item.value = 12000;
    Item.rare = 2;
    Item.UseSound = SoundID.Item5;
    Item.noMelee = true;
    Item.shoot = 1;
    Item.useAmmo = AmmoID.Arrow;
    Item.shootSpeed = 10f;
    Item.autoReuse = false;
    }
  public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.WoodenArrowFriendly) 
			{
				type = ModContent.ProjectileType<DesertArrowP>();
			} 
	   }
	}
}
