using System.Runtime.CompilerServices;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos
{
    [JITWhenModsEnabled("FargowiltasSouls")]
    public class FargosOverride : ModType
    {
        public void AddToggle(Mod mod,string toggle, object item, string color = "ffffff")
        {
            if (item.GetType() == typeof(int))
            {
                Language.GetOrRegister(toggle,
                     () => $"[i:{item}] [c/{color}:{{$Mods.{mod.Name}.Toggler.{toggle}}}]");
            }
            else if (item.GetType() == typeof(string))
            {
                Language.GetOrRegister(toggle,
                 () => $"[i:{mod.Name}/] [c/{color}:{{$Mods.{mod.Name}.Toggler.{toggle}}}]");
            }
        }

        protected override void Register()
        {
            //throw new System.NotImplementedException();
        }
    }
}
