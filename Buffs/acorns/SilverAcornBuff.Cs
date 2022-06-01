using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using opswordsII.Dusts;
using opswordsII.Items.Summon.Saplings;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Buffs.acorns
{
        public class SilverAcornBuff  : ModBuff
	    {

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Animated Silver Sapling");
			Description.SetDefault("The Animated Silver Sapling will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			if (player.ownedProjectileCounts[ProjectileType<SaplingSilverMinion>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}