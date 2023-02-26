using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Buffs.Buffs.Kits
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
            ref StatModifier GenericDamage = ref (player.GetDamage<GenericDamageClass>());
            ref StatModifier MagicDamage = ref (player.GetDamage<MagicDamageClass>());
            GenericDamage += 0.10f;
            MagicDamage += 0.20f;
            player.GetCritChance(DamageClass.Generic) += 10f;
            player.moveSpeed += 0.25f;
			player.lifeRegen +=4;
            player.manaRegen +=4;
            player.maxMinions++;
            player.inferno = true;
            player.archery = true;
			player.statManaMax2 += 40;
            player.statLifeMax2 += (player.statLifeMax *20)/100;
			player.endurance += 0.10f;
            player.statDefense += 8; 
			
            
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
  