using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Prefixe;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class GlobalAccsoriesPrefix : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix == ModContent.PrefixType<Healthy>())
            {
                int lifeBonus = Reaper.ReaperMode ? 1 : 5;
                PrefixUpdate(player, lifeMax: lifeBonus);
                CustomTooltip.HealthyReforgeValue = lifeBonus;
            }
            if (item.prefix == ModContent.PrefixType<Healer>())
            {
                int lifeBonus = Reaper.ReaperMode ? 2 : 10;
                PrefixUpdate(player, lifeMax: lifeBonus);
                CustomTooltip.HealerReforgeValue = lifeBonus;
            }
            if (item.prefix == ModContent.PrefixType<Atletic>())
            {
                int lifeBonus = Reaper.ReaperMode ? 3 : 15;
                PrefixUpdate(player, lifeMax: lifeBonus);
                CustomTooltip.AtleticReforgeValue = lifeBonus;
            }
            if (item.prefix == ModContent.PrefixType<Gigant>())
            {
                int lifeBonus = Reaper.ReaperMode ? 5 : 20;
                PrefixUpdate(player, lifeMax: lifeBonus);
                CustomTooltip.GigantReforgeValue = lifeBonus;
            }
            if (item.prefix == ModContent.PrefixType<Titanic>())
            {
                int lifeBonus = Reaper.ReaperMode ? 10 : 40;
                PrefixUpdate(player, lifeMax: lifeBonus, defense: 0, speed: 10);
                CustomTooltip.TitanicReforgeValue = lifeBonus;
            }
            if (item.prefix == ModContent.PrefixType<Impenetrable>())
            {
                int Basedefense = 8;
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    Basedefense = CalamityPrefixChanger();
                    player.endurance += 0.02f;
                }
                CustomTooltip.UnpenetrableReforgeValue = Basedefense;
                PrefixUpdate(player, defense: Basedefense);
            }
            if (item.prefix == ModContent.PrefixType<Supersonic>())
            {
                PrefixUpdate(player, speed: 0.08f);
            }
            if (item.prefix == ModContent.PrefixType<Acurate>())
            {
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                   player.luck += 0.1f;
                }
                PrefixUpdate(player, crit: 8f);

            }
            if (item.prefix == ModContent.PrefixType<Sharp>())
            {
                PrefixUpdate(player, damage: 0.08f);
            }
            if (item.prefix == ModContent.PrefixType<Uncontrolled>())
            {
                PrefixUpdate(player, meleeSpeed: 0.08f);
            }
            if (item.prefix == ModContent.PrefixType<Regenerative>())
            {
                PrefixUpdate(player, lifeRegen: 2);
            }
            if(item.prefix == ModContent.PrefixType<Pure>())
            {
                PrefixUpdate(player,damage:1,crit:1,speed:1,defense:1);
            }
            if (item.prefix == ModContent.PrefixType<Harmful>())
            {
                PrefixUpdate(player, damage: 2, crit: 2);
            }
            if (item.prefix == ModContent.PrefixType<Cursed>())
            {
                PrefixUpdate(player, damage: 5, defense: -3);
            }

        }
        [JITWhenModsEnabled("CalamityMod")]
        private static int CalamityPrefixChanger()
        {

            bool downedYharon = CalamityUtils.IsBossDefeated("yharon");
            bool downedDoG = CalamityUtils.IsBossDefeated("devourerofgods");
            bool downedPolterghast = CalamityUtils.IsBossDefeated("polterghast");
            bool downedProvidence = CalamityUtils.IsBossDefeated("providence");

            if (downedYharon)
            {
                return 20;
            }
            else if (downedDoG)
            {
                return 19;
            }
            else if (downedPolterghast)
            {
                return 18;
            }
            else if (downedProvidence)
            {
                return 16;
            }
            else if (NPC.downedMoonlord)
            {
                return 14;
            }
            else if (NPC.downedGolemBoss)
            {
                return 12;
            }
            else if (Main.hardMode)
            {
                return 10;
            }
            return 8;
        }
        private static void PrefixUpdate(Player player, int lifeMax = 0, int defense = 0, float speed = 0, float crit = 0, float damage = 0, int lifeRegen = 0, float meleeSpeed = 0)
        {
            if (lifeMax != 0)
            {
                player.statLifeMax2 += lifeMax;
            }
            if (defense != 0)
            {
                player.statDefense += defense;
            }
            if (speed != 0)
            {
                float bonus = (float)speed/100f;
                player.moveSpeed += bonus;
            }
            if (crit != 0)
            {
                player.GetCritChance(DamageClass.Generic) += crit;
            }
            if (damage != 0)
            {
                ref StatModifier GenericDamage = ref (player.GetDamage<GenericDamageClass>());
                GenericDamage *= 1+(damage/100f);
            }
            if (lifeRegen != 0)
            {
                player.lifeRegen += lifeRegen;
            }
            if (meleeSpeed != 0)
            {
                player.GetAttackSpeed(DamageClass.Melee) += meleeSpeed;
            }
        }
        public override bool InstancePerEntity => true;
    }
}