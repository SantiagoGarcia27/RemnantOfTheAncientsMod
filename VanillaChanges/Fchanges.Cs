using RemnantOfTheAncientsMod.Items.accesorios;
using RemnantOfTheAncientsMod.Items.BossSummon;
using RemnantOfTheAncientsMod.Items.ReaperSouls;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.VanillaChanges
{
	public class FchangesItem : GlobalItem
	{
		public override bool InstancePerEntity => true; //InstancePerEntity

		//public static bool Fscale = false;
		public static float originalsize = 1;
		public float newsize = 0;
		



        public override void ModifyItemScale(Item item, Player player, ref float scale)
        {
            RemnantPlayer modPlayer = player.GetModPlayer<RemnantPlayer>();

            if (modPlayer.ChaliceOn && Reaper.ReaperMode && !item.IsAir && item.damage > 0 && !item.noMelee && item.active)
            {
                scale *= 2.5f;
            }
        }


		//         if (item.GetGlobalItem<GlobalItem1>().SizedByReaper)
		//{
		//	originalsize = item.scale;
		//}
		//Player player = new Player();
		//if (Reaper.ReaperMode && item.CountsAsClass(DamageClass.Melee) && item.GetGlobalItem<GlobalItem1>().SizedByReaper && item.active)
		//{


		//             if (item.scale <= originalsize)
		//	{
		//                 originalsize = item.scale;
		//                 //if (originalsize <= 0f) originalsize = 1f;
		//                 item.GetGlobalItem<FchangesItem>().newsize = originalsize * 2.50f;
		//		item.scale = item.GetGlobalItem<FchangesItem>().newsize;
		//		//Player1.FWeapons = false;
		//		item.GetGlobalItem<GlobalItem1>().SizedByReaper = false;
		//		UpdateInventory(item, player);
		//             }
		//}
		//else
		//{
		//	if (!Reaper.ReaperMode && item.CountsAsClass(DamageClass.Melee) && item.active)
		//	{
		//                 //item.active = false;
		//                 if (originalsize <= 0f) originalsize = 1f;
		//		item.active = true;
		//		item.GetGlobalItem<GlobalItem1>().SizedByReaper = true;
		//                 UpdateInventory(item, player);
		//             }
		//}
		//	}
		
        public static void ReaperSetDefaut(Item item)
        {
			if (item.type == ModContent.ItemType<ReaperChalice>())
			{
				item.accessory = Reaper.ReaperMode ? true : false;
			}
		}
		public static void ReaperConsumable(Item item)
		{
			if (RemnantOfTheAncientsMod.CalamityMod == null)
			{
				if (item.type == ItemID.SlimeCrown ||
					item.type == ItemID.SuspiciousLookingEye ||
					item.type == ItemID.WormFood ||
					item.type == ItemID.BloodySpine ||
					item.type == ItemID.Abeemination ||
					item.type == ItemID.MechanicalEye ||
					item.type == ItemID.MechanicalWorm ||
					item.type == ItemID.MechanicalSkull ||
					item.type == ItemID.CelestialSigil ||
					item.type == ModContent.ItemType<DesertChest>() ||
					item.type == ModContent.ItemType<FrozenArtifact>() ||
					item.type == ModContent.ItemType<InfernalCalis>())
				{
					item.consumable = Reaper.ReaperMode ? false : true;
					if (Reaper.ReaperMode) item.GetGlobalItem<GlobalItem1>().BossSummon = true;
					else item.GetGlobalItem<GlobalItem1>().BossSummon = false;
				}
			}
		}
		public static void ReaperWingsNerf(Player player)
		{
			int OgWingTime;
			if (Reaper.ReaperMode)
			{
				if (player.wingTimeMax >= 4)
				{
					OgWingTime = player.wingTimeMax;
					if (!player.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade || !player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade) player.wingTimeMax = OgWingTime / 2;
					else player.wingTimeMax = OgWingTime;				
				}
			}
		}
	}
	public class FchangesNpc : GlobalNPC
	{
		public override void SetDefaults(NPC npc)
		{
			if (npc.boss) ModifyBossStats(npc, ref npc.damage, npc.lifeMax);
		}
		public static void ModifyBossStats(NPC npc, ref int damage, int lifeMax)
		{
			npc.lifeMax = (int)NpcChanges1.ExpertLifeScale(lifeMax);
			npc.damage = (int)NpcChanges1.ExpertDamageScale(damage);
		}

	}
}
