using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class Reinforced_iron_ore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced iron ore");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wzmocniona ruda żelaza");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Minerai de fer renforcé");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mineral de hierro reforzado");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 100;
			Item.value = 100;
			Item.rare = ItemRarityID.Green;
			Item.scale = 1f;
		}
	}
}
