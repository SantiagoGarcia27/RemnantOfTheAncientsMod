using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Common.GlobalInfoDisplay
{
    public class HelperClassList 
    {
        public static List<DamageClass> ScaleWithMeleeSpeed = new List<DamageClass>()
        {
            DamageClass.Melee,
            DamageClass.SummonMeleeSpeed
        };
        

        
    }
    public class MaxMinionsDisplay : InfoDisplay
	{
		public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMaxMinion && Utils1.GetClassForHeldItem(Main.LocalPlayer) == DamageClass.Summon;
        public override string DisplayValue(ref Color displayColor) => $"{Main.LocalPlayer.maxMinions} minions.";
    }
    public class MaxSentryDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMaxSentry && Utils1.GetClassForHeldItem(Main.LocalPlayer) == DamageClass.Summon;
        public override string DisplayValue(ref Color displayColor) => $"{Main.LocalPlayer.maxTurrets} turrets.";
    }
    public class DamageReductionDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showDamageReduction;
        public override string DisplayValue(ref Color displayColor) => $"{Main.LocalPlayer.endurance * 100}% Damage Reduction";
    }
    public class DamageBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showDamageBonus;
        public override string DisplayValue(ref Color displayColor)
        {
            var damageBonus = Main.LocalPlayer.GetDamage(Utils1.GetClassForHeldItem(Main.LocalPlayer)).Multiplicative;
            var GenericDamageBonus = Main.LocalPlayer.GetDamage(DamageClass.Generic).Multiplicative;
            var FinalDamage = (damageBonus * 100) - 100 + (GenericDamageBonus * 100) - 100;
            return $"{FinalDamage}%{Utils1.GetClassForHeldItem(Main.LocalPlayer).DisplayName} bonus";
        }
    }
    public class CritBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showCritBonus;
        public override string DisplayValue(ref Color displayColor)
        {
            var CritBonus = Main.LocalPlayer.GetCritChance(Utils1.GetClassForHeldItem(Main.LocalPlayer));
            var GenericCritBonus = Main.LocalPlayer.GetCritChance(DamageClass.Generic);
            var FinalCrit = CritBonus  + GenericCritBonus;
            return $"{FinalCrit}%{Utils1.GetClassForHeldItem(Main.LocalPlayer).DisplayName} crit chance bonus";
        }
    }
    public class LifeRegenBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showLifeRegen && Main.LocalPlayer.statLife < Main.LocalPlayer.statLifeMax2;
        public override string DisplayValue(ref Color displayColor)
        {
            return $"{Main.LocalPlayer.lifeRegen} Life regen bonus";
        }
    }
    public class ManaRegenBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showLifeRegen && Main.LocalPlayer.statMana < Main.LocalPlayer.statManaMax2;
        public override string DisplayValue(ref Color displayColor)
        {
            return $"{Main.LocalPlayer.manaRegenBonus} Mana regen bonus";
        }
    }
    public class ArmorPenetrationBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showArmorPenetration && Main.LocalPlayer.HeldItem.damage > 0;
        public override string DisplayValue(ref Color displayColor)
        {
            var ApBonus = Main.LocalPlayer.GetTotalArmorPenetration(Utils1.GetClassForHeldItem(Main.LocalPlayer));
            var GenericAPBonus = Main.LocalPlayer.GetTotalArmorPenetration(DamageClass.Generic);
            var FinalAP = ApBonus + GenericAPBonus;
            return $"{FinalAP} Armor penetration";
        }
    }
    public class MeleeAttackspeedBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMeleeAttackspeed && Main.LocalPlayer.HeldItem.damage > 0;
        public override string DisplayValue(ref Color displayColor)
        {
            return $"{(Main.LocalPlayer.GetTotalAttackSpeed(Main.LocalPlayer.HeldItem.DamageType) *100) -100}% Attack speed bonus";
        }
    }
    public class FlyTimeBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showWingsTime && Main.LocalPlayer.equippedWings != null;
        public override string DisplayValue(ref Color displayColor)
        {
            return $"{Main.LocalPlayer.wingTime}s /{Main.LocalPlayer.wingTimeMax}s Wing Time";
        }
    }
    public class MiningSpeedBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showMiningSpeedBonus && Main.LocalPlayer.HeldItem.pick > 0 || Main.LocalPlayer.HeldItem.axe > 0;
        public override string DisplayValue(ref Color displayColor)
        {
            return $"{100 - Main.LocalPlayer.pickSpeed * 100}% Mining speed bonus";
        }
    }
    public class LuckBonusDisplay : InfoDisplay
    {
        public override bool Active() => Main.LocalPlayer.GetModPlayer<InfoDisplayPlayer>().showLuck;
        public override string DisplayValue(ref Color displayColor)
        {
            return $"{Main.LocalPlayer.luck}/{Main.LocalPlayer.luckMaximumCap} Luck";
        }
    }
}
