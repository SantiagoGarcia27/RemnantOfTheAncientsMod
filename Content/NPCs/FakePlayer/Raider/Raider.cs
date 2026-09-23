using RemnantOfTheAncientsMod.Common.Systems;
using SangarUtilities.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using static FakePlayer_Setup;

namespace RemnantOfTheAncientsMod.Content.NPCs.FakePlayer.Raider
{
    public class Raider : FakePlayer
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
                RaidersInvasionSystem.EnemyKilled();

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
                            int armorIndex = Main.rand.Next(inventory.armor.Length);
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

            inventory.meleeWeapon = Raider_setup.GetRandomMelee();
            inventory.rangedWeapon = Raider_setup.GetRandomBow();
            List<FakePlayerEquipment> armorSet = Raider_setup.GetRandomArmorSet();

            inventory.armor[0] = armorSet[0];
            inventory.armor[1] = armorSet[1];
            inventory.armor[2] = armorSet[2];


             
            inventory.accessories[0] = new FakePlayerEquipment(ItemID.SharkToothNecklace);

            inventory.healPotion = new FakePlayerEquipment(ItemID.LesserHealingPotion, stack: 2);

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
