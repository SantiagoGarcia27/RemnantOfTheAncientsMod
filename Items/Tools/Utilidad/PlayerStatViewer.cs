using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Tools.Utilidad
{
	public class PlayerStatViewer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("stats checker");
			Tooltip.SetDefault("Shows most of the player's stats while in your inventory");
			
			ModTranslation val = LocalizationLoader.CreateTranslation("Stats checker");
			val.SetDefault("Melee damage/critical strike chance boosts are ");
			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text2");
			val.SetDefault("Ranged damage/critical strike chance boosts are ");
			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text3");
			val.SetDefault("Magic damage/critical strike chance boosts are ");
			
			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text4");
			val.SetDefault("Thrown damage/critical strike chance boosts are ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text5");
			val.SetDefault("Summoner damage boost is ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text6");
			val.SetDefault("Damage Reduction boost is ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text7");
			val.SetDefault("Movement speed boost is ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text8");
			val.SetDefault("Max life boost is ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text9");
			val.SetDefault("Life regeneration is ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text10");
			val.SetDefault("Mana usage reduction is ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text11");
			val.SetDefault("Max amounts of minions/sentries are ");

			LocalizationLoader.AddTranslation(val);
			val = LocalizationLoader.CreateTranslation("Pip-Boy3000text12");
			val.SetDefault("Melee swing time is ");

			LocalizationLoader.AddTranslation(val);
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = 5000000;
			Item.rare = 8;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c7: Expected O, but got Unknown
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02da: Expected O, but got Unknown
			//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ed: Expected O, but got Unknown
			//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0301: Expected O, but got Unknown
			//IL_030e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0315: Expected O, but got Unknown
			//IL_0322: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Expected O, but got Unknown
			//IL_0336: Unknown result type (might be due to invalid IL or missing references)
			//IL_033d: Expected O, but got Unknown
			//IL_034a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0351: Expected O, but got Unknown
			//IL_035e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0365: Expected O, but got Unknown
			//IL_0372: Unknown result type (might be due to invalid IL or missing references)
			//IL_0379: Expected O, but got Unknown
			//IL_0386: Unknown result type (might be due to invalid IL or missing references)
			//IL_038d: Expected O, but got Unknown
			//IL_039a: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a1: Expected O, but got Unknown
			Player val = Main.player[Main.myPlayer];
			string text = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text1") + (int)(Player.meleeDamage * 100f - 100f) + "% / " + (val.meleeCrit - 4) + "%";
			string text2 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text2") + (int)(val.rangedDamage * 100f - 100f) + "% / " + (val.rangedCrit - 4) + "%";
			string text3 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text3") + (int)(val.magicDamage * 100f - 100f) + "% / " + (val.magicCrit - 4) + "%";
			string text4 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text4") + (int)(val.thrownDamage * 100f - 100f) + "% / " + (val.thrownCrit - 4) + "%";
			string text5 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text5") + (int)(val.minionDamage * 100f - 100f) + "%";
			string text6 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text6") + (int)(val.endurance * 100f) + "%";
			string text7 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text7") + (int)(val.moveSpeed * 100f - 100f) + "%";
			string text8 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text8") + (val.statLifeMax2 - val.statLifeMax);
			string text9 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text9") + val.lifeRegen;
			string text10 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text10") + (int)(val.manaCost * 100f - 100f) + "%";
			string text11 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text11") + val.maxMinions + " / " + val.maxTurrets;
			string text12 = Language.GetTextValue("Mods.AlchemistNPC.Pip-Boy3000text12") + (int)(val.meleeSpeed * 100f) + "%";
			TooltipLine val2 = new TooltipLine(, "text1", text);
			TooltipLine val3 = new TooltipLine(Item.get_mod(), "text2", text2);
			TooltipLine val4 = new TooltipLine(Item.get_mod(), "text3", text3);
			TooltipLine val5 = new TooltipLine(Item.get_mod(), "text4", text4);
			TooltipLine val6 = new TooltipLine(Item.get_mod(), "text5", text5);
			TooltipLine val7 = new TooltipLine(Item.get_mod(), "text6", text6);
			TooltipLine val8 = new TooltipLine(Item.get_mod(), "text7", text7);
			TooltipLine val9 = new TooltipLine(Item.get_mod(), "text8", text8);
			TooltipLine val10 = new TooltipLine(Item.get_mod(), "text9", text9);
			TooltipLine val11 = new TooltipLine(Item.get_mod(), "text10", text10);
			TooltipLine val12 = new TooltipLine(Item.get_mod(), "text11", text11);
			TooltipLine val13 = new TooltipLine(Item.get_mod(), "text12", text12);
			val2.OverrideColor = ItemRarityID.Red;
			val3.OverrideColor = Color.LimeGreen;
			val4.OverrideColor = Color.SkyBlue;
			val5.OverrideColor = Color.Orange;
			val6.OverrideColor = Color.MediumVioletRed;
			val7.OverrideColor = Color.Gray;
			val8.OverrideColor = Color.Green;
			val9.OverrideColor = Color.Yellow;
			val10.OverrideColor = Color.Brown;
			val11.OverrideColor = Color.SkyBlue;
			val12.OverrideColor = Color.Magenta;
			val13.OverrideColor = Color.Red;
			tooltips.Insert(2, val2);
			tooltips.Insert(3, val3);
			tooltips.Insert(4, val4);
			tooltips.Insert(5, val5);
			tooltips.Insert(6, val6);
			tooltips.Insert(7, val7);
			tooltips.Insert(8, val8);
			tooltips.Insert(9, val9);
			tooltips.Insert(10, val10);
			tooltips.Insert(11, val11);
			tooltips.Insert(12, val12);
			tooltips.Insert(13, val13);
		}

		
	}
}
