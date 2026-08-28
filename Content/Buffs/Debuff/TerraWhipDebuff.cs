using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Debuff
{
	public class TerraWhipDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			BuffID.Sets.IsATagBuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<TerraWhipDebuffNPC>().markedByWhip = true;
		}
	}

	public class TerraWhipDebuffNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool markedByWhip;

		public override void ResetEffects(NPC npc) {
            markedByWhip = false;
		}
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{ 
			if (markedByWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type])) 
			{
				projectile.damage += 18;
			}
		}
	}
}
