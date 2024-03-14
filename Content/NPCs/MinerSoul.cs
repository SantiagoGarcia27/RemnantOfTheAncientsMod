using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;
using System.Linq;
using Terraria.Utilities;
using RemnantOfTheAncientsMod.Content.Items.Tools;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.GameContent.Bestiary;
using Humanizer;
using Microsoft.CodeAnalysis.Operations;

namespace RemnantOfTheAncientsMod.Content.NPCs
{ 
    public class MinerSoul : Hover
    {
        public const string ShopName = "Shop";
        public MinerSoul()
        {
            acceleration = 0.06f;
            accelerationY = 0.025f;
        }

        public override void SetStaticDefaults()
        {         

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Wraith];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Wraith);
            NPC.width = 28;
            NPC.height = 36;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.friendly = true;
            NPC.rarity = 4;
            NPC.dontTakeDamageFromHostiles = true;
            AnimationType = NPCID.Wraith;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {    
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

                // Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A wandering soul looking to leave its regrets behind"),
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Cavern.Chance * 0.09f;//0.0009
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return false;
        }

        // Same as the above but with projectiles.
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return projectile.friendly && projectile.owner < 255;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int i = 0; i < hit.Damage / NPC.lifeMax * 100; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Ghost, hit.HitDirection, -1f, 100, new Color(100, 100, 100, 100), 1f);
                    dust.noGravity = true;
                }
                return;
            }
            for (int i = 0; i < 50; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Ghost, 2 * hit.HitDirection, -2f, 100, new Color(100, 100, 100, 100), 1f);
                dust.noGravity = true;
            }
        }
        public override bool CanChat()
        {
            return true;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.CommonDialogue1"));
            chat.Add(Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.CommonDialogue2"));

            Main.LocalPlayer.currentShoppingSettings.HappinessReport = "";
            return chat;
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            Player player = Main.LocalPlayer;
            if (firstButton)
            {
                shop = ShopName;
            }
            else
            {
                if (player.HasItem(ItemID.Terragrim) && Main.hardMode)
                {
                    UpgradeItem(player, ItemID.Terragrim, 1, ItemID.Arkhalis, 1, ItemID.GoldCoin, 100);
                }
                else if (player.HasItem(ItemID.EnchantedSword))
                {
                    UpgradeItem(player, ItemID.EnchantedSword, 1, ItemID.Terragrim, 1, ItemID.GoldCoin, 50);
                }
                else if (player.inventory.Any(item => !item.IsAir && item.pick < 70 && item.pick > 0))
                {
                    Item item = player.inventory.FirstOrDefault(item => !item.IsAir && item.pick < 70 && item.pick > 0);
                    Main.npcChatText = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.ThanksDialogue1");
                    int IronPickaxeItemIndex = player.FindItem(item.type);
                    player.inventory[IronPickaxeItemIndex].TurnToAir();
                    player.QuickSpawnItem(NPC.GetSource_Loot(), ItemType<Ghost_Pickaxe>());
                    player.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoldCoin, 10);
                    player.ApplyDamageToNPC(NPC, Main.DamageVar(500), 5f, player.direction, true);
                    return;
                }
            }
        }
        public void UpgradeItem(Player player, int input, int inputCount, int output, int outputCount, int money, int moneyCount)
        {
            Main.npcChatText = Main.npcChatText = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Dialogue.MinerSoul.ThanksDialogue2");
            int ItemIndex = player.FindItem(input);
            player.inventory[ItemIndex].TurnToAir();
            player.QuickSpawnItem(NPC.GetSource_Loot(), output, outputCount);
            player.QuickSpawnItem(NPC.GetSource_Loot(), money, moneyCount);
            player.ApplyDamageToNPC(NPC, Main.DamageVar(500), 5f, player.direction, true);
            return;
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");

            if (Main.LocalPlayer.inventory.Any(item => !item.IsAir && item.pick < 70 && item.pick > 0))
            {
                button2 = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.Buttoms.DeliverPickaxe");
            }
            if (Main.LocalPlayer.HasItem(ItemID.EnchantedSword))
            {
                Item item = new(ItemID.EnchantedSword);
                button2 = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.Buttoms.UpgradeItem", item.Name);
            }

        }
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, ShopName);

            npcShop.Add(new Item(ItemType<ironstonepickaxe>()) { shopCustomPrice = Utils1.FormatMoney(0, 0, 3, 0, 0) })
            .Add(new Item(ItemID.Torch) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 30, 0) })
            .Add(new Item(ItemID.Wood) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 20, 0) })
            .Add(new Item(ItemID.DirtBlock) { shopCustomPrice = Item.buyPrice(0, 0, 0, 2) })
            .Add(new Item(ItemID.StoneBlock) { shopCustomPrice = Item.buyPrice(0, 0, 0, 5) })
            .Add(new Item(ItemID.CopperBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 2, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationTin", () => WorldGen.SavedOreTiers.Copper == TileID.Tin))
            .Add(new Item(ItemID.TinBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 2, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationCopper", () => WorldGen.SavedOreTiers.Copper == TileID.Copper))
            .Add(new Item(ItemID.IronBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 5, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationLead", () => WorldGen.SavedOreTiers.Iron == TileID.Lead))
            .Add(new Item(ItemID.LeadBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 5, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationIron", () => WorldGen.SavedOreTiers.Iron == TileID.Iron))
            .Add(new Item(ItemID.TungstenBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 25, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationSilver", () => WorldGen.SavedOreTiers.Silver == TileID.Silver))
            .Add(new Item(ItemID.SilverBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 25, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationTungsten", () => WorldGen.SavedOreTiers.Silver == TileID.Tungsten))
            .Add(new Item(ItemID.GoldBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 60, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationPlatinum", () => WorldGen.SavedOreTiers.Gold == TileID.Platinum))
            .Add(new Item(ItemID.PlatinumBar) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 70, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationGold", () => WorldGen.SavedOreTiers.Gold == TileID.Gold))
            .Add(new Item(ItemID.PinkGel) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 50, 0) }, Condition.Hardmode)
            .Add(new Item(ItemID.CobaltOre) { shopCustomPrice = Utils1.FormatMoney(0, 0, 1, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationPalladium", () => WorldGen.SavedOreTiers.Cobalt == TileID.Palladium))
            .Add(new Item(ItemID.PalladiumOre) { shopCustomPrice = Utils1.FormatMoney(0, 0, 1, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationCobalt", () => WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt))
            .Add(new Item(ItemID.MythrilOre) { shopCustomPrice = Utils1.FormatMoney(0, 0, 6, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationOrichalcum", () => WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum))
            .Add(new Item(ItemID.OrichalcumOre) { shopCustomPrice = Utils1.FormatMoney(0, 0, 6, 0, 0) }, new Condition("Mods.RemnantOfTheAncientsMod.Conditions.OpositeOreGenerationMythril", () => WorldGen.SavedOreTiers.Mythril == TileID.Mythril))
            .Add(new Item(ItemID.Amethyst) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 5, 0) },Condition.DownedKingSlime)
            .Add(new Item(ItemID.Topaz) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 5, 0) }, Condition.DownedKingSlime)
            .Add(new Item(ItemID.Emerald) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 10, 0) }, Condition.DownedKingSlime)
            .Add(new Item(ItemID.Sapphire) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 10, 0) }, Condition.DownedKingSlime)
            .Add(new Item(ItemID.Ruby) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 15, 0) }, Condition.DownedKingSlime)
            .Add(new Item(ItemID.Diamond) { shopCustomPrice = Utils1.FormatMoney(0, 0, 0, 20, 0) }, Condition.DownedKingSlime)
            .Register();
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            if (distance <= 200)
            {
                if (distance > 100)
                {
                    // Make the health bar become smaller the farther away the NPC is.
                    scale *= (100 - (distance - 100)) / 100;
                }
                return null;
            }
            return false;
        }

        // Make the NPC invisible when far away from the player.
        public override void CustomBehavior(ref float ai)
        {
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            if (distance <= 250)
            {
                NPC.alpha = 100;
                if (distance > 100)
                {
                    // Make the NPC fade out the farther away the NPC is.
                    NPC.alpha += (int)(155 * ((distance - 100) / 150));
                }
                return;
            }
            NPC.alpha = 255;
        }

        // Make the NPC stop moving if it is close to the player.
        public override bool ShouldMove(float ai)
        {
            NPC.ai[2] = 0; // Prevents the NPC from stopping following their target.
            if (NPC.Distance(Main.player[NPC.target].Center) < 150f)
            {
                NPC.velocity *= 0.95f;
                if (Math.Abs(NPC.velocity.X) < 0.1f)
                {
                    NPC.spriteDirection = Main.player[NPC.target].Center.X > NPC.Center.X ? 1 : -1;
                    NPC.velocity.X = 0;
                }
                return false;
            }
            return true;
        }
    }

    public class PurificationPowder : GlobalProjectile
    {
        // Make purification powder transform wraiths into purified ghosts.
        public override void PostAI(Projectile projectile)
        {
            if (projectile.type != ProjectileID.PurificationPowder || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == NPCID.Wraith || Main.npc[i].type == NPCID.Ghost) && projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
                {
                    Main.npc[i].Transform(NPCType<MinerSoul>());
                }
            }
        }
    }
}