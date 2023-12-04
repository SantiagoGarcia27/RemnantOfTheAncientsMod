using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie.InfernumBossIntroScreen
{
    [JITWhenModsEnabled("InfernumMode")]
    public class DesertAnhilatorIntroScreen : BaseIntroScreenTitle
    {
        public override TextColorData TextColor => new(completionRatio =>
        {
            float colorInterpolant = (float)(Math.Sin(completionRatio * Math.PI * 4f + AnimationCompletion * MathHelper.TwoPi * 0.4f) * 0.5f + 0.5f);
            Color dark = Color.Lerp(Color.Navy, Color.Black, 0.7f);
            Color light = Color.Lerp(Color.Blue, Color.White, 0.65f);
            return Color.Lerp(light, dark,  CalamityUtils.Convert01To010(colorInterpolant * 3f % 1f) * 0.6f);
        });
        public override LocalizedText TextToDisplay => GetLocalizedText(IntroScreenManager.ShouldDisplayJokeIntroText || Utils1.IsAprilFoolDay() ? "JokeTextToDisplay" : "TextToDisplay");
        public override bool TextShouldBeCentered => true;

        public override bool ShouldCoverScreen => true;

        public override bool CaresAboutBossEffectCondition => true;

        public override bool ShouldBeActive() => NPC.AnyNPCs(ModContent.NPCType<DesertAniquilator>());

        public override SoundStyle? SoundToPlayWithTextCreation => null;
    }
}