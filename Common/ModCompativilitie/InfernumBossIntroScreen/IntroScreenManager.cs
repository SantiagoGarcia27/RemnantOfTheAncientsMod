using CalamityMod.Events;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie.InfernumBossIntroScreen
{
    [JITWhenModsEnabled("InfernumMode")]
    public static class IntroScreenManager
    {
        internal static List<BaseIntroScreenTitle> IntroScreens = new();

        public static bool ScreenIsObstructed
        {
            get => IntroScreens.Any(s => s.ShouldCoverScreen && s.ShouldBeActive() && s.AnimationCompletion < 1f)/* && InfernumConfig.Instance.BossIntroductionAnimationsAreAllowed*/ && Main.netMode == NetmodeID.SinglePlayer;
        }

        public static bool ShouldDisplayJokeIntroText
        {
            get
            {
                int introTextDisplayChance = Utils1.IsAprilFoolDay() ? 5 : 500;
                return Main.rand.NextBool(introTextDisplayChance);
            }
        }

        internal static void Load()
        {
            IntroScreens = new List<BaseIntroScreenTitle>();
            foreach (Type introScreen in RemnantOfTheAncientsMod.RemnantOfTheAncients.Code.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(BaseIntroScreenTitle))))
                IntroScreens.Add(FormatterServices.GetUninitializedObject(introScreen) as BaseIntroScreenTitle);
        }

        internal static void Unload()
        {
            IntroScreens = null;
        }

        public static void UpdateScreens()
        {
            foreach (BaseIntroScreenTitle introScreen in IntroScreens)
                introScreen.Update();
        }

        public static void Draw()
        {
            UpdateScreens();
            foreach (BaseIntroScreenTitle introScreen in IntroScreens)
            {
                if (introScreen.ShouldBeActive() && !BossRushEvent.BossRushActive)
                {
                    if (introScreen.AnimationTimer < introScreen.AnimationTime)
                    {
                        introScreen.Draw(Main.spriteBatch);
                        break;
                    }
                    else if (!introScreen.CaresAboutBossEffectCondition)
                        introScreen.AnimationTimer = 0;
                }
            }
        }
    }
}