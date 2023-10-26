using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetR : ModItem
	{
        int RangedDamageBonus = 13;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBonus);
        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Night Mask");
			//Tooltip.SetDefault("Ranged damage increased by 13%");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mascara de la Noche");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta un 13% el daño a distancia");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Masque de Nuit");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Dégâts à distance augmentés de 13%");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 6;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) *= 1.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+5 defense, Ranged damage increased 6%, Movement speed increased";
			player.GetDamage(DamageClass.Ranged) *= 1.06f;
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