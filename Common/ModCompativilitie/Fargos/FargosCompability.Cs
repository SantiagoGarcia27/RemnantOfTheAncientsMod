using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class FargosToggles : ToggleCollection
    {
        public int ForceOfRemantsHeader = ModContent.ItemType<ForceOfRemants>();

        public string NightEnchantTeleport;

        public string DesertHeraldEnchantmentCactus;

        public string ReinforcedIronEnchantmentBallista;

        public string ReinforcedIronEnchantmentDamageReductionPlusSlow;

        public string DaylightEnchantmentSunflower;

        public string TuxoniteEnchantmentCritBonus;

        public int GuardiansShieldHeader = ModContent.ItemType<GuardiansShield>();

        public string DesertMedalionExtraProjectile;

        public string FrostBarrierEffect;

        public string TyrantMarkEnemyProjectileSize;

        public string TyrantMarkEnemyProjectileSpeed;

        public int SoulOfDivinityHeader = ModContent.ItemType<SoulOfDivinity>();

        public string DivineAura;

        public string Inmortality;


        //public string a; 

        public override string Mod => "RemnantOfTheAncientsMod.Toggler";

        public override string SortCategory => "Enchantments";

        public override int Priority => 1;

        public override bool Active => true;

    }

}
