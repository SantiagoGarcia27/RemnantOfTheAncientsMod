using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Items.BossSummon;
using RemnantOfTheAncientsMod.Items.Fmode;
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
		public static float originalsize;
		public float newsize = 0;
		public float originalsizep = 0;
		public float newsizep = 0;

		public void ReaperSize(Item item)
		{
			if (item.GetGlobalItem<GlobalItem1>().SizedByReaper)
			{
				originalsize = item.scale;
			}

			if (Reaper.ReaperMode && item.CountsAsClass(DamageClass.Melee) && item.GetGlobalItem<GlobalItem1>().SizedByReaper && item.active)
			{
				if (item.scale <= originalsize)
				{
					if (originalsize <= 0f) originalsize = 1f;
					item.GetGlobalItem<FchangesItem>().newsize = originalsize * 2.50f;
					item.scale = item.GetGlobalItem<FchangesItem>().newsize;
					//Player1.FWeapons = false;
					item.GetGlobalItem<GlobalItem1>().SizedByReaper = false;
                    
                }
			}
			else
			{
				if (!Reaper.ReaperMode && item.CountsAsClass(DamageClass.Melee) && item.active)
				{
					//item.active = false;
					if (originalsize <= 0f) originalsize = 1f;
					item.scale = originalsize;
					item.active = true;
					item.GetGlobalItem<GlobalItem1>().SizedByReaper = true;
                    
                }
			}

		
		}
        public void ReaperSize(Projectile projectile)
        {
            if (Reaper.ReaperMode)
            {
                if (projectile.CountsAsClass(DamageClass.Melee))
                {
					if (Player1.FWeapons)
					{
						originalsizep = projectile.scale;
					}
                    if (projectile.active)
                    {
                        newsizep = originalsizep + 1.50f;
                        projectile.scale = newsize;
                        Player1.FWeapons = false;
                        projectile.active = false;
                    }
                }
            }
        }
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
			npc.lifeMax = (int)NpcChanges1.ExpertLifeScale(lifeMax, true);
			npc.damage = (int)NpcChanges1.ExpertDamageScale(damage);
		}

	}
}
