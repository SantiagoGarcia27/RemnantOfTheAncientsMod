using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles.Pets;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets
{
	public class DakoVanityBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{	
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{ 
			player.buffTime[buffIndex] = 18000;

			int projType = ModContent.ProjectileType<DakoPetProjectile>();
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
			}
		}
	}
}