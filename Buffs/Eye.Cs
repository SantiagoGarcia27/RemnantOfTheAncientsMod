using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace opswordsII.Buffs
{
    public class Eye : ModBuff
    {
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Dash master");
              Description.SetDefault("");

             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Maître de la fente");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Maestro de las embestidas");
             
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.dash = 1;
            //player.dashType = 1;
            player.jumpBoost = true;
			player.noFallDmg = true;
			player.GetModPlayer<DashPlayer>().DashEquipped = true;

			player.buffImmune[BuffType<Bee>()] = true;
            player.buffImmune[BuffType<AoD>()] = true;
            player.buffImmune[BuffType<Infernal>()] = true;
            player.buffImmune[BuffType<MasterD>()] = true;
            player.buffImmune[BuffType<Putrid>()] = true;
            player.buffImmune[BuffType<Skeleton>()] = true;
            player.buffImmune[BuffType<Slim>()] = true;
        }
		
		}
	}

  