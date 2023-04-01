using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class Crimson_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Blood Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Sangre");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Sangre");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item Base = new Item(ItemID.BloodButcherer);
            Item.damage = Base.damage - 3;
            Item.DamageType = DamageClass.Melee;
Item.width = 40;
Item.height = 40;
Item.useTime = 25;
Item.useAnimation = 25;
Item.useStyle = ItemUseStyleID.Swing;
Item.knockBack = 10;
Item.value = Base.value;
Item.rare = Base.rare;
Item.UseSound = SoundID.Item1;
Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.CrimtaneBar,10)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}
