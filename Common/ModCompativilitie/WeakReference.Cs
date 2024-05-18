using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.NPCs.Town;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod
{
    internal class WeakReference : ModSystem
    {
        public override void PostSetupContent()
        {

			if (RemnantOfTheAncientsMod.Census != null)
			{
                string message = $"Have either an [i:{ItemID.GoldWatch}] or an [i:{ItemID.PlatinumWatch}] in your inventory after defeating the corrupt boss";
                RemnantOfTheAncientsMod.Census.Call("TownNPCCondition", NPCType<Time_Wizard>(), message);
			}
        }
    }
}

