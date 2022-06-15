using opswordsII.Items.BossSummon;
using opswordsII.Items.Fmode;
using opswordsII.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace opswordsII.VanillaChanges
{
	public class FchangesItem : GlobalItem
	{
		public override bool InstancePerEntity => true; //InstancePerEntity

		//public static bool Fscale = false;
		public static float originalsize = 0;
		public static float newsize = 0;


		public static void ReaperSize(Item item)
		{
			if (Reaper.ReaperMode)
			{
				if (item.CountsAsClass(DamageClass.Melee))
				{
					if (Player1.FWeapons)
						originalsize = item.scale;
					if (item.active)
					{
						newsize = originalsize + 1.50f;
						item.scale = newsize;
						Player1.FWeapons = false;
						item.active = false;
					}
				}
			}
			else
			{
				if (item.CountsAsClass(DamageClass.Melee) && !item.active)
				{
					item.scale = originalsize;
					item.active = true;
				}
			}
		}

		public static void ReaperConsumable(Item item)
        {
			/*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
			if (CalamityMod != null)
            {

            }
            else
            {*/
				if (item.type == 560 || item.type == 43 || item.type == 70 || item.type == 1331 || item.type == 1133 || item.type == 544 || item.type == 556 || item.type == 557 || item.type == 3601 || item.type == ModContent.ItemType<DesertChest>() || item.type == ModContent.ItemType<FrozenArtifact>() || item.type == ModContent.ItemType<InfernalCalis>())
				{
                    if (Reaper.ReaperMode)
                    {
						item.consumable = false;
						item.GetGlobalItem<GlobalItem1>().BossSummon = true;
					}
                    else
                    {
						item.consumable = true;

						item.GetGlobalItem<GlobalItem1>().BossSummon = false;
					}
				} 

            }

		//}
		public static void ReaperWingsNerf(Player player)
		{
			int OgWingTime;
			if (Reaper.ReaperMode)
			{
				if (player.wingTimeMax >= 4)
				{

					OgWingTime = player.wingTimeMax;
					if (!player.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade)
					{
						player.wingTimeMax = OgWingTime / 2;
					}
					else
					{
						player.wingTimeMax = OgWingTime;
					}
				}
			}
		}
	}
	public class FchangesNpc : GlobalNPC
	{
		public override void SetDefaults(NPC npc)
		{
			if (npc.boss)
			{
				ModifyBossStats(npc, ref npc.damage, npc.lifeMax);
			}
		}
		public static void ModifyBossStats(NPC npc, ref int damage, int lifeMax)
		{
				npc.lifeMax = (int)NpcChanges1.ExpertLifeScale(lifeMax,"n");
				npc.damage = (int)NpcChanges1.ExpertDamageScale(damage,"n");
		}
			
	}
}
