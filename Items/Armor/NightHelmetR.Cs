using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetR : ModItem
	{
		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Night Mask");
			Tooltip.SetDefault(""
			+"\nRanged damage increased by 13%");

	     DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "M�scara de la Noche");
		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
		 +"\nAumenta un 13% el da�o a distancia");

		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Masque de Nuit");
		   Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
		 +"\nD�g�ts � distance augment�s de 13%");

		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = 3;
			Item.defense = 6;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) *=1.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus ="+5 defense, Ranged damage increased 6%, Movement speed increased";
			player.GetDamage(DamageClass.Ranged) *= 1.06f;
			player.statDefense += 5;
            player.moveSpeed += 0.05f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"NightBar",15)
			.AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}