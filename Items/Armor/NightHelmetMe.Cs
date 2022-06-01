using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetMe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Helmet");
			Tooltip.SetDefault(""
			+"\nIncrease melee damage by 13%");

	     DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Casco de la Noche");
		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
		 +"\nAumenta un 13% el da�o cuerpo a cuerpo");

		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Casque de Nuit");
		   Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
		 +"\nAugmente les d�g�ts de m�l�e de 13%");

		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = 3;
			Item.defense = 10;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) +=0.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType < NightLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus ="+5 Defense, Increase Melee Damage 6%, Increase Movement Speed";
			player.GetDamage(DamageClass.Melee) += 0.06f;
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