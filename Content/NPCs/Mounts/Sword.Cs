using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Dusts;
using RemnantOfTheAncientsMod.Content.Items.Mount;
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
		static Item baseItem;
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

					if (inmuniyTimer % 2 == 0)
					{
						foreach (NPC npc in Main.ActiveNPCs)
						{
							if (npc.active && npc.immune[player.whoAmI] == 0 && npc.life > 0 && mountHitbox.Intersects(npc.getRect()))
							{
								int damage = baseItem.damage;
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
            baseItem = player.HeldItem.Clone();
			if (player.HeldItem.type == ModContent.ItemType<StarRampage>())
			{
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					if (!baseItem.IsAir)
						player.HeldItem.TurnToAir();
				}
			}
            base.SetMount(player, ref skipDust);
        }

        public override void Dismount(Player player, ref bool skipDust)
        {
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				player.QuickSpawnItem(player.GetSource_FromThis(), baseItem);
			}
            base.Dismount(player, ref skipDust);
        }
		float _rotation = 44.79f;
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            if (baseItem == null || baseItem.IsAir)
                baseItem = new Item(ItemID.CopperShortsword);
			Texture2D textureRequest;
            try
            {
                string texturePath;
                if (baseItem.type < ItemID.Count)
                {
                    texturePath = "Terraria/Images/Item_" + baseItem.type;
                }
                else
                {
                    texturePath = ItemLoader.GetItem(baseItem.type).Texture;
                }

                // Use ModContent.Request for proper texture loading
                textureRequest =  (Texture2D)ModContent.Request<Texture2D>(texturePath);
                if (textureRequest == null)
                {
					textureRequest = ModContent.Request<Texture2D>("Terraria/Images/Item_" + ItemID.CopperShortsword).Value;
                }
            }
            catch
            {
                // Fallback to default texture if loading fails
                textureRequest = ModContent.Request<Texture2D>("Terraria/Images/Item_" + ItemID.CopperShortsword).Value;
            }
			Player player = Main.player[Main.myPlayer];
            Vector2 origin = textureRequest.Size() / 2f;
            //45
			bool touchFloor = DistanceUtils.GetPlayerFlour(player) != 0 || DistanceUtils.GetPlayerFlour(player,3) != 0;
			if (touchFloor) {
				if (_rotation >= 44.79f) _rotation -= 0.01f;
			}
			else {
				if (_rotation <= 45) _rotation += 0.01f;
			}
            
			
				Main.spriteBatch.Draw(textureRequest, (player.Center - Main.screenPosition) + new Vector2(0, 10), null, drawColor, _rotation * player.direction, origin, drawScale, spriteEffects, 0f);
			return false;//base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);
			/*if (baseItem.IsAir || baseItem == null) baseItem = new Item(ItemID.CopperShortsword);
                string texturePath = baseItem.type < ItemID.Count ? "Terraria/Images/Item_" + baseItem.type : ItemLoader.GetItem(baseItem.type).Texture;
			Texture2D request = (Texture2D)ModContent.Request<Texture2D>(texturePath);
			texture = request;
            return base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);*/
        }
    }
}
