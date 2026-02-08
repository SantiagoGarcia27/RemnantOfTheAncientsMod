using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Achievements;

// This example showcases more advanced features of ModAchievement.
// See MinionBossKilled and then ManyExampleWormsKilled first to learn the basics.
public class DefeatInfernalTyrantAchievement : ModAchievement
{
    public override void SetStaticDefaults()
    {
        AddNPCKilledCondition(ModContent.NPCType<InfernalTyrantHead>());
    }

    // By default a ModAchievement will be placed at the end of the achievement ordering.
    // GetDefaultPosition is used to position a ModAchievement in relation to vanilla achievements.
    // Since MinionBoss is similar to Eye of Cthulhu, we place it after its achievement, "EYE_ON_YOU".
    public override Position GetDefaultPosition() => new After("PURIFY_ENTIRE_WORLD");
}
