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
    }
    public class EndlesAcidRound : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "AcidRound"));
        public override string TexturePath => null;
    }
    public class EndlesBloodfireBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "BloodfireBullet"));
        public override string TexturePath => null; 
    }
    public class EndlesEnhancedNanoRound : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EnhancedNanoRound"));
        public override string TexturePath => null; 
    }
    public class EndlesGodSlayerSlug : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "GodSlayerSlug"));
        public override string TexturePath => null;
    }
    public class EndlesHolyFireBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "HolyFireBullet"));
        public override string TexturePath => null;
    }
    public class EndlesHyperiusBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "HyperiusBullet"));
        public override string TexturePath => null;
    }
    public class EndlesIcyBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "IcyBullet"));
        public override string TexturePath => null;
    }
    public class EndlesVeriumBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "VeriumBullet"));
        public override string TexturePath => null;
    }
    public class EndleSuperballBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "SuperballBullet"));
        public override string TexturePath => null;
    }
    public class EndlesTerraBullet : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "TerraBullet"));
        public override string TexturePath => null;
    }
}