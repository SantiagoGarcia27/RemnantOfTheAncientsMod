using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{
    public class KingSoul : SoulModel
    {
        int SpeedBoost = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBoost);
        public override int SoulID => BossID.VanillaID.KingSlime;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class EyeSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override int SoulID => BossID.VanillaID.EyeOfChutulu;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class CorruptSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.CorruptBoss;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class BeeSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.QueenBee;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class SkeletonSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.Skeletron;

        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class DeerclopsSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.Deerclops; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class DesertSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.DesertAnhilator; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
   
    public class FleshSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
        public override int SoulID => BossID.VanillaID.WallOfFlesh; 
        public override string ItemMod => "RemnantOfTheAncientsMod";


    }
    public class FrozenSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.FrozenAssaulter; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class QueenSoul : SoulModel
    {
        int LifeBoost = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override int SoulID => BossID.VanillaID.QueenSlime;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class SpazmatismEnergyCell : SoulModel
    {
        public override int SoulID => BossID.VanillaID.Spazmatism; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class RetinazorEnergyCell : SoulModel
    {
        public override int SoulID => BossID.VanillaID.Retinazor;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class SkeletronPrimeEnergyCell : SoulModel
    {
        public override int SoulID => BossID.VanillaID.SkeletronPrime;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class DestroyerEnergyCell : SoulModel
    {
        int MiningBoost = 35;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningBoost);
        public override int SoulID => BossID.VanillaID.Destroyer;
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class PlantSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override int SoulID => BossID.VanillaID.Plantera; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class InfernalSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
        public override int SoulID => BossID.VanillaID.InfernalTyrant; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class GolemSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.Golem; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class EmpressSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.EmpressOfLight; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class FishSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.DukeFishron; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class CultistSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.Cultist; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
    public class MoonSoul : SoulModel
    {
        public override int SoulID => BossID.VanillaID.MoonLord; 
        public override string ItemMod => "RemnantOfTheAncientsMod";
    }
}

