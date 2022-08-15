using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Text;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Tools.Utilidad
{
    public class PlayerStatViewer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("stats checker");
			Tooltip.SetDefault("Shows most of the player's stats while in your inventory");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
			
			
			foreach (TooltipLine item in list)
			{
				if (item.Mod == "Terraria" && item.Name == "Tooltip0")
				{
					item.Text = CreateStatMeterTooltip(val, modPlayer);
				}
			}
			list.RemoveAll((TooltipLine l) => l.Mod == "Terraria" && (l.Name == "Favorite" || l.Name == "FavoriteDesc"));
		}
		private string CreateStatMeterTooltip(Player player, Player1 modPlayer)
		{
			int value = player.statDefense;
			float DamageReductionStat = player.endurance * 100f;
			float meleeSpeedStat = player.GetAttackSpeed(DamageClass.Melee);
			int minionSlotStat = player.maxMinions;
			int turretSlotStat = player.maxTurrets;
			int lifeRegenStat = player.lifeRegenCount;
			int lifeRegenBonusStat = player.lifeRegen;
			int manaRegenStat = player.manaRegen;
			float armorPenetrationStat = player.GetArmorPenetration(DamageClass.Generic);
			string wingTime = player.wingTime.ToString("n2");
			float moveSpeedStat = player.moveSpeed;
			float pickspeed = player.pickSpeed;

			StringBuilder stringBuilder = new StringBuilder("Displays almost all player stats", 1024);
			stringBuilder.Append("\nMelee Speed Boost: ").Append((meleeSpeedStat-1f)*100f).Append("%\n");
			stringBuilder.Append("Mana Regen: ").Append(manaRegenStat).Append("\n");
			stringBuilder.Append("Minion Slots: ").Append(minionSlotStat).Append("\n");
			stringBuilder.Append("Turret Slots: ").Append(turretSlotStat).Append("\n");
			stringBuilder.Append("Defense: ").Append(value).Append("\n"); 
			stringBuilder.Append("DR: ").Append(DamageReductionStat).Append("%\n");
			stringBuilder.Append("Life Regen: ").Append(lifeRegenStat).Append("\n");
			stringBuilder.Append("Life Regen Bonus: ").Append(lifeRegenBonusStat).Append("\n");
			stringBuilder.Append("Armor Penetration: ").Append(armorPenetrationStat).Append("\n");
			stringBuilder.Append("Wing Flight Time: ").Append(wingTime).Append(" seconds\n");
			stringBuilder.Append("Movement Speed Boost: ").Append(moveSpeedStat*100f).Append("%\n");
			stringBuilder.Append("Pick Speed Boost: ").Append(100f - (pickspeed *100f)).Append("%\n");
			return stringBuilder.ToString();
		}
	}
}
