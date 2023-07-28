using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Dusts;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.Mount;

namespace RemnantOfTheAncientsMod.Content.Mounts
{
	public class Sword : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = ModContent.DustType<Smoke>();
			MountData.buff = ModContent.BuffType<SwordMount>();
			MountData.heightBoost = 20;
			MountData.fallDamage = 0.5f;
			MountData.dashSpeed = 8f;
			MountData.jumpHeight = 5;
			MountData.jumpSpeed = 4f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 4;
			MountData.constantJump = false;
			MountData.flightTimeMax = 10;
			MountData.acceleration = 0.3f;
			MountData.runSpeed = 11f;
			MountData.flightTimeMax = 1000;
			MountData.fatigueMax = 0;

			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
			}
			MountData.playerYOffsets = array;
			MountData.xOffset = 5;
			MountData.bodyFrame = 3;
			MountData.yOffset = 0;
			MountData.playerHeadOffset = 25;
			MountData.standingFrameCount = 4;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 12;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 1;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 4;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = true;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				MountData.textureWidth = MountData.backTexture.Width() + 20;
				MountData.textureHeight = MountData.backTexture.Height();
			}
		}

		/*
		public override void SetMount(Player player) {

		if (NPC.downedBoss1 == true){
mountData.runSpeed = 5f;
mountData.flightTimeMax = 100;
mountData.fatigueMax = 5;
mountData.acceleration = 0.3f;
		}
		if(NPC.downedBoss1 == true && NPC.downedBoss2 == true){
mountData.runSpeed = 6f;
mountData.flightTimeMax = 200;
mountData.fatigueMax = 4;
mountData.acceleration = 0.3f;
		}
		if(NPC.downedBoss1 == true && NPC.downedBoss2 == true && NPC.downedBoss3 == true){
mountData.runSpeed = 7f;
mountData.flightTimeMax = 300;
mountData.fatigueMax = 2;
		}
		if (Main.hardMode)
		{
mountData.runSpeed = 8f;
mountData.flightTimeMax = 400;
mountData.fatigueMax = 1;
		}
		if(NPC.downedMechBossAny == true){
mountData.runSpeed = 9f;
mountData.flightTimeMax = 600;
mountData.fatigueMax = 1;
		}
		if(NPC.downedPlantBoss == true){
mountData.runSpeed = 10f;
mountData.flightTimeMax = 800;
mountData.fatigueMax = 0;
mountData.acceleration = 0.2f;
		}
		if(NPC.downedGolemBoss){
mountData.runSpeed = 11f;
mountData.flightTimeMax = 1000;
mountData.fatigueMax = 0;
mountData.acceleration = 0.2f;
		}
		if(NPC.downedFishron){
mountData.runSpeed = 12f;
mountData.flightTimeMax = 1100;
mountData.fatigueMax = 0;
mountData.acceleration = 0.2f;
		}
		if(NPC.downedAncientCultist){
mountData.runSpeed = 13f;
mountData.flightTimeMax = 1300;
mountData.fatigueMax = 0;
mountData.acceleration = 0.2f;
		}
		if(NPC.downedTowers){
mountData.runSpeed = 14f;
mountData.flightTimeMax = 1500;
mountData.fatigueMax = 0;
mountData.acceleration = 0.1f;
		}
		if(NPC.downedMoonlord){
mountData.runSpeed = 15f;
mountData.flightTimeMax = 100000;
mountData.fatigueMax = 0;
mountData.acceleration = 0.3f;
		}
		}
*/

		public override void UpdateEffects(Player player)
		{
			if (player.mount.Active)
			{
				if (Math.Abs(player.velocity.X) > 10f)
				{
					Rectangle rect = player.getRect();
					Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, ModContent.DustType<Smoke>());

					// Check if the mount is currently colliding with an NPC
					Rectangle mountHitbox = player.getRect();
					mountHitbox.Inflate(10, 10);
					for (int i = 0; i < Main.npc.Length; i++)
					{
						NPC npc = Main.npc[i];
						if (npc.active && npc.immune[player.whoAmI] == 0 && npc.life > 0 && mountHitbox.Intersects(npc.getRect()))
						{
							// Damage the NPC and add knockback
							int damage = 20; // Change this value to adjust the amount of damage dealt
							float knockback = 6f; // Change this value to adjust the knockback


							//npc.StrikeNPC(damage, knockback, npc.direction, true, false);
						}
					}
				}
			}
		}

	}
}
