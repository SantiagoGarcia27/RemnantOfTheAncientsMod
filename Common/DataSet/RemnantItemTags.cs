using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.DataSet
{
    public static class RemnantItemTags
    {
        public static readonly List<int> lanceId =
        [
            ItemID.Spear,
            ItemID.Trident,
            ItemID.DarkLance,
            ItemID.TheRottedFork,
            ItemID.ThunderSpear,
            ItemID.Swordfish,
            ItemID.CobaltNaginata,
            ItemID.PalladiumPike,
            ItemID.MythrilHalberd,
            ItemID.OrichalcumHalberd,
            ItemID.AdamantiteGlaive,
            ItemID.TitaniumTrident,
            ItemID.ChlorophytePartisan,
            ItemID.MushroomSpear,
            ItemID.ObsidianSwordfish,
            ItemID.NorthPole,
            ItemID.JoustingLance,
            ItemID.HallowJoustingLance,
            ItemID.ShadowJoustingLance,
            ItemID.UnholyTrident,
            ItemID.Gungnir
        ];
        public static readonly HashSet<int> CoinsId =
        [
            ItemID.CopperCoin,
            ItemID.SilverCoin,
            ItemID.GoldCoin,
            ItemID.PlatinumCoin
        ];
        public static readonly HashSet<int> SecondClickWeapons =
        [
            ItemID.BookofSkulls,
            ItemID.FlowerofFire,
            ItemID.FlowerofFrost,
            ItemID.SharpTears,
            ItemID.AmethystStaff,
            ItemID.TopazStaff,
            ItemID.SapphireStaff,
            ItemID.EmeraldStaff,
            ItemID.RubyStaff,
            ItemID.DiamondStaff,
            ItemID.AmberStaff,
            ItemID.BubbleGun,
            ItemID.AquaScepter,
            ItemID.WandofFrosting,
            ItemID.WandofSparking,
        ];
        public static readonly Dictionary<int, int> GemStaffs = new()
        {
            { ItemID.AmethystStaff,0},
            { ItemID.TopazStaff ,1},
            { ItemID.EmeraldStaff,2 },
            { ItemID.SapphireStaff,3 },
            { ItemID.RubyStaff,4 },
            { ItemID.DiamondStaff,5 },
            { ItemID.AmberStaff,6 }
        };
        public static readonly HashSet<int> Phones =
        [
            ItemID.CellPhone,
            ItemID.Shellphone,
            ItemID.ShellphoneDummy,
            ItemID.ShellphoneHell,
            ItemID.ShellphoneOcean,
            ItemID.ShellphoneSpawn,
            ItemID.ShellPileBlock
        ];
        public static readonly HashSet<Mod> ModsWith30Endless = [RemnantOfTheAncientsMod.FargowiltasMod];
        public static readonly HashSet<int> LowFood = [];
        public static readonly HashSet<int> MediumFood = [];
        public static readonly HashSet<int> HightFood = [];
        public static readonly HashSet<int> LowBait = [];
        public static readonly HashSet<int> MediumBait = [];
        public static readonly HashSet<int> HightBait = [];
        public static HashSet<int> MusicBox = [];
       




        public static HashSet<int> FillMusicBox()
        {

            List<int> musicBox =
            [
                576,2742,3044,3370,3371,3796,3869,4237,
                4421,4606,4979,4985,5006,5044,5112,5362,
                ModContent.ItemType<DesertMusicBox>(),
                ModContent.ItemType<FrozenMusicBox>(),
                ModContent.ItemType<Frozenp2MusicBox>(),
                ModContent.ItemType<InfernalMusicBox>()
            ];

            for (int i = 562; i <= 574; i++)
                ListUtils.AddSecure(ref musicBox, i);

            for (int i = 1596; i <= 1610; i++)
                ListUtils.AddSecure(ref musicBox, i);

            for (int i = 1963; i <= 1965; i++)
                ListUtils.AddSecure(ref musicBox, i);

            for (int i = 3235; i <= 3237; i++)
                ListUtils.AddSecure(ref musicBox, i);

            for (int i = 4077; i <= 4082; i++)
                ListUtils.AddSecure(ref musicBox, i);

            for (int i = 4356; i <= 4358; i++)
                ListUtils.AddSecure(ref musicBox, i);

            for (int i = 4990; i <= 4992; i++)
                ListUtils.AddSecure(ref musicBox, i);

            for (int i = 5014; i <= 5040; i++)
                ListUtils.AddSecure(ref musicBox, i);

            return musicBox.ToHashSet();
        }
    }
}
