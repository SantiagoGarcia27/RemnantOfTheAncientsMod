using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class Utils1
    {
        internal static readonly Color ReaperRarityColor = new Color(48, 48, 48); // 
        internal static readonly Color LegendaryRarityColor = new Color(0, 255, 200);


        internal static bool IsABoss()
        {
            throw new NotImplementedException();
        }

        public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
        {
            float amount = (float)((Math.Sin(Math.PI * 2f / seconds * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
            return Color.Lerp(firstColor, secondColor, amount);
        }
        public static Color? GetRarityColor(CustomRarity rare)
        {
            switch (rare)
            {
                default:
                    return null;
                case CustomRarity.Reaper:
                    return ReaperRarityColor;
                case CustomRarity.Legendary:
                    return LegendaryRarityColor;
            }
        }

        public static bool anyBossIsAlive;

        public static bool IsAnyBossAlive()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                    return true;
            }
            return false;
        }

        public static string ColorMessage(string msg, Color color)
        {

            StringBuilder stringBuilder;
            if (!msg.Contains("\n"))
            {
                stringBuilder = new StringBuilder(msg.Length + 12);
                stringBuilder.Append("[c/").Append(Utils.Hex3(color)).Append(':')
                    .Append(msg)
                    .Append(']');
            }
            else
            {
                stringBuilder = new StringBuilder();
                string[] array = msg.Split('\n');
                foreach (string value in array)
                {
                    stringBuilder.Append("[c/").Append(Utils.Hex3(color)).Append(':')
            .Append(value)
            .Append(']')
            .Append('\n');
                }
            }
            return stringBuilder.ToString();
        }
        internal static Color GetRarityColor(Color rarityColorOne, Color rarityColorTwo)
        {
            return ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
        public static Color GetReaperColor(int x)
        {
            //Color color;// = new Color(100, 100, 100);
            if (x == 1) return new Color(46, 45, 45);
            else if (x == 2) return new Color(212, 210, 210);//191,187,187
            else return new Color(255, 255, 255);
            //return color;
        }
        public static Rectangle GetCurrentFrame(Item item, ref int frame, ref int frameCounter, int frameDelay, int frameAmt, bool frameCounterUp = true)
        {
            if (frameCounter >= frameDelay)
            {
                frameCounter = -1;
                frame = frame != frameAmt - 1 ? frame + 1 : 0;
            }
            if (frameCounterUp)
            {
                frameCounter++;
            }
            return new Rectangle(0, item.height * frame, item.width, item.height);
        }

        public static RemnantPlayer RemnantOfTheAncientsMod(this Player player)
        {
            return player.GetModPlayer<RemnantPlayer>();
        }
        public static Item ActiveItem(this Player player)
        {
            return Main.mouseItem;
        }
        public static float GetValueFromPorcentage(float number, float porcentage)
        {
            return porcentage * number / 100;
        }
        public static float GetPorcentage(float intitialNumber, float total)
        {
            return intitialNumber * 100 / total;
        }
        public static int FormatTime(int Day, int Hour, int Minute, int Second)
        {
            return Second * 60 + Minute * 60 * 60 + Hour * 60 * 60 * 60 + Day * 60 * 60 * 60 * 60;
        }
        public static float FormatTime(float Day, float Hour, float Minute, float Second)
        {
            return Second * 60 + Minute * 60 * 60 + Hour * 60 * 60 * 60 + Day * 60 * 60 * 60 * 60;
        }
        public static Vector2 FormatToPositive(Vector2 imput)
        {
            Vector2 Output;
            Output.X = imput.X < 0 ? -imput.X : imput.X;
            Output.Y = imput.Y < 0 ? -imput.Y : imput.Y;
            return Output;
        }

        public static bool NameHasWord(string word, string SeachedWord)
        {
            return word.Contains(SeachedWord) ? true : false;
        }
        public static int ReaperDropScaler(int i)
        {
            int a = i;
            if (Reaper.ReaperMode) a = i * 5;
            return a;
        }

        public static string IncreasedMinionTooltip(int amount)
        {
            return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedMinion", amount);
        }
        public static string IncreasedCentryTooltip(int amount)
        {
            return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedSentry", amount);
        }
        public static string IncreasedLifeMaxTooltip(int amount)
        {
            return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedLifeMax", amount);
        }
        public static string IncreasedManaMaxTooltip(int amount)
        {
            return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedManaMax", amount);
        }
        public static string IncreasedDamageReductionTooltip(int amount)
        {
            return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedDamageReduction", amount);
        }
        public static string IncreasedDamageByTooltip(int amount, DamageClass WeaponClass)
        {
            if (WeaponClass == DamageClass.Generic)
            {
                return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedGenericDamage", amount);
            }
            else
            {
                return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedClassDamage", amount, WeaponClass.DisplayName);
            }
            //else if (WeaponClass == DamageClass.Melee)
            //         {
            //             return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedClassDamage", amount, WeaponClass.ClassName);
            //         }
            //         else if (WeaponClass == DamageClass.Ranged)
            //         {
            //             return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedClassDamage", amount);
            //         }
            //         else if (WeaponClass == DamageClass.Summon)
            //         {
            //             return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedClassDamage", amount);
            //         }
            //         else if (WeaponClass == DamageClass.Magic)
            //         {
            //             return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedClassDamage", amount);
            //         } 
            //else
            //{
            //             return $"{amount}% increased {WeaponClass} damage";
            //         }
        }
        public static string IncreasedCritByTooltip(int amount, DamageClass WeaponClass)
        {
            if (WeaponClass == DamageClass.Generic)
            {
                return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedGenericCritChance", amount);
            }
            else
            {
                return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedClassCritChance", amount, WeaponClass.DisplayName);
            }
        }
        public static string IncreasedMovmentSpeedTooltip(int amount)
        {
            return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedMovementSpeed", amount);
        }
       
        
        internal static readonly MethodInfo LocalizationLoader_AutoloadTranslations = typeof(LocalizationLoader).GetMethod("AutoloadTranslations", BindingFlags.NonPublic | BindingFlags.Static)!;
        internal static readonly MethodInfo LocalizationLoader_SetLocalizedText = typeof(LocalizationLoader).GetMethod("SetLocalizedText", BindingFlags.NonPublic | BindingFlags.Static)!;
        internal static readonly FieldInfo LanguageManager__localizedTexts = typeof(LanguageManager).GetField("_localizedTexts", BindingFlags.NonPublic | BindingFlags.Instance)!;

        /// <summary>
        /// Force's the localization for the given mod, <paramref name="mod"/>, to be loaded for use with <seealso cref="Language"/>
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void ForceLoadModHJsonLocalization(Mod mod)
        {
            Dictionary<string, ModTranslation> modTranslationDictionary = new();

            LocalizationLoader_AutoloadTranslations.Invoke(null, new object[] { mod, modTranslationDictionary });

            Dictionary<string, LocalizedText> dict = LanguageManager__localizedTexts.GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>;

            var culture = Language.ActiveCulture;
            foreach (ModTranslation translation in modTranslationDictionary.Values)
            {
                //LocalizedText text = new LocalizedText(translation.Key, translation.GetTranslation(culture));
                LocalizedText text = Activator.CreateInstance(typeof(LocalizedText), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, new object[] { translation.Key, translation.GetTranslation(culture) }, CultureInfo.InvariantCulture) as LocalizedText;

                LocalizationLoader_SetLocalizedText.Invoke(null, new object[] { dict, text });
            }
        }
    }
}