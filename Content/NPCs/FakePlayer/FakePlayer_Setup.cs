using System.IO;
using Terraria;
using Terraria.ID;

public static class FakePlayer_Setup
{
   
    public class FakePlayerEquipment
    {

        public int type { get; set; }
        public float weight { get; set; }
        private int _stack;
        public int stack
        {
            get => _item?.stack ?? _stack;
            set
            {
                _stack = value;
                if (_item != null)
                    _item.stack = value;
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
                _item.stack = _stack;
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

        public FakePlayerEquipment Clone() => new(type, weight, stack, category);

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

        public void Write(BinaryWriter writer)
        {
            WriteEquipmentArray(writer, armor);
            WriteEquipment(writer, meleeWeapon);
            WriteEquipment(writer, rangedWeapon);
            WriteEquipmentArray(writer, ammo);
            WriteEquipmentArray(writer, potions);
            WriteEquipment(writer, healPotion);
            WriteEquipmentArray(writer, accessories);
        }

        public static FakePlayerEquipmentList Read(BinaryReader reader)
        {
            FakePlayerEquipmentList inventory = new();
            ReadEquipmentArray(reader, inventory.armor);
            inventory.meleeWeapon = ReadEquipment(reader);
            inventory.rangedWeapon = ReadEquipment(reader);
            ReadEquipmentArray(reader, inventory.ammo);
            ReadEquipmentArray(reader, inventory.potions);
            inventory.healPotion = ReadEquipment(reader);
            ReadEquipmentArray(reader, inventory.accessories);
            return inventory;
        }

        private static void WriteEquipmentArray(BinaryWriter writer, FakePlayerEquipment[] equipment)
        {
            for (int i = 0; i < equipment.Length; i++)
                WriteEquipment(writer, equipment[i]);
        }

        private static void ReadEquipmentArray(BinaryReader reader, FakePlayerEquipment[] equipment)
        {
            for (int i = 0; i < equipment.Length; i++)
                equipment[i] = ReadEquipment(reader);
        }

        private static void WriteEquipment(BinaryWriter writer, FakePlayerEquipment equipment)
        {
            writer.Write(equipment != null);
            if (equipment == null)
                return;

            writer.Write(equipment.type);
            writer.Write(equipment.weight);
            writer.Write(equipment.stack);
            writer.Write((byte)equipment.category);
        }

        private static FakePlayerEquipment ReadEquipment(BinaryReader reader)
        {
            if (!reader.ReadBoolean())
                return null;

            int type = reader.ReadInt32();
            float weight = reader.ReadSingle();
            int stack = reader.ReadInt32();
            FakePlayerEquipment.itemType category = (FakePlayerEquipment.itemType)reader.ReadByte();
            return new FakePlayerEquipment(type, weight, stack, category);
        }
    }
}
