using Terraria;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Dusts;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using System;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using static RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Endless_Basic_Potion_Kit;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using CalamityMod.NPCs;
using CalamityMod;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod.Graphics.Renderers;
using Terraria.Graphics;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Strawberry;

namespace RemnantOfTheAncientsMod.Common.Global.NPCs
{
    public class RemnantGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Burn_Sand;
        public bool Hell_Fire;
        public bool Javalina;
        public bool hBurn;
        public bool Marble_Erosion;
        public bool Can_Marble;
        public bool CanMakeCrit;
        public int CritChance;
        public bool CanDrop = true;

        #region shadowDodge 
        public bool shadowDodge;

        public float shadowDodgeCount;
        public int shadowDodgeTimer;
        public bool onHitDodge;
        #endregion

        #region inmune
        public bool immune;
        public bool immuneNoBlink;

        public int immuneTime;

        public int immuneAlphaDirection;

        public int immuneAlpha;

        #endregion

        public bool longInvince;
        public int[] hurtCooldowns = new int[5];

        public override void ResetEffects(NPC NPC)
        {
            Burn_Sand = false;
            Javalina = false;
            Hell_Fire = false;
            hBurn = false;
            Marble_Erosion = false;
        }

        public override void SetDefaults(NPC npc)
        {
            if (npc.type == NPCID.BigMimicCrimson)
            {
                npc.lifeMax *= 2;
                npc.defense += 10;
            }
            if (npc.type == NPCID.BigMimicCorruption)
            {
                npc.lifeMax *= 2;
                npc.defense += 10;
            }

            //if (npc.boss) 
            //{
            //	GetItemForTreasureBag(npc.type);
            //}
        }

