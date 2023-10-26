using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Arrows
{

    public class EndlessBone : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.BoneArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessChlorophyte : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ChlorophyteArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessCurced : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CursedArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessFlaming : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.FlamingArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessFrostburn : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.FrostburnArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessHell : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.HellfireArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessHoly : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.HolyArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessIchor : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.IchorArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessJest : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.JestersArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessLuminite : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.MoonlordArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessUnholy : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.UnholyArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessVenom : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.VenomArrow);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
    public class EndlessDesert : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<DesertArrow>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Arrows/" + Name;
    }
}