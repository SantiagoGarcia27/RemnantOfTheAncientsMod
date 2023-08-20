using Terraria;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Dusts;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
	public class RemnantGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool Burn_Sand;
		public bool Hell_Fire;
		public bool Javalina;
		public bool hBurn;
		public bool Marble_Erosion;
		public bool Can_Marble;
		public bool CanMakeCrit;
		public int CritChance;
		public bool CanDrop = true;
		public override void ResetEffects(NPC NPC)
		{
			Burn_Sand = false;
			Javalina = false;
			Hell_Fire = false;
			hBurn = false;
			Marble_Erosion = false;
		}

		public override void SetDefaults(NPC npc)
		{
			if (npc.type == NPCID.BigMimicCrimson)
			{
				npc.lifeMax *= 2;
				npc.defense += 10;
			}
            if (npc.type == NPCID.BigMimicCorruption)
            {
                npc.lifeMax *= 2;
                npc.defense += 10;
            }
			//if (npc.boss) 
			//{
			//	GetItemForTreasureBag(npc.type);
			//}
        }

		public override void UpdateLifeRegen(NPC NPC, ref int damage)
		{
			if (Burn_Sand)
			{
				if (NPC.lifeRegen > 0)
				{
					NPC.lifeRegen = 0;
				}
				NPC.lifeRegen -= 16;
				if (damage < 4)
				{
					damage = 4;
				}
			}
			if (hBurn)
			{
				if (NPC.lifeRegen > 0)
				{
					NPC.lifeRegen = 0;
				}
				NPC.lifeRegen -= 16;
				if (damage < 4)
				{
					damage = 4;
				}
			}
			if (Hell_Fire)
			{
				if (NPC.lifeRegen > 0)
				{
					NPC.lifeRegen = 0;
				}
				NPC.lifeRegen -= 16;
				if (damage < 6)
				{
					damage = 6;
				}
			}
		}
        public void GetItemForTreasureBag(int npcId)
        {
            List<IItemDropRule> rulesForNPCID = Main.ItemDropsDB.GetRulesForItemID(npcId);
            List<DropRateInfo> list = new List<DropRateInfo>();
            DropRateInfoChainFeed ratesInfo = new DropRateInfoChainFeed(1f);
            foreach (IItemDropRule item3 in rulesForNPCID)
            {
                item3.ReportDroprates(list, ratesInfo);
            }
        }
        //public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hit.HitDirection, ref bool crit)
        //{
        //	if (Marble_Erosion)
        //	{
        //		npc.defense = npc.defDefense - 2;
        //		Can_Marble = true;
        //	}
        //	else
        //	{
        //		npc.defense = npc.defDefense;
        //	}
        //	return base.StrikeNPC(npc, ref damage, defense, ref knockback,hit.HitDirection, ref crit);
        //}

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
			if (CanMakeCrit)
			{
				if((int)Main.rand.NextFloat(100) <= CritChance) 
				{
					npc.damage *= 2;
				}
			}
        }

        public override void DrawEffects(NPC NPC, ref Color drawColor)
		{
			int type = DustType<PlaceHolder>();
			int torchColor = 0;
			if (Burn_Sand)
			{
				type = DustType<QuemaduraA>();
				torchColor = TorchID.Desert;
            }
			if (Hell_Fire)
			{
                type = DustType<Hell_Fire_P>();
                torchColor = TorchID.Torch;
            }
			if (hBurn)
			{
                type = DustType<HollyBurn_P>();
                torchColor = TorchID.White;
            }

            if (Main.rand.Next(4) < 3 && type != DustType<PlaceHolder>())
            {
                int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, type, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust].noGravity = false;
                    Main.dust[dust].scale *= 0.5f;
                }
                Lighting.AddLight(NPC.position, torchColor);
            }      
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (CanDrop)
			{
				base.ModifyNPCLoot(npc, npcLoot);
            }
		}

    }
}

	

