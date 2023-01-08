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
		public static float originalsize = 0;
		public static float newsize = 0;
        public static float originalsizep = 0;
        public static float newsizep = 0;

        public static void ReaperSize(Item item)
		{
			if (Reaper.ReaperMode)
			{
				if (item.CountsAsClass(DamageClass.Melee))
				{
					if (Player1.FWeapons) originalsize = item.scale;
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
        public static void ReaperSize(Projectile projectile)
        {
            if (Reaper.ReaperMode)
            {
                if (projectile.CountsAsClass(DamageClass.Melee))
                {
                    if (Player1.FWeapons) originalsizep = projectile.scale;
                    if (projectile.active)
                    {
                        newsizep = originalsizep + 1.50f;
                        projectile.scale = newsize;
                        Player1.FWeapons = false;
                        projectile.active = false;
                    }
                }
            }
           /* else
            {
                if (projectile.CountsAsClass(DamageClass.Melee) && !projectile.active)
                {
                    projectile.scale = originalsizep;
                    projectile.active = true;
                }
            }*/
        }
        public static void ReaperSetDefaut(Item item)
        {
			if (item.type == ModContent.ItemType<ReaperChalice>()) item.accessory = Reaper.ReaperMode ? true : false;
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
			npc.damage = (int)NpcChanges1.ExpertDamageScale(damage, true);
		}

	}
}
