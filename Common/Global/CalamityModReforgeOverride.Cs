using Terraria.ModLoader;
using Terraria;
using Terraria.Utilities;
using Terraria.ID;
using System.Collections.Generic;
using CalamityMod.Items;
using Terraria.DataStructures;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Common.Global
{

    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class FixCalamityReforge : CalamityGlobalItem
    {
        public static int storedPrefix = -1;
        internal float StealthGenBonus;
        internal float StealthStrikePrefixBonus;


        [JITWhenModsEnabled("CalamityMod")]
        public override void OnCreated(Item item, ItemCreationContext context)
        {
            storedPrefix = -1;

        }
        [JITWhenModsEnabled("CalamityMod")]
        public override void PreReforge(Item item)
        {
            StealthGenBonus = 1f;
            StealthStrikePrefixBonus = 0f;
            storedPrefix = item.prefix;
          //  return true;
        }
        [JITWhenModsEnabled("CalamityMod")]

        public override int ChoosePrefix(Item item, UnifiedRandom rand)
        {
            int prefix = RandomRoguePrefix();
            if (storedPrefix == -1 && item.CountsAsClass(CallUtils.GetDamageClassFromMod(RemnantOfTheAncientsMod.CalamityMod, "RogueDamageClass")) && item.Prefix(-3))
            {
                if (NegativeRoguePrefix(prefix) && !Main.rand.NextBool(3))
                {
                    return 0;
                }
                return prefix;
            }
            //if (!CalamityMod.CalamityConfig.Instance.RemoveReforgeRNG || Main.gameMenu || storedPrefix == -1)
            //{
            //    var damageClass = CallUtils.GetDamageClassFromMod(RemnantOfTheAncients.CalamityMod, "RogueDamageClass");
            //    if (item.CountsAsClass(damageClass)) return prefix;
            //    else return -1;
            //}
            return base.ChoosePrefix(item, rand) ;
        }
        public static Mod calamityMod = RemnantOfTheAncientsMod.CalamityMod;
        public static Mod RemnantOfTheAncient = RemnantOfTheAncientsMod.RemnantOfTheAncients;

        [JITWhenModsEnabled("CalamityMod")]
        internal static int GetReworkedReforge(Item item, UnifiedRandom rand, int currentPrefix)
        {

            int prefix = -1;
            if (item.accessory)
            {
                int[][] array = new int[5][];
                int[] obj = [62, 69, 73, 77, 0, 0];
                obj[4] = CallUtils.GetModPrefix(calamityMod, "Quiet");
                obj[5] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Healthy");
                array[0] = obj;
                int[] obj2 = [63, 70, 67, 74, 78, 0, 0];
                obj2[5] = CallUtils.GetModPrefix(calamityMod, "Cloaked");
                obj2[6] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Healer");
                array[1] = obj2;
                int[] obj3 = [64, 71, 75, 79, 66, 0, 0];
                obj3[5] = CallUtils.GetModPrefix(calamityMod, "Camouflaged");
                obj3[6] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Atletic");
                array[2] = obj3;
                int[] obj4 = [65, 72, 68, 76, 80, 0, 0];
                obj4[5] = CallUtils.GetModPrefix(calamityMod, "Silent");
                obj4[6] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Gigant");
                array[3] = obj4;
                int[] obj5 = [0, 0, 0, 0, 0, 0, 0];
                obj4[0] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Impenetrable");
                obj4[1] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Uncontrolled");
                obj4[2] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Supersonic");
                obj4[3] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Sharp");
                obj4[4] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Accurate");
                obj4[5] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Shadow");
                obj4[6] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Titanic");
                array[4] = obj5;
                int[][] accessoryReforgeTiers = array;
                prefix = IteratePrefix(rand, accessoryReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<SummonMeleeSpeedDamageClass>())
            {
                if (item.type == ItemID.Terrarian)
                {
                    int[][] terrarianReforgeTiers =
                    [
                        [36, 38, 54],
                        [53, 57, 61],
                        [37, 60, 59],
                        [84]
                    ];
                    prefix = IteratePrefix(rand, terrarianReforgeTiers, currentPrefix);
                }
                else if ((item.channel || item.noMelee) && item.type != ItemID.Zenith && item.useStyle != ItemUseStyleID.Rapier && !item.CountsAsClass<SummonMeleeSpeedDamageClass>())
                {
                    int[][] meleeNoSpeedReforgeTiers =
                    [
                        [36, 38, 54],
                        [53, 57, 61],
                        [37, 60],
                        [59]
                    ];
                    prefix = IteratePrefix(rand, meleeNoSpeedReforgeTiers, currentPrefix);
                }
                else
                {
                    int[][] meleeReforgeTiers =
                    [
                        [36, 45, 51, 15, 14, 15, 38, 54],
                        [53, 57, 61, 42, 6, 12],
                        [46, 44, 1, 3, 5],
                        [2, 55, 4, 37],
                        [60, 43, 59],
                        [81]
                    ];
                    int[][] toolReforgeTiers =
                    [
                        [36, 45, 51, 14, 15, 38, 54],
                        [53, 57, 61, 42, 6, 12],
                        [46, 44, 1, 3, 5],
                        [2, 55, 4, 37],
                        [60, 43, 59],
                        [81, 15]
                    ];
                    int[][] tierListToUse = ((item.pick > 0 || item.axe > 0 || item.hammer > 0) ? toolReforgeTiers : meleeReforgeTiers);
                    prefix = IteratePrefix(rand, tierListToUse, currentPrefix);
                }
            }
            else if (item.CountsAsClass<RangedDamageClass>())
            {
                int[][] rangedReforgeTiers =
                [
                    [36, 45, 51, 25, 38, 54],
                    [53, 57, 61, 42, 19],
                    [46, 44, 18, 21, 55],
                    [37, 60, 16],
                    [59, 17, 20, 43],
                    [82]
                ];
                prefix = IteratePrefix(rand, rangedReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<MagicSummonHybridDamageClass>())
            {
                int[][] magicReforgeTiers =
                [
                    [36, 45, 51, 35, 38, 54],
                    [53, 57, 61, 42, 33, 52],
                    [46, 44, 27, 34, 55],
                    [37, 60, 26],
                    [59, 28, 43],
                    [83]
                ];
                prefix = IteratePrefix(rand, magicReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<SummonDamageClass>())
            {
                int[][] summonReforgeTiers =
                [
                    [45, 35],
                    [38, 54, 42, 33, 52],
                    [53, 27, 34],
                    [37, 60, 26, 43],
                    [28, 59],
                    [83, 57]
                ];
                prefix = IteratePrefix(rand, summonReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<ThrowingDamageClass>())
            {
                int[][] array2 = new int[6][];
                int[] obj5 = [36, 45, 51, 38, 54, 0, 0];
                obj5[5] = CallUtils.GetModPrefix(calamityMod, "Radical");
                obj5[6] = CallUtils.GetModPrefix(calamityMod, "Pointy");
                array2[0] = obj5;
                int[] obj6 = [53, 57, 61, 42, 0, 0];
                obj6[4] = CallUtils.GetModPrefix(calamityMod, "Sharp");
                obj6[5] = CallUtils.GetModPrefix(calamityMod, "Glorious");
                array2[1] = obj6;
                int[] obj7 = [46, 44, 55, 0, 0, 0];
                obj7[3] = CallUtils.GetModPrefix(calamityMod, "Feathered");
                obj7[4] = CallUtils.GetModPrefix(calamityMod, "Sleek");
                obj7[5] = CallUtils.GetModPrefix(calamityMod, "Hefty");
                array2[2] = obj7;
                array2[3] =
                [
                    37,
                    60,
                    CallUtils.GetModPrefix(calamityMod,"Mighty"),
                    CallUtils.GetModPrefix(calamityMod,"Serrated")
                ];
                array2[4] =
                [
                    59,
                    43,
                    CallUtils.GetModPrefix(calamityMod, "Vicious"),
                    CallUtils.GetModPrefix(calamityMod,"Lethal")
                ];
                array2[5] =
                [
                    CallUtils.GetModPrefix(calamityMod, "Flawless"),
                    CallUtils.GetModPrefix(RemnantOfTheAncient, "Exquisite")
                ];
                int[][] rogueReforgeTiers = array2;
                prefix = IteratePrefix(rand, rogueReforgeTiers, currentPrefix);
            }
            return prefix;

        }
        [JITWhenModsEnabled("CalamityMod")]
        private static int GetPrefixTier(int[][] tiers, int currentPrefix)
        {
            for (int checkingTier = 0; checkingTier < tiers.Length; checkingTier++)
            {
                int[] tierList = tiers[checkingTier];
                for (int i = 0; i < tierList.Length; i++)
                {
                    if (tierList[i] == currentPrefix)
                    {
                        return checkingTier;
                    }
                }
            }
            return -1;
        }
        [JITWhenModsEnabled("CalamityMod")]
        private static int IteratePrefix(UnifiedRandom rand, int[][] reforgeTiers, int currentPrefix)
        {
            int currentTier = GetPrefixTier(reforgeTiers, currentPrefix);
            int newTier = ((currentTier == reforgeTiers.Length - 1) ? currentTier : (currentTier + 1));
            return rand.Next(reforgeTiers[newTier]);
        }

        [JITWhenModsEnabled("CalamityMod")]
        public static int RandomRoguePrefix()
        {
            UnifiedRandom rand = Main.rand;
            int[] obj =
            [
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 36, 37, 38,
            39, 40, 53, 54, 55, 56, 57, 59, 60, 61,
            42, 43, 44, 45, 46, 47, 48, 49, 50, 51
            ];
            obj[0] = CallUtils.GetModPrefix(calamityMod, "Radical");
            obj[1] = CallUtils.GetModPrefix(calamityMod, "Pointy");
            obj[2] = CallUtils.GetModPrefix(calamityMod, "Sharp");
            obj[3] = CallUtils.GetModPrefix(calamityMod, "Glorious");
            obj[4] = CallUtils.GetModPrefix(calamityMod, "Feathered");
            obj[5] = CallUtils.GetModPrefix(calamityMod, "Sleek");
            obj[6] = CallUtils.GetModPrefix(calamityMod, "Hefty");
            obj[7] = CallUtils.GetModPrefix(calamityMod, "Mighty");
            obj[8] = CallUtils.GetModPrefix(calamityMod, "Serrated");
            obj[9] = CallUtils.GetModPrefix(calamityMod, "Vicious");
            obj[10] = CallUtils.GetModPrefix(calamityMod, "Lethal");
            obj[11] = CallUtils.GetModPrefix(calamityMod, "Flawless");
            obj[12] = CallUtils.GetModPrefix(calamityMod, "Blunt");
            obj[13] = CallUtils.GetModPrefix(calamityMod, "Flimsy");
            obj[14] = CallUtils.GetModPrefix(calamityMod, "Unbalanced");
            obj[16] = CallUtils.GetModPrefix(calamityMod, "Atrocious");
            obj[15] = CallUtils.GetModPrefix(RemnantOfTheAncient, "Exquisite");
            return Utils.SelectRandom(rand, obj);
        }
        [JITWhenModsEnabled("CalamityMod")]
        public static bool NegativeRoguePrefix(int prefix)
        {
            return new List<int>
            {
               CallUtils.GetModPrefix(calamityMod,"Blunt"),
               CallUtils.GetModPrefix(calamityMod,"Flimsy"),
               CallUtils.GetModPrefix(calamityMod,"Unbalanced"),
               CallUtils.GetModPrefix(calamityMod,"Atrocious")
            }.Contains(prefix);
        }
    }
}