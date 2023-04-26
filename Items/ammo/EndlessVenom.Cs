using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    [CloneByReference]
    public class EndlessVenom : ModItem
	{
        public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Endless Venom Quiver");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois de venin sans fin");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj Venenoso eterno");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 20;
Item.DamageType = DamageClass.Ranged;
Item.width = 8;
Item.height = 8;
Item.maxStack = 1;
Item.consumable = false;             
Item.knockBack = 3.5f;
Item.value = 10;
Item.rare = ItemRarityID.Green;
Item.shoot = ProjectileID.VenomArrow;   
Item.shootSpeed = 26f;                
Item.ammo = AmmoID.Arrow;             
		}


        public override void AddRecipes()
		{
CreateRecipe()
		    .AddIngredient(ItemID.VenomArrow,3996)
.AddTile(TileID.CrystalBall)
.Register();
		}
	}
}