using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Kits;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using CalamityMod;
using CalamityMod.Buffs.Potions;
using FargowiltasSouls.Content.Buffs.Masomode;

namespace RemnantOfTheAncientsMod
{

    public class PlayerBuffEffects : ModPlayer
    {
        public static void BasicInfusion(Player player)
        {
            player.moveSpeed += 0.25f;
            player.lifeRegen += 4;
            player.statDefense += 8;
            player.buffImmune[2] = true;
            player.buffImmune[3] = true;
            player.buffImmune[5] = true;
        }
        public static void AdvancedInfusion(Player player)
        {
            BasicInfusion(player);
            ref StatModifier GenericDamage = ref (player.GetDamage<GenericDamageClass>());
            GenericDamage += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 10f;
            player.buffImmune[115] = true;
            player.buffImmune[117] = true;
            player.buffImmune[BuffType<Basic_Infusion>()] = true;
        }
        public static void MeleeInfusion(Player player)
        {
            player.GetModPlayer<RemnantPlayer>().MeleeBuffInflict.Add(BuffID.Ichor);  
            if (player.inventory[player.selectedItem].CountsAsClass(DamageClass.Melee)) player.statDefense -= 4;
            player.GetCritChance(DamageClass.Melee) += 2f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
            player.kbBuff = true;

            player.buffImmune[25] = true;
            player.buffImmune[108] = true;
        }
        public static void MiningInfusion(Player player)
        {
            player.findTreasure = true;
            player.nightVision = true;
            player.pickSpeed -= 0.25f;
            Lighting.AddLight((int)((double)player.position.X + player.width / 2) / 16, (int)(player.position.Y + (double)(player.height / 2)) / 16, 1f, 1f, 2f);//16,3f,3f,3f
            player.buffImmune[11] = true;
            player.buffImmune[12] = true;
            player.buffImmune[104] = true;
            player.buffImmune[9] = true;
        }
        public static void FishPotion(Player player)
        {
            player.waterWalk = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            if (GetInstance<ConfigClient>().KitsGills) player.gills = true;
            player.buffImmune[4] = true;
            player.buffImmune[15] = true;
            player.buffImmune[109] = true;

            if(RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Calamity();
            }

            [JITWhenModsEnabled("CalamityMod")]
            void Calamity()
            { 
                player.buffImmune[ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "AnechoicCoatingBuff")] = true;
                player.Calamity().anechoicCoating = true;
                player.buffImmune[ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "SulphurskinBuff")] = true;
                player.Calamity().sulphurskin = true;
            }

        }
        public static void RangerInfusion(Player player)
        {
            player.arrowDamage.Flat *= 0.2f;
            player.archery = true;
            player.ammoPotion = true;

            player.buffImmune[16] = true;
            player.buffImmune[112] = true;
        }

        public static void GravityControlPotion(Player player)
        {
            if (GetInstance<ConfigClient>().KitsGrav) player.gravControl = true;
            if (GetInstance<ConfigClient>().KitsFeatherFall) player.slowFall = true;

            player.buffImmune[8] = true;
            player.buffImmune[18] = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Calamity();
            }
            [JITWhenModsEnabled("CalamityMod")]
            void Calamity()
            {
                player.buffImmune[ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "CalciumBuff")] = true;
                player.Calamity().calcium = true;
                player.buffImmune[ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "GravityNormalizerBuff")] = true;
                player.Calamity().gravityNormalizer = true;
                player.buffImmune[ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "Soaring")] = true;
                player.Calamity().soaring = true;
            }
        }

        public static void FishingInfusion(Player player)
        {
            player.calmed = true;
            player.fishingSkill += 15;
            player.sonarPotion = true;
            player.cratePotion = true;
            if (ModContent.GetInstance<ConfigClient>().KitsInvis) player.invis = true;
            player.luck += 0.4f;

            player.buffImmune[10] = true;
            player.buffImmune[106] = true;
            player.buffImmune[121] = true;
            player.buffImmune[122] = true;
            player.buffImmune[123] = true;
            player.buffImmune[257] = true;
        }

        public static void MageInfusion(Player player)
        {
            ref StatModifier MagicDamage = ref (player.GetDamage<MagicDamageClass>());
            MagicDamage += 0.20f;
            player.manaRegen += 4;

            player.buffImmune[6] = true;
            player.buffImmune[7] = true;
        }

        public static void TankInfusion(Player player)
        {
            player.statLifeMax2 += (player.statLifeMax * 20) / 100;
            player.endurance += 0.10f;
            player.thorns += 0.33f;
            player.lifeMagnet = true;
            player.resistCold = true;

            player.buffImmune[113] = true;
            player.buffImmune[114] = true;
            player.buffImmune[124] = true;
            player.buffImmune[14] = true;
            player.buffImmune[105] = true;
        }

        public static void SummonInfusion(Player player)
        {
            player.maxMinions++;
            player.maxTurrets++;
            if (ModContent.GetInstance<ConfigClient>().KitsInferno) player.inferno = true;

            player.buffImmune[110] = true;
            player.buffImmune[116] = true;
            player.buffImmune[BuffType<CentryBuff>()] = true;

            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Calamity();
            }

            [JITWhenModsEnabled("CalamityMod")]
            void Calamity()
            {
                player.buffImmune[ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "TeslaBuff")] = true;
                player.Calamity().tesla = true;
            }
        }
        public static void SuperSenseBuff(Player player)
        {
            player.detectCreature = true;
            player.dangerSense = true;

            player.buffImmune[BuffID.Dangersense] = true;
            player.buffImmune[BuffID.Hunter] = true;
        }

        public static void ExplorationInfusion(Player player)
        {
            player.lavaImmune = true;
            GravityControlPotion(player);
            SuperSenseBuff(player);
            MiningInfusion(player);
            player.buffImmune[1] = true;
            player.buffImmune[BuffType<GravityControlBuff>()] = true;
            player.buffImmune[BuffType<SuperSenseBuff>()] = true;
            player.buffImmune[BuffType<Mining_infusion>()] = true;

        }

        public static void PoseidonInfusion(Player player)
        {
            FishingInfusion(player);
            FishPotion(player);
            player.buffImmune[BuffType<Fishing_Infusion>()] = true;
            player.buffImmune[BuffType<FishBuff>()] = true;
        }
        public static void CombatInfusion(Player player)
        {
            MeleeInfusion(player);
            RangerInfusion(player);
            MageInfusion(player);
            SummonInfusion(player);
            TankInfusion(player);
            AdvancedInfusion(player);

            player.buffImmune[BuffType<Melee_Infusion>()] = true;
            player.buffImmune[BuffType<Ranger_Infusion>()] = true;
            player.buffImmune[BuffType<Mage_Infusion>()] = true;
            player.buffImmune[BuffType<Summon_Infusion>()] = true;
            player.buffImmune[BuffType<Tank_Infusion>()] = true;
            player.buffImmune[BuffType<Advanced_Infusion>()] = true;

        }
        public static void DefinitiveInfusion(Player player)
        {
            CombatInfusion(player);
            ExplorationInfusion(player);
            PoseidonInfusion(player);
            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;
            player.blockRange++;
            player.buffImmune[107] = true;
            player.GetModPlayer<RemnantPlayer>().MoneyCollector = true;
            player.buffImmune[BuffType<MoneyColectorBuff>()] = true;
            player.buffImmune[BuffType<Combat_infusion>()] = true;
            player.buffImmune[BuffType<Exploration_infusion>()] = true;
            player.buffImmune[BuffType<Poseidon_Infusion>()] = true;
            player.biomeSight = true;
        }
    }
}