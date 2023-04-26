using RemnantOfTheAncientsMod.Projectiles.Summon.Minioms;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs.Minions
{
    public class DesertMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Baby Desert Aniquilator");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Aniquilateur bébé désert");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pequeño aniquilador");
            //Description.SetDefault("The Baby Desert will fight for you");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le bébé désert se battra pour vous");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "El pequeño aniquilador luchará por ti");

            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ProjectileType<DesertMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}