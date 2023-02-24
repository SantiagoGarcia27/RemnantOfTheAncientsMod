using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.accesorios.Alas
{
	[AutoloadEquip(EquipType.Wings)]
	public class LuminiteWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Wings");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Ailes de luminite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alas de Luminita");
			Tooltip.SetDefault("Allows flight and fall slowly");	
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Permet de voler et de tomber lentement");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Permite volar y caer lentamente ");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(199, 10f, 3f);
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
            Item.value = Item.buyPrice(0, 8, 90, 0);
            Item.rare = ItemRarityID.Red;
			Item.accessory = true;
			
		}
		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.LunarBar, 10)
			.AddIngredient(ModContent.ItemType<LuminiteFeathers>(),10)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}