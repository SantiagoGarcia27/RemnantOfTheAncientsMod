using RemnantOfTheAncientsMod.Content.Items.Armor.ReinforcedIron;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static FakePlayer_Setup;

public static class RaiderBoss_setup
{
    internal static void SetKit(ref FakePlayerEquipmentList inventory)
    {
        inventory.accessories[0] = new FakePlayerEquipment(ItemID.SharkToothNecklace);
        inventory.armor[3] = new FakePlayerEquipment(ItemID.FamiliarWig);

        FakePlayerEquipmentList inv = Lodauts[Main.rand.Next(Lodauts.Count)];

        FakePlayerEquipmentList inve = inventory.Merge(inv);
        inventory = inve;
    }

    static List<FakePlayerEquipmentList> Lodauts =
    [
        new FakePlayerEquipmentList()
        {
            meleeWeapon = new FakePlayerEquipment(ModContent.ItemType<ReinforcedIronBroadsword>()),
            armor = [
                new FakePlayerEquipment(ModContent.ItemType<ReinforcedIronHelmet>()),
                new FakePlayerEquipment(ModContent.ItemType<ReinforcedIronChesplate>()),
                new FakePlayerEquipment(ModContent.ItemType<ReinforcedIronGreaves>())
            ],
            rangedWeapon =  new FakePlayerEquipment(ModContent.ItemType<ReinforcedIronBow>()),
            healPotion =  new FakePlayerEquipment(ItemID.GreaterHealingPotion, 2)
        },

        new FakePlayerEquipmentList()
        {
            meleeWeapon = new FakePlayerEquipment(WorldGen.crimson ? ItemID.BloodButcherer : ItemID.LightsBane),
            armor = [
                new FakePlayerEquipment(WorldGen.crimson ? ItemID.CrimsonHelmet : ItemID.ShadowHelmet),
                new FakePlayerEquipment(WorldGen.crimson ? ItemID.CrimsonScalemail : ItemID.ShadowScalemail),
                new FakePlayerEquipment(WorldGen.crimson ? ItemID.CrimsonGreaves : ItemID.ShadowGreaves)
            ],
            rangedWeapon =  new FakePlayerEquipment(WorldGen.crimson ? ItemID.TheUndertaker : ItemID.Musket),
            healPotion =  new FakePlayerEquipment(ItemID.GreaterHealingPotion, 2)
        },

        new FakePlayerEquipmentList()
        {
            meleeWeapon = new FakePlayerEquipment(ItemID.EnchantedSword),
            armor = [
                new FakePlayerEquipment(ItemID.PlatinumHelmet),
                new FakePlayerEquipment(ItemID.PlatinumChainmail),
                new FakePlayerEquipment(ItemID.PlatinumGreaves),
                new FakePlayerEquipment(ItemID.PlatinumCrown)
            ],
            accessories =
            [
                new FakePlayerEquipment(ItemID.SharkToothNecklace),
                new FakePlayerEquipment(ItemID.PrinceCape)
            ],
            rangedWeapon = new FakePlayerEquipment(ItemID.EnchantedSword),
            healPotion =  new FakePlayerEquipment(ItemID.GreaterHealingPotion, 2),
            misc = 
            [
                new FakePlayerEquipment(ItemID.DogWhistle),
                null,
                null,
                new FakePlayerEquipment(ItemID.MajesticHorseSaddle),
                null,
            ],

        },
    ];
}
