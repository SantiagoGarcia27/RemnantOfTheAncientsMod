using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using opswordsII.Dusts;
using opswordsII.Projectiles.Minioms;
using opswordsII.VanillaChanges;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Buffs
{
	public class Sand_Weapons : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("[c/48E16F:Wepon Imbue: Burning Sand]");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
			Main.persistentBuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex) {
			Player1 player1 = player.GetModPlayer<Player1>();
			player1.SandWeapons = true;
			player.GetModPlayer<Player1>().VanillaFlaskInmune();
		}
	}
}