using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Strawberry;
using RemnantOfTheAncientsMod.Content.NPCs.FakePlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static FakePlayer_Setup;

namespace RemnantOfTheAncientsMod.Content.NPCs.FakePlayer.Outlaw
{
    public class Outlaw : FakePlayer
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
        public override void OnKill()
        {
            OutlawInvasionSystem.EnemyKilled();
            base.OnKill();
        }
        public override void ModifyInventory(ref FakePlayerEquipmentList inventory)
        {
            inventory ??= new();

            inventory.meleeWeapon = GetRandomMelee();
            inventory.rangedWeapon = GetRandomBow();
            List<FakePlayerEquipment> armorSet = GetRandomArmorSet();

            inventory.armor[0] = armorSet[0];
            inventory.armor[1] = armorSet[1];
            inventory.armor[2] = armorSet[2];

            inventory.accessories[0] = new FakePlayerEquipment(ItemID.SharkToothNecklace);

            inventory.healPotion = new FakePlayerEquipment(ItemID.LesserHealingPotion, stack: 2);

            base.ModifyInventory(ref inventory);
        }
    }
}