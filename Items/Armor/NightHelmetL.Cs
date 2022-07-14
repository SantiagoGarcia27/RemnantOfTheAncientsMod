using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Items.Items;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetL : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Hood");
			Tooltip.SetDefault(""
			+ "\nIncreases Throwing damage by 13%.");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Capucha de la Noche");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
		   + "\nAumenta el daño de lanzamiento en un 13%.");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Gr�ves de Nuit");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
		  + "\nAugmente les d�g�ts de projection de 13%");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Throwing) *= 1.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
		
			player.setBonus ="+5 Defense, Increases throwable damage by 6%, Increases movement speed";
			player.GetDamage(DamageClass.Throwing) += 0.06f;
			player.statDefense += 5;
            player.moveSpeed += 0.05f;


			/*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null){
			CalamityConvert();
			CalamityPlayer.rogueStealthMax.SetValue(CalamityPlayer, 1);
			CalamityPlayer.wearingRogueArmor.SetValue(CalamityPlayer, true);
			
			}*/

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