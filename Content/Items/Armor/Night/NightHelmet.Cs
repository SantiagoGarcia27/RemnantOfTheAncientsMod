using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Night Headgear");
			Tooltip.SetDefault(""
			+ "\nIncreases magic damage by 13%");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tocado de la Noche");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
					   + "\nAumenta un 13% de daño magico");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Couvre-chef de Nuit");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
					  + "\nAugmente les dégâts magiques de 13%");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 7;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Magic) *= 1.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+5 Defense, Increase Magic Damage 6%, Increase Movement Speed";
			player.GetDamage(DamageClass.Magic) += 0.06f;
			player.statDefense += 5;
			player.moveSpeed += 0.05f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<NightBar>(), 15)
			.AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}