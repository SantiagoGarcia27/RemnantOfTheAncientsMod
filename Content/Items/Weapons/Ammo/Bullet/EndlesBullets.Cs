using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Bullet
{
    public class EndlessCursedPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CursedBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessHighVelocityPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.HighVelocityBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name; 
    }
    public class EndlessChlorophytePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ChlorophyteBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
       
    }
    public class EndlessCrystalPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.CrystalBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessExplosivePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.ExplodingBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessGoldPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.GoldenBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessIchorPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.IchorBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessLuminitePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.MoonlordBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessMeteoritePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.MeteorShot);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessNanoPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.NanoBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessPartyPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.PartyBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessVenomPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.VenomBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessSilverPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ItemID.SilverBullet);
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessTombPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<Tomb_bullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;      
    }
    public class EndlessIcePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<ice_bullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessWrathSlimePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<WrathSlimeBullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessFirePouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<FireBullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
    public class EndlessReinforcedIronPouch : EndlessAmmo
    {
        public override Item ItemBase => new(ModContent.ItemType<ReinforcedIronBullet>());
        public override string TexturePath => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ammo/Bullet/" + Name;
    }
}
