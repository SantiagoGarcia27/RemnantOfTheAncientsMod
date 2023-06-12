using System;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.frozen_assaulter;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.NPCs.Town;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod
{
    internal class WeakReference
	{
		private static void AddBoss(Mod bossChecklist, Mod hostMod, string name, float difficulty, List<int> npcType, Func<bool> downed, object summon, List<int> collection, string instructions, string despawn, string bossTexture = null, string bossHead = null)
		{
			bossChecklist.Call(new object[13]
			{
				"AddBoss",
				hostMod,
				name,
				npcType,
				difficulty,
				downed,
				() => true,
				collection,
				summon ?? null,
				instructions,
				despawn,
                //loot,
				bossTexture == null ? null :
				(SpriteBatch sb, Rectangle rect, Color color) => {
				Texture2D texture = ModContent.Request<Texture2D>(bossTexture).Value;
				Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
				sb.Draw(texture, centered, color);
				},
				bossHead
			});
		}
		public static void Setup()
		{
			BossChecklistSupport();
		}
		private static void BossChecklistSupport()
		{
			if (RemnantOfTheAncientsMod.BossChecklist != null) AddBossesChecklist();
			if (RemnantOfTheAncientsMod.Census != null) AddNpcChecklist();
		}


		private static void AddNpcChecklist()
		{
			int npcType = ModContent.NPCType<Time_Wizard>();

			// The message makes use of chat tags to make the item appear directly, making it more fancy
			string message = $"Have either an [i:{ItemID.GoldWatch}] or an [i:{ItemID.PlatinumWatch}] in your inventory after defeating the corrupt boss";

			// Finally, call the desired method
			RemnantOfTheAncientsMod.Census.Call("TownNPCCondition", npcType, message);
		}


		private static void AddBossesChecklist()
		{
			string DespawnMessage;
			List<int> collection;

			#region desert	
			collection = new List<int>
			{
				ItemType<DesertTrophy>(),
				ItemType<DesertAMask>(),
				ItemType<DesertMusicBox>()
			};
			DespawnMessage = Utils1.ColorMessage("The desert guardian has teleport.", new Color(100, 149, 237));
			AddGenericBoss("Desert Annihilator", 6.8f, new List<int> { NPCType<DesertAniquilator>() }, () => RemnantDownedBossSystem.downedDesert, ItemType<DesertChest>(), collection, "desert", DespawnMessage);
			#endregion


			#region frozen
			collection = new List<int>
			{
				ItemType<FrostTrophy>(),
				ItemType<FrozenMask>(),
				ItemType<FrozenMusicBox>(),
				ItemType<Frozenp2MusicBox>()
			};
			DespawnMessage = Utils1.ColorMessage("The frozen threat disappears on the horizon", new Color(238, 232, 170));
			AddGenericBoss("Frozen Assaulter", 7.2f, new List<int> { NPCType<frozen_assaulter>() }, () => RemnantDownedBossSystem.downedFrozen, ItemType<FrozenArtifact>(), collection, "snow at the night", DespawnMessage);
			#endregion

			#region tyrant
			List<int> npcTypes = new List<int>
			{
				NPCType<InfernalTyrantHead>(),
				NPCType<InfernalTyrantBody>(),
				NPCType<InfernalTyrantTail>()
			};
			collection = new List<int>
			{
				ItemType<InfernalTrophy>(),
				ItemType<InfernalMask>(),
				ItemType<InfernalMusicBox>()
			};
			DespawnMessage = Utils1.ColorMessage("The tyrant retreats into the depths of the lava", new Color(238, 232, 170));
			string path = "RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/";

			string bossImage = path + "InfernalTyrant_BossChecklist";
			string bossHead = path + "InfernalTyrantHead_Head_Boss";

			AddGenericBoss("Infernal Tyrant", 12.9f, npcTypes, () => RemnantDownedBossSystem.downedTyrant, ItemType<InfernalCalis>(), collection, "Underworld", DespawnMessage, bossHead, bossImage);
			#endregion
		}
		public static void AddGenericBoss(string BossName, float progrecion, List<int> npcType, Func<bool> Downed, int Spawner, List<int> Collection,string spawnBiome,string DespawnText,string BossHead = null,string BossImage = null)
		{
            string SpawnDescription = $"Use a [i:{Spawner}] in the {spawnBiome}";
            AddBoss(RemnantOfTheAncientsMod.BossChecklist, GetInstance<RemnantOfTheAncientsMod>(), BossName, progrecion, npcType, Downed, Spawner, Collection, SpawnDescription, DespawnText,BossImage,BossHead);
        }
	}
}
