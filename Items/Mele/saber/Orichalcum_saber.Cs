using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class Orichalcum_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orichalcum Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Orichalque");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Oricalco");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 43;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(silver: 3);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.OrichalcumBar, 12)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}

