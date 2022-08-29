using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class Reforced_iron_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Helmet");
			Tooltip.SetDefault("Increases melee damage by 5%");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Casque en fer renforc�");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les d�g�ts de m�l�e de 5%");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Casco de hierro reforzado");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta un 5% de daño cuerpo a cuerpo");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 5;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) *=1.5f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Reforced_iron_chesplate>() && legs.type == ModContent.ItemType<R_I_L>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus ="+2 defense, + 2% damage, + 15% movement speed";
			player.GetDamage(DamageClass.Generic) *= 1.2f;
			player.statDefense += 2;
		    player.moveSpeed += 0.15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(), 4)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}