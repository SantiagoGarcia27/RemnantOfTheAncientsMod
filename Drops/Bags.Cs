using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Items.Summon.Buf;
using RemnantOfTheAncientsMod.Items.Fmode;

namespace RemnantOfTheAncientsMod.Drops
{
    public class BossBags : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag" && arg == ItemID.KingSlimeBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.KingSlimeBossBag), ItemType<KingSlimeScroll>());
                if(!Main.LocalPlayer.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.KingSlimeBossBag), ItemType<KingSoul>());
            }

            if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ItemType<PerEoC>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<EyeReaperSoulPlayer>().EyeeReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ItemType<EyeSoul>());
            }
            if (context == "bossBag" && arg == ItemID.EaterOfWorldsBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EaterOfWorldsBossBag), ItemType<PerEoW>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EaterOfWorldsBossBag), ItemType<CorruptSoul>());
            }
            if (context == "bossBag" && arg == ItemID.BrainOfCthulhuBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.BrainOfCthulhuBossBag), ItemType<WFScroll>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.BrainOfCthulhuBossBag), ItemType<CorruptSoul>());
            }
            if (context == "bossBag" && arg == ItemID.QueenBeeBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.QueenBeeBossBag), ItemType<PerQnBee>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.QueenBeeBossBag), ItemType<BeeSoul>());
            }

            if (context == "bossBag" && arg == ItemID.SkeletronBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 2) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.SkeletronBossBag), ItemType<PerSktn>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.SkeletronBossBag), ItemType<SkeletonSoul>());
            }

            if (context == "bossBag" && arg == ItemID.DeerclopsBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 2) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DeerclopsBossBag), ItemType<PerSktn>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DeerclopsBossBag), ItemType<SkeletonSoul>());
            }
            if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag)
            {
                if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.WallOfFleshBossBag), ItemType<PerWlloFsh>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.WallOfFleshBossBag), ItemType<InfernalSoul>());
            }
            if (context == "bossBag" && arg == ItemID.DestroyerBossBag)
            {
               // if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DestroyerBossBag), ItemType<PerWlloFsh>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DestroyerBossBag), ItemType<DestroyerEnergyCell>());
            }
            if (context == "bossBag" && arg == ItemID.TwinsBossBag)
            {
                // if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DestroyerBossBag), ItemType<PerWlloFsh>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.TwinsBossBag), ItemType<DestroyerEnergyCell>());
                if (!Main.LocalPlayer.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.TwinsBossBag), ItemType<SpazmatismEnergyCell>());
            }
            if (context == "bossBag" && arg == ItemID.DestroyerBossBag)
            {
                // if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DestroyerBossBag), ItemType<PerWlloFsh>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DestroyerBossBag), ItemType<DestroyerEnergyCell>());
            }
            if (context == "bossBag" && arg == ItemID.SkeletronPrimeBossBag)
            {
                // if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.DestroyerBossBag), ItemType<PerWlloFsh>(), Main.rand.Next(1, 1));
                if (!Main.LocalPlayer.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.SkeletronPrimeBossBag), ItemType<SkeletronPrimeEnergyCell>());
            }

        }
    }
}