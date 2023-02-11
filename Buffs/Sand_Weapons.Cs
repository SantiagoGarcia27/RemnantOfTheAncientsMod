using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using RemnantOfTheAncientsMod.Dusts;
using RemnantOfTheAncientsMod.Projectiles.Minioms;
using RemnantOfTheAncientsMod.VanillaChanges;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs
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
			RemnantPlayer player1 = player.GetModPlayer<RemnantPlayer>();
			player1.SandWeapons = true;
			player.GetModPlayer<RemnantPlayer>().VanillaFlaskInmune();
		}
	}
}