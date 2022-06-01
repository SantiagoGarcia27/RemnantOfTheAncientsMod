using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class Nightchesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Night Breastplate");
			Tooltip.SetDefault(""
			+ "\nIncrease maximum Mana by 20 "
			+ "\nIncrease maximum life by 50 "
			+ "\nImmune to glow effect, fire and broken armor");


	     DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Coraza de la Noche");
		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "" 
		 +"\nAumenta el Maná maxima en 20 "
		 +"\nAumenta la vida maxima en 50 puntos"
		 +"\nEres immune a los efectos de brillo, fuego y armadura rota");


		  DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cuirasse de Nuit");
		   Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
		 + "\n Augmente le maximum de mana de 20" 
		 + "\n Immunité aux effets de lueur, au feu et à l'armure brisée");
		
	
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = 3;
			Item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.buffImmune[BuffID.Shine] = true;
			player.buffImmune[BuffID.Burning] = true;
			player.buffImmune[BuffID.BrokenArmor] = true;
			player.statManaMax2 += 20;
			player.statLifeMax2 += 50;
			player.maxMinions++;
		}

	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"NightBar",20)
			.AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}

