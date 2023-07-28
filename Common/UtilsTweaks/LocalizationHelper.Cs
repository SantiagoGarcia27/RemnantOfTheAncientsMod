using System.Reflection;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class LocalizationHelper
    {
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
            if (amount > 0)
            {
                return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.IncreasedMovementSpeed", amount);
            }
            else
            {              
                return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.CommonTooltip.DecreasedMovementSpeed", -amount);
            }
        }
        public static string GetNpcName(int npcId)
        {     
            string npcName = GetFullNameFromNetId(npcId);
            string test = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.NPCs." + npcName + ".//DisplayName");
            return Language.GetTextValue("Mods.RemnantOfTheAncientsMod.NPCs."+ npcName+ ".//DisplayName");
        }
        private static string GetFullNameFromNetId(int netId)
        {
            return netId < NPCID.Count ? NPCID.Search.GetName(netId) : NPCLoader.GetNPC(netId).Name;
        }

        /// <summary>
        ///     Get the npc net id from a loaded npc's full name.
        /// </summary>
        private static bool GetNetIdFromFullName(string fullName, out int netId)
        {
            if (NPCID.Search.TryGetId(fullName, out netId))
            {
                return true;
            }

            if (ModContent.TryFind(fullName, out ModNPC npc))
            {
                netId = npc.Type;
                return true;
            }

            netId = default;
            return false;
        }

        internal static readonly MethodInfo LocalizationLoader_AutoloadTranslations = typeof(LocalizationLoader).GetMethod("AutoloadTranslations", BindingFlags.NonPublic | BindingFlags.Static)!;
        internal static readonly MethodInfo LocalizationLoader_SetLocalizedText = typeof(LocalizationLoader).GetMethod("SetLocalizedText", BindingFlags.NonPublic | BindingFlags.Static)!;
        internal static readonly FieldInfo LanguageManager__localizedTexts = typeof(LanguageManager).GetField("_localizedTexts", BindingFlags.NonPublic | BindingFlags.Instance)!;

      //  /// <summary>
      //  /// Force's the localization for the given mod, <paramref name="mod"/>, to be loaded for use with <seealso cref="Language"/>
      //  /// </summary>
      //  /// <param name="mod">The mod instance</param>
        //public static void ForceLoadModHJsonLocalization(Mod mod)
        //{
        //    Dictionary<string, ModTranslation> modTranslationDictionary = new();

        //    LocalizationLoader_AutoloadTranslations.Invoke(null, new object[] { mod, modTranslationDictionary });

        //    Dictionary<string, LocalizedText> dict = LanguageManager__localizedTexts.GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>;

        //    var culture = Language.ActiveCulture;
        //    foreach (ModTranslation translation in modTranslationDictionary.Values)
        //    {
        //        //LocalizedText text = new LocalizedText(translation.Key, translation.GetTranslation(culture));
        //        LocalizedText text = Activator.CreateInstance(typeof(LocalizedText), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, new object[] { translation.Key, translation.GetTranslation(culture) }, CultureInfo.InvariantCulture) as LocalizedText;

        //        LocalizationLoader_SetLocalizedText.Invoke(null, new object[] { dict, text });
        //    }
        //}
    }
}