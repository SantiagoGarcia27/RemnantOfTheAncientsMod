using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.Prefixe;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Drops
{
    public class CustomizeDrops : GlobalItem
    {

        public override void OnSpawn(Item item, IEntitySource source)
        {

            if (source is EntitySource_Loot)
            {
                EntitySource_Loot sourceLoot = source as EntitySource_Loot;
                if (sourceLoot != null)
                {
                    var entity = sourceLoot.Entity;
                    NPC npc = (NPC)entity;


                    if (item.type == ItemID.Shotgun && npc.type == ModContent.NPCType<CursedShotgunEnemy>())
                    {
                        item.Prefix(ModContent.PrefixType<Veteran>());
                    }
                }
            }
            base.OnSpawn(item, source);
        }
    }
}
