using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    [CloneByReference]
    public class EndlesCursed : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Endless Cursed Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette incurvée sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de malditas eterna");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        Item Base = new Item(ItemID.CursedBullet);
        public override void SetDefaults()
		{
Item.damage = Base.damage;
Item.DamageType = DamageClass.Ranged;
Item.width = 8;
Item.height = 8;
Item.maxStack = 1;
Item.consumable = false;
Item.knockBack = Base.knockBack;
Item.value = Base.value + Item.buyPrice(0,1,0,0);
Item.rare = Base.rare;
Item.shoot = Base.shoot;
Item.shootSpeed = Base.shootSpeed;
Item.ammo = Base.ammo;
		}

        public override void AddRecipes()
		{
CreateRecipe()
		    .AddIngredient(ItemID.CursedBullet, 3996)
.AddTile(TileID.CrystalBall)
.Register();
		}
	}
}