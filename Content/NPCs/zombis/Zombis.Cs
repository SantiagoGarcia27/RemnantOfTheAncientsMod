using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Armor.ReinforcedIron;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Armor.Night;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using CalamityMod;
using Terraria.GameContent.Bestiary;
using RemnantOfTheAncientsMod.Common.Enums;
using Microsoft.Xna.Framework.Graphics;
using System;
using PlayerProxyLib.Common;

namespace RemnantOfTheAncientsMod.Content.NPCs.zombis
{
    public class Fastzombie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new() { Velocity = 1f };
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
            bestiaryEntry.Info.AddRange([			
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				new FlavorTextBestiaryInfoElement("A weak enemy"),
            ]);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot) => Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.Gel);
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3) return SpawnCondition.OverworldNightMonster.Chance * 0f;
            else if (NPC.downedBoss1) return SpawnCondition.OverworldNightMonster.Chance * 0.001f;
            else return SpawnCondition.OverworldNightMonster.Chance * 1.03f;
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
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.2f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class Tinzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.TinHelmet, ItemID.TinChainmail, ItemID.TinGreaves);
            SetStats(DefenseSetBonus: 2);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.2f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class Leadzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.LeadHelmet,ItemID.LeadChainmail,ItemID.LeadGreaves);
            SetStats(DefenseSetBonus: 2);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.1f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class Ironzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.IronHelmet, ItemID.IronChainmail, ItemID.IronGreaves);
            SetStats(DefenseSetBonus: 2);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.1f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class Silverzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.SilverHelmet, ItemID.SilverChainmail, ItemID.SilverGreaves);
            SetStats(DefenseSetBonus: 3);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.05f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class Tungstenzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.TungstenHelmet, ItemID.TungstenChainmail, ItemID.TungstenGreaves);
            SetStats(DefenseSetBonus: 3);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.05f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class Goldzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.GoldHelmet, ItemID.GoldChainmail, ItemID.GoldGreaves);
            SetStats(DefenseSetBonus: 3);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.1f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class Platinumzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.PlatinumHelmet, ItemID.PlatinumChainmail, ItemID.PlatinumGreaves);
            SetStats(DefenseSetBonus: 4);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.1f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }

    public class ReinforcedIronZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DefenseSetBonus: 2, DamageBonus: 1.02f,MovmentSpeedBonus:-50f);
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.1f, GlobalEnum.GameStage.PreBoss);
            return base.SpawnChanceScale(!NPC.downedBoss3, chance);
        }
    }
    public class Shadowzombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.ShadowHelmet, ItemID.ShadowScalemail, ItemID.ShadowGreaves);
            SetStats(CritChance: 15,MovmentSpeedBonus:15);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.PostCorruptBoss);
            return base.SpawnChanceScale(NPC.downedBoss1, chance);
        }
    }

    public class Crimsonzombi : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.CrimsonHelmet, ItemID.CrimsonScalemail, ItemID.CrimsonGreaves);
            SetStats(DamageBonus: 1.09f);
            base.SetDefaults();
        }
        int RegenCounter = 0;
        public override void AI()
        {
            if (NPC.life >= NPC.lifeMax) return; 
            if (RegenCounter == 190) NPC.life++;
            if (RegenCounter++ >= 200) RegenCounter = 0;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.PostCorruptBoss);
            return base.SpawnChanceScale(NPC.downedBoss1, chance);
        }

    }
    public class MoltenZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.MoltenHelmet, ItemID.MoltenBreastplate, ItemID.MoltenGreaves);
            SetStats(DamageBonus: 1.17f, CritChance: 7);
            NPC.buffImmune[BuffID.OnFire] = true;
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.PostCorruptBoss);
            return base.SpawnChanceScale(NPC.downedBoss2, chance);
        }
    }
    public class NightZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus:1.19f, DefenseSetBonus:5,lifeMaxBonus:50,MovmentSpeedBonus:20);
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        { 
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.PostSkeletron);
            return base.SpawnChanceScale(NPC.downedBoss3, chance);
        }
    }
    public class CobaltZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.18f, CritChance: 5,MovmentSpeedBonus: 20);
            SetArmor(ItemID.CobaltHelmet, ItemID.CobaltBreastplate, ItemID.CobaltLeggings);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt;
            if (RemnantOfTheAncientsMod.CalamityMod != null) condition &= CalamityConfigEnabled() && Utils1.MechBossDefeatedTierReworkForClamity() >= 1;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.Hardmode);
            return base.SpawnChanceScale(condition, chance);
        }
    }
    public class PaladiumZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.PalladiumHelmet, ItemID.PalladiumBreastplate, ItemID.PalladiumLeggings);
            SetStats(DamageBonus:1.17f,CritChance:3);
            base.SetDefaults();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            NPC.AddBuff(BuffID.RapidHealing, Utils1.FormatTimeToTick(0, 0, 0, 5));
            base.OnHitPlayer(target, hurtInfo);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && WorldGen.SavedOreTiers.Cobalt == TileID.Palladium;
            if (RemnantOfTheAncientsMod.CalamityMod != null) condition &= CalamityConfigEnabled() && Utils1.MechBossDefeatedTierReworkForClamity() >= 1;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.Hardmode);
            return base.SpawnChanceScale(condition, chance);
        }
    }

    public class MythrilZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.17f, CritChance: 28);
            SetArmor(ItemID.MythrilHelmet, ItemID.MythrilChainmail, ItemID.MythrilGreaves);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {  
            bool condition = Main.hardMode && WorldGen.SavedOreTiers.Mythril == TileID.Mythril;
            if (RemnantOfTheAncientsMod.CalamityMod != null) condition &= CalamityConfigEnabled() && Utils1.MechBossDefeatedTierReworkForClamity() >= 2;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.Hardmode);
            return base.SpawnChanceScale(condition, chance);
        }    
    }
    public class OrichalcumZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus:1.19f,CritChance:6,MovmentSpeedBonus:18);
            SetArmor(ItemID.OrichalcumHelmet, ItemID.OrichalcumBreastplate, ItemID.OrichalcumLeggings);
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum;
            if (RemnantOfTheAncientsMod.CalamityMod != null) condition &= CalamityConfigEnabled() && Utils1.MechBossDefeatedTierReworkForClamity() >= 2;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.Hardmode);
            return base.SpawnChanceScale(condition, chance);
        }
    }
    public class AdamantiteZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.22f, CritChance: 14, MovmentSpeedBonus: 25f);
            SetArmor(ItemID.AdamantiteHelmet, ItemID.AdamantiteBreastplate, ItemID.AdamantiteLeggings);
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && NPC.downedMechBossAny && WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite;
            if (RemnantOfTheAncientsMod.CalamityMod != null) condition &= CalamityConfigEnabled() && Utils1.MechBossDefeatedTierReworkForClamity() >= 3;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.Hardmode);
            return base.SpawnChanceScale(condition, chance);
        }
    }
    public class TitaniumZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus: 1.16f, CritChance: 18, MovmentSpeedBonus: 6);
            SetArmor(ItemID.TitaniumHelmet, ItemID.TitaniumBreastplate, ItemID.TitaniumLeggings);
            base.SetDefaults();
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            Player proxyPlayer = NPC.GetPlayerProxy(sync: false); // Instance the proxy 
            proxyPlayer.name = NPC.FullName;
            proxyPlayer.hasTitaniumStormBuff = true;
            proxyPlayer.AddBuff(BuffID.TitaniumStorm, Utils1.FormatTimeToTick(0, 0, 0, 6));
            NPC.ConfigureProxyPlayer(targetable: false, shouldBeDrawn:true);
            if (proxyPlayer.ownedProjectileCounts[ProjectileID.TitaniumStormShard] < 5)
            {
                var p = Projectile.NewProjectile(Projectile.GetSource_None(), NPC.Center + new Vector2(-1 * 16, -3 * 16), Vector2.Zero, ProjectileID.TitaniumStormShard, 30, 0, proxyPlayer.whoAmI);
                Main.projectile[p].hostile = true;
                Main.projectile[p].friendly = false;
                Main.projectile[p].penetrate = 2;
                Main.projectile[p].timeLeft = 1000;
            }
            base.OnHitPlayer(target, hurtInfo);
        }
        public override void AI()
        {
            NPC.UpdatePlayerProxy(); // Keeps the proxy synchronized with the NPC.
            base.AI();
        }
        
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && NPC.downedMechBossAny && WorldGen.SavedOreTiers.Adamantite == TileID.Titanium;
            if (RemnantOfTheAncientsMod.CalamityMod != null) condition &= CalamityConfigEnabled() && Utils1.MechBossDefeatedTierReworkForClamity() >= 3;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.Hardmode);
            return base.SpawnChanceScale(condition, chance);
        }   
    }
    public class HallowedZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetStats(DamageBonus:1.17f,CritChance:17,MovmentSpeedBonus:10);
            SetArmor(ItemID.HallowedMask, ItemID.HallowedPlateMail, ItemID.HallowedGreaves);
            base.SetDefaults();
        } 
        public override void AI()
        {
            ModContent.GetInstance<RemnantGlobalNPC>().onHitDodge = true;
            base.AI();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            NPC.AddBuff(BuffID.ShadowDodge, Utils1.FormatTimeToTick(0, 0, 0, 30));
            base.OnHitPlayer(target, hurtInfo);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && NPC.downedMechBossAny;
            if(RemnantOfTheAncientsMod.CalamityMod != null) condition &= CalamityConfigEnabled() && Utils1.MechBossDefeatedTierReworkForClamity() >= 4;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.PostMech);
            return base.SpawnChanceScale(condition, chance);
        }
    }

    public class ChlorophyteZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.ChlorophyteMask, ItemID.ChlorophytePlateMail, ItemID.ChlorophyteGreaves);
            SetStats(CritChance: 21, DamageReduction: 5, MovmentSpeedBonus: 5, DamageBonus: 1.21f);  
            base.SetDefaults();
        }
        public override void AI()
        {
            var a = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ProjectileID.CrystalLeaf, 20, 0, Main.myPlayer,ai2: NPC.whoAmI + 1);
            Main.projectile[a].hostile = true;
            base.AI();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Projectile_" + ProjectileID.CrystalLeaf).Value;
            Main.EntitySpriteDraw(texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY) - new Vector2(0, 3) * 16, null, Color.White, NPC.rotation, new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.PostMech);
            return base.SpawnChanceScale(condition, chance);
        }
    }
    public class TurtleZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.TurtleHelmet, ItemID.TurtleScaleMail, ItemID.TurtleLeggings);
            SetStats(DamageReduction: 17,DamageBonus: 1.14f,CritChance:12);
            base.SetDefaults();
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            player.Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), damageDone * 2, hit.HitDirection);
            base.OnHitByItem(player, item, hit, damageDone);
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (!projectile.minion) Main.player[projectile.owner].Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), damageDone / 2, hit.HitDirection);           
            base.OnHitByProjectile(projectile, hit, damageDone);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && NPC.downedPlantBoss;
            float chance = AdjustProgresionChance(0.4f, GlobalEnum.GameStage.PostPlantera, 0.05f);
            return base.SpawnChanceScale(condition,chance);
        }
    }

    public class BeetleZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.BeetleHelmet, ItemID.BeetleShell, ItemID.BeetleLeggings);
            SetStats(DamageBonus: 1.11f, CritChance: 5,MovmentSpeedBonus: 6);
            base.SetDefaults();
        }
        int timmer = 0;
        public override void AI()
        {

            if (timmer < Utils1.FormatTimeToTick(0, 0, 0, 3))
            {
                timmer++;
                return;
            }

            if (!NPC.HasBuff(BuffID.BeetleEndurance1) && !NPC.HasBuff(BuffID.BeetleEndurance2) && !NPC.HasBuff(BuffID.BeetleEndurance3)) NPC.AddBuff(BuffID.BeetleEndurance1, Utils1.FormatTimeToTick(1, 0, 0, 0));
            else if (NPC.HasBuff(BuffID.BeetleEndurance1)) NPC.AddBuff(BuffID.BeetleEndurance2, Utils1.FormatTimeToTick(1, 0, 0, 0));     
            else if (NPC.HasBuff(BuffID.BeetleEndurance2)) NPC.AddBuff(BuffID.BeetleEndurance3, Utils1.FormatTimeToTick(1, 0, 0, 0));  
                
            timmer = 0;
         
            base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && NPC.downedGolemBoss;
            float chance = AdjustProgresionChance(0.3f, GlobalEnum.GameStage.PostGolem, 0.01f);
            return base.SpawnChanceScale(condition, chance);
        }
    }
    public class SolarZombie : BaseZombie
    {
        public override void SetDefaults()
        {
            SetArmor(ItemID.SolarFlareHelmet, ItemID.SolarFlareBreastplate, ItemID.SolarFlareLeggings);
            SetStats(DamageBonus: 1.29f, CritChance: 26, MovmentSpeedBonus: 15,DamageReduction:12);
            base.SetDefaults();
        }
        int timmer = 0;
        int dashTimmer = 0;
        public override void AI()
        {
            Player target = Main.player[NPC.target];
           
            if (timmer >= Utils1.FormatTimeToTick(0, 0, 0, 3))
            {
                if (NPC.HasBuff(BuffID.SolarShield2))
                {
                    NPC.AddBuff(BuffID.SolarShield3, Utils1.FormatTimeToTick(1, 0, 0, 0));
                    RemnantGlobalNPC.DeleteBuff(NPC, BuffID.SolarShield2);
                }
                else if (NPC.HasBuff(BuffID.SolarShield1))
                {
                    NPC.AddBuff(BuffID.SolarShield2, Utils1.FormatTimeToTick(1, 0, 0, 0));
                    RemnantGlobalNPC.DeleteBuff(NPC, BuffID.SolarShield1);
                }
                else if (!NPC.HasBuff(BuffID.SolarShield1) && !NPC.HasBuff(BuffID.SolarShield2) && !NPC.HasBuff(BuffID.SolarShield3))
                    NPC.AddBuff(BuffID.SolarShield1, Utils1.FormatTimeToTick(1, 0, 0, 0));

                timmer = 0;
            }
            else timmer++;


            int DashTimmerMax = Utils1.FormatTimeToTick(0, 0, 0, 3);
            int DashDuration = Utils1.FormatTimeToTick(0, 0, 0, 0.1f);

            if (target == null) return;   
            if (!NPC.HasBuff(BuffID.SolarShield3)) return;
            if (dashTimmer++ < DashTimmerMax) return;
                
            int direction =  Math.Sign(target.Center.X - NPC.Center.X);
 
            NPC.velocity.X = 10f * direction;

            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(20); i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SolarFlare, 0f, 0f, 100, default, 0.5f);
            }
            if (dashTimmer >= DashTimmerMax + DashDuration || target.Distance(NPC.Center) < 100)
            {
                dashTimmer = 0;
            }
                      
            base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool condition = Main.hardMode && NPC.downedMoonlord;
            float chance = 0.6f;
            return base.SpawnChanceScale(condition, chance);
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

           
            NPC.GetGlobalNPC<RemnantGlobalNPC>().CanMakeCrit = CritChance > 0;
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
            if (NPC.velocity.Y == 0 && Math.Sign(NPC.velocity.X) == NPC.direction)
            {
                float velocity = (1 + (MovmentSpeedBonus / 100f));
                NPC.velocity.X *= velocity;
            }
            base.AI();
        }

        public virtual float SpawnChanceScale(bool condition, float chance) => SpawnCondition.OverworldNightMonster.Chance * (condition ? chance : 0);
        

        public virtual float AdjustProgresionChance(float initialChance, GlobalEnum.GameStage stage, float decreasemant = 0.1f)
        {
            if (Utils1.IsGameStage(stage))
            {
                if (stage == GlobalEnum.GameStage.None || stage == GlobalEnum.GameStage.PreHardmode || stage == GlobalEnum.GameStage.Hardmode || stage == GlobalEnum.GameStage.PreBoss || stage == GlobalEnum.GameStage.PostMech)
                {
                    if (stage == GlobalEnum.GameStage.None)
                    {
                        if (Main.hardMode) return 0;
                        else if (NPC.downedBoss2) return initialChance - decreasemant * 3;
                    }
                    else if (stage == GlobalEnum.GameStage.PreHardmode)
                    {
                        if (Main.hardMode) return 0;
                    }
                    else if (stage == GlobalEnum.GameStage.Hardmode)
                    {
                        if (NPC.downedPlantBoss) return 0;
                    }
                    else if (stage == GlobalEnum.GameStage.PreBoss)
                    {
                        if (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedQueenBee || NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3 || NPC.downedPlantBoss || NPC.downedMoonlord)
                            return 0;
                    }
                    else if (stage == GlobalEnum.GameStage.PostMech)
                    {
                        if (NPC.downedGolemBoss) return 0;
                    }
                }
                else
                {
                    int idex = (int)stage - 5;
                    int Count = Enum.GetNames(typeof(GlobalEnum.GameStage)).Length - 5;
                    float multilier = Count - idex;
                    initialChance -= decreasemant * multilier;
                }
            }

            if (initialChance < 0) initialChance = 0;
            return initialChance;
        }
       
        [JITWhenModsEnabled("CalamityMod")]
        public bool CalamityConfigEnabled() => ModContent.GetInstance<CalamityServerConfig>().EarlyHardmodeProgressionRework;
    }
}