using System;
using Microsoft.Xna.Framework;
using Terraria;
using System.Text;
using static Humanizer.In;
using Terraria.ModLoader;
using Terraria.Localization;
using IL.Terraria.ID;
using Terraria.Utilities;
using RemnantOfTheAncientsMod.World;

namespace RemnantOfTheAncientsMod
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
		public static Rectangle GetCurrentFrame(Item item, ref int frame, ref int frameCounter, int frameDelay, int frameAmt, bool frameCounterUp = true)
		{
			if (frameCounter >= frameDelay)
			{
				frameCounter = -1;
				frame = ((frame != frameAmt - 1) ? (frame + 1) : 0);
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
			return (porcentage * number) / 100;
		}
		public static float GetPorcentage(float intitialNumber, float total)
		{
			return (intitialNumber * 100) / total;
		}
		public static int FormatTime(int Day, int Hour, int Minute, int Second)
		{
			return (Second * 60) + (Minute * 60 * 60) + (Hour * 60 * 60 * 60) + (Day * 60 * 60 * 60 * 60);
		}
        public static float FormatTime(float Day, float Hour, float Minute, float Second)
        {
            return (Second * 60) + (Minute * 60 * 60) + (Hour * 60 * 60 * 60) + (Day * 60 * 60 * 60 * 60);
        }
        public static int GetItemFromMod(Mod mod, string item)
		{
			return mod.Find<ModItem>(item).Type;
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
        public static int GetModPrefix(Mod mod, string name)
		{
			return mod.TryFind<ModPrefix>(name, out var ret) ? ret.Type : 0;
		}

   //     public override struct DropAttemptInfo : 
   //     {
   //         public NPC npc;

   //         public int item;

   //         public Player player;

   //         public UnifiedRandom rng;

   //         public bool IsInSimulation;

   //         public bool IsExpertMode;

   //         public bool IsMasterMode;

			//public bool IsReaperMode;
   //     }


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
        public static string IncreasedDamageByTooltip(int amount,DamageClass WeaponClass)
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

            //if (WeaponClass == DamageClass.Generic)
            //{
            //    return Language.GetTextValue("{$CommonItemTooltip.PercentIncreasedMagicCritChance}", amount);
            //}
            //else if (WeaponClass == DamageClass.Melee)
            //{
            //    return Language.GetTextValue("{$CommonItemTooltip.PercentIncreasedMeleeCritChance}", amount);
            //}
            //else if (WeaponClass == DamageClass.Ranged)
            //{
            //    return Language.GetTextValue("{$CommonItemTooltip.PercentIncreasedRangedCritChance}", amount);
            //}
            //else if (WeaponClass == DamageClass.Summon)
            //{
            //    return Language.GetTextValue("{$CommonItemTooltip.PercentSummonTagCritChance}", amount);
            //}
            //else if (WeaponClass == DamageClass.Magic)
            //{
            //    return Language.GetTextValue("{$CommonItemTooltip.PercentIncreasedMagicCritChance}", amount);
            //}
            //else
            //{
            //    return $"{amount}% increased magic {WeaponClass} strike chance";
            //}
        }

    }
}