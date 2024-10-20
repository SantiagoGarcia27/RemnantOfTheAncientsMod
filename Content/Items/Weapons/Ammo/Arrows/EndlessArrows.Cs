using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Arrows
{

    public class EndlessBoneQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.BoneArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessChlorophyteQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ChlorophyteArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessCurcedQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CursedArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessFlamingQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.FlamingArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessFrostburnQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.FrostburnArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessHellQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.HellfireArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessHolyQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.HolyArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessIchorQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.IchorArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessJestQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.JestersArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessLuminiteQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.MoonlordArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessUnholyQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.UnholyArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessVenomQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.VenomArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessDesertQuiver : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<DesertArrow>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
}