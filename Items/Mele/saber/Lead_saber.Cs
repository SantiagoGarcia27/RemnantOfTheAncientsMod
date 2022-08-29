using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class Lead_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lead Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Plomb");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Plomo");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(silver: 6);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.LeadBar, 10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

