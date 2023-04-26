using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    [CloneByReference]
    public class EndlesSilver : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Endless Silver Pouch");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sac à balles éternel en argent");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de Plata eterna");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 14;
Item.DamageType = DamageClass.Ranged;
Item.width = 8;
Item.height = 8;
Item.maxStack = 1;
Item.consumable = false;           
Item.knockBack = 3.5f;
Item.value = 10;
Item.rare = ItemRarityID.LightRed;
Item.shoot = ProjectileID.Bullet;
Item.shootSpeed = 16f;             
Item.ammo = AmmoID.Bullet;    
		}

        public override void AddRecipes()
		{
CreateRecipe()
		    .AddIngredient(ItemID.SilverBullet,3996)
.AddTile(TileID.CrystalBall)
.Register();
		}
	}
}