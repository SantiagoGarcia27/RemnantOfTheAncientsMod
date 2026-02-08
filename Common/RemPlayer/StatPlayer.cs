using InfernumMode.Content.Achievements;
using RemnantOfTheAncientsMod.Content.Achievements;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Terraria;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.RemPlayer
{
    public partial class ExtraProjectile
    {
        public int ProjectileType { get; set; }
        public int Chance { get; set; }

        public ExtraProjectile() {}
       
        public ExtraProjectile(int projectileType, int chance)
        {
            ProjectileType = projectileType;
            Chance = chance;
        }
    }


    public class StatPlayer : ModPlayer
    {
        public int StyleStat = 0;
        public const int MaxStyleStat = 110;


        public float ArrowSpeedBonus = 1;
        private Dictionary<DamageClass, float> ProjectilePenetrationBonus = [];
        private Dictionary<DamageClass, float> ProjectilePenetrationChance = [];
        public Dictionary<DamageClass, float> ChargeBonus = [];



      


        private Dictionary<int, List<ExtraProjectile>> RangerExtraProjectile = [];

        public override void ResetEffects()
        {

            StyleStat = 0;


            ArrowSpeedBonus = 1;

            RangerExtraProjectile = [];
          
            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
            {
                DamageClass damageClass = DamageClassLoader.GetDamageClass(i);
               
                if (!ProjectilePenetrationBonus.ContainsKey(damageClass))
                {
                    ProjectilePenetrationBonus.Add(damageClass,1);
                    ProjectilePenetrationChance.Add(damageClass, 0);
                    ChargeBonus.Add(damageClass,1);
                }
                else
                {
                    ProjectilePenetrationBonus[damageClass] = 1;
                    ChargeBonus[damageClass] = 1;
                    ProjectilePenetrationChance[damageClass] = 0;
                }
            }
        }

        public override void UpdateEquips()
        {
            if(!ModContent.GetInstance<MaxStyleAchievement>().Condition.IsCompleted && StyleStat > ModContent.GetInstance<MaxStyleAchievement>().Condition.Value)
            {
                ModContent.GetInstance<MaxStyleAchievement>().Condition.Value = StyleStat;
            }
            base.UpdateEquips();
        }

        public float GetProjectilePenetration(DamageClass damageClass)
        {
            return ProjectilePenetrationBonus[damageClass];
        }
        public void SetProjectilePenetration(DamageClass damageClass, float value)
        {
            ProjectilePenetrationBonus[damageClass] = value;
        }
        public void AddProjectilePenetration(DamageClass damageClass, float value)
        {
            ProjectilePenetrationBonus[damageClass] += value;
        }

        public float GetProjectilePenetrationChance(DamageClass damageClass)
        {
            return ProjectilePenetrationChance[damageClass];
        }
        public void SetProjectilePenetrationChance(DamageClass damageClass, float value)
        {
            ProjectilePenetrationChance[damageClass] = value;
        }
        public void AddProjectilePenetrationChance(DamageClass damageClass, float value)
        {
            ProjectilePenetrationChance[damageClass] += value;
        }

        public List<ExtraProjectile> GetRangerExtraProjectile(int ammoType)
        {
            if (!RangerExtraProjectile.ContainsKey(ammoType))
                return [];
            return RangerExtraProjectile[ammoType];
        }
        public void SetRangerExtraProjectile(int ammoType, int projectileType, int chance = 100)
        {
            if (!RangerExtraProjectile.ContainsKey(ammoType))
            {
                RangerExtraProjectile.Add(ammoType, []);
            }

            List<ExtraProjectile> list = RangerExtraProjectile[ammoType];

            int index = list.FindIndex(x => x.ProjectileType == projectileType);
            if (index != -1)
            {
                if (list[index].Chance < chance)
                {
                    list[index] = new ExtraProjectile(projectileType, chance);
                }
            }
            else
            {
                list.Add(new ExtraProjectile(projectileType, chance));
            }
        }
    }
}
