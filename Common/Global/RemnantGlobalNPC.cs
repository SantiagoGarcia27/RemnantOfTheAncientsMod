using Terraria;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Dusts;

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
		public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (Marble_Erosion)
			{
				npc.defense = npc.defDefense - 2;
				Can_Marble = true;
			}
			else
			{
				npc.defense = npc.defDefense;
			}
			return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
		}

		public override void DrawEffects(NPC NPC, ref Color drawColor)
		{
			int type = DustType<PlaceHolder>(); 
			if (Burn_Sand)
			{
				type = DustType<QuemaduraA>();
			}
			if (Hell_Fire)
			{
                type = DustType<Hell_Fire_P>();
            }
			if (hBurn)
			{
                type = DustType<HollyBurn_P>();
            }

            if (Main.rand.Next(4) < 3)
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
            }
            Lighting.AddLight(NPC.position, 0.1f, 0.2f, 0.7f);
        }
	}
}

	

