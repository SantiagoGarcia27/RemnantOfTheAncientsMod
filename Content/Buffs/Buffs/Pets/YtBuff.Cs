using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Pets;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets
{
	public class YtBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//// //DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			//// //DisplayName.SetDefault("Paper Airplane");
			// ////Description.SetDefault("\"Let this pet be an example to you!\"");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
			// Main.vanityPet[Type] = true;
			//DisplayName.SetDefault("Yt");
			//Description.SetDefault("...");
		}

		public override void Update(Player player, ref int buffIndex)
		{ // This method gets called every frame your buff is active on your player.
			player.buffTime[buffIndex] = 18000;

			int projType = ModContent.ProjectileType<YtPet>();

			// If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<YtPet>(), 0, 0f, player.whoAmI);
			}
		}
	}
}