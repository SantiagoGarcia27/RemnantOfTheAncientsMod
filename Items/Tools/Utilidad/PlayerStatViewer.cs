using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace opswordsII.Items.Tools.Utilidad
{
	public class PlayerStatViewer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("stats checker");
			Tooltip.SetDefault("Shows most of the player's stats while in your inventory");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = 5000000;
			Item.rare = ItemRarityID.Yellow;
		}
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			Player val = Main.player[Main.myPlayer];
			if (val == null)
			{
				return;
			}
			Player1 modPlayer = val.OpswordsII();
			Item heldItem = null;
			if (val.selectedItem >= 0 && val.selectedItem < 58)
			{
				heldItem = val.ActiveItem();
			}
			foreach (TooltipLine item in list)
			{
				if (item.Mod == "Terraria" && item.Name == "Tooltip0")
				{
					item.Text = CreateStatMeterTooltip(val, modPlayer, heldItem);
				}
			}
			list.RemoveAll((TooltipLine l) => l.Mod == "Terraria" && (l.Name == "Favorite" || l.Name == "FavoriteDesc"));
		}
		private string CreateStatMeterTooltip(Player player, Player1 modPlayer, Item heldItem)
		{
			int value = player.statDefense;
			int DamageReductionStat = (int)player.endurance;
			int meleeSpeedStat = (int)player.GetAttackSpeed(DamageClass.Melee);
			int minionSlotStat = player.maxMinions;
			int lifeRegenStat = player.lifeRegen;
			int manaRegenStat = player.manaRegen;
			int armorPenetrationStat = (int)player.GetArmorPenetration(DamageClass.Generic);
			string value4 = player.wingTimeMax.ToString("n2");
			int moveSpeedStat = (int)player.moveSpeed;

			StringBuilder stringBuilder = new StringBuilder("Displays almost all player stats\nOffensive stats displayed vary with held item\n\n", 1024);

			if (heldItem != null)
			{
				if (!heldItem.noMelee)
				{
					stringBuilder.Append("%\nMelee Speed Boost: ")
						.Append(meleeSpeedStat)
						.Append("%\n\n");
				}
				else if (heldItem.CountsAsClass(DamageClass.Ranged))
				{
					stringBuilder.Append("% | Ranged Crit Chance: ")
						.Append("%\n\n");
				}
				else if (heldItem.CountsAsClass(DamageClass.Magic))
				{
					stringBuilder.Append("% | Magic Crit Chance: ")
						.Append("% | Mana Regen: ")
						.Append(manaRegenStat)
						.Append("\n\n");
				}
				else if (heldItem.CountsAsClass(DamageClass.Summon))
				{
					stringBuilder.Append("% | Minion Slots: ")
						.Append(minionSlotStat)
						.Append("\n\n");
				}
			}
			stringBuilder.Append("Defense: ").Append(value);
			stringBuilder.Append(" | DR: ").Append(DamageReductionStat).Append("%");
			stringBuilder.Append(" | Life Regen: ").Append(lifeRegenStat).Append("\n");
			stringBuilder.Append("Armor Penetration: ").Append(armorPenetrationStat);
			stringBuilder.Append(" | Wing Flight Time: ").Append(value4).Append(" seconds\n");
			stringBuilder.Append(" | Movement Speed Boost: ").Append(moveSpeedStat).Append("%\n\n");
			return stringBuilder.ToString();
		}
	}
}
