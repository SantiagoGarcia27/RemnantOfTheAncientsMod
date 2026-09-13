using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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
        new(type: ModContent.ItemType<ReinforcedIronBow>(), category: itemType.bow),
    ];
    public static List<FakePlayerEquipment> Guns = [
        new(type: ItemID.Minishark, category: itemType.bow),
        new(type: ModContent.ItemType<minigun>(), category: itemType.bow),
    ];


    public static List<List<FakePlayerEquipment>> ArmorSets = [
        [
            new(type: ItemID.CopperHelmet, category: itemType.helmet),
            new(type: ItemID.CopperChainmail, category: itemType.chestplate),
            new(type: ItemID.CopperGreaves, category: itemType.helmet)
        ],
        [
            new(type: ItemID.TinHelmet, category: itemType.helmet),
            new(type: ItemID.TinChainmail, category: itemType.chestplate),
            new(type: ItemID.TinGreaves, category: itemType.helmet)
        ],
        [
            new(type: ItemID.IronHelmet, category: itemType.helmet),
            new(type: ItemID.IronChainmail, category: itemType.chestplate),
            new(type: ItemID.IronGreaves, category: itemType.helmet)
        ],
        [
            new(type: ItemID.LeadHelmet, category: itemType.helmet),
            new(type: ItemID.LeadChainmail, category: itemType.chestplate),
            new(type: ItemID.LeadGreaves, category: itemType.helmet)
        ],
        [
            new(type: ItemID.SilverHelmet, category: itemType.helmet),
            new(type: ItemID.SilverChainmail, category: itemType.chestplate),
            new(type: ItemID.SilverGreaves, category: itemType.helmet)
        ],
        [
            new(type: ItemID.TungstenHelmet, category: itemType.helmet),
            new(type: ItemID.TungstenChainmail, category: itemType.chestplate),
            new(type: ItemID.TungstenGreaves, category: itemType.helmet)
        ],
        [
            new(type: ItemID.GoldHelmet, category: itemType.helmet),
            new(type: ItemID.GoldChainmail, category: itemType.chestplate),
            new(type: ItemID.GoldGreaves, category: itemType.helmet)
        ],
        [
            new(type: ItemID.PlatinumHelmet, category: itemType.helmet),
            new(type: ItemID.PlatinumChainmail, category: itemType.chestplate),
            new(type: ItemID.PlatinumGreaves, category: itemType.helmet)
        ],
    ];

    public static FakePlayerEquipment GetRandomMelee()
    {

        int index = 0;

        switch (Main.rand.Next(4))
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
        int index = 0;

        switch (Main.rand.Next(2))
        {
            case 0:
                index = Main.rand.Next(Guns.Count);
                return Guns[index];
            case 1:
                index = Main.rand.Next(Bow.Count);
                return Bow[index];
            default:
                index = Main.rand.Next(Bow.Count);
                return Bow[index];
        }
    }

    public static List<FakePlayerEquipment> GetRandomArmorSet()
    {
        int index = Main.rand.Next(ArmorSets.Count);
        List<FakePlayerEquipment> armorSet = ArmorSets[index];
        return ArmorSets[index]; ;
    }

    public class FakePlayerEquipment
    {

        public int type { get; set; }
        public float weight { get; set; }
        public Item item {
            get
            {
                if (!ContentSamples.ItemsByType.TryGetValue(this.type, out Item item)) return null;
                return item;
            }
        }
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