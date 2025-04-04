using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs
{
    public class OverrideBuffsToNpc : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if(npc.HasBuff(BuffID.RapidHealing))
            {
                npc.lifeRegen += 4;
            }


            base.UpdateLifeRegen(npc, ref damage);
        }

        public override bool InstancePerEntity => true;
    }
}
