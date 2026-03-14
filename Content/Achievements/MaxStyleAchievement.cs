using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using Terraria.GameContent;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Achievements;

// This example showcases more advanced features of ModAchievement.
// See MinionBossKilled and then ManyExampleWormsKilled first to learn the basics.
public class MaxStyleAchievement : ModAchievement
{
    public CustomIntCondition Condition { get; private set; }

    // This achievement is "hidden", meaning its name and description will both appear as "???" in the achievements menu, until at least 1 ExampleWormHead has been killed.
    public override bool Hidden => Condition.Value == 0;

    public override void SetStaticDefaults()
    {
        // There are 4 AchievementCategory options: Slayer, Collector, Explorer, and Challenger.
        // Slayer is the default.
        // If you want to change the achievement's category, you can do this:
        // Achievement.SetCategory(AchievementCategory.Collector);

        // Unlike MinionBossKilled, which uses AddNPCKilledCondition, this ModAchievement uses AddIntCondition to track the 5 kills. This is necessary because AddNPCKilledCondition only supports tracking a single kill.
        Condition = AddIntCondition(StatPlayer.MaxStyleStat);

        // This approach requires manually incrementing Condition.Value to track the kill count. To do this, we subscribe to the AchievementsHelper.OnNPCKilled event, then in NPCKilledListener we increment our Condition by 1. See the NPCKilledListener method below.
        // We can't use the ExampleWormHead.OnKill method for this because that doesn't run on multiplayer clients.
        // The AchievementsHelper.OnNPCKilled event, however, properly runs for all clients that participated in a fight and damaged the NPC.

        // Other AchievementCondition options include: AddCondition, AddFloatCondition, AddItemCraftCondition, AddItemPickupCondition, AddNPCKilledCondition, and AddTileDestroyedCondition.
        // If making a custom condition (AddCondition, AddFloatCondition, AddItemCraftCondition), make sure the logic is correct for multiplayer. Syncing effects through ModPacket might be required. Achievements are not loaded on the server so be sure to only increment custom condition values on clients to avoid null exceptions.
    }
}
