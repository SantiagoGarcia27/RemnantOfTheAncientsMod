using Terraria;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.ModLoader;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo.Calamity
{
    public class EndlesArcticArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ArcticArrow"));
        public override string TexturePath => null;
    }
    public class EndlesBloodfireArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "BloodfireArrow"));
        public override string TexturePath => null;  
    }
    public class EndlesElysianArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "ElysianArrow"));
        public override string TexturePath => null;
    }
    public class EndlesIcicleArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "IcicleArrow"));
        public override string TexturePath => null;
    }
    public class EndlesNapalmArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "NapalmArrow"));
        public override string TexturePath => null;
    }
    public class EndlesTerraArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "TerraArrow"));
        public override string TexturePath => null;
    }
    public class EndlesVanquisherArrow : EndlessAmmo
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("CalamityMod", out mod);
        public override Item ItemBase => new(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "VanquisherArrow"));
        public override string TexturePath => null;
    }
}