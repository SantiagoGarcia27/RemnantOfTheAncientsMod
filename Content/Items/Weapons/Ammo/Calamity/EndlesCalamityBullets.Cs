using Terraria;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Calamity
{
    public class EndlesAccelerationRound : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AccelerationRound"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Aceleration Round Pouch");
        }
    }
    public class EndlesAcidRound : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AcidRound"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Bubonic Round Pouch");
        }
    }
    public class EndlesBloodfireBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "BloodfireBullet"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Bloodfire Bullet Pouch");
        }
    }
    public class EndlesEnhancedNanoRound : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EnhancedNanoRound"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Enhanced Nano Round Pouch");
        }
    }
    public class EndlesGodSlayerSlug : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "GodSlayerSlug"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless God Slayer Slug Pouch");
        }
    }
    public class EndlesHolyFireBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "HolyFireBullet"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Holy Fire Bullet Pouch");
        }
    }
    public class EndlesHyperiusBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "HyperiusBullet"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Hyperius Bullet Pouch");
        }
    }
    public class EndlesIcyBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "IcyBullet"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Icy Bullet Pouch");
        }
    }
    public class EndlesVeriumBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "VeriumBullet"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Verium Bullet Pouch");
        }
    }
    public class EndleSuperballBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "SuperballBullet"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Superball Bullet Pouch");
        }
    }
    public class EndlesTerraBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "TerraBullet"));
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless Terra Bullet Pouch");
        }
    }
}