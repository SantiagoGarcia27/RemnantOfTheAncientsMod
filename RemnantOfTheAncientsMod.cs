using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Items.Items;
using System;

namespace RemnantOfTheAncientsMod
{
    class RemnantOfTheAncientsMod : Mod
    {
        public static Mod CalamityMod;
        public static Mod BossChecklist;
        public static bool DebuggMode;
        public static int CustomCurrencyId;
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
           // CustomCurrencyId = CustomCurrencyManager.RegisterCurrency(new Currencies.RemnantCurrency(ModContent.ItemType<Terracoin>(), 999L, "Mods.RemnantOfTheAncientsMod.Currencies.ExampleCustomCurrency"));


         
            
            if (ModContent.GetInstance<Terracoin>() != null) // Verifica si el tipo no es nulo.
            {
                ModContent.GetInstance<Terracoin>(); // Crea una instancia del tipo para registrar tu moneda personalizada.
     
            }
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


        public int ParticleMeter(int i)
        {
            float lagLevel = ModContent.GetInstance<ConfigClient1>().LagReducer;

            if (lagLevel == 3f)
            {
                return 0;
            }

            return (int)(i / Math.Pow(2, (int)lagLevel));
        }
    }
}