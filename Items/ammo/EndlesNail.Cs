using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    public class EndlesNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Nail");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Clou sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Clavo Infinito");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        Item Base = new Item(ItemID.Nail);
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