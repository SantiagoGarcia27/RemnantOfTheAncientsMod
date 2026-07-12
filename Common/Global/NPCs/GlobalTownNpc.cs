using RemnantOfTheAncientsMod.Common.RemPlayer;
using System;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Boots;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using static RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Endless_Basic_Potion_Kit;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Items.Guides;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Strawberry;
using SangarUtilities.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Furniture;
using RemnantOfTheAncientsMod.Content.Items.ReforgeCatalyst;
using RemnantOfTheAncientsMod.Content.Currencies;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs
{
    public class GlobalTownNpc : GlobalNPC
    {

        public override void ModifyShop(NPCShop shop)
        {
            if (RemnantOfTheAncientsMod.AlchemistNPCMod != null)
            {
                if (shop.NpcType == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Brewer"))
                {
                    string SHOP_4 = "MorePotions/Atheria";
                    if (shop.Name == SHOP_4)
                    {
                        shop.Add(new Item(ItemType<Money_Collector_Potion>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 1, 50, 0) })
                        .Add(new Item(ItemType<Commander_Potion>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 2, 50, 0) }, Condition.DownedEowOrBoc);
                    }
                }
                if (shop.NpcType == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Alchemist"))
                {
                    string BaseShop = "BaseShop";
                    if (shop.Name == BaseShop)
                    {
                        shop.Add(new Item(ItemType<NecroticRestauration>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 50, 0) }, Condition.DownedSkeletron);
                    }
                }
                if (shop.NpcType == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Musician"))
                {
                    string Sh5 = "Sh5";
                    if (shop.Name == Sh5)
                    {
                        shop.Add(new Item(ItemType<DesertMusicBox>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 10, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedDesert", () => RemnantDownedBossSystem.downedDesert))
                            .Add(new Item(ItemType<FrozenMusicBox>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 10, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedFrozen", () => RemnantDownedBossSystem.downedFrozen))
                            .Add(new Item(ItemType<Frozenp2MusicBox>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 10, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedFrozen", () => RemnantDownedBossSystem.downedFrozen))
                            .Add(new Item(ItemType<InfernalMusicBox>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 10, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedTyrant", () => RemnantDownedBossSystem.downedTyrant));
                    }
                }
                if (shop.NpcType == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Tinkerer"))
                {
                    string Shop1 = "MovementMisc";
                    string Shop2 = "Combat";
                    if (shop.Name == Shop1)
                    {
                        shop.Add(new Item(ItemType<Boot>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 5, 0, 0) });
                    }
                    if (shop.Name == Shop2)
                    {
                        shop.Add(new Item(ItemType<magic_stick>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 20, 0, 0) }, Condition.DownedEyeOfCthulhu);
                    }
                }
                if (shop.NpcType == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "YoungBrewer"))
                {
                    string Shop1 = "Combinations";
                    string Shop2 = "Flasks";
                    if (shop.Name == Shop1)
                    {
                        shop.Add(new Item(ItemType<Ranger_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 5, 0, 0) })
                            .Add(new Item(ItemType<Summon_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 4, 0, 0) })
                            .Add(new Item(ItemType<Melee_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 3, 0, 0) })
                            .Add(new Item(ItemType<Mage_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 4, 0, 0) })
                            .Add(new Item(ItemType<Advanced_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 4, 0, 0) })
                            .Add(new Item(ItemType<Tank_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 8, 0, 0) })
                            .Add(new Item(ItemType<Exploration_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 15, 0, 0) })
                            .Add(new Item(ItemType<Ultimate_Potion_Kit>()) { shopCustomPrice = Utils1.FormatMoney(0, 1, 0, 0, 0) }, Condition.DownedMoonLord);
                    }
                    if (shop.Name == Shop2)
                    {
                        shop.Add(new Item(ItemType<Sand_Flask>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 2, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedDesert", () => RemnantDownedBossSystem.downedDesert));
                    }
                }
                if (shop.NpcType == CallUtils.TryGetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Operator"))
                {
                    string MaterialShop = "Materials";
                    string ModMaterialShop = "ModMaterials";
                    string Bags1Shop = "ModBags1";
                    string Bags2Shop = "ModBags2";
                    if (shop.Name == MaterialShop)
                    {
                        shop.Add(new Item(ItemID.Cobweb) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 50, 0) });
                    }
                    if (shop.Name == ModMaterialShop)
                    {
                        shop.Add(new Item(ItemType<Sand_escense>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 5, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedDesert", () => RemnantDownedBossSystem.downedDesert))
                            .Add(new Item(ItemType<Ice_escense>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 1, 50, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedFrozen", () => RemnantDownedBossSystem.downedFrozen))
                            .Add(new Item(ItemType<Neutrum_Fragment>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 10, 0, 0) }, Condition.DownedMoonLord)
                            .Add(new Item(ItemType<CelestialAmalgamate>()) { shopCustomPrice = Utils1.FormatMoney(0, 40, 0, 0, 0) }, Condition.DownedMoonLord);
                    }
                    if (shop.Name == Bags1Shop)
                    {
                        shop.Add(new Item(ItemType<desertBag>()) { shopCustomPrice = Utils1.FormatMoney(0, 1, 50, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedDesert", () => RemnantDownedBossSystem.downedDesert));

                    }
                    if (shop.Name == Bags2Shop)
                    {
                        shop.Add(new Item(ItemType<frostBag>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 1, 50, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedFrozen", () => RemnantDownedBossSystem.downedFrozen))
                            .Add(new Item(ItemType<infernalBag>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 2, 50, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.DownedTyrant", () => RemnantDownedBossSystem.downedTyrant));
                    }
                }
            }
            if (shop.NpcType == NPCID.GoblinTinkerer)
            {
                shop.Add(new Item(ItemType<Terracoin>()) { shopCustomPrice = Utils1.FormatMoney(0, 5, 50, 0, 0) }, Condition.IsNpcShimmered, Condition.Hardmode,Condition.DownedPlantera);
                shop.Add(new Item(ItemType<ReforgeAnvil>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 5, 0, 0) }, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<BerserkStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<VeteranStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<RelicStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<SharpStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<ImpenetrableStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<AcurateStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<SupersonicStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<UncontrolledStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                shop.Add(new Item(ItemType<TitanicStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);

                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    shop.Add(new Item(ItemType<ExquisiteStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                    shop.Add(new Item(ItemType<ShadowStone>()) { shopCustomPrice = 1, shopSpecialCurrency = CustomCurrencies.TerracoinCurrency }, Condition.IsNpcShimmered, Condition.DownedEyeOfCthulhu);
                }
            }
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                shop.Add(new Item(ItemType<QuickDraw>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 1, 0, 0) })
                .Add(new Item(ItemType<ReinforcedAmmoBox>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 10, 0, 0) }, Condition.DownedEyeOfCthulhu)
                .Add(new Item(ItemID.ChlorophyteBullet) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 5, 0) }, Condition.DownedPlantera);

                if (RemnantOfTheAncientsMod.CalamityMod == null)
                {
                    shop.Add(new Item(ItemID.Uzi) { shopCustomPrice = Utils1.FormatMoney(0, 0, 50, 0, 0) }, Condition.IsNpcShimmered, Condition.Hardmode, Condition.InJungle);
                }

            }
            if (shop.NpcType == NPCID.SkeletonMerchant)
            {
                shop.Add(new Item(ItemType<Darksign>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 7, 0, 0) }, Condition.DownedEyeOfCthulhu);
            }
            base.ModifyShop(shop);
        }
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            foreach (Item item in items)
            {
                if (item is not null)
                {
                    decimal discount = (decimal)ShopUtils.GetShopDiscount(Main.LocalPlayer);
                    int? finalPrice = (int?)Math.Round((item.shopCustomPrice ?? item.value) * discount);
                    if (finalPrice <= 0) finalPrice = 1;
                    item.shopCustomPrice = finalPrice;
                }
            }
        }
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            shop[nextSlot] = ItemType<Strawberry_Hairpin>();
            nextSlot++;
            base.SetupTravelShop(shop, ref nextSlot);
        }

        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            Player player = Main.LocalPlayer;
            if (npc.type == NPCID.ArmsDealer)
            {
                if (npc.IsShimmerVariant)
                {
                    RemnantPlayer.PlayerTalkToday.TryAdd(npc.type, false);
                    if (!RemnantPlayer.PlayerTalkToday[npc.type])
                    {

                        if (firstButton && Main.rand.NextBool(4))
                        {
                            RemnantPlayer.PlayerTalkToday[npc.type] = true;
                            player.AddBuff(BuffID.AmmoBox, Utils1.FormatTimeToTick(0, 0, 10, 0));
                        }
                    }
                }

            }
            base.OnChatButtonClicked(npc, firstButton);
        }
        public override void AI(NPC npc)
        {
            if(npc.type == NPCID.ArmsDealer && npc.IsShimmerVariant)
            {
                RemnantPlayer.PlayerTalkToday.TryAdd(npc.type, false);
                if (RemnantPlayer.PlayerTalkToday[npc.type])
                {
                    float hour = Utils.GetDayTimeAs24FloatStartingFromMidnight();
                    if ((hour >= 3f && hour <= 4f) || (hour >= 23f && hour <= 24f))
                    {
                        RemnantPlayer.PlayerTalkToday[npc.type] = false;
                    }
                }
            }



            base.AI(npc);
        }
    }
}
