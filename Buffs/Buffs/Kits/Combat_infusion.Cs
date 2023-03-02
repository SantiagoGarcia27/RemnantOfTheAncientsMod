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
            ref StatModifier MeleeDamage = ref player.GetDamage(DamageClass.Melee);
            GenericDamage += 0.10f;
            MagicDamage += 0.20f;
            MeleeDamage += 0.1f;
            player.GetCritChance(DamageClass.Generic) += 10f;
            player.GetCritChance(DamageClass.Melee) += 2f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
            player.arrowDamage.Flat *= 0.2f; 
            player.moveSpeed += 0.25f;
			player.lifeRegen +=4;
            player.manaRegen +=4;
            player.maxMinions++;
            player.maxTurrets++;      
			player.statManaMax2 += 40;
            player.statLifeMax2 += (player.statLifeMax *20)/100;
			player.endurance += 0.10f;
            player.thorns += 0.33f;
            player.statDefense += 8;
            player.lifeMagnet = true;
            player.inferno = true;
            player.archery = true;
            player.resistCold = true;
            player.ammoPotion = true;
            player.kbBuff = true;
           
            if (player.inventory[player.selectedItem].CountsAsClass(DamageClass.Melee)) player.statDefense -= 4;
                     
            Inmunity(player);
        }
        public static void Inmunity(Player player)
        {
            player.buffImmune[2] = true;
            player.buffImmune[3] = true;
            player.buffImmune[5] = true;
            player.buffImmune[6] = true;
            player.buffImmune[7] = true;
            player.buffImmune[14] = true;
            player.buffImmune[16] = true;
            player.buffImmune[25] = true;
            player.buffImmune[105] = true;
            player.buffImmune[108] = true;
            player.buffImmune[112] = true;
            player.buffImmune[113] = true;
            player.buffImmune[114] = true;
            player.buffImmune[115] = true;
            player.buffImmune[116] = true;
            player.buffImmune[117] = true;
            player.buffImmune[124] = true;
        }
    }
}
  