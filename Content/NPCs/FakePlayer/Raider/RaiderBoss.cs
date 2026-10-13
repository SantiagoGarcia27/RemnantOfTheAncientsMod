using RemnantOfTheAncientsMod.Common.World;
using SangarUtilities.Common.UtilsTweaks;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static FakePlayer_Setup;

namespace RemnantOfTheAncientsMod.Content.NPCs.FakePlayer.Raider
{
    [AutoloadBossHead]
    public class RaiderBoss : FakePlayer
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Skeleton];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPC.spriteDirection = NPC.direction;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 400;
            NPC.boss = true;
        }

        List<int> MiscDrop =
        [
            ItemID.CopperBar,
            ItemID.TinBar,
            ItemID.LeadBar,
            ItemID.IronBar,
            ItemID.TungstenBar,
            ItemID.SilverBar,
            ItemID.ApprenticeBait,
            ItemID.Worm,
            ItemID.Wood,
            ItemID.Apple,
            ItemID.Mushroom,
            ItemID.Torch,
            ItemID.Gel,
            ItemID.MagicMirror,
            ItemID.Amethyst,
            ItemID.GoldCoin,
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ItemID.CopperPickaxe,
            ItemID.Rope,
            ItemID.HermesBoots,
        ];



        public override void OnKill()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                RemnantDownedBossSystem.downedRaiderLeader = true;
                RaidersInvasionSystem.EndEvent();

                int choice = Main.rand.Next(0, 101);
                int chance = 5 * (DificultyUtils.ReaperMode ? 5 : 1);
                if (choice <= chance && inventory != null)
                {
                    int rand = Main.rand.Next(0, 3);
                    switch (rand)
                    {
                        case 0:
                            DropItem(inventory.meleeWeapon.item.type);
                            break;
                        case 1:
                            DropItem(inventory.rangedWeapon.item.type);
                            break;
                        case 2:
                            int count = inventory.armor.Count(x => x != null);
                            int armorIndex = Main.rand.Next(count);
                            DropItem(inventory.armor[armorIndex].item.type);
                            break;
                    }
                }
                else if (choice <= chance * 2)
                {
                    int rand3 = Main.rand.Next(MiscDrop.Count);
                    DropItem(MiscDrop[rand3]);
                }
            }

            base.OnKill();
        }
        public override void ModifyInventory(ref FakePlayerEquipmentList inventory)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            inventory ??= new();

            RaiderBoss_setup.SetKit(ref inventory);
 
            this.inventory = inventory;
            base.ModifyInventory(ref inventory);
        }

        private void DropItem(int type, int stack = 1)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (inventory == null) return;
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), type, Stack: stack);
            }
        }
    }
}
