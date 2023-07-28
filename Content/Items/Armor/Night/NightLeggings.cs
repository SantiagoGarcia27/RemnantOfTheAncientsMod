using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Legs)]
	public class NightLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Night Greaves");
			//Tooltip.SetDefault("15% increased movement speed");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Perneras De La Noche");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta un 15% la velocidad de movimiento");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grèves de Nuit");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "15% D'augmentation de la Vitesse de Déplacement");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 40000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<NightBar>(), 13)
			.AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}

