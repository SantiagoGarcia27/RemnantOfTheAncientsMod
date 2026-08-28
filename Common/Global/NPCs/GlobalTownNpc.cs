using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Currencies;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Boots;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Strawberry;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Furniture;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Content.Items.ReforgeCatalyst;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Drawing;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Endless_Basic_Potion_Kit;
using static Terraria.ModLoader.ModContent;

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
                if (item is  null) continue;
               
                float discount = ShopUtils.GetShopDiscount(Main.LocalPlayer);
                int basePrice = item.shopCustomPrice ?? item.value;
                int finalPrice = (int)Math.Max(1, Math.Round(basePrice * discount));
                item.shopCustomPrice = finalPrice; 
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
            RemnantPlayer modPlayer = Main.LocalPlayer.GetModPlayer<RemnantPlayer>();
            Player player = Main.LocalPlayer;

            if(!modPlayer.PlayerTalkToday.ContainsKey(npc.type)) modPlayer.PlayerTalkToday.TryAdd(npc.type, false);
            
            if (npc.type == NPCID.ArmsDealer) ApplyEffectOnTimePreDay(npc, firstButton, () => { player.AddBuff(BuffID.AmmoBox, Utils1.FormatTimeToTick(Minute: 29)); });
            if(npc.type == NPCID.Angler) ApplyEffectOnTimePreDay(npc,firstButton, () => { player.QuickSpawnItem(Item.GetSource_TownSpawn(), ItemID.CanOfWorms, 1); });       
            base.OnChatButtonClicked(npc, firstButton);
        }

        public void ApplyEffectOnTimePreDay(NPC npc, bool firstButton, Action action)
        {
            RemnantPlayer modPlayer = Main.LocalPlayer.GetModPlayer<RemnantPlayer>();
            Player player = Main.LocalPlayer;
            if (!npc.IsShimmerVariant) return;
            if (modPlayer.PlayerTalkToday[npc.type]) return;
            if (!firstButton) return;
            
            modPlayer.PlayerTalkToday[npc.type] = true;
            action();
        }
        public override void GetChat(NPC npc, ref string chat)
        {
            if(npc.type == NPCID.Clothier)
            {
                Player player = Main.LocalPlayer;
                int style = player.GetModPlayer<StatPlayer>().StyleStat;
                if (style > 0)
                {
                    
                    string text;
                    if (style < 50)
                    {
                        int choice = Main.rand.Next(1, 5);
                        text = Language.GetTextValue($"Mods.RemnantOfTheAncientsMod.Dialogue.Clothier.StyleDialogue{choice}", style, ShopUtils.getStyleDiscountPorcentage(style));
                    }
                    else if(style < (int)ShopUtils.maxStyleStat) text = Language.GetTextValue($"Mods.RemnantOfTheAncientsMod.Dialogue.Clothier.HighStyleDialogue1", style, ShopUtils.getStyleDiscountPorcentage(style));
                    else text = Language.GetTextValue($"Mods.RemnantOfTheAncientsMod.Dialogue.Clothier.MaxStyleDialogue1", style, ShopUtils.getStyleDiscountPorcentage(style));

                    string hex = $"{Color.Pink.R:X2}{Color.Pink.G:X2}{Color.Pink.B:X2}";
                    string finalText = $"\n[c/{hex}:{text}]";
                    chat += finalText;
                }
            }
            base.GetChat(npc, ref chat);
        }
        public override void AI(NPC npc)
        {
            base.AI(npc);
        }
    }
}
