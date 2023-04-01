using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.ammo
{
    [CloneByReference]
    public class EndlesFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Endless Flare");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Flare sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bengala Eterna");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        Item Base = new Item(ItemID.Flare);
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
            .AddIngredient(ItemID.Flare, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}