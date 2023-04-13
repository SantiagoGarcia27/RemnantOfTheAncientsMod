using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Items.Items;
using System;
using Terraria.ID;

namespace RemnantOfTheAncientsMod
{
    class RemnantOfTheAncientsMod : Mod
    {
        public static Mod CalamityMod;
        public static Mod BossChecklist;
        public static Mod ThoriumMod;
        public static bool ThoriumModLoad;
        public static bool CalamityModLoad;
        public static bool BossChecklistLoad;
        public static bool DebuggMode;
        public static int CustomCurrencyId;
        public static readonly int MaxRarity = GetMaxRarity();
        public RemnantOfTheAncientsMod()
        {

        }


        public override void AddRecipes()
        {
            RecipeMaker.AddRecipes();
        }

        public override void Load()
        {
            //  ThoriumModLoad = ModLoader.GetMod("ThoriumMod") != null;
            //CalamityModLoad = ModLoader.GetMod("CalamityMod") != null;
            //BossChecklistLoad = ModLoader.GetMod("BossChecklist") != null;
            ModLoader.TryGetMod("CalamityMod", out CalamityMod); //CalamityMod = ModLoader.GetMod("CalamityMod");
            ModLoader.TryGetMod("BossChecklist", out BossChecklist);
            // else CalamityMod = null;
            // if (ModLoader.HasMod("ThoriumMod")) ThoriumMod = ModLoader.GetMod("ThoriumMod");
            // else ThoriumMod = null;
            //if (ModLoader.HasMod("BossChecklist")) BossChecklist = ModLoader.GetMod("BossChecklist");
            //else BossChecklist = null;
           // CustomCurrencyId = CustomCurrencyManager.RegisterCurrency(new Currencies.RemnantCurrency(ModContent.ItemType<Terracoin>(), 999L, "Mods.RemnantOfTheAncientsMod.Currencies.ExampleCustomCurrency"));


         
            
            if (ModContent.GetInstance<Terracoin>() != null) // Verifica si el tipo no es nulo.
            {
                ModContent.GetInstance<Terracoin>(); // Crea una instancia del tipo para registrar tu moneda personalizada.
     
            }
        }

        private static int GetMaxRarity()
        {
            int max = 0;
            for (int i = 0; i < RarityLoader.RarityCount; i++)
            {
               if(max < i) max = i;
            }
            return max;
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