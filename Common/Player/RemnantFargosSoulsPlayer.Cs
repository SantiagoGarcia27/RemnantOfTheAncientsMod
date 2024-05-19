using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Fargos;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;

namespace RemnantOfTheAncientsMod
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class RemnantFargosSoulsPlayer : ModPlayer
    {

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }

        public bool DaylightEnchantment;

        public bool IronBallistaEnchantment;
        public bool FrostBarrier;
        public static int FrostBarrierCounter;
        public bool DesertMedalion;


        public bool NightTp;
        public static int NightTpCouldown { get; set; }
        public static int NightTpCouldownMax = (int)Utils1.FormatTimeToTick(0, 0, 1, 0);

        public static bool tuxoniteEnchantCrit;
        public static int tuxoniteEnchantCritDuration = 0;
        public static float tuxoniteEnchantCritCounter = 0;

        public static float ProjectileSpeedBonus = 0;
        public static float ProjectileSizeBonus = 0;

        public override void ResetEffects()
        {
            FrostBarrier = false;
            DesertMedalion = false;
            IronBallistaEnchantment = false;
            NightTp = false;
            DaylightEnchantment = false;
            tuxoniteEnchantCrit = false;
        }

        public override void PostUpdate()
        {
            if (NightTp)
            {
                Player.opacityForAnimation = NightTpCouldown == 0 ? 0.5f : 1;
                if (NightTpCouldown > 0)
                {
                    NightTpCouldown--;
                }
            }
        }
    }
}