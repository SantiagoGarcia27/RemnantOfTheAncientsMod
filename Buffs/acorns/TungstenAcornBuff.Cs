using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using RemnantOfTheAncientsMod.Dusts;
using RemnantOfTheAncientsMod.Items.Summon.Saplings;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Projectiles.Minioms.Saplings;

namespace RemnantOfTheAncientsMod.Buffs.acorns
{
        public class TungstenAcornBuff  : ModBuff
	    {

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Animated Tungsten Sapling");
			Description.SetDefault("The Animated Tungsten Sapling will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			if (player.ownedProjectileCounts[ProjectileType<SaplingTungstenMinion>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}