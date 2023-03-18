using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    public class EndlesIchor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Ichor Pouch ");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette Ichor sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bolsa de Balas de Ichor eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        Item Base = new Item(ItemID.IchorBullet);
        public override void SetDefaults()
        {
            Item.damage = Base.damage;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.knockBack = Base.knockBack;
            Item.value = Base.value + Item.buyPrice(0, 1, 0, 0);
            Item.rare = Base.rare + 1;
            Item.shoot = Base.shoot;
            Item.shootSpeed = Base.shootSpeed;
            Item.ammo = Base.ammo;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(Base.type, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}