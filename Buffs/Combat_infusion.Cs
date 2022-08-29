using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Buffs
{
    public class Combat_infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combat infusion");
            Description.SetDefault("Definitive offensive buff");
             DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infusion de combat");
              Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Buff d'attaque définitif");
              DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Infusión de combate");
               Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mejora ofensiva definitiva");
               

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
          	player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetCritChance(DamageClass.Melee) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Summon) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
            player.GetDamage(DamageClass.Magic) += 0.20f;
            player.moveSpeed += 0.25f;
			player.lifeRegen +=4;
            player.manaRegen +=4;
            player.maxMinions++;
            player.inferno = true;
            player.archery = true;
			player.statManaMax2 += 40;
            player.statLifeMax2 += 100;
			player.endurance += 0.10f;
            player.statDefense += 5; 
			player.buffImmune[2] = true;
			player.buffImmune[5] = true;
            player.buffImmune[16] = true;
			player.buffImmune[113] = true;
			player.buffImmune[114] = true;
			player.buffImmune[115] = true;
			player.buffImmune[117] = true;
            player.buffImmune[116] = true;

        }
    }
}
  