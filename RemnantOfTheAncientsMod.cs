﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Items;
using System;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod
{
    class RemnantOfTheAncientsMod : Mod
    {
        public static Mod CalamityMod;
        public static Mod BossChecklist;
        public static Mod ThoriumMod;
        public static Mod TerrariaOverhaul;
        public static Mod Census;
        public static Mod RemnantOfTheAncients;
        public static bool DebuggMode;
        public static int CustomCurrencyId;
        public static int MaxRarity = GetMaxRarity();
        public static int MaxPlayers = 0;
        public RemnantOfTheAncientsMod()
        {

        }
        public override void AddRecipes()
        {
            RecipeMaker.AddRecipes();
        }


        public override void Load()
        {

            ModLoader.TryGetMod("CalamityMod", out CalamityMod);
            ModLoader.TryGetMod("ThoriumMod", out ThoriumMod);
            ModLoader.TryGetMod("BossChecklist", out BossChecklist);
            ModLoader.TryGetMod("TerrariaOverhaul", out TerrariaOverhaul);
            ModLoader.TryGetMod("Census", out Census);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncients);

            if (ModContent.GetInstance<Terracoin>() != null) // Verifica si el tipo no es nulo.
            {
                ModContent.GetInstance<Terracoin>(); // Crea una instancia del tipo para registrar tu moneda personalizada.
            }
            LocalizationHelper.ForceLoadModHJsonLocalization(this);
        }

        public static int GetMaxRarity()
        {
            int max = 0;
            for (int i = 0; i < RarityLoader.RarityCount; i++)
            {
                if (max < i) max = i;
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
            float lagLevel = ModContent.GetInstance<ConfigServer>().LagReducer;

            if (lagLevel == 3f)
            {
                return 0;
            }

            return (int)(i / Math.Pow(2, (int)lagLevel));
        }
        public static int MaxPlayerOnline()
        {
            MaxPlayers = 0;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (MaxPlayers < 10)
                {
                    Player plr = Main.player[i];
                    if (plr.active && !plr.dead)
                    {
                        MaxPlayers++;
                    }
                }   
            }
            return MaxPlayers -1;
        }
    }
}