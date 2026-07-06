using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace RemnantOfTheAncientsMod.Common.Extensions
{
    public static class DictionaryExtension
    {
        public static TKey RandomKey<TKey, TValue>(this Dictionary<TKey, TValue> diccionario)
        {
            if (diccionario.Count == 0)
                throw new InvalidOperationException("The dictionary is empty.");

            int indice = Main.rand.Next(diccionario.Count);
            return diccionario.Keys.ElementAt(indice);
        }
    }
}
