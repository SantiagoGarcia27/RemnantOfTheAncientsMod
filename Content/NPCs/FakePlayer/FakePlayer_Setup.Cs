using Terraria;
using Terraria.ID;

public static class FakePlayer_Setup
{
   
    public class FakePlayerEquipment
    {

        public int type { get; set; }
        public float weight { get; set; }
        public int stack
        {
            get => item?.stack ?? 0;
            set
            {
                if (item != null)
                    item.stack = value;
            }
        }

        private Item _item { get; set; }
        public Item item {
            get
            {
                if (_item != null)
                    return _item;

                if (!ContentSamples.ItemsByType.TryGetValue(type, out Item sample))
                    return null;

                _item = sample.Clone();
                _item.stack = stack;
                return _item;
            }
        }
        public itemType category { get; set; }

        public FakePlayerEquipment(int type, float weight = 1f, int stack = 1,  itemType category = itemType.none)
        {
            this.type = type;
            this.weight = weight;
            this.category = category;
            this.stack = stack;
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
            accessory,
            ammo,
            none

        }
    }

    public class FakePlayerEquipmentList
    {
        public FakePlayerEquipment[] armor = new FakePlayerEquipment[3];
        public FakePlayerEquipment meleeWeapon = null;
        public FakePlayerEquipment rangedWeapon = null;
        public FakePlayerEquipment[] ammo = new FakePlayerEquipment[4];
        public FakePlayerEquipment[] potions = new FakePlayerEquipment[2];
        public FakePlayerEquipment healPotion = null;
        public FakePlayerEquipment[] accessories = new FakePlayerEquipment[7];
    }
}