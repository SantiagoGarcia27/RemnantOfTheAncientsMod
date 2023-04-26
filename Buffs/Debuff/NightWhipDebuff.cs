﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Buffs.Debuff
{
	public class NightWhipDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
			// Other mods may check it for different purposes.
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<NightWhipDebuffNPC>().markedByWhip = true;
		}
	}

	public class NightWhipDebuffNPC : GlobalNPC
	{
		// This is required to store information on entities that isn't shared between them.
		public override bool InstancePerEntity => true;

		public bool markedByWhip;

		public override void ResetEffects(NPC npc) {
            markedByWhip = false;
		} 
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers) { 
			// Only player attacks should benefit from this buff, hence the NPC and trap checks.
			if (markedByWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type])) {
				projectile.damage += 9;
			}
		}
	}
}
