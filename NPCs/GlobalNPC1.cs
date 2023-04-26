using Terraria;
using RemnantOfTheAncientsMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using static Terraria.NPC;

namespace RemnantOfTheAncientsMod.NPCs
{
	public class GlobalNPC1 : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool Burn_Sand;
		public bool Hell_Fire;
		public bool Javalina;
		public bool hBurn;
		public bool Marble_Erosion;
		public bool Can_Marble;


		public override void ResetEffects(NPC NPC)
		{
			Burn_Sand = false;
			Javalina = false;
			Hell_Fire = false;
			hBurn = false;
			Marble_Erosion = false;
		}

		public override void SetDefaults(NPC NPC)
		{
			// We want our ExampleJavelin buff to follow the same immunities as BoneJavelin
			//NPC.buffImmune[BuffType<Buffs.Javalina>()] = NPC.buffImmune[BuffID.BoneJavelin];
		}

		public override void UpdateLifeRegen(NPC NPC, ref int damage)
		{

			/*if (Javalina) {
					if (NPC.lifeRegen > 0) {
			NPC.lifeRegen = 0;
					}
					int JavalinaCount = 0;
					for (int i = 0; i < 1000; i++) {
			Projectile p = Main.projectile[i];
			if (p.active && p.type == ProjectileType<Projectiles.LanzaLunarP>() && p.ai[0] == 1f && p.ai[1] == NPC.whoAmI) {
				JavalinaCount++;
			}
					}
					NPC.lifeRegen -= JavalinaCount * 2 * 3;
					if (damage < JavalinaCount * 3) {
			damage = JavalinaCount * 3;
					}
				}*/
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
		//public override int StrikeNPC(HitInfo hit)
  //      {
		//	if (Marble_Erosion)
		//	{
		//		npc.defense = npc.defDefense - 2;
		//		Can_Marble = true;
		//	}
		//	else
		//	{
		//		npc.defense = npc.defDefense;
		//	}
		//	return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
		//}
       
        public override void DrawEffects(NPC NPC, ref Color drawColor)
		{
			if (Burn_Sand)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, DustType<QuemaduraA>(), NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(NPC.position, 0.1f, 0.2f, 0.7f);
			}
			if (Hell_Fire)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, DustType<Hell_Fire_P>(), NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(NPC.position, 0.1f, 0.2f, 0.7f);
			}
		}




		// Make any NPC with a chat complain to the player if they have the stinky debuff.
		public override void GetChat(NPC NPC, ref string chat)
		{
			if (Main.LocalPlayer.HasBuff(BuffID.Stinky))
			{
				switch (Main.rand.Next(3))
				{
					case 0:
						chat = "Eugh, you smell of rancid fish!";
						break;
					case 1:
						chat = "What's that horrid smell?!";
						break;
					default:
						chat = "Get away from me, i'm not doing any business with you.";
						break;
				}
			}
		}

		// If the player clicks any chat button and has the stinky debuff, prevent the button from working.
		public override bool PreChatButtonClicked(NPC NPC, bool firstButton)
		{
			return !Main.LocalPlayer.HasBuff(BuffID.Stinky);

		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot NPCLoot)
		{
		}
	}
}

	

