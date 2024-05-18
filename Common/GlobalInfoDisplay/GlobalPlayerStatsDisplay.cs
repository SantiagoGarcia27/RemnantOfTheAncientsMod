using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Common.GlobalInfoDisplay
{
    public class HelperClassList 
    {
        public static List<DamageClass> ScaleWithMeleeSpeed =
        [
            DamageClass.Melee,
            DamageClass.SummonMeleeSpeed
        ];
        

        
    }
    public class MaxMinionsDisplay : InfoDisplay
	{
		public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMaxMinion && Main.LocalPlayer.HeldItem.DamageType == DamageClass.Summon;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.maxMinions} minions.";
        }
    }
    public class MaxSentryDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMaxSentry && Main.LocalPlayer.HeldItem.DamageType == DamageClass.Summon;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
           return $"{Main.LocalPlayer.maxTurrets} turrets.";
        }
    }
    public class DamageReductionDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showDamageReduction;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.endurance * 100}% Damage Reduction";
        }
    }
    public class DamageBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showDamageBonus;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        { 
            
            var damageBonus = Main.LocalPlayer.GetDamage(Main.LocalPlayer.HeldItem.DamageType).Multiplicative;
            var GenericDamageBonus = Main.LocalPlayer.GetDamage(DamageClass.Generic).Multiplicative;
            var FinalDamage = (damageBonus * 100) - 100 + (GenericDamageBonus * 100) - 100;
            return $"{FinalDamage}%{Main.LocalPlayer.HeldItem.DamageType.DisplayName} bonus";
        }
    }
    public class CritBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showCritBonus;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            var CritBonus = Main.LocalPlayer.GetCritChance(Main.LocalPlayer.HeldItem.DamageType);
            var GenericCritBonus = Main.LocalPlayer.GetCritChance(DamageClass.Generic);
            var FinalCrit = CritBonus  + GenericCritBonus;
            return $"{FinalCrit}%{Main.LocalPlayer.HeldItem.DamageType.DisplayName} crit chance bonus";
        }
    }
    public class LifeRegenBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showLifeRegen && Main.LocalPlayer.statLife < Main.LocalPlayer.statLifeMax2;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.lifeRegen} Life regen bonus";
        }
    }
    public class ManaRegenBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showLifeRegen && Main.LocalPlayer.statMana < Main.LocalPlayer.statManaMax2;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.manaRegenBonus} Mana regen bonus";
        }
    }
    public class ArmorPenetrationBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showArmorPenetration && Main.LocalPlayer.HeldItem.damage > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            var ApBonus = Main.LocalPlayer.GetTotalArmorPenetration(Main.LocalPlayer.HeldItem.DamageType);
            var GenericAPBonus = Main.LocalPlayer.GetTotalArmorPenetration(DamageClass.Generic);
            var FinalAP = ApBonus + GenericAPBonus;
            return $"{FinalAP} Armor penetration";
        }
    }
    public class MeleeAttackspeedBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMeleeAttackspeed && Main.LocalPlayer.HeldItem.damage > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{(Main.LocalPlayer.GetTotalAttackSpeed(Main.LocalPlayer.HeldItem.DamageType) *100) -100}% Attack speed bonus";
        }
    }
    public class FlyTimeBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showWingsTime && Main.LocalPlayer.equippedWings != null;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.wingTime}s /{Main.LocalPlayer.wingTimeMax}s Wing Time";
        }
    }
    public class MiningSpeedBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMiningSpeedBonus && Main.LocalPlayer.HeldItem.pick > 0 || Main.LocalPlayer.HeldItem.axe > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{100 - Main.LocalPlayer.pickSpeed * 100}% Mining speed bonus";
        }
    }
    public class LuckBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showLuck;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.luck}/{Main.LocalPlayer.luckMaximumCap} Luck";
        }
    }
    public class MovmentSpeedBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMovmentSpeedBonus;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{(Main.LocalPlayer.moveSpeed * 100f) - 100f}% Movment speed bonus";
        }
    }
    public class StyleStatBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showStyleStatBonus;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.GetModPlayer<RemnantPlayer>().StyleStat} Style bonus";
        }
    }
}
