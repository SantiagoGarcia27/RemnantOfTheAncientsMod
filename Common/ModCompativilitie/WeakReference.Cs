using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.NPCs.Town;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod
{
    internal class WeakReference : ModSystem
    {
        //private static void AddBoss(Mod bossChecklist, Mod hostMod, string name, float difficulty, List<int> npcType, Func<bool> downed, object summon, List<int> collection, string instructions, string despawn, string bossTexture = null, string bossHead = null)
        //{
        //	bossChecklist.Call(new object[13]
        //	{
        //		"AddBoss",
        //		hostMod,
        //		name,
        //		npcType,
        //		difficulty,
        //		downed,
        //		() => true,
        //		collection,
        //		summon ?? null,
        //		instructions,
        //		despawn,
        //              //loot,
        //		bossTexture == null ? null :
        //		(SpriteBatch sb, Rectangle rect, Color color) => {
        //		Texture2D texture = ModContent.Request<Texture2D>(bossTexture).Value;
        //		Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
        //		sb.Draw(texture, centered, color);
        //		},
        //		bossHead
        //	});
        //}
        public override void PostSetupContent()
        {

			if (RemnantOfTheAncientsMod.Census != null)
			{
                string message = $"Have either an [i:{ItemID.GoldWatch}] or an [i:{ItemID.PlatinumWatch}] in your inventory after defeating the corrupt boss";
                RemnantOfTheAncientsMod.Census.Call("TownNPCCondition", NPCType<Time_Wizard>(), message);
                //AddNpcChecklist();
			}
        }
		private static void AddNpcChecklist()
		{
			//int npcType = ModContent.NPCType<Time_Wizard>();

			// The message makes use of chat tags to make the item appear directly, making it more fancy
			string message = $"Have either an [i:{ItemID.GoldWatch}] or an [i:{ItemID.PlatinumWatch}] in your inventory after defeating the corrupt boss";

			// Finally, call the desired method
			RemnantOfTheAncientsMod.Census.Call("TownNPCCondition", ModContent.NPCType<Time_Wizard>(), message);
		}


		//	private static void AddBossesChecklist()
		//	{
		//		string DespawnMessage;
		//		List<int> collection;

		//		#region desert	
		//		collection = new List<int>
		//		{
		//			ItemType<DesertTrophy>(),
		//			ItemType<DesertAMask>(),
		//			ItemType<DesertMusicBox>()
		//		};
		//		DespawnMessage = Utils1.ColorMessage("The desert guardian has teleport.", new Color(100, 149, 237));
		//		AddGenericBoss(LocalizationHelper.GetNpcName(NPCType<DesertAniquilator>()), 6.8f, new List<int> { NPCType<DesertAniquilator>() }, () => RemnantDownedBossSystem.downedDesert, ItemType<DesertChest>(), collection, "desert", DespawnMessage);
		//		#endregion


		//		#region frozen
		//		collection = new List<int>
		//		{
		//			ItemType<FrostTrophy>(),
		//			ItemType<FrozenMask>(),
		//			ItemType<FrozenMusicBox>(),
		//			ItemType<Frozenp2MusicBox>()
		//		};
		//		DespawnMessage = Utils1.ColorMessage("The frozen threat disappears on the horizon", new Color(238, 232, 170));
		//		AddGenericBoss(LocalizationHelper.GetNpcName(NPCType<FrozenAssaulter>()), 7.2f, new List<int> { NPCType<FrozenAssaulter>() }, () => RemnantDownedBossSystem.downedFrozen, ItemType<FrozenArtifact>(), collection, "snow at the night", DespawnMessage);
		//		#endregion

		//		#region tyrant
		//		List<int> npcTypes = new List<int>
		//		{
		//			NPCType<InfernalTyrantHead>(),
		//			NPCType<InfernalTyrantBody>(),
		//			NPCType<InfernalTyrantTail>()
		//		};
		//		collection = new List<int>
		//		{
		//			ItemType<InfernalTrophy>(),
		//			ItemType<InfernalMask>(),
		//			ItemType<InfernalMusicBox>()
		//		};
		//		DespawnMessage = Utils1.ColorMessage("The tyrant retreats into the depths of the lava", new Color(238, 232, 170));
		//		string path = "RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/";

		//		string bossImage = path + "InfernalTyrant_BossChecklist";
		//		string bossHead = path + "InfernalTyrantHead_Head_Boss";

		//		AddGenericBoss(LocalizationHelper.GetNpcName(NPCType<InfernalTyrantHead>()), 12.9f, npcTypes, () => RemnantDownedBossSystem.downedTyrant, ItemType<InfernalCalis>(), collection, "Underworld", DespawnMessage, bossHead, bossImage);
		//		#endregion
		//	}
		//	public static void AddGenericBoss(string BossName, float progrecion, List<int> npcType, Func<bool> Downed, int Spawner, List<int> Collection, string spawnBiome, string DespawnText, string BossHead = null, string BossImage = null)
		//	{
		//		string SpawnDescription = $"Use a [i:{Spawner}] in the {spawnBiome}";
		//		AddBoss(RemnantOfTheAncientsMod.BossChecklist, GetInstance<RemnantOfTheAncientsMod>(), BossName, progrecion, npcType, Downed, Spawner, Collection, SpawnDescription, DespawnText, BossImage, BossHead);
		//	}
	}
}

