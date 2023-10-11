using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using static RemnantOfTheAncientsMod.Common.Drops.DropRules.RemnantConditionDropRules;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Buf;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Drops
{
    public class BossBags : GlobalItem
    {
        public override void RightClick(Item item, Player player)
        {
            int commonScroll = Utils1.GetReaperNumber(5, 1);
            int SkeletronScroll = Utils1.GetReaperNumber(2, 1);
            switch (item.type)
            {
                case ItemID.KingSlimeBossBag:
                    Utils1.DroppItem(player,item,ItemType<KingSlimeScroll>(),1, commonScroll);
                    break;
                case ItemID.EyeOfCthulhuBossBag:
                    Utils1.DroppItem(player, item, ItemType<EyeOfCthulhuScroll>(), 1, commonScroll);
                    break;
                case ItemID.EaterOfWorldsBossBag:
                    Utils1.DroppItem(player, item, ItemType<EaterOfWorldsScroll>(), 1, commonScroll);
                    if (!Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[2] && Reaper.ReaperMode) 
                    {
                        Utils1.DroppItem(player, item, ItemType<CorruptSoul>(), 1, 1);
                    }
                    break;
                case ItemID.BrainOfCthulhuBossBag:
                    Utils1.DroppItem(player, item, ItemType<BrainOfCthulhuScroll>(), 1, commonScroll);
                    break;
                case ItemID.QueenBeeBossBag:
                    Utils1.DroppItem(player, item, ItemType<QueenBeeScroll>(), 1, commonScroll);
                    break;
                case ItemID.SkeletronBossBag:
                    Utils1.DroppItem(player, item, ItemType<SkeletronScroll>(), 1,SkeletronScroll);
                    break;
                case ItemID.WallOfFleshBossBag:
                    Utils1.DroppItem(player, item, ItemType<WallOfFleshScroll>(), 1, commonScroll);
                    break;
                case ItemID.GolemBossBag:
                    Utils1.DroppItem(player, item, ItemType<Legendary_Great_Sword>(), Utils1.GetReaperNumber(5, 1), 800);
                    break;
                case ItemID.MoonLordBossBag:
                    Utils1.DroppItem(player, item, ItemType<LaserMachineGun>(), Utils1.GetReaperNumber(5, 1), 100);
                    break;
                default:
                    if (item.type == ModContent.ItemType<infernalBag>())
                    {
                        Utils1.DroppItem(player, item, ItemType<TheSpiker>(), Utils1.GetReaperNumber(5, 1),200);
                    }
                    
                    break;

               

            }
            base.RightClick(item, player);
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
           
            switch (item.type)
            {         
                case ItemID.KingSlimeBossBag:
                    itemLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<GelatinousGlove>()));
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    break;
                case ItemID.EyeOfCthulhuBossBag:       
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    break;
                case ItemID.EaterOfWorldsBossBag:                    
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    break;
                case ItemID.BrainOfCthulhuBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss3, 7));
                    break;
                case ItemID.QueenBeeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss5, 7));
                    break;
                case ItemID.SkeletronBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));
                    break;
                case ItemID.DeerclopsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxDeerclops, 7));
                    break;
                case ItemID.WallOfFleshBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss2, 7));
                    break;
                case ItemID.QueenSlimeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxQueenSlime, 7));
                    break;
                case ItemID.TwinsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss2, 7)); 
                    break;
                case ItemID.SkeletronPrimeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss1, 7));     
                    break;
                case ItemID.DestroyerBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss3, 7));            
                    itemLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<MechanicalPersonalLifeSafeSystem>()));
                    break;
                case ItemID.PlanteraBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxPlantera, 7));
                    break;
                case ItemID.FairyQueenBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxEmpressOfLight, 7));           
                    break;
                case ItemID.GolemBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss4, 7));
                    break;
                case ItemID.FishronBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxDukeFishron, 7));
                    break;
                case ItemID.CultistBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxBoss4, 7));
                    break;
                case ItemID.MoonLordBossBag:
                    itemLoot.Add(ItemDropRule.Common(ItemID.MusicBoxLunarBoss, 7));
                    break;

                case ItemID.JungleFishingCrate:
                    itemLoot.Add(ItemDropRule.Common(ItemType<JungleBar>(), 4,1,3));
                    break;
                case ItemID.JungleFishingCrateHard:
                    itemLoot.Add(ItemDropRule.Common(ItemType<JungleBar>(), 4, 2, 5));
                    break;

                case ItemID.DungeonFishingCrate:
                    itemLoot.Add(ItemDropRule.Common(ItemType<BoneBar>(), 2, 1, 3));
                    break;
                case ItemID.DungeonFishingCrateHard:
                    itemLoot.Add(ItemDropRule.Common(ItemType<BoneBar>(), 2, 2, 5));
                    break;
            }
        }
    }
}