        public override void UpdateLifeRegen(NPC NPC, ref int damage)
        {
            if (Burn_Sand)
            {
                if (NPC.lifeRegen > 0)
                {
                    NPC.lifeRegen = 0;
                }
                NPC.lifeRegen -= 16;
                if (damage < 4)
                {
                    damage = 4;
                }
            }
            if (hBurn)
            {
                if (NPC.lifeRegen > 0)
                {
                    NPC.lifeRegen = 0;
                }
                NPC.lifeRegen -= 16;
                if (damage < 4)
                {
                    damage = 4;
                }
            }
            if (Hell_Fire)
            {
                if (NPC.lifeRegen > 0)
                {
                    NPC.lifeRegen = 0;
                }
                NPC.lifeRegen -= 16;
                if (damage < 6)
                {
                    damage = 6;
                }
            }
        }
        int timer = 0;
        public void setDebuffs(NPC NPC)
        {
            int damage = 0;

            if (NPC.HasBuff(BuffID.Electrified))
            {
                if (NPC.lifeRegen > 0)
                {
                    NPC.lifeRegen = 0;
                }
                NPC.lifeRegen -= 16;
                damage = 1;
                if (timer++ % 6 == 0)
                    NPC.SimpleStrikeNPC(damage, Main.player[Main.myPlayer].direction, false, 0f, DamageClass.Generic, false, 0, false);
            }
            if (NPC.HasBuff(BuffID.Stoned))
            {
                NPC.velocity = Vector2.Zero;
            }
            if (NPC.HasBuff(BuffID.TitaniumStorm))
            {
                //NPC.velocity = Vector2.Zero;

            }

        }
        public static bool BigMimicSummonCheck(int x, int y, Player user)
        {
            if (Main.netMode == 1 || !Main.hardMode)
            {
                return false;
            }
            int num = Chest.FindChest(x, y);
            if (num < 0)
            {
                return false;
            }
            int num2 = 0;
            int KeyId = 0;
            int num4 = 0;
            for (int i = 0; i < 40; i++)
            {
                ushort num5 = Main.tile[Main.chest[num].x, Main.chest[num].y].TileType;
                int num6 = Main.tile[Main.chest[num].x, Main.chest[num].y].TileFrameX / 36;
                if (TileID.Sets.BasicChest[num5] && (num5 != 21 || num6 < 5 || num6 > 6) && Main.chest[num].item[i] != null && Main.chest[num].item[i].type > 0)
                {
                    if (Main.chest[num].item[i].type == ItemID.GoldenKey)
                    {
                        num2 += Main.chest[num].item[i].stack;
                        KeyId = ItemID.GoldenKey;
                    }
                    else if (Main.chest[num].item[i].type == ModContent.ItemType<JungleKey>())
                    {
                        num2 += Main.chest[num].item[i].stack;
                        KeyId = ModContent.ItemType<JungleKey>();
                    }
                    //else if (Main.chest[num].item[i].type == 3091)
                    //{
                    //    num3 += Main.chest[num].item[i].stack;
                    //}
                    else
                    {
                        num4++;
                    }
                }
            }
            if (num4 == 0 && num2 == 1)
            {
                //if (num2[0] != 1)
                //{
                //_ = 1;
                // }
                if (TileID.Sets.BasicChest[Main.tile[x, y].TileType])
                {
                    if (Main.tile[x, y].TileFrameX % 36 != 0)
                    {
                        x--;
                    }
                    if (Main.tile[x, y].TileFrameY % 36 != 0)
                    {
                        y--;
                    }
                    int number = Chest.FindChest(x, y);
                    for (int j = 0; j < 40; j++)
                    {
                        Main.chest[num].item[j] = new Item();
                    }
                    Chest.DestroyChest(x, y);
                    for (int k = x; k <= x + 1; k++)
                    {
                        for (int l = y; l <= y + 1; l++)
                        {
                            if (TileID.Sets.BasicChest[Main.tile[k, l].TileType])
                            {
                                Main.tile[k, l].ClearTile();
                            }
                        }
                    }
                    int number2 = 1;
                    if (Main.tile[x, y].TileType == 467)
                    {
                        number2 = 5;
                    }
                    NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, number2, x, y, 0f, number);
                    NetMessage.SendTileSquare(-1, x, y, 3);
                }
                int npcId = GetMimicId(KeyId, user);
                int num8 = NPC.NewNPC(user.GetSource_TileInteraction(x, y), x * 16 + 16, y * 16 + 32, npcId);
                Main.npc[num8].whoAmI = num8;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num8);
                Main.npc[num8].BigMimicSpawnSmoke();
            }
            return false;
        }
        public static int GetMimicId(int KeyId, Player user)
        {
            if (KeyId == ItemID.GoldenKey)
            {
                return user.ZoneSnow ? NPCID.IceMimic : NPCID.Mimic;
            }
            else if (KeyId == ModContent.ItemType<JungleKey>())
            {
                return NPCID.BigMimicJungle;
            }
            return NPCID.Mimic;
        }


        public void GetItemForTreasureBag(int npcId)
        {
            List<IItemDropRule> rulesForNPCID = Main.ItemDropsDB.GetRulesForItemID(npcId);
            List<DropRateInfo> list = new List<DropRateInfo>();
            DropRateInfoChainFeed ratesInfo = new DropRateInfoChainFeed(1f);
            foreach (IItemDropRule item3 in rulesForNPCID)
            {
                item3.ReportDroprates(list, ratesInfo);
            }
        }
        public override void AI(NPC npc)
        {
            UpdateImmunity();
            setDebuffs(npc);

            if (npc.HasBuff(BuffID.ShadowDodge))
            {
                shadowDodge = true;
            }
            base.AI(npc);
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (shadowDodge)
            {
                ShadowDodge(npc);
                modifiers.FinalDamage.Flat = 0;
            }
            base.ModifyHitByItem(npc, player, item, ref modifiers);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (shadowDodge)
            {
                ShadowDodge(npc);
                modifiers.FinalDamage.Flat = 0;
            }
            base.ModifyHitByProjectile(npc, projectile, ref modifiers);
        }
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {

            base.HitEffect(npc, hit);
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (CanMakeCrit)
            {
                if ((int)Main.rand.NextFloat(100) <= CritChance)
                {
                    modifiers.FinalDamage *= 2;
                    //npc.damage *= 2;
                }
            }
        }

        public override void DrawEffects(NPC NPC, ref Color drawColor)
        {
            int type = DustType<PlaceHolder>();
            int torchColor = 0;
            if (Burn_Sand)
            {
                type = DustType<QuemaduraA>();
                torchColor = TorchID.Desert;
            }
            if (Hell_Fire)
            {
                type = DustType<Hell_Fire_P>();
                torchColor = TorchID.Torch;
            }
            if (hBurn)
            {
                type = DustType<HollyBurn_P>();
                torchColor = TorchID.White;
            }

            if (Main.rand.Next(4) < 3 && type != DustType<PlaceHolder>())
            {
                int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, type, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust].noGravity = false;
                    Main.dust[dust].scale *= 0.5f;
                }
                Lighting.AddLight(NPC.position, torchColor);
            }
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (CanDrop)
            {
                base.ModifyNPCLoot(npc, npcLoot);
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.boss)
            {
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    if (Main.netMode != 2 && !Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, npc.Center) < 6400f)
                    {
                        Main.player[Main.myPlayer].AddBuff(ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "BossEffects"), 2);
                    }
                }
            }
            base.OnSpawn(npc, source);
        }
        public override bool PreAI(NPC npc)
        {
            if (npc.boss)
            {
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    if (Main.netMode != 2 && !Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, npc.Center) < 6400f)
                    {
                        Main.player[Main.myPlayer].AddBuff(ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod, "BossEffects"), 2);
                    }
                }
            }
            return base.PreAI(npc);
        }

        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            foreach (Item item in items)
            {
                if (item is not null)
                {
                    decimal discount = Main.LocalPlayer.GetModPlayer<RemnantPlayer>().StyleStat != 0 ? (decimal)(1f - (Main.LocalPlayer.GetModPlayer<RemnantPlayer>().StyleStat / 100f)) : 1;
                    discount = (float)discount > 0.1f ? discount : (decimal)0.1;

                    item.shopCustomPrice = (int?)Math.Round((item.shopCustomPrice ?? item.value) * discount);
                }
            }
        }
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            shop[nextSlot] = ModContent.ItemType<Strawberry_Hairpin>();
            nextSlot++;
            base.SetupTravelShop(shop, ref nextSlot);
        }

        public override void ModifyShop(NPCShop shop)
        {
            if (RemnantOfTheAncientsMod.AlchemistNPCMod != null)
            {
                if (shop.NpcType == ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Brewer"))
                {
                    string SHOP_4 = "MorePotions/Atheria";
                    if (shop.Name == SHOP_4)
                    {
                        shop.Add(new Item(ItemType<Money_Collector_Potion>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 1, 50, 0) })
                        .Add(new Item(ItemType<Commander_Potion>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 2, 50, 0) }, Condition.DownedEowOrBoc);
                    }
                }
                if (shop.NpcType == ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Alchemist"))
                {
                    string BaseShop = "BaseShop";
                    if (shop.Name == BaseShop)
                    {
                        shop.Add(new Item(ItemType<NecroticRestauration>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 50, 0) }, Condition.DownedSkeletron);
                    }
                }
                if (shop.NpcType == ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Musician"))
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
                if (shop.NpcType == ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Tinkerer"))
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
                if (shop.NpcType == ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "YoungBrewer"))
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
                if (shop.NpcType == ExternalModCallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.AlchemistNPCMod, "Operator"))
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
                shop.Add(new Item(ItemType<Terracoin>()) { shopCustomPrice = Utils1.FormatMoney(0, 5, 50, 0, 0) }, Condition.IsNpcShimmered, Condition.Hardmode);
            }
            base.ModifyShop(shop);
        }
        public void SetImmuneTimeForAllTypes(int time)
        {

            immune = true;
            immuneTime = time;
            for (int i = 0; i < hurtCooldowns.Length; i++)
            {
                hurtCooldowns[i] = time;
            }
        }
        public void ShadowDodge(NPC npc)
        {
            SetImmuneTimeForAllTypes(longInvince ? 120 : 80);

            if (npc.HasBuff(BuffID.ShadowDodge))
            {
                npc.RequestBuffRemoval(BuffID.ShadowDodge);
            }

            PutHallowedArmorSetBonusOnCooldown();
        }
        private void PutHallowedArmorSetBonusOnCooldown()
        {
            shadowDodgeTimer = 1800;
        }
        public void UpdateImmunity()
        {
            if (immune)
            {
                immuneTime--;
                if (immuneTime <= 0)
                {
                    immune = false;
                    immuneNoBlink = false;
                }
                if (immuneNoBlink)
                {
                    immuneAlpha = 0;
                }
                else
                {
                    immuneAlpha += immuneAlphaDirection * 50;
                    if (immuneAlpha <= 50)
                    {
                        immuneAlphaDirection = 1;
                    }
                    else if (immuneAlpha >= 205)
                    {
                        immuneAlphaDirection = -1;
                    }
                }
            }
            else
            {
                immuneAlpha = 0;
            }
            for (int i = 0; i < hurtCooldowns.Length; i++)
            {
                if (hurtCooldowns[i] > 0)
                {
                    hurtCooldowns[i]--;
                }
            }
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (shadowDodge)
            {
                shadowDodgeCount += 1f;
                if (shadowDodgeCount > 30f)
                {
                    shadowDodgeCount = 30f;
                }
            }
            else
            {
                shadowDodgeCount -= 1f;
                if (shadowDodgeCount < 0f)
                {
                    shadowDodgeCount = 0f;
                }
            }
            if (shadowDodgeCount > 0f)
            {
                string _texture = npc.type < NPCID.Count ? "Terraria/Images/NPC_" + npc.type : NPCLoader.GetNPC(npc.type).Texture;
                var Texture = ModContent.Request<Texture2D>(_texture);

                Vector2 pos = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);

                SpriteEffects rotation = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                Main.EntitySpriteDraw((Texture2D)Texture, pos - new Vector2(3 * 16, 0), npc.frame, Color.Gray, npc.rotation, Texture.Size() * 0.18f, npc.scale, rotation, 0);
                Main.EntitySpriteDraw((Texture2D)Texture, pos + new Vector2(1.5f * 16f, 0), npc.frame, Color.Gray, npc.rotation, Texture.Size() * 0.18f, npc.scale, rotation, 0);
            }
            base.PostDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (onHitDodge && shadowDodgeTimer == 0)
            {
                npc.AddBuff(BuffID.ShadowDodge, 1800);
            }
            base.OnHitPlayer(npc, target, hurtInfo);
        }
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (immune)
            {
                return false;
            }
            return base.CanBeHitByItem(npc, player, item);
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (immune)
            {
                return false;
            }
            return base.CanBeHitByProjectile(npc, projectile);
        }
        [JITWhenModsEnabled("CalamityMod")]
        public static void SetNpcDamageReductionCalamity(NPC npc, float normal, float revenge, float death, float bossrush, float infernum)
        {
            if (DificultyUtils.InfernumMode)
            {
                npc.GetGlobalNPC<CalamityGlobalNPC>().DR = infernum;
            }
            else npc.DR_NERD(normal, revenge, death, bossrush);
        }
    }
}



	

