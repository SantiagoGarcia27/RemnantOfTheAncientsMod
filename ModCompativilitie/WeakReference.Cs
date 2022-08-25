using System;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Mele;
using opswordsII.Items.Ranger;
using opswordsII.Items.Magic;
using opswordsII.NPCs.ITyrant;
using opswordsII.Items.BossSummon;
using opswordsII.Items.tresure_bag;
using opswordsII.Items.Armor.Masks;
using opswordsII.Items.Bloques.MusicBox;
using opswordsII.Items.Ranger.Rep;
using opswordsII.Items.Mele.saber;
using opswordsII.Items.Summon;
using opswordsII.Items.Core;
using opswordsII.NPCs.DAniquilator;
using opswordsII.Items.Bloques;
using opswordsII.NPCs.frozen_assaulter;
using opswordsII.Common.Systems;

namespace opswordsII
{
	internal class WeakReference
	{
		private static void AddBoss(Mod bossChecklist, Mod hostMod, string name, float difficulty, int npcType, Func<bool> downed, object summon, List<int> loot, List<int> collection, string instructions, string despawn, string bossLogTex = null, string bossHeadTex = null)
		{
			bossChecklist.Call(new object[13]
			{
				"AddBoss",
				difficulty,
				npcType,
				hostMod,
				name,
				downed,
				summon ?? null,
				collection,
				loot,
				instructions,
				despawn,
				bossLogTex,
				bossHeadTex
			});
		}

		private static void AddBossWorm(Mod bossChecklist, Mod hostMod, string name, float difficulty, List<int> npcTypes, Func<bool> downed, object summon, List<int> loot, List<int> collection, string instructions, string despawn, string bossLogTex = null, string bossHeadTex = null)
		{
			bossChecklist.Call(new object[13]
			{
				"AddBoss",
				difficulty,
				npcTypes,
				hostMod,
				name,
				downed,
				summon ?? null,
				collection,
				loot,
				instructions,
				despawn,
				bossLogTex,
				bossHeadTex
			});
		}




		public static void Setup()
		{
			BossChecklistSupport();
		}
		private static void BossChecklistSupport()
		{
			opswordsII instance = GetInstance<opswordsII>();
			if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist)) AddOpsBosses(opswordsII.BossChecklist, (Mod)(object)instance);

		}

		private static void AddOpsBosses(Mod bossChecklist, Mod OpswordsII)
		{
            #region desert
            float value = 6.8f;
			int npcType = NPCType<DesertAniquilator>();
			int num = ItemType<DesertChest>();
			List<int> loot = new List<int>
				{
					ItemType < desertBag >(),
					ItemType < DesertEdge >(),
					ItemType < DesertTome >()
				};
			List<int> collection = new List<int>
				{
					ItemType<DesertTrophy>(),
					ItemType<DesertAMask>(),
					ItemType<DesertMusicBox>()
				};
			string instructions = $"Use a [i:{num}] in the desert";
			string despawn = Utils1.ColorMessage("The guardian of the desert has returned to rest.", new Color(100, 149, 237));
			AddBoss(bossChecklist, OpswordsII, "Desert Annihilator", value, npcType, () => DownedBossSystem.downedDesert, num, loot, collection, instructions, despawn);
            #endregion
            #region frozen
            float value2 = 7.2f;
			int npcType2 = NPCType<frozen_assaulter>();
			int num2 = ItemType<FrozenArtifact>();
			List<int> loot2 = new List<int>
			{
				ItemType<frostBag>(),
				ItemType<Permafrost>(),
				ItemType<frozen_staff>(),
				ItemType<FrozenStafff>(),
				ItemType<FrostShark>(),
				ItemType<Frost_core>(),
				593,
				188,
			};
			List<int> collection2 = new List<int>
			{
				ItemType<FrostTrophy>(),
				ItemType<FrozenMask>(),
				ItemType<FrozenMusicBox>(),
				ItemType<Frozenp2MusicBox>()
			};
			string instructions2 = $"Use a [i:{num2}] in the snow at the night";
			string despawn2 = Utils1.ColorMessage("The desert guardian has teleport.", new Color(100, 149, 237));
			AddBoss(bossChecklist, OpswordsII,"Frozen Assaulter", value2, npcType2, () => DownedBossSystem.downedFrozen, num2, loot2, collection2, instructions2, despawn2);
            #endregion
            #region tyrant
            float value3 = 12.9f;
			List<int> npcTypes3 = new List<int>
			{
				NPCType<InfernalTyrantHead>(),
				NPCType<InfernalTyrantBody>(),
				NPCType<InfernalTyrantTail>()
			};

			int num3 = ModContent.ItemType<InfernalCalis>();
			List<int> loot3 = new List<int>
			{
				ItemType<infernalBag>(),
				ItemType<Tyrant_repeater>(),
				ItemType<Spike_saber>(),
				ItemType<TheSpiker>(),
				ItemType<Tyran_Blast>(),
				ItemType<Infernal_core>(),
				499,
			};
			List<int> collection3 = new List<int>
			{
				ItemType<InfernalTrophy>(),
				ItemType<InfernalMask>(),
				ItemType<InfernalMusicBox>()
			};
			string instructions3 = $"Use a [i:{num3}] in the Underworld";
			string despawn3 = Utils1.ColorMessage("The scourge of the desert delved back into the sand.", new Color(238, 232, 170));
			string bossLogTex = "opswordsII/NPCs/ITyrant/InfernalTyrant_BossChecklist";
			AddBossWorm(bossChecklist, OpswordsII, "Infernal Tyrant", value3, npcTypes3, () => DownedBossSystem.downedTyrant, num3, loot3, collection3, instructions3, despawn3, bossLogTex);
            #endregion
        }

    }
}
