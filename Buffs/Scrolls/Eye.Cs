using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;

namespace RemnantOfTheAncientsMod.Buffs.Scrolls
{
    public class Eye : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dash master");
            Description.SetDefault("");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Maître de la fente");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Maestro de las embestidas");
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.dash = 1;
            player.dashType = 1;
            player.jumpBoost = true;
            player.noFallDmg = true;
            player.GetModPlayer<DashPlayer>().DashEquipped = true;
            player.GetModPlayer<Player1>().ScrollInmunity(BuffType<Eye>());
        }

    }
}

  