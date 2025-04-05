using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Armor.ReinforcedIron;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Armor.Night;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using CalamityMod;
using Terraria.GameContent.Bestiary;
using RemnantOfTheAncientsMod.Common.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace RemnantOfTheAncientsMod.Content.NPCs.zombis
{
    public class Fastzombie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * 0.5f);
            NPC.defense = 5;
            NPC.lifeMax = ContentSamples.NpcsByNetId[NPCID.Zombie].lifeMax / 2;
            NPC.value = 60f;
            NPC.knockBackResist = 1.2f;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A weak enemy"),
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot) => Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.Gel);
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.001f;
            else return SpawnCondition.OverworldNightMonster.Chance * 0.03f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
    }

    public class Copperzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.CopperHelmet,ItemID.CopperChainmail,ItemID.CopperGreaves);
            SetStats(DefenseSetBonus: 2);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class Tinzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.TinHelmet, ItemID.TinChainmail, ItemID.TinGreaves);
            SetStats(DefenseSetBonus: 2);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class Leadzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.LeadHelmet,ItemID.LeadChainmail,ItemID.LeadGreaves);
            SetStats(DefenseSetBonus: 2);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class Ironzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.IronHelmet, ItemID.IronChainmail, ItemID.IronGreaves);
            SetStats(DefenseSetBonus: 2);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class Silverzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.SilverHelmet, ItemID.SilverChainmail, ItemID.SilverGreaves);
            SetStats(DefenseSetBonus: 3);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class Tungstenzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.TungstenHelmet, ItemID.TungstenChainmail, ItemID.TungstenGreaves);
            SetStats(DefenseSetBonus: 3);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class Goldzombie : BaseZombie
    {

        public override void SetDefaults()
        {
            SetArmor(ItemID.GoldHelmet, ItemID.GoldChainmail, ItemID.GoldGreaves);
            SetStats(DefenseSetBonus: 3);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class Platinumzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.PlatinumHelmet, ItemID.PlatinumChainmail, ItemID.PlatinumGreaves);
            SetStats(DefenseSetBonus: 4);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.3f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }

    public class ReinforcedIronZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DefenseSetBonus: 2, DamageBonus: 1.02f,MovmentSpeedBonus:-50f);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0.2f, 0.1f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<ReinforcedIronOre>());
        }    
        public override void OnSpawn(IEntitySource source)
        {
            int Helmet = ContentSamples.ItemsByType[ModContent.ItemType<ReinforcedIronHelmet>()].defense;
            int Chestplate = ContentSamples.ItemsByType[ModContent.ItemType<ReinforcedIronChesplate>()].defense;
            int Greaves = ContentSamples.ItemsByType[ModContent.ItemType<ReinforcedIronGreaves>()].defense;
            int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
            NPC.defense += Defense;
        }
    }


    //public class Tuxonitezombie : BaseZombie
    //{
    //    static int DefenseSetBonus = 0;
    //    static float AttackBonus = 1.00f;
    //    static int CritBonus = 5;
    //    public override void SetDefaults()
    //    {
    //        base.SetDefaults();
    //        NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * AttackBonus);
           
    //        NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritBonus > 0 ? true : false;
    //        NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritBonus;
    //    }
    //    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //    {
    //        if (Main.hardMode) return SpawnCondition.OverworldNightMonster.Chance * 0f;
    //        else if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
    //        else if (NPC.downedSlimeKing) return SpawnCondition.OverworldNightMonster.Chance * 0.016f;
    //        else return SpawnCondition.OverworldNightMonster.Chance * 0f;
    //    }
    //    public override void OnSpawn(IEntitySource source)
    //    {
    //        int Helmet = ContentSamples.ItemsByType[ModContent.ItemType<TuxoniteHelmet>()].defense;
    //        int Chestplate = ContentSamples.ItemsByType[ModContent.ItemType<Tuxonite_chesplate>()].defense;
    //        int Greaves = ContentSamples.ItemsByType[ModContent.ItemType<Tuxonite_Legging>()].defense;
    //        int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
    //        NPC.defense += Defense;
    //    }

    //}

    public class Shadowzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.ShadowHelmet, ItemID.ShadowScalemail, ItemID.ShadowGreaves);
            SetStats(CritChance: 15,MovmentSpeedBonus:15);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss2, 0f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        } 
    }

    public class Crimsonzombi : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.CrimsonHelmet, ItemID.CrimsonScalemail, ItemID.CrimsonGreaves);
            SetStats(DamageBonus: 1.09f);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss1, 0f, 0.2f, true);
            SetSpawnChance(condition);

            NPC.lifeRegen = 100;
            //NPC.lifeRegenCount = 2000;
            base.SetDefaults();
        }
        int RegenCounter = 0;
        public override void AI()
        {

            if (NPC.life < NPC.lifeMax)
            {
                if (RegenCounter == 190)
                {
                    NPC.life++;
                }
                if (RegenCounter >= 200)
                {
                    RegenCounter = 0;
                }
                else
                {
                    RegenCounter++;
                }
            }
        }
    }
    public class MoltenZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.MoltenHelmet, ItemID.MoltenBreastplate, ItemID.MoltenGreaves);
            SetStats(DamageBonus: 1.17f, CritChance: 7);
            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss2, 0f, 0.1f, true);
            SetSpawnChance(condition);

            NPC.buffImmune[BuffID.OnFire] = true;
            base.SetDefaults();
        }   
    }
    public class NightZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus:1.19f, DefenseSetBonus:5,lifeMaxBonus:50,MovmentSpeedBonus:20);
            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PreHardmode, NPC.downedBoss3, 0f, 0.1f, true);
            SetSpawnChance(condition);

            NPC.buffImmune[BuffID.BrokenArmor] = true;
            NPC.buffImmune[BuffID.Shine] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            base.SetDefaults();
        }        
        public override void OnSpawn(IEntitySource source)
        {
            int Helmet = ContentSamples.ItemsByType[ModContent.ItemType<NightHelmetMe>()].defense;
            int Chestplate = ContentSamples.ItemsByType[ModContent.ItemType<Nightchesplate>()].defense;
            int Greaves = ContentSamples.ItemsByType[ModContent.ItemType<NightLeggings>()].defense;
            int Defense = DefenseSetBonus + Helmet + Chestplate + Greaves;
            NPC.defense += Defense;
        }
    }
    public class CobaltZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.18f, CritChance: 5,MovmentSpeedBonus: 20);
            SetArmor(ItemID.CobaltHelmet, ItemID.CobaltBreastplate, ItemID.CobaltLeggings);
            CustomSpawnCondition condition = new(1, RemnantDownedBossSystem.downedFrozen, 0.16f, 0.2f, true);
            SetSpawnChance(condition);
            NPC.buffImmune[BuffID.OnFire] = true;
            base.SetDefaults();
        }  
    }
    public class PaladiumZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.PalladiumHelmet, ItemID.PalladiumBreastplate, ItemID.PalladiumLeggings);
            SetStats(DamageBonus:1.17f,CritChance:3);
            CustomSpawnCondition condition = new(1, RemnantDownedBossSystem.downedFrozen, 0.16f, 0.2f, true);
            SetSpawnChance(condition);
            NPC.buffImmune[BuffID.OnFire] = true;
            base.SetDefaults();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            NPC.AddBuff(BuffID.RapidHealing, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));
            base.OnHitPlayer(target, hurtInfo);
        }
    }

    public class MythrilZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.17f, CritChance: 28);
            SetArmor(ItemID.MythrilHelmet, ItemID.MythrilChainmail, ItemID.MythrilGreaves);
            CustomSpawnCondition condition = new(2, RemnantDownedBossSystem.downedFrozen , 0.16f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }
    public class OrichalcumZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus:1.19f,CritChance:6,MovmentSpeedBonus:18);
            SetArmor(ItemID.OrichalcumHelmet, ItemID.OrichalcumBreastplate, ItemID.OrichalcumLeggings);
            CustomSpawnCondition condition = new(2, RemnantDownedBossSystem.downedFrozen , 0.16f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            Vector2 pos = target.position + new Vector2(100 * 16 *NPC.direction, Main.rand.Next(-10, 10) * 16);
            int p = Projectile.NewProjectile(Projectile.GetSource_None(),pos, (target.position - pos)*0.005f,ProjectileID.FlowerPetal,NPC.damage/4,0,target.whoAmI);
            Main.projectile[p].hostile = true;
            Main.projectile[p].friendly = false;
            Main.projectile[p].timeLeft *= 2;
            Main.projectile[p].usesLocalNPCImmunity = true;
            Main.projectile[p].localNPCHitCooldown = 2;
            base.OnHitPlayer(target, hurtInfo);
        }
    }
    public class AdamantiteZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.22f, CritChance: 14, MovmentSpeedBonus: 25f);
            SetArmor(ItemID.AdamantiteHelmet, ItemID.AdamantiteBreastplate, ItemID.AdamantiteLeggings);
            CustomSpawnCondition condition = new(3, RemnantDownedBossSystem.downedFrozen, 0.16f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
    }
    public class TitaniumZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.16f, CritChance: 18, MovmentSpeedBonus: 6);
            SetArmor(ItemID.TitaniumHelmet, ItemID.TitaniumBreastplate, ItemID.TitaniumLeggings);
            CustomSpawnCondition condition = new(3, RemnantDownedBossSystem.downedFrozen, 0.16f, 0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            NPC.AddBuff(BuffID.TitaniumStorm, (int)Utils1.FormatTimeToTick(0, 0, 0, 6));
            var p = Projectile.NewProjectile(Projectile.GetSource_None(), NPC.Center + new Vector2 (-1 *16 , -3 * 16), Vector2.Zero, ProjectileID.TitaniumStormShard, 50, 0, NPC.whoAmI);
            Main.projectile[p].hostile = true;
            Main.projectile[p].friendly = false;
            target.AddBuff(BuffID.TitaniumStorm, (int)Utils1.FormatTimeToTick(0, 0, 0, 6));
            base.OnHitPlayer(target, hurtInfo);
        }
    }
    public class HallowedZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus:1.17f,CritChance:17,MovmentSpeedBonus:10);
            SetArmor(ItemID.HallowedMask, ItemID.HallowedPlateMail, ItemID.HallowedGreaves);

            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PostMech,0.2f, true);
            SetSpawnChance(condition);
            base.SetDefaults();
        } 
        public override void AI()
        {
            ModContent.GetInstance<RemnantGlobalNPC>().onHitDodge = true;
            base.AI();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            NPC.AddBuff(BuffID.ShadowDodge, (int)Utils1.FormatTimeToTick(0, 0, 0, 30));
          
            base.OnHitPlayer(target, hurtInfo);
        }
    }

    public class ChlorophyteZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.ChlorophyteMask, ItemID.ChlorophytePlateMail, ItemID.ChlorophyteGreaves);
            SetStats(CritChance: 21, DamageReduction: 5, MovmentSpeedBonus: 5, DamageBonus: 1.21f);  
            CustomSpawnCondition condition = new(GlobalEnum.gameStage.PostPlantera, 0.2f,true);
            SetSpawnChance(condition);
            base.SetDefaults();
        }
        public override void AI()
        {
            var a = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center /*- new Vector2(0, 4)*16*/, Vector2.Zero, ProjectileID.CrystalLeaf, 20, 0, Main.myPlayer,0,0, NPC.whoAmI);
            Main.projectile[a].hostile = true;
            Main.projectile[a].hostile = false;
            base.AI();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Projectile_" + ProjectileID.CrystalLeaf).Value;

            Main.EntitySpriteDraw(texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY) - new Vector2(0, 3) * 16, null, Color.White, NPC.rotation, new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);

            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
    } 


    public class CustomSpawnCondition
    {
        public GlobalEnum.gameStage GameStage = GlobalEnum.gameStage.None;
        public bool Condition = false;
        public float ChanceDefault = 0f;
        public float ChancePostCondition = 0f;
        public int HardmodeOreZombieLevel = 0;
        public bool IsHardmode = false;



        public CustomSpawnCondition(GlobalEnum.gameStage gameState, float chance, bool isHardmode = false)
        {
            GameStage = gameState;
            ChancePostCondition = chance;
            ChanceDefault = 0;
            IsHardmode = isHardmode;
        }
        public CustomSpawnCondition(bool condition, float chance, float chancePostCondition, bool isHardmode = false)
        {
            this.Condition = condition;
            ChanceDefault = chance;
            ChancePostCondition = chancePostCondition;
            IsHardmode = isHardmode;
        }
        public CustomSpawnCondition(GlobalEnum.gameStage gameState,bool condition, float chance, float chancePostCondition, bool isHardmode = false)
        {
            GameStage = gameState;
            this.Condition = condition;
            ChanceDefault = chance;
            ChancePostCondition = chancePostCondition;
            IsHardmode = isHardmode;
        }
        public CustomSpawnCondition(int isHardmodeOreZombie,bool condition, float chance, float chancePostCondition, bool isHardmode = false)
        {
            HardmodeOreZombieLevel = isHardmodeOreZombie;
            ChanceDefault = chance;
            ChancePostCondition = chancePostCondition;
            Condition = condition;
            IsHardmode = isHardmode;
        }
    }
    public abstract class BaseZombie : ModNPC
    {
        Item Helmet = null;
        Item Chestplate = null;
        Item Greaves = null;
         

        protected int DefenseSetBonus = 0;
        protected float DamageBonus = 1;
        protected int CritChance = 0;
        protected float DamageReduction = 0;
        protected float MovmentSpeedBonus = 1;
        protected int LifeBonus = 0;

        protected CustomSpawnCondition SpawnConditions = new CustomSpawnCondition(GlobalEnum.gameStage.None, 0f);


        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {  
                Velocity = 1f 
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public virtual void SetArmor(int helmet, int chestplate,int greaves, bool isHardmode = false) 
        {
            Helmet = new Item (helmet);
            Chestplate = new Item(chestplate);
            Greaves = new Item(greaves);
        }
        public virtual void SetStats(int DefenseSetBonus = 0, float DamageBonus = 1, int CritChance = 0, float DamageReduction = 0, float MovmentSpeedBonus = 1, int lifeBonus = 1,int lifeMaxBonus = 0)
        {
            NPC zombieBase = ContentSamples.NpcsByNetId[NPCID.Zombie];
            this.DefenseSetBonus = DefenseSetBonus;
            this.DamageBonus = DamageBonus;
            this.CritChance = CritChance;
            this.DamageReduction = DamageReduction;
            this.MovmentSpeedBonus = MovmentSpeedBonus;
            this.LifeBonus = (zombieBase.lifeMax * lifeBonus) + lifeMaxBonus;
        }
        public void SetSpawnChance(CustomSpawnCondition condition) 
        { 
            SpawnConditions = condition; 
            SpawnConditions.ChanceDefault = SpawnCondition.OverworldNightMonster.Chance * condition.ChanceDefault;
            SpawnConditions.ChancePostCondition = SpawnCondition.OverworldNightMonster.Chance * condition.ChancePostCondition;
        }

        public override void SetDefaults()
        {
            NPC zombieBase = ContentSamples.NpcsByNetId[NPCID.Zombie];

            NPC.CloneDefaults(NPCID.Zombie);
            NPC.lifeMax = LifeBonus;
            NPC.damage = (int)(ContentSamples.NpcsByNetId[NPCID.Zombie].damage * DamageBonus);
            NPC.value += 60f;
            NPC.defense = zombieBase.defense + DefenseSetBonus;
            if (Helmet != null)
                NPC.defense += Helmet.defense + Chestplate.defense + Greaves.defense;
            NPC.knockBackResist = 0;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);

           
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritChance > 0 ? true : false;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CritChance = CritChance;
            NPC.GetGlobalNPC<RemnantGlobalNPC>().DamageReduction = DamageReduction;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("A heavily armored enemy"),
            ]);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(139, 143));
                dust.velocity = new Vector2(Main.rand.NextFloat(-50f, 51f) * 1110.01f, Main.rand.NextFloat(-50f, 51f) * 111110.01f);
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Vector2 targetPosition = Main.player[NPC.target].position;
            if (NPC.velocity.Y == 0)
            {
                NPC.velocity.X *=  (1+ (MovmentSpeedBonus / 100f))/* * NPC.direction*/;
            }
            base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(SpawnConditions.GameStage == GlobalEnum.gameStage.None)
            {
                if(SpawnConditions.Condition)
                {
                    return SpawnConditions.ChancePostCondition;
                }
                return SpawnConditions.ChanceDefault;
            }
            else
            {
                if(SpawnConditions.HardmodeOreZombieLevel > 0)
                {
                    if (Main.hardMode)
                    {
                        if (RemnantOfTheAncientsMod.CalamityMod != null )
                        {
                            if (CalamityConfigEnabled())
                            {
                                if (Utils1.MechBossDefeatedTierReworkForClamity() >= SpawnConditions.HardmodeOreZombieLevel)
                                    return SpawnConditions.ChancePostCondition;
                            }
                        }
                        else
                        {
                            if (SpawnConditions.Condition)
                            {
                                return SpawnConditions.ChancePostCondition;
                            }
                            return SpawnConditions.ChanceDefault;
                        }
                    }
                }
                else
                {
                    if(Utils1.IsGameStage(SpawnConditions.GameStage))
                    {
                        if (SpawnConditions.Condition)
                        {
                            return SpawnConditions.ChancePostCondition;
                        }
                        return SpawnConditions.ChanceDefault;
                    }
                }
            }      
            return 0;
        }
        [JITWhenModsEnabled("CalamityMod")]
        bool CalamityConfigEnabled() => ModContent.GetInstance<CalamityConfig>().EarlyHardmodeProgressionRework;
    }
}