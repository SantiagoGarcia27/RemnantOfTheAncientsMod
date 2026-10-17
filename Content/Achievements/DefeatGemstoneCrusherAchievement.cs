using RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Achievements;

public class DefeatGemstoneCrusherAchievement : ModAchievement
{
    public override void SetStaticDefaults()
    {
        AddNPCKilledCondition(ModContent.NPCType<GemstoneCrusher>());
    } 
}
