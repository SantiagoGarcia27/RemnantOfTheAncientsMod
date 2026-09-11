using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Dusts;
using RemnantOfTheAncientsMod.Content.Items.Mounts;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.IL_Mount;
using static Terraria.Mount;

namespace RemnantOfTheAncientsMod.Content.Mounts
{
	public class Sword : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = DustID.YellowStarDust;
			MountData.buff = ModContent.BuffType<SwordMount>();
			MountData.heightBoost = 20;
			MountData.fallDamage = 0.5f;
			MountData.dashSpeed = 8f;
			MountData.jumpHeight = 5;
			MountData.jumpSpeed = 4f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 4;
			MountData.constantJump = false;
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

			if (Main.netMode != NetmodeID.Server)
			{
				MountData.textureWidth = MountData.backTexture.Width() + 20;
				MountData.textureHeight = MountData.backTexture.Height();
			}
		}

		int inmuniyTimer = 0;
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

                    int damage;
					if (Main.netMode == NetmodeID.SinglePlayer) damage = player.GetModPlayer<RemnantPlayer>().starRampageBaseItem.damage;
					else
					{
						damage = (player.HeldItem != null && player.HeldItem.type == ModContent.ItemType<StarRampage>()) ?
							player.HeldItem.damage :
							ContentSamples.ItemsByType[ModContent.ItemType<StarRampage>()].damage;
					}

                    if (inmuniyTimer % 2 == 0)
					{
						foreach (NPC npc in Main.ActiveNPCs)
						{
							if (npc.active && npc.immune[player.whoAmI] == 0 && npc.life > 0 && mountHitbox.Intersects(npc.getRect()))
							{
								npc.SimpleStrikeNPC(damage, player.direction, true, 6f, DamageClass.Melee, false, 0, false);
							}
						}
					}
                    inmuniyTimer++;

                }
			}
		}
	
        public override void SetMount(Player player, ref bool skipDust)
        {
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				player.GetModPlayer<RemnantPlayer>().starRampageBaseItem = player.HeldItem.Clone();

				if (player.HeldItem.type == ModContent.ItemType<StarRampage>())
				{
					if (!player.GetModPlayer<RemnantPlayer>().starRampageBaseItem.IsAir) player.HeldItem.TurnToAir();		
				}
			}
            base.SetMount(player, ref skipDust);
        }

        public override void Dismount(Player player, ref bool skipDust)
        {
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				player.QuickSpawnItem(player.GetSource_FromThis(), player.GetModPlayer<RemnantPlayer>().starRampageBaseItem);
			}
            base.Dismount(player, ref skipDust);
        }


		float _rotation = 44.79f;
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
			Texture2D textureRequest;

            try
            {
                string texturePath;
				int type = ModContent.ItemType<StarRampage>();

                if (type < ItemID.Count) texturePath = "Terraria/Images/Item_" + type;
                else  texturePath = ItemLoader.GetItem(type).Texture;
                
                // Use ModContent.Request for proper texture loading
                textureRequest =  (Texture2D)ModContent.Request<Texture2D>(texturePath);
                textureRequest ??= ModContent.Request<Texture2D>("Terraria/Images/Item_" + ItemID.CopperShortsword).Value;
                
            }
            catch
            {
                // Fallback to default texture if loading fails
                textureRequest = ModContent.Request<Texture2D>("Terraria/Images/Item_" + ItemID.CopperShortsword).Value;
            }
			
            Vector2 origin = textureRequest.Size() / 2f;
            //45
			bool touchFloor = DistanceUtils.GetPlayerFlour(drawPlayer) != 0 || DistanceUtils.GetPlayerFlour(drawPlayer, 3) != 0;
			
			if (touchFloor && _rotation >= 44.79f) _rotation -= 0.01f;
			else if (_rotation <= 45) _rotation += 0.01f;

			Main.spriteBatch.Draw(textureRequest, (drawPlayer.Center - Main.screenPosition) + new Vector2(0, 10), null, drawColor, _rotation * drawPlayer.direction, origin, drawScale, spriteEffects, 0f);
			return false;
        }
    }
}
