using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class TextUtils
    {

        public static int GetTooltipDuplicatedIndex(List<TooltipLine> tooltips)
        {
            List<string> SubLine = new List<string>();
            string line = "";

          
           
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (line.Contains("\n"))
                {
                    SubLine = SeparateSringOnList(line);
                }
                if (line != "" && line == tooltips[i].Text)
                {
                    return i;
                }
                line = tooltips[i].Text;
            }
            return -1;
        }
        public static int GetTooltipIndex(List<TooltipLine> tooltips,string word)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Text == word)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int GetTooltipIndexWithaWord(List<TooltipLine> tooltips, string word)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Text.Contains(word))
                {
                    return i;
                }
            }
            return -1;
        }

        public static List<string> SeparateSringOnList(string text) 
        {
            List<string> list = new List<string>();
            char[] delims = new[] { '\r', '\n' };
            list = text.Split(delims, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            return list;
        }
    }
}