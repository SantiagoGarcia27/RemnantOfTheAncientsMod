using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.NPCs.DAniquilator;

namespace RemnantOfTheAncientsMod
{
    class RemnantOfTheAncientsMod : Mod
    {
        public static Mod CalamityMod;
        public static Mod BossChecklist;
        public static bool DebuggMode;
        public RemnantOfTheAncientsMod()
        {
           
        }

        [System.Obsolete]
        public override void AddRecipes()
        {
            RecipeMaker.AddRecipes(this);
        }

        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityMod = ModLoader.GetMod("CalamityMod");
            else CalamityMod = null;
            if (ModLoader.HasMod("BossChecklist")) BossChecklist = ModLoader.GetMod("BossChecklist");
            else BossChecklist = null;
        }
        public override void Unload()
        {
            BossChecklist = null;
        }
        public static Color GetLightColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
        }
        public override void PostSetupContent()
        {
            WeakReference.Setup();
        }


        public int ParticlleMetter(int i)
        {
            float LagLevel = ModContent.GetInstance<ConfigClient1>().LagReducer;
            int n = 0;

            if (LagLevel == 0) n = i;
            else if (LagLevel == 1) n = i / 2;
            else if (LagLevel == 2) n = i / 4;
            else if (LagLevel == 3) n = 0;
            return n;
        }
         
    }
}

