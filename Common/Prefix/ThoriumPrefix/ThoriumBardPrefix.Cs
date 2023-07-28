//using RemnantOfTheAncientsMod.Common.UtilsTweaks;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.ModLoader;

//namespace RemnantOfTheAncientsMod.Prefix.ThoriumPrefix
//{
//    public abstract class ThoriumBardPrefix : ModPrefix
//    {
//        internal static List<byte> BardPrefixes;

//        internal string// //DisplayName;

//        internal float damageMult = 1f;

//        internal float shootSpeedMult = 1f;

//        internal float useTimeMult = 1f;

//        internal int durationBonus;

//        internal int critBonus;

//        public override PrefixCategory Category => (PrefixCategory)5;

//        public ThoriumBardPrefix(string// //DisplayName, float damageMult = 1f, float shootSpeedMult = 1f, float useTimeMult = 1f, int durationBonus = 0, int critBonus = 0)
//        {
//            this.//DisplayName =// //DisplayName;
//            this.damageMult = damageMult;
//            this.shootSpeedMult = shootSpeedMult;
//            this.useTimeMult = useTimeMult;
//            this.durationBonus = durationBonus;
//            this.critBonus = critBonus;
//        }

//        public static void Load()
//        {
//            BardPrefixes = new List<byte>();
//        }

//        public static void Unload()
//        {
//            BardPrefixes = null;
//        }

//        public static bool DoConditionsApply(Item item)
//        {
//            // Mod ThoiumMod = RemnantOfTheAncientsMod.ThoriumMod;
//            if (ModLoader.TryGetMod("ThoiumMod", out Mod ThoriumMod))
//            {
//                if (item is ThoriumMod.GetContent<BardItem>() && item.maxStack == 1 && item.damage > 0)
//                {
//                    return item.useStyle > 0;
//                }
//            }
//            return false;
//        }

//        public override void SetDefaults()
//        {
//            ((ModPrefix)this).get_//DisplayName().SetDefault(//DisplayName);
//            BardPrefixes.Add(((ModPrefix)this).get_Type());
//        }

//        public override void Apply(Item item)
//        {
//            if (ModLoader.TryGetMod("ThoiumMod", out Mod ThoriumMod))
//            {
//                if (item.CountsAsClass(ExternalModCallUtils.GetDamageClassFromMod(RemnantOfTheAncientsMod.CalamityMod, "RogueDamageClass")) && item is BardItem bardItem)
//            {
//                    ThoriumMod.GetContent<BardItem>().DurationBonus = (short)(durationBonus * 60);
//                }
//            }
//        }

//        public override void ModifyValue(ref float valueMult)
//        {
//            valueMult *= 1f + durationBonus * 0.025f;
//        }

//        public override bool CanRoll(Item item)
//        {
//            return DoConditionsApply(item);
//        }

//        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
//        {
//            damageMult = this.damageMult;
//            shootSpeedMult = this.shootSpeedMult;
//            useTimeMult = this.useTimeMult;
//            critBonus = this.critBonus;
//        }
//    }
//}
