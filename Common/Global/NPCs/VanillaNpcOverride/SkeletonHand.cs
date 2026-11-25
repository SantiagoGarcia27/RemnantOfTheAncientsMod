using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class SkeletronHand : GlobalNPC
{
    public override void PostAI(NPC npc)
    {
        if (npc.type != NPCID.SkeletronHand)
            return;

        float rangeToSeparation = 100f;
        float separationStrenght = 112.8f;

        foreach (NPC other in Main.ActiveNPCs)
        {
            if (other.type != npc.type ||
                other.whoAmI == npc.whoAmI ||
                other.ai[1] != npc.ai[1])
                continue;

            Vector2 diff = npc.Center - other.Center;
            float distance = diff.Length();

            if (distance < rangeToSeparation)
            {
                Vector2 direction = diff.SafeNormalize(
                    new Vector2(npc.whoAmI < other.whoAmI ? -1 : 1, 0)
                );

                npc.velocity += direction *
                    (1f - distance / 100f) * separationStrenght;
            }
        }
    }
}