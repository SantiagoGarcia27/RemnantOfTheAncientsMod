using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common
{
    public partial class BossID
    {
        public partial class VanillaID
        {
            public const short KingSlime = 0;
            public const short EyeOfChutulu = 1;
            public const short CorruptBoss = 2;
            public const short QueenBee = 3;
            public const short Skeletron = 4;
            public const short Deerclops = 5;
            public const short DesertAnhilator = 6;
            public const short WallOfFlesh = 7;
            public const short FrozenAssaulter = 8;
            public const short QueenSlime = 9;
            public const short Spazmatism = 10;
            public const short Retinazor = 11;
            public const short SkeletronPrime = 12;
            public const short Destroyer = 13;
            public const short Plantera = 14;
            public const short InfernalTyrant = 15;
            public const short Golem = 16;
            public const short EmpressOfLight = 17;
            public const short DukeFishron = 18;
            public const short Cultist = 19;
            public const short MoonLord = 20;
        }
        public partial class CalamityID
        {
            public const short DesertScourge = 1;
            public const short Crabulon = 2;
            public const short CalCorruptBoss = 3;
            public const short SlimeGod = 4;
            public const short Cryogen = 5;
            public const short AquaticScourge = 6;
            public const short BrimstoneElemental = 7;
            public const short CalamitasClone = 8;
            public const short Leviathan = 9;
            public const short AstrumAureus = 10;
            public const short Plaguebringer = 11;
            public const short Ravager = 12;
            public const short AstrumDeus = 13;
            public const short ProfanedGuardian = 14;
            public const short Dragonfolly = 15;
            public const short Providence = 16;
            public const short SormWeaver = 17;
            public const short CeaselessVoid = 18;
            public const short Signus = 19;
            public const short Polterghast = 20;
            public const short OldDuke = 21;
            public const short DevourerOfGods = 22;
            public const short Yharon = 23;
            public const short ExoMech = 24;
            public const short SupremeCalamitas = 25;
        }
    }
    public partial class ModID
    {
        public const short AnotherMod = -1;
        public const short RemnantOfTheAncients = 0;
        public const short Calamity = 1;
        public const short FargosSoul = 2;

        public static short GetModID(Mod mod)
        {
            if (mod == RemnantOfTheAncientsMod.RemnantOfTheAncients)
                return 0;
            if (mod == RemnantOfTheAncientsMod.CalamityMod)
                return 1;
            if (mod == RemnantOfTheAncientsMod.FargosSoulMod)
                return 2;
            return -1;
        }
    }
}
