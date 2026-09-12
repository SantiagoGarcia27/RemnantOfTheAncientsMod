using RemnantOfTheAncientsMod.Content.NPCs.FakePlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using static FakePlayer_Setup.FakePlayerEquipment;

public static class FakePlayer_Setup
{
    public static List<FakePlayerEquipment> ShortSword =
    [
        new(type: ItemID.CopperShortsword, category: itemType.shortSword),
        new(type: ItemID.TinShortsword, category: itemType.shortSword),
        new(type: ItemID.LeadShortsword, category: itemType.shortSword),
        new(type: ItemID.IronShortsword, category: itemType.shortSword),
        new(type: ItemID.TungstenShortsword, category: itemType.shortSword),
        new(type: ItemID.SilverShortsword, category: itemType.shortSword),
        new(type: ItemID.GoldShortsword, category: itemType.shortSword),
        new(type: ItemID.PlatinumShortsword, category: itemType.shortSword),
    ];

    public static List<FakePlayerEquipment> LongSword = [
        new(type: ItemID.CopperBroadsword, category: itemType.broadsword),
        new(type: ItemID.TinBroadsword, category: itemType.broadsword),
        new(type: ItemID.LeadBroadsword, category: itemType.broadsword),
        new(type: ItemID.IronBroadsword, category: itemType.broadsword),
        new(type: ItemID.TungstenBroadsword, category: itemType.broadsword),
        new(type: ItemID.SilverBroadsword, category: itemType.broadsword),
        new(type: ItemID.GoldBroadsword, category: itemType.broadsword),
        new(type: ItemID.PlatinumBroadsword, category: itemType.broadsword),
    ];

    public static List<FakePlayerEquipment> Spear = [
        new(type: ItemID.Spear, category: itemType.spear),
        new(type: ItemID.Trident, category: itemType.spear),
        new(type: ItemID.ThunderSpear, category: itemType.spear),
    ];

    public static List<FakePlayerEquipment> Mace = [
        new(type: ItemID.Mace, category: itemType.mace),
        new(type: ItemID.FlamingMace, category: itemType.mace),
    ];


    public static List<FakePlayerEquipment> Bow = [
        new(type: ItemID.WoodenBow, category: itemType.bow),
        new(type: ItemID.TinBow, category: itemType.bow),
        new(type: ItemID.CopperBow, category: itemType.bow),
        new(type: ItemID.IronBow, category: itemType.bow),
        new(type: ItemID.LeadBow, category: itemType.bow),
        new(type: ItemID.SilverBow, category: itemType.bow),
        new(type: ItemID.TungstenBow, category: itemType.bow),
        new(type: ItemID.GoldBow, category: itemType.bow),
        new(type: ItemID.PlatinumBow, category: itemType.bow),
    ];


    public static FakePlayerEquipment GetRandomMelee()
    {
        int index = 0;
        switch (Main.rand.Next(2))
        {
            case 0:
                index = Main.rand.Next(ShortSword.Count);
                return ShortSword[index];
            case 1:
                index = Main.rand.Next(Spear.Count);
                return Spear[index];
            case 2:
                index = Main.rand.Next(Mace.Count);
                return Mace[index];
            default:
                index = Main.rand.Next(LongSword.Count);
                return LongSword[index];
        }
       
    }

    public static FakePlayerEquipment GetRandomBow()
    {
        int index = Main.rand.Next(Bow.Count);
        return Bow[index];
    }

    public class FakePlayerEquipment
    {

        public int type { get; set; }
        public float weight { get; set; }
        public Item item => ContentSamples.ItemsByType[this.type];
        public itemType category { get; set; }

        public FakePlayerEquipment(int type, float weight = 1f, itemType category = itemType.none)
        {
            this.type = type;
            this.weight = weight;
            this.category = category;
        }

        public enum itemType
        {
            shortSword,
            broadsword,
            spear,
            mace,
            bow,
            potion,
            healing,
            helmet,
            chestplate,
            boots,

            none

        }
    }

    public class FakePlayerEquipmentList
    {
       public FakePlayerEquipment[] armor = new FakePlayerEquipment[3];
       public FakePlayerEquipment meleeWeapon = null;
       public FakePlayerEquipment rangedWeapon = null;
       public FakePlayerEquipment[] potions = new FakePlayerEquipment[2];
       public FakePlayerEquipment healPotion = null;
    }
}