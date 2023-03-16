using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.NPCs.DAniquilator;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Items.Pociones;

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


        public override void AddRecipes()
        {
            RecipeMaker.AddRecipes();
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
            switch (LagLevel)
            {
                case 0:
                    return i;
                case 1:
                    return i / 2;
                case 2:
                    return i / 4;
                case 3:
                    return 0;
                default:
                    return i;
            }
        }
    }
}

