using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Common.RemPlayer;

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
            
            return $"{Main.LocalPlayer.numMinions}/{Main.LocalPlayer.maxMinions} minions.";
        }
    }
    public class MaxSentryDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMaxSentry && Main.LocalPlayer.HeldItem.DamageType == DamageClass.Summon;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
           return $"{Main.LocalPlayer.GetModPlayer<RemnantPlayer>().numTurrets}/{Main.LocalPlayer.maxTurrets} turrets.";
        }
    }
    public class DamageReductionDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showDamageReduction && Main.LocalPlayer.endurance > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {      
            return $"{Main.LocalPlayer.endurance * 100}% Damage Reduction";
        }
    }
    public class DamageBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showDamageBonus && GetFinalDamage() > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        { 
            return $"{GetFinalDamage()}%{Main.LocalPlayer.HeldItem.DamageType.DisplayName} bonus";
        }
        public float GetFinalDamage()
        {
            var damageBonus = Main.LocalPlayer.GetDamage(Main.LocalPlayer.HeldItem.DamageType).Multiplicative;
            var GenericDamageBonus = Main.LocalPlayer.GetDamage(DamageClass.Generic).Multiplicative;
            var FinalDamage = (damageBonus * 100) - 100 + (GenericDamageBonus * 100) - 100;
            return FinalDamage;
        }
    }
    public class CritBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showCritBonus && GetFinalCrit() > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            return $"{GetFinalCrit()}%{Main.LocalPlayer.HeldItem.DamageType.DisplayName} crit chance bonus";
        }
        public float GetFinalCrit()
        {
            var CritBonus = Main.LocalPlayer.GetCritChance(Main.LocalPlayer.HeldItem.DamageType);
            var GenericCritBonus = Main.LocalPlayer.GetCritChance(DamageClass.Generic);
            var FinalCrit = CritBonus + GenericCritBonus;
            return FinalCrit;
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
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showArmorPenetration && Main.LocalPlayer.HeldItem.damage > 0 && GetFinalArmorPenetration() > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        { 
            return $"{GetFinalArmorPenetration()} Armor penetration";
        }
        public float GetFinalArmorPenetration()
        {
            var ApBonus = Main.LocalPlayer.GetTotalArmorPenetration(Main.LocalPlayer.HeldItem.DamageType);
            var GenericAPBonus = Main.LocalPlayer.GetTotalArmorPenetration(DamageClass.Generic);
            var FinalAP = ApBonus + GenericAPBonus;
            return FinalAP;
        }
    }
    public class MeleeAttackspeedBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMeleeAttackspeed && Main.LocalPlayer.HeldItem.damage > 0 && GetFinalMeleeAttackspeed() > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {  
            return $"{GetFinalMeleeAttackspeed()}% Attack speed bonus";
        }
        public float GetFinalMeleeAttackspeed()
        {
            return (Main.LocalPlayer.GetTotalAttackSpeed(Main.LocalPlayer.HeldItem.DamageType) * 100) -100;
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
            float luck = Main.LocalPlayer.luck;
            return $"{luck}/{Main.LocalPlayer.luckMaximumCap} Luck";
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
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showStyleStatBonus && Main.LocalPlayer.GetModPlayer<StatPlayer>().StyleStat > 0;
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            
            return $"{Main.LocalPlayer.GetModPlayer<StatPlayer>().StyleStat}/102 Style bonus";
        }
    }
    public class PierceChanceBonusDisplay : InfoDisplay
    {
        public override bool Active()
        {
            Player player = Main.LocalPlayer;
            return player.GetModPlayer<InfoDisplayPlayer>().showPierceChanceStatBonus && player.GetModPlayer<StatPlayer>().GetProjectilePenetrationChance(player.HeldItem.DamageType) > 0;
        }
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            Player player = Main.LocalPlayer;
            //var CritBonus = Main.LocalPlayer.GetCritChance(Main.LocalPlayer.HeldItem.DamageType);
            var GenericPierceChanceBonus = player.GetModPlayer<StatPlayer>().GetProjectilePenetrationChance(player.HeldItem.DamageType);
           // var FinalCrit = CritBonus + GenericCritBonus;
            return $"{GenericPierceChanceBonus}%{Main.LocalPlayer.HeldItem.DamageType.DisplayName} pierce strike chance bonus";
        }
    }
    public class PiercePowerBonusDisplay : InfoDisplay
    {
   
        public override bool Active()
        {
            Player player = Main.LocalPlayer;
            return player.GetModPlayer<InfoDisplayPlayer>().showPiercePowerStatBonus && player.GetModPlayer<StatPlayer>().GetProjectilePenetration(player.HeldItem.DamageType) > 1;
        }
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            Player player = Main.LocalPlayer;
            //var CritBonus = Main.LocalPlayer.GetCritChance(Main.LocalPlayer.HeldItem.DamageType);
            var GenericPiercePowerBonus = player.GetModPlayer<StatPlayer>().GetProjectilePenetration(player.HeldItem.DamageType);
            // var FinalCrit = CritBonus + GenericCritBonus;
            return $"{GenericPiercePowerBonus} {player.HeldItem.DamageType.DisplayName} pierce strike penetration bonus";
        }
    }
}
