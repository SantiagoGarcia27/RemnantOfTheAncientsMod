using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Items.Summon.Buf;
using RemnantOfTheAncientsMod.Items.ReaperSouls;
using Terraria.GameContent.ItemDropRules;
using static RemnantOfTheAncientsMod.Drops.RemnantDropRules;

namespace RemnantOfTheAncientsMod.Drops
{
    public class BossBags : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch (item.type)
            {
                case ItemID.KingSlimeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemType<KingSlimeScroll>(), 5));
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new SlimeReaperSoulRule(), ItemType<DesertSoul>()));
                    break;
                case ItemID.EyeOfCthulhuBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemType<EyeOfCthulhuScroll>(), 5));
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new EyeOfChutuluReaperSoulRule(), ItemType<EyeSoul>()));
                    break;
                case ItemID.EaterOfWorldsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemType<EaterOfWorldsScroll>(), 5));
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new CorruptReaperSoulRule(), ItemType<CorruptSoul>()));
                    break;
                case ItemID.BrainOfCthulhuBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemType<BrainOfCthulhuScroll>(), 5));
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss3, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new CorruptReaperSoulRule(), ItemType<CorruptSoul>()));
                    break;
                case ItemID.QueenBeeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss5, 7));
                    itemLoot.Add(ItemDropRule.Common(ItemType<QueenBeeScroll>(), 5));
                    itemLoot.Add(ItemDropRule.ByCondition(new QueenBeeReaperSoulRule(), ItemType<QueenSoul>()));
                    break;
                case ItemID.SkeletronBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemType<SkeletronScroll>(), 5));
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new SkeletronReaperSoulRule(), ItemType<SkeletonSoul>()));
                    break;
                case ItemID.DeerclopsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxDeerclops, 7));
                    itemLoot.Add(ItemDropRule.Common(ItemType<DeerclopsScroll>(), 5));
                    itemLoot.Add(ItemDropRule.ByCondition(new DeerclopsReaperSoulRule(), ItemType<DeerclopsSoul>()));
                    break;
                case ItemID.WallOfFleshBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemType<WallOfFleshScroll>(), 5));
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss2, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new DeerclopsReaperSoulRule(), ItemType<DeerclopsSoul>()));
                    break;
                case ItemID.QueenSlimeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxQueenSlime, 7));
                    itemLoot.Add(ItemDropRule.Common(ItemType<QueenSlimeScroll>(), 5));
                    itemLoot.Add(ItemDropRule.ByCondition(new QueenSlimeReaperSoulRule(), ItemType<QueenSoul>()));
                    break;
                case ItemID.TwinsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss2, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new RetinazorReaperSoulRule(), ItemType<RetinazorEnergyCell>()));
                    itemLoot.Add(ItemDropRule.ByCondition(new SpazmatismReaperSoulRule(), ItemType<SpazmatismEnergyCell>()));
                    break;
                case ItemID.SkeletronPrimeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new SkeletronPrimeReaperSoulRule(), ItemType<SkeletronPrimeEnergyCell>()));
                    break;
                case ItemID.DestroyerBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss3, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new DestroyerReaperSoulRule(), ItemType<DestroyerEnergyCell>()));
                    break;
                case ItemID.PlanteraBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxPlantera, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new PlanteraReaperSoulRule(), ItemType<PlantSoul>()));
                    break;
                case ItemID.FairyQueenBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxEmpressOfLight, 7));
                    //itemLoot.Add(ItemDropRule.Common(ItemType<EmpressOfLightScroll>(), 5));
                    itemLoot.Add(ItemDropRule.ByCondition(new EmpressOfLightReaperSoulRule(), ItemType<EmpressSoul>()));
                    break;
                case ItemID.GolemBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss4, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new GolemReaperSoulRule(), ItemType<GolemSoul>()));
                    break;
                case ItemID.FishronBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxDukeFishron, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new DukeFishronReaperSoulRule(), ItemType<FishSoul>()));
                    break;
                case ItemID.CultistBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss4, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new LunaticCultistReaperSoulRule(), ItemType<CultistSoul>()));
                    break;
                case ItemID.MoonLordBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxLunarBoss, 7));
                    itemLoot.Add(ItemDropRule.ByCondition(new MoonLordReaperSoulRule(), ItemType<MoonSoul>()));
                    break;
            }
        }
    }
}