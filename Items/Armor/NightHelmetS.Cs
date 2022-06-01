using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetS : ModItem
	{
		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Night Hat");
			Tooltip.SetDefault(""
			+"\nSummon damage increased by 13%");

	     DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mascara de la noche");
		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
		 +"\nDa�o de invocacion aumentado en un 13%");

		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Masque de nuit");
		   Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
		 +"\nLes d�g�ts d'invocation ont augment� de 13%");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = 3;
			Item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += 0.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus ="+5 defense, Summon damage increased 6%, Movement speed increased +2 minions";
			player.GetDamage(DamageClass.Summon) += 0.06f;
			player.maxMinions+= 2;
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