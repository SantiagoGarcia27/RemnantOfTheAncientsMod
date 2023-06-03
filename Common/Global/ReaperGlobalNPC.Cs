using Terraria.ID;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using CalamityMod.NPCs.VanillaNPCOverrides.Bosses;

public class FchangesNpc : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public override void SetDefaults(NPC npc)
    {
        if (npc.boss)
        {
            ModifyBossStats(npc, ref npc.damage, npc.lifeMax);
        }

        if (Reaper.ReaperMode)
        {
            if (destroyer.Contains(npc.type))
            {
                npc.scale = 2f;
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npc.buffImmune[BuffID.OnFire] = true;
                npc.buffImmune[BuffID.Frostburn] = true;
                npc.buffImmune[BuffID.Poisoned] = true;
                npc.defense += 5;
            }
            if (npc.type == NPCID.SkeletronHand)
            {
                npc.scale = 1.5f;
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npc.scale = 0.8f;
            }
        }
    }
    public List<int> destroyer = new List<int>
    {
        NPCID.TheDestroyer ,
        NPCID.TheDestroyerBody,
        NPCID.TheDestroyerTail
    };
    public static void ModifyBossStats(NPC npc, ref int damage, int lifeMax)
    {
        npc.lifeMax = (int)NpcChanges1.ExpertLifeScale(lifeMax);
        npc.damage = (int)NpcChanges1.ExpertDamageScale(damage);
    }
    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        if (Reaper.ReaperMode)
        {
            if (npc.type == NPCID.SkeletronHead)
            {
                for (int i = 0; i < 3; i++)
                {
                    NPC.SpawnOnPlayer(Main.player[npc.target].whoAmI, NPCID.SkeletronHand);
                }
            }
        }
        base.OnSpawn(npc, source);
    }
    public override void AI(NPC npc)
    {
        base.AI(npc);
        if (Reaper.ReaperMode)
        {
            if (npc.type == NPCID.SkeletronHead)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].type == NPCID.SkeletronHand)
                    {
                        if (Main.npc[i].dontTakeDamage != true)
                        {
                            npc.dontTakeDamage = NPC.AnyNPCs(NPCID.SkeletronHand);
                        }
                    }
                }
            }
        }
    }
}