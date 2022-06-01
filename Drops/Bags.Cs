using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Summon.Buf;

namespace opswordsII.Drops
{
    public class BossBags : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag" && arg == ItemID.KingSlimeBossBag){
                if (Main.rand.NextFloat () <= (float) 1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.KingSlimeBossBag),ItemType<SScroll>());}

            if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag){
                if (Main.rand.NextFloat() <= (float)1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag),ItemType<PerEoC>(), Main.rand.Next(1, 1));}

            if (context == "bossBag" && arg == ItemID.EaterOfWorldsBossBag){
               if (Main.rand.NextFloat () <= (float) 1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EaterOfWorldsBossBag),ModContent.ItemType <PerEoW>(), Main.rand.Next(1, 1));}

            if (context == "bossBag" && arg == ItemID.BrainOfCthulhuBossBag){
               if (Main.rand.NextFloat () <= (float) 1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.BrainOfCthulhuBossBag),ModContent.ItemType<WFScroll>(), Main.rand.Next(1, 1));} 

            if (context == "bossBag" && arg == ItemID.QueenBeeBossBag){
               if (Main.rand.NextFloat () <= (float) 1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.QueenBeeBossBag),ModContent.ItemType <PerQnBee>(), Main.rand.Next(1, 1));}   

            if (context == "bossBag" && arg == ItemID.SkeletronBossBag){
               if (Main.rand.NextFloat () <= (float) 1 / 2) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.SkeletronBossBag),ModContent.ItemType <PerSktn>(), Main.rand.Next(1, 1));}  

            if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag){
               if (Main.rand.NextFloat () <= (float) 1 / 5) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.WallOfFleshBossBag), ModContent.ItemType <PerWlloFsh>(), Main.rand.Next(1, 1));}

            if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag){
               if (Main.rand.NextFloat () <= (float) 1 / 3) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.WallOfFleshBossBag), ModContent.ItemType <Items.accesorios.TrowerEmblem>(), Main.rand.Next(1, 1));}   

            if (context == "bossBag" && arg == ItemID.GolemBossBag){
               if (Main.rand.NextFloat () <= (float) 1 / 800) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.GolemBossBag), ModContent.ItemType <Items.Mele.Legendary_Great_Sword>(), Main.rand.Next(1, 1));}       
        }
    }
}