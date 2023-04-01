using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class tungsteno_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Tungsten Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Tungstène");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Tungsteno");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item Base = new Item(ItemID.TungstenBroadsword);
            Item.damage = Base.damage - 3;
            Item.DamageType = DamageClass.Melee;
Item.width = 40;
Item.height = 80;
Item.useTime = 20;
Item.useAnimation = 20;
Item.useStyle = ItemUseStyleID.Swing;
Item.knockBack = 10;
Item.value = Base.value;
Item.rare = ItemRarityID.White;
Item.UseSound = SoundID.Item1;
Item.autoReuse = true;
		}
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.TungstenBar, 10)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}

