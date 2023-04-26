using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    [CloneByReference]
    public class EndlesStar : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Endless Star");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Étoile sans fin");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Estrella Eterna");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 0;
Item.DamageType = DamageClass.Ranged;
Item.width = 8;
Item.height = 8;
Item.maxStack = 1;
Item.consumable = false;          
Item.knockBack = 3.5f;
Item.value = 10;
Item.rare = ItemRarityID.Orange;
Item.shoot = ProjectileID.FallingStar;
Item.shootSpeed = 1f;                 
Item.ammo = AmmoID.FallenStar;             
		}

        public override void AddRecipes()
		{
CreateRecipe()
		    .AddIngredient(ItemID.FallenStar,500)
.AddTile(TileID.TinkerersWorkbench)
.Register();
		}
	}
}