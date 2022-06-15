using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using opswordsII.Dusts;
using opswordsII.Projectiles.Minioms;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Buffs
{
        public class DesertMinionBuff : ModBuff
	    {

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Baby Desert Aniquilator");
			Description.SetDefault("The Baby Desert will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			if (player.ownedProjectileCounts[ProjectileType<DesertMinion>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}