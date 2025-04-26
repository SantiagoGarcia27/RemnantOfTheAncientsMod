using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using SangarUtilities.Common;
using Terraria.ID;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{

    class GetVanillaMod
    {
        public static Mod Get(string name)
        {
            ModLoader.TryGetMod(name, out Mod Terraria);
            return Terraria;
        }
    }

    public class KingSlimeSoul : SoulModel
    {
        int SpeedBoost = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBoost);

        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.KingSlime];
       
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");


    }
    public class EyeOfCthulhuSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.EyeofCthulhu];
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class CorruptSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.BrainofCthulhu];
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class QueenBeeSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.QueenBee];
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class SkeletonSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.SkeletronHead];

       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class DeerclopsSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.Deerclops]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class DesertAnnihilatorSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[ModContent.NPCType<DesertAniquilator>()]; 
       public override Mod ItemMod => GetVanillaMod.Get("RemnantOfTheAncientsMod");
    }
   
    public class WallOfFleshSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.WallofFlesh]; 
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class FrozenAssaulterSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[ModContent.NPCType<FrozenAssaulter>()]; 
       public override Mod ItemMod => GetVanillaMod.Get("RemnantOfTheAncientsMod");
    }
    public class QueenSlimeSoul : SoulModel
    {
        int LifeBoost = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.QueenSlimeBoss];
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class SpazmatismEnergyCell : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.Spazmatism]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class RetinazorEnergyCell : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.Retinazer];
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class SkeletronPrimeEnergyCell : SoulModel
    {
        int DefenseBoost = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DefenseBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.SkeletronPrime];
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class DestroyerEnergyCell : SoulModel
    {
        int MiningBoost = 35;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.TheDestroyer];
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class PlanteraSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.Plantera]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class InfernalTyrantSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[ModContent.NPCType<InfernalTyrantHead>()];
       public override Mod ItemMod => GetVanillaMod.Get("RemnantOfTheAncientsMod");
    }
    public class GolemSoul : SoulModel
    {
        int DefenseBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DefenseBoost);
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.Golem]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class EmpressOfLightSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.HallowBoss]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class DukeFishronSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.DukeFishron]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class LunaticCultistSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.CultistBoss]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
    public class MoonLordSoul : SoulModel
    {
        public override NPC BaseNpc => ContentSamples.NpcsByNetId[NPCID.MoonLordCore]; 
       public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
    }
}

