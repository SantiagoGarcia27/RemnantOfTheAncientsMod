using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Items.accesorios.Alas
{
	[AutoloadEquip(EquipType.Wings)]
	public class BirdWings : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Bird Wings");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Ailes D'oiseau");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alas de Ave");
            Tooltip.SetDefault("Allows flight and fall slowly");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pozwala latać i opadać powoli");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Permet de voler et de tomber lentement");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Permite volar y caer lentamente ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(39, 5f, 0.2f);
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = Item.buyPrice(0,0,7,0);
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;
		}
		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.35f;
			ascentWhenRising = 0.05f;
			maxCanAscendMultiplier = 0.2f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		
	}
}