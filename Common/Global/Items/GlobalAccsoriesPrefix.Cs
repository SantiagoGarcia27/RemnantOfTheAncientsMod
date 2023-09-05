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
                PrefixUpdate(player, 5, 0, 1);
                CustomTooltip.HealthyReforgeValue = Reaper.ReaperMode ? 1 : 5;
            }
            if (item.prefix == ModContent.PrefixType<Healer>())
            {
                PrefixUpdate(player, 10, 0, 2);
                CustomTooltip.HealerReforgeValue = Reaper.ReaperMode ? 2 : 10;
            }
            if (item.prefix == ModContent.PrefixType<Atletic>())
            {
                PrefixUpdate(player, 15, 0, 3);
                CustomTooltip.AtleticReforgeValue = Reaper.ReaperMode ? 3 : 15;
            }
            if (item.prefix == ModContent.PrefixType<Gigant>())
            {
                PrefixUpdate(player, 20, 0, 5);
                CustomTooltip.GigantReforgeValue = Reaper.ReaperMode ? 5 : 20;
            }
            if (item.prefix == ModContent.PrefixType<Titanic>())
            {
                PrefixUpdate(player, 40, 0, 10);
                CustomTooltip.TitanicReforgeValue = Reaper.ReaperMode ? 10 : 40;
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
                PrefixUpdate(player, Basedefense, 1);
            }
            if (item.prefix == ModContent.PrefixType<Supersonic>())
            {
                PrefixUpdate(player, 0.08f, 2);
            }
            if (item.prefix == ModContent.PrefixType<Acurate>())
            {
                PrefixUpdate(player, 8f, 3);
            }
            if (item.prefix == ModContent.PrefixType<Sharp>())
            {
                PrefixUpdate(player, 0.08f, 4);
            }
            if (item.prefix == ModContent.PrefixType<Uncontrolled>())
            {
                PrefixUpdate(player, 0.08f, 5);
            }
            if (item.prefix == ModContent.PrefixType<Regenerative>())
            {
                PrefixUpdate(player, 2f, 6);
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
        private static void PrefixUpdate(Player player, float value, int stat)
        {
            switch (stat)
            {
                case 0:
                    player.statLifeMax2 += (int)value;
                    break;
                case 1:
                    player.statDefense += (int)value;
                    break;
                case 2:
                    player.moveSpeed += value;
                    break;
                case 3:
                    player.GetCritChance(DamageClass.Generic) += value;
                    break;
                case 4:
                    ref StatModifier GenericDamage = ref (player.GetDamage<GenericDamageClass>());
                    GenericDamage += value;
                    break;
                case 5:
                    player.GetAttackSpeed(DamageClass.Melee) += value;
                    break;
                case 6:
                    player.lifeRegen += (int)value;
                    break;
            }
        }
        private void PrefixUpdate(Player player, float value, int stat, float reaperValue)
        {
            if (stat == 0)
            {
                player.statLifeMax2 += Reaper.ReaperMode ? (int)reaperValue : (int)value;
            }
        }
        public override bool InstancePerEntity => true;
    }
}