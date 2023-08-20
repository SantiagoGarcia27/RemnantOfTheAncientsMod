using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Text;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
    public class PlayerStatViewer : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Player stat meter");
			//Tooltip.SetDefault("Shows most of the player's stats");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Compteur de statistiques du joueur");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Medidor de estadísticas del jugador");
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
			
		}
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<InfoDisplayPlayer>().showMaxMinion = true;
            base.UpdateInventory(player);
        }
        private string CreateStatMeterTooltip(Player player, RemnantPlayer modPlayer)
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
			stringBuilder.Append("\nMelee Speed Boost: ").Append((meleeSpeedStat - 1f) * 100f).Append("%\n");
			stringBuilder.Append("Mana Regen: ").Append(manaRegenStat).Append("\n");
			stringBuilder.Append("Minion Slots: ").Append(minionSlotStat).Append("\n");
			stringBuilder.Append("Turret Slots: ").Append(turretSlotStat).Append("\n");
			stringBuilder.Append("Defense: ").Append(value).Append("\n");
			stringBuilder.Append("DR: ").Append(DamageReductionStat).Append("%\n");
			stringBuilder.Append("Life Regen: ").Append(lifeRegenStat).Append("\n");
			stringBuilder.Append("Life Regen Bonus: ").Append(lifeRegenBonusStat).Append("\n");
			stringBuilder.Append("Armor Penetration: ").Append(armorPenetrationStat).Append("\n");
			stringBuilder.Append("Wing Flight Time: ").Append(wingTime).Append(" seconds\n");
			stringBuilder.Append("Movement Speed Boost: ").Append((moveSpeedStat * 100f) - 100f).Append("%\n");
			stringBuilder.Append("Pick Speed Boost: ").Append(100f - (pickspeed * 100f)).Append("%\n");
			return stringBuilder.ToString();
		}
	}
}
