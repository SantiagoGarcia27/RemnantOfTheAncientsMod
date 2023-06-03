using Terraria.ModLoader;
using Terraria;
using Terraria.Utilities;
using Terraria.ID;
using System.Collections.Generic;
using CalamityMod.Items;

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
        public override void OnCreate(Item item, ItemCreationContext context)
        {
            storedPrefix = -1;
            
        }
        [JITWhenModsEnabled("CalamityMod")]
        public override bool PreReforge(Item item)
        {
            StealthGenBonus = 1f;
            StealthStrikePrefixBonus = 0f;
            storedPrefix = item.prefix;
            return true;
        }
        [JITWhenModsEnabled("CalamityMod")]

        public override int ChoosePrefix(Item item, UnifiedRandom rand)
        {
            int prefix = RandomRoguePrefix();
            if (storedPrefix == -1 && item.CountsAsClass(Utils1.GetDamageClassFromMod(RemnantOfTheAncientsMod.CalamityMod, "RogueDamageClass")) && item.IsCandidateForReforge)
            {
                if (NegativeRoguePrefix(prefix) && !Main.rand.NextBool(3))
                {
                    return 0;
                }
                return prefix;
            }
            if (!CalamityMod.CalamityConfig.Instance.RemoveReforgeRNG || Main.gameMenu || storedPrefix == -1)
            {
                var damageClass = Utils1.GetDamageClassFromMod(RemnantOfTheAncientsMod.CalamityMod, "RogueDamageClass");
                if (item.CountsAsClass(damageClass)) return prefix;
                else return -1;
            }
            return GetReworkedReforge(item, rand, storedPrefix);
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
                int[] obj = new int[6] { 62, 69, 73, 77, 0, 0 };
                obj[4] = Utils1.GetModPrefix(calamityMod, "Quiet");
                obj[5] = Utils1.GetModPrefix(RemnantOfTheAncient, "Healthy");
                array[0] = obj;
                int[] obj2 = new int[7] { 63, 70, 67, 74, 78, 0, 0 };
                obj2[5] = Utils1.GetModPrefix(calamityMod, "Cloaked");
                obj2[6] = Utils1.GetModPrefix(RemnantOfTheAncient, "Healer");
                array[1] = obj2;
                int[] obj3 = new int[7] { 64, 71, 75, 79, 66, 0, 0 };
                obj3[5] = Utils1.GetModPrefix(calamityMod, "Camouflaged");
                obj3[6] = Utils1.GetModPrefix(RemnantOfTheAncient, "Atletic");
                array[2] = obj3;
                int[] obj4 = new int[7] { 65, 72, 68, 76, 80, 0, 0 };
                obj4[5] = Utils1.GetModPrefix(calamityMod, "Silent");
                obj4[6] = Utils1.GetModPrefix(RemnantOfTheAncient, "Gigant");
                array[3] = obj4;
                int[] obj5 = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
                obj4[0] = Utils1.GetModPrefix(RemnantOfTheAncient, "Impenetrable");
                obj4[1] = Utils1.GetModPrefix(RemnantOfTheAncient, "Uncontrolled");
                obj4[2] = Utils1.GetModPrefix(RemnantOfTheAncient, "Supersonic");
                obj4[3] = Utils1.GetModPrefix(RemnantOfTheAncient, "Sharp");
                obj4[4] = Utils1.GetModPrefix(RemnantOfTheAncient, "Accurate");
                obj4[5] = Utils1.GetModPrefix(RemnantOfTheAncient, "Shadow");
                obj4[6] = Utils1.GetModPrefix(RemnantOfTheAncient, "Titanic");
                array[4] = obj5;
                int[][] accessoryReforgeTiers = array;
                prefix = IteratePrefix(rand, accessoryReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<SummonMeleeSpeedDamageClass>())
            {
                if (item.type == ItemID.Terrarian)
                {
                    int[][] terrarianReforgeTiers = new int[4][]
                    {
                    new int[3] { 36, 38, 54 },
                    new int[3] { 53, 57, 61 },
                    new int[3] { 37, 60, 59 },
                    new int[1] { 84 }
                    };
                    prefix = IteratePrefix(rand, terrarianReforgeTiers, currentPrefix);
                }
                else if ((item.channel || item.noMelee) && item.type != ItemID.Zenith && item.useStyle != ItemUseStyleID.Rapier && !item.CountsAsClass<SummonMeleeSpeedDamageClass>())
                {
                    int[][] meleeNoSpeedReforgeTiers = new int[4][]
                    {
                    new int[3] { 36, 38, 54 },
                    new int[3] { 53, 57, 61 },
                    new int[2] { 37, 60 },
                    new int[1] { 59 }
                    };
                    prefix = IteratePrefix(rand, meleeNoSpeedReforgeTiers, currentPrefix);
                }
                else
                {
                    int[][] meleeReforgeTiers = new int[6][]
                    {
                    new int[8] { 36, 45, 51, 15, 14, 15, 38, 54 },
                    new int[6] { 53, 57, 61, 42, 6, 12 },
                    new int[5] { 46, 44, 1, 3, 5 },
                    new int[4] { 2, 55, 4, 37 },
                    new int[3] { 60, 43, 59 },
                    new int[1] { 81 }
                    };
                    int[][] toolReforgeTiers = new int[6][]
                    {
                    new int[7] { 36, 45, 51, 14, 15, 38, 54 },
                    new int[6] { 53, 57, 61, 42, 6, 12 },
                    new int[5] { 46, 44, 1, 3, 5 },
                    new int[4] { 2, 55, 4, 37 },
                    new int[3] { 60, 43, 59 },
                    new int[2] { 81, 15 }
                    };
                    int[][] tierListToUse = ((item.pick > 0 || item.axe > 0 || item.hammer > 0) ? toolReforgeTiers : meleeReforgeTiers);
                    prefix = IteratePrefix(rand, tierListToUse, currentPrefix);
                }
            }
            else if (item.CountsAsClass<RangedDamageClass>())
            {
                int[][] rangedReforgeTiers = new int[6][]
                {
                new int[6] { 36, 45, 51, 25, 38, 54 },
                new int[5] { 53, 57, 61, 42, 19 },
                new int[5] { 46, 44, 18, 21, 55 },
                new int[3] { 37, 60, 16 },
                new int[4] { 59, 17, 20, 43 },
                new int[1] { 82 }
                };
                prefix = IteratePrefix(rand, rangedReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<MagicSummonHybridDamageClass>())
            {
                int[][] magicReforgeTiers = new int[6][]
                {
                new int[6] { 36, 45, 51, 35, 38, 54 },
                new int[6] { 53, 57, 61, 42, 33, 52 },
                new int[5] { 46, 44, 27, 34, 55 },
                new int[3] { 37, 60, 26 },
                new int[3] { 59, 28, 43 },
                new int[1] { 83 }
                };
                prefix = IteratePrefix(rand, magicReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<SummonDamageClass>())
            {
                int[][] summonReforgeTiers = new int[6][]
                {
                new int[2] { 45, 35 },
                new int[5] { 38, 54, 42, 33, 52 },
                new int[3] { 53, 27, 34 },
                new int[4] { 37, 60, 26, 43 },
                new int[2] { 28, 59 },
                new int[2] { 83, 57 }
                };
                prefix = IteratePrefix(rand, summonReforgeTiers, currentPrefix);
            }
            else if (item.CountsAsClass<ThrowingDamageClass>())
            {
                int[][] array2 = new int[6][];
                int[] obj5 = new int[7] { 36, 45, 51, 38, 54, 0, 0 };
                obj5[5] = Utils1.GetModPrefix(calamityMod, "Radical");
                obj5[6] = Utils1.GetModPrefix(calamityMod, "Pointy");
                array2[0] = obj5;
                int[] obj6 = new int[6] { 53, 57, 61, 42, 0, 0 };
                obj6[4] = Utils1.GetModPrefix(calamityMod, "Sharp");
                obj6[5] = Utils1.GetModPrefix(calamityMod, "Glorious");
                array2[1] = obj6;
                int[] obj7 = new int[6] { 46, 44, 55, 0, 0, 0 };
                obj7[3] = Utils1.GetModPrefix(calamityMod, "Feathered");
                obj7[4] = Utils1.GetModPrefix(calamityMod, "Sleek");
                obj7[5] = Utils1.GetModPrefix(calamityMod, "Hefty");
                array2[2] = obj7;
                array2[3] = new int[4]
                {
                    37,
                    60,
                    Utils1.GetModPrefix(calamityMod,"Mighty"),
                    Utils1.GetModPrefix(calamityMod,"Serrated")
                };
                array2[4] = new int[4]
                {
                    59,
                    43,
                    Utils1.GetModPrefix(calamityMod, "Vicious"),
                    Utils1.GetModPrefix(calamityMod,"Lethal")
                };
                array2[5] = new int[2]
                {
                    Utils1.GetModPrefix(calamityMod, "Flawless"),
                    Utils1.GetModPrefix(RemnantOfTheAncient, "Exquisite")
                };
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
            int[] obj = new int[40]
            {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 36, 37, 38,
            39, 40, 53, 54, 55, 56, 57, 59, 60, 61,
            42, 43, 44, 45, 46, 47, 48, 49, 50, 51
            };
            obj[0] = Utils1.GetModPrefix(calamityMod, "Radical");
            obj[1] = Utils1.GetModPrefix(calamityMod, "Pointy");
            obj[2] = Utils1.GetModPrefix(calamityMod, "Sharp");
            obj[3] = Utils1.GetModPrefix(calamityMod, "Glorious");
            obj[4] = Utils1.GetModPrefix(calamityMod, "Feathered");
            obj[5] = Utils1.GetModPrefix(calamityMod, "Sleek");
            obj[6] = Utils1.GetModPrefix(calamityMod, "Hefty");
            obj[7] = Utils1.GetModPrefix(calamityMod, "Mighty");
            obj[8] = Utils1.GetModPrefix(calamityMod, "Serrated");
            obj[9] = Utils1.GetModPrefix(calamityMod, "Vicious");
            obj[10] = Utils1.GetModPrefix(calamityMod, "Lethal");
            obj[11] = Utils1.GetModPrefix(calamityMod, "Flawless");
            obj[12] = Utils1.GetModPrefix(calamityMod, "Blunt");
            obj[13] = Utils1.GetModPrefix(calamityMod, "Flimsy");
            obj[14] = Utils1.GetModPrefix(calamityMod, "Unbalanced");
            obj[16] = Utils1.GetModPrefix(calamityMod, "Atrocious");
            obj[15] = Utils1.GetModPrefix(RemnantOfTheAncient, "Exquisite");
            return Utils.SelectRandom(rand, obj);
        }
        [JITWhenModsEnabled("CalamityMod")]
        public static bool NegativeRoguePrefix(int prefix)
        {
            return new List<int>
            {
                Utils1.GetPrefixFromMod(calamityMod,"Blunt"),
                Utils1.GetPrefixFromMod(calamityMod,"Flimsy"),
                Utils1.GetPrefixFromMod(calamityMod,"Unbalanced"),
                Utils1.GetPrefixFromMod(calamityMod,"Atrocious")
            }.Contains(prefix);
        }
    }
}