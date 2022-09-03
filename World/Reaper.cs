using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.World
{
    public class Reaper : ModSystem
    {
        public static bool ReaperMode;
        public override void OnWorldLoad() => ReaperMode = false;
        public override void OnWorldUnload() => ReaperMode = false;
    }
}
