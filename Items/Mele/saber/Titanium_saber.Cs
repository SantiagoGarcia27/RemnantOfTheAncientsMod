using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class Titanium_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Titanium Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Titane");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Titanio");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item Base = new Item(ItemID.TitaniumSword);
            Item.damage = Base.damage-3;
            Item.DamageType = DamageClass.Melee;
Item.width = 40;
Item.height = 80;
Item.useTime = 20;
Item.useAnimation = 20;
Item.useStyle = ItemUseStyleID.Swing;
Item.knockBack = 10;
Item.value = Base.value;
Item.rare = ItemRarityID.LightRed;
Item.UseSound = SoundID.Item1;
Item.autoReuse = true;
		}
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.TitaniumBar, 13)
.AddTile(TileID.MythrilAnvil)
.Register();
		}
	}
}

