using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public class EndlesFlare : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Flare);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
        }
    }
    public class EndlesBlueFlare : EndlessBase
    {
        public override Item ItemBase => new(ItemID.BlueFlare);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
        }
    }
    public class EndlesStar : EndlessBase
    {
        public override Item ItemBase => new(ItemID.FallenStar);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 8));
            Item.useAnimation = ItemID.FallenStar;
        }
    }
    public class EndlesNail : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Nail);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
        }
    }
    public class EndlesGel : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Gel);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
        }
    }
    public class EndlesStake : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Stake);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
        }
    }
    public class EndlesSnowBall : EndlessBase
    {
        public override Item ItemBase => new(ItemID.Snowball);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
        }
    }
}
