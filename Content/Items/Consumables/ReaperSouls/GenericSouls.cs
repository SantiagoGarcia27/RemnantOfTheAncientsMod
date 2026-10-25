using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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

    public class CrusherSoul : SoulModel
    {
        int MiningBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningBoost);
          public override int NpcType => ModContent.NPCType<GemstoneCrusher>();
        public override Mod ItemMod => GetVanillaMod.Get("RemnantOfTheAncientsMod");
        public override float Progression => 0.2f;
    }
    public class KingSlimeSoul : SoulModel
    {
        int SpeedBoost = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBoost);
          public override int NpcType => NPCID.KingSlime;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 1f;
    }
    public class EyeOfCthulhuSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
          public override int NpcType => NPCID.EyeofCthulhu;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 2f;
    }
    public class CorruptSoul : SoulModel
    {
          public override int NpcType => NPCID.BrainofCthulhu;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 3f;
    }
    public class QueenBeeSoul : SoulModel
    {
          public override int NpcType => NPCID.QueenBee;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 4f;
    }
    public class SkeletonSoul : SoulModel
    {
        int DefenseBoost = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DefenseBoost);
          public override int NpcType => NPCID.SkeletronHead;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 5f;
    }
    public class DeerclopsSoul : SoulModel
    {
          public override int NpcType => NPCID.Deerclops;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 6f;
    }
    public class DesertAnnihilatorSoul : SoulModel
    {
          public override int NpcType => ModContent.NPCType<DesertAnnihilator>();
        public override Mod ItemMod => GetVanillaMod.Get("RemnantOfTheAncientsMod");
        public override float Progression => 6.7f;
    }

    public class WallOfFleshSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
          public override int NpcType => NPCID.WallofFlesh;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 7f;
    }
    public class FrozenAssaulterSoul : SoulModel
    {
          public override int NpcType => ModContent.NPCType<FrozenAssaulter>();
        public override Mod ItemMod => GetVanillaMod.Get("RemnantOfTheAncientsMod");
        public override float Progression => 7.2f;
    }
    public class QueenSlimeSoul : SoulModel
    {
        int LifeBoost = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
          public override int NpcType => NPCID.QueenSlimeBoss;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 8f;
    }
    public class SpazmatismEnergyCell : SoulModel
    {
        public override int NpcType => NPCID.Spazmatism;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 9f;

    }
    public class RetinazorEnergyCell : SoulModel
    {
        public override int NpcType => NPCID.Retinazer;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 9f;
    }
    public class SkeletronPrimeEnergyCell : SoulModel
    {
        int DefenseBoost = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DefenseBoost);
        public override int NpcType => NPCID.SkeletronPrime;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 11f;
    }
    public class DestroyerEnergyCell : SoulModel
    {
        int MiningBoost = 35;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningBoost);
          public override int NpcType => NPCID.TheDestroyer;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 10f;
    }
    public class PlanteraSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override int NpcType => NPCID.Plantera;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 12f;
    }
    public class InfernalTyrantSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
        public override int NpcType => ModContent.NPCType<InfernalTyrantHead>();
        public override Mod ItemMod => GetVanillaMod.Get("RemnantOfTheAncientsMod");
        public override float Progression => 13f;
    }
    public class GolemSoul : SoulModel
    {
        int DefenseBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DefenseBoost);
        public override int NpcType => NPCID.Golem;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 13f;
    }
    public class EmpressOfLightSoul : SoulModel
    {
        public override int NpcType => NPCID.HallowBoss;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 15f;
    }
    public class DukeFishronSoul : SoulModel
    {
        public override int NpcType => NPCID.DukeFishron;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 14f;
    }
    public class LunaticCultistSoul : SoulModel
    {
        public override int NpcType => NPCID.CultistBoss;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 17f;
    }
    public class MoonLordSoul : SoulModel
    {
        public override int NpcType => NPCID.MoonLordCore;
        public override Mod ItemMod => GetVanillaMod.Get("SangarUtilities");
        public override float Progression => 18f;
    }
}

