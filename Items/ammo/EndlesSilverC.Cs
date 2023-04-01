using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    [CloneByReference]
    public class EndlesSilverC : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Endless Silver Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'argent sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda de plata infinita");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 50;
Item.DamageType = DamageClass.Ranged;
Item.width = 8;
Item.height = 8;
Item.maxStack = 1;
Item.consumable = false;            
Item.knockBack = 3.5f;
Item.value = 10;
Item.rare = ItemRarityID.Lime;
Item.shoot = ProjectileID.SilverCoin;
Item.shootSpeed = 16f;                  
Item.ammo = AmmoID.Coin;  
		}

        public override void AddRecipes()
		{
CreateRecipe()
		    .AddIngredient(ItemID.SilverCoin,3996)
.AddTile(TileID.CrystalBall)
.Register();
		}
	}
}