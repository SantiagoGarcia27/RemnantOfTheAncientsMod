using Terraria;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Dusts;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using CalamityMod.Buffs.StatBuffs;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs
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
        int timer = 0;
        public void setDebuffs(NPC NPC)
		{
			int damage = 0;
			
            if (NPC.HasBuff(BuffID.Electrified))
            {
                if (NPC.lifeRegen > 0)
                {
                    NPC.lifeRegen = 0;
                }
                NPC.lifeRegen -= 16;
				damage = 1;
				if(timer++ % 6 == 0)
                NPC.SimpleStrikeNPC(damage, Main.player[Main.myPlayer].direction, false, 0f, DamageClass.Generic, false, 0, false);
            }
			if (NPC.HasBuff(BuffID.Stoned))
			{
				NPC.velocity = Vector2.Zero;
			}
            if (NPC.HasBuff(BuffID.TitaniumStorm))
            {
                //NPC.velocity = Vector2.Zero;

            }

        }
        public static bool BigMimicSummonCheck(int x, int y, Player user)
        {
            if (Main.netMode == 1 || !Main.hardMode)
            {
                return false;
            }
            int num = Chest.FindChest(x, y);
            if (num < 0)
            {
                return false;
            }
            int num2 = 0;
            int KeyId = 0;
            int num4 = 0;
            for (int i = 0; i < 40; i++)
            {
                ushort num5 = Main.tile[Main.chest[num].x, Main.chest[num].y].TileType;
                int num6 = Main.tile[Main.chest[num].x, Main.chest[num].y].TileFrameX / 36;
                if (TileID.Sets.BasicChest[num5] && (num5 != 21 || num6 < 5 || num6 > 6) && Main.chest[num].item[i] != null && Main.chest[num].item[i].type > 0)
                {
                    if (Main.chest[num].item[i].type == ItemID.GoldenKey)
                    {
                        num2 += Main.chest[num].item[i].stack;
                        KeyId = ItemID.GoldenKey;
                    }
                    if (Main.chest[num].item[i].type == ModContent.ItemType<JungleKey>())
                    {
                        num2 += Main.chest[num].item[i].stack;
                        KeyId = ModContent.ItemType<JungleKey>();
                    }
                    //else if (Main.chest[num].item[i].type == 3091)
                    //{
                    //    num3 += Main.chest[num].item[i].stack;
                    //}
                    else
                    {
                        num4++;
                    }
                }
            }
            if (num4 == 0 && num2 == 1)
            {
                //if (num2[0] != 1)
                //{
                    _ = 1;
               // }
                if (TileID.Sets.BasicChest[Main.tile[x, y].TileType])
                {
                    if (Main.tile[x, y].TileFrameX % 36 != 0)
                    {
                        x--;
                    }
                    if (Main.tile[x, y].TileFrameY % 36 != 0)
                    {
                        y--;
                    }
                    int number = Chest.FindChest(x, y);
                    for (int j = 0; j < 40; j++)
                    {
                        Main.chest[num].item[j] = new Item();
                    }
                    Chest.DestroyChest(x, y);
                    for (int k = x; k <= x + 1; k++)
                    {
                        for (int l = y; l <= y + 1; l++)
                        {
                            if (TileID.Sets.BasicChest[Main.tile[k, l].TileType])
                            {
                                Main.tile[k, l].ClearTile();
                            }
                        }
                    }
                    int number2 = 1;
                    if (Main.tile[x, y].TileType == 467)
                    {
                        number2 = 5;
                    }
                    NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, number2, x, y, 0f, number);
                    NetMessage.SendTileSquare(-1, x, y, 3);
                }
                int npcId = GetMimicId(KeyId,user); 
                int num8 = NPC.NewNPC(user.GetSource_TileInteraction(x, y), x * 16 + 16, y * 16 + 32, npcId);
                Main.npc[num8].whoAmI = num8;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num8);
                Main.npc[num8].BigMimicSpawnSmoke();
            }
            return false;
        }
        public static int GetMimicId(int KeyId,Player user)
        {
            if(KeyId == ItemID.GoldenKey)
            {
                return user.ZoneSnow ? NPCID.IceMimic:NPCID.Mimic;
            }
            else if (KeyId == ModContent.ItemType<JungleKey>())
            {
                return NPCID.BigMimicJungle;
            }
            return NPCID.Mimic;
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
        public override void AI(NPC npc)
        {
			setDebuffs(npc);
            base.AI(npc);
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
                    modifiers.FinalDamage *= 2;                    
					//npc.damage *= 2;
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
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.boss)
            {
                if(RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    if (Main.netMode != 2 && !Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, npc.Center) < 6400f)
                    {
                        Main.player[Main.myPlayer].AddBuff(ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod,"BossEffects"), 2);
                    }   
                }
            }
            base.OnSpawn(npc, source);
        }
        public override bool PreAI(NPC npc)
        {
            if (npc.boss)
            {
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    if (Main.netMode != 2 && !Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, npc.Center) < 6400f)
                    {
                        Main.player[Main.myPlayer].AddBuff(ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "BossEffects"), 2);
                    }
                }
            }
            return base.PreAI(npc);
        }
    }
}

	

