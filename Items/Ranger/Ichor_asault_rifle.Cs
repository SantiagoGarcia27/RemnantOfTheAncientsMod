using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Ranger
{
    public class Ichor_asault_rifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichor Assault Rifle");
		    DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rifle de asalto Ichor"); 
			Tooltip.SetDefault("Transform musket bullets into ichor bullets");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Transforma las balas de mosqueton en balas de ichor");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; 
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 100000; 
            Item.rare = ItemRarityID.LightRed;
   		    Item.UseSound = SoundID.Item36;
			Item.autoReuse = true;
            Item.shoot = ProjectileID.IchorBullet; 
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
            	}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet) type = ProjectileID.IchorBullet;
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 5);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Ichor,40)
			.AddIngredient(ItemID.Minishark,1)
			.AddTile(TileID.MythrilAnvil)
			.Register();		
	   }
	}
}
