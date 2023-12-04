using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class ExternalModCallUtils
    {
        public static int GetItemFromMod(Mod mod, string item)
        {
            return mod.Find<ModItem>(item).Type;
        }
        public static int GetProjectileFromMod(Mod mod, string projectile)
        {
            return mod.Find<ModProjectile>(projectile).Type;
        }
        public static int GetTileFromMod(Mod mod, string tile)
        {
            return mod.Find<ModTile>(tile).Type;
        }
        public static int GetNpcFromMod(Mod mod, string npc)
        {
            return mod.Find<ModNPC>(npc).Type;
        }
        public static DamageClass GetDamageClassFromMod(Mod mod, string damageClass)
        {
            return mod.Find<DamageClass>(damageClass);
        }
        public static int GetBuffFromMod(Mod mod, string buff)
        {
            return mod.Find<ModBuff>(buff).Type;
        }
        public static int GetDustFromMod(Mod mod, string dust)
        {
            return mod.Find<ModDust>(dust).Type;
        }
        public static int GetPrefixFromMod(Mod mod, string prefix)
        {
            return mod.Find<ModPrefix>(prefix).Type;
        }
        public static int GetRarityFromMod(Mod mod, string rarity)
        {
            return mod.Find<ModRarity>(rarity).Type;
        }
        public static int GetModPrefix(Mod mod, string name)
        {
            return mod.TryFind<ModPrefix>(name, out var ret) ? ret.Type : 0;
        }
        //public static bool GetInstanceFromMod(Mod mod, string config, bool expectedValue)
        //{

        //	var value = mod.Find<ModConfig>(config);
        //	var a = (bool)value.GetType().GetField("MyBooleanName").GetValue(value);
        //	if (a == expectedValue)
        //	{
        //		return true;
        //	}
        //	else
        //	{
        //		return false;
        //	}
        //      }
        /* public static T GetValue<T>(this ModConfig config, string name)
         {
             return config.GetType().GetMember(name) switch
             {
                 FieldInfo field => (T)field.GetValue(config),
                 PropertyInfo property => (T)property.GetValue(config),
                 _ => throw new Exception("The specified value is not a field or a property or does not exist")
             };
         }*/
    }
}