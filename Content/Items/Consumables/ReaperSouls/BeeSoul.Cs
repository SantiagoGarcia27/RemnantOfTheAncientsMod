using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{
    public class KingSoul : SoulModel
    {
        int SpeedBoost = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBoost);
        public override int SoulID => 0;
    }
    public class EyeSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override int SoulID => 1;
    }
    public class CorruptSoul : SoulModel
    {
        public override int SoulID => 2;
    }
    public class BeeSoul : SoulModel
    {
        public override int SoulID => 3;
    }
    public class SkeletonSoul : SoulModel
    {
        public override int SoulID => 4;
    }
    public class DeerclopsSoul : SoulModel
    {
        public override int SoulID => 5;
    }
    public class DesertSoul : SoulModel
    {
        public override int SoulID => 6;
    }
   
    public class FleshSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
        public override int SoulID => 7;
    }
    public class FrozenSoul : SoulModel
    {
        public override int SoulID => 8;
    }
    public class QueenSoul : SoulModel
    {
        int LifeBoost = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override int SoulID => 9;
    }
    public class SpazmatismEnergyCell : SoulModel
    {
        public override int SoulID => 10;
    }
    public class RetinazorEnergyCell : SoulModel
    {
        public override int SoulID => 11;
    }
    public class SkeletronPrimeEnergyCell : SoulModel
    {
        public override int SoulID => 12;
    }
    public class DestroyerEnergyCell : SoulModel
    {
        int MiningBoost = 35;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningBoost);
        public override int SoulID => 13;
    }
    public class PlantSoul : SoulModel
    {
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);
        public override int SoulID => 14;
    }
    public class InfernalSoul : SoulModel
    {
        int DamageBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
        public override int SoulID => 15;
    }
    public class GolemSoul : SoulModel
    {
        public override int SoulID => 16;
    }
    public class EmpressSoul : SoulModel
    {
        public override int SoulID => 17;
    }
    public class FishSoul : SoulModel
    {
        public override int SoulID => 18;
    }
    public class CultistSoul : SoulModel
    {
        public override int SoulID => 19;
    }
    public class MoonSoul : SoulModel
    {
        public override int SoulID => 20;
    }
}

