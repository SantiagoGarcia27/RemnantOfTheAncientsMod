using Terraria;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Calamity
{
    public class EndlesArcticArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ArcticArrow"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Arctic Arrow Quiver");
        }
    }
    public class EndlesBloodfireArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "BloodfireArrow"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Bloodfire Arrow Quiver");
        }
    }
    public class EndlesElysianArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ElysianArrow"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Elysian Arrow Quiver");
        }
    }
    public class EndlesIcicleArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "IcicleArrow"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Icicle Arrow Quiver");
        }
    }
    public class EndlesNapalmArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "NapalmArrow"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Napalm Arrow Quiver");
        }
    }
    public class EndlesTerraArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "TerraArrow"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Terra Arrow Quiver");
        }
    }
    public class EndlesVanquisherArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "VanquisherArrow"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Vanquisher Arrow Quiver");
        }
    }
}