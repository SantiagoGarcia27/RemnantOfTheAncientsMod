using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie.InfernumBossIntroScreen
{
    [JITWhenModsEnabled("InfernumMode")]
    public class UIRenderingSystem : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("InfernumMode", out mod);
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
            if (mouseIndex != -1)
            {
                layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Boss Introduction Screens", () =>
                {
                    IntroScreenManager.Draw();
                    return true;
                }, InterfaceScaleType.None));
            }
        }
    }
}