using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.accesorios
{
    [AutoloadEquip(EquipType.Waist)]
    public class ManaO : ModItem
    {
     public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mana Overcharger");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Mana Overcharger");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Surcharge de mana");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sobrecargador de Maná");
            
			Tooltip.SetDefault("Increases mana regeneration in exchange for magic damage by 5%"
            +"\n 10% reduce mana usage"
            +"\n Automatically use mana potions when needed"
            +"\n increases pickup range for mana stars"
             +"\n you are immune to Mana Sickness");

            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwiększa regenerację many w zamian za obrażenia magiczne o 5%"
             +"\n 10% zmniejsza zużycie many"
             +"\n W razie potrzeby automatycznie używaj mikstur many"
             +"\n zwiększyć zasięg zbierania gwiazdek many"
              +"\n jesteś odporny na chorobę many");

            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la régénération de mana en échange de dégâts magiques de 5%"
             +"\n 10% de réduction de l'utilisation du mana"
             +"\n Utilisez automatiquement les potions de mana si nécessaire"
             +"\n augmenter la portée de ramassage des étoiles de mana"
              +"\n vous êtes immunisé contre la maladie du mana");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la regeneración de maná a cambio de daño mágico en un 5%."
             +"\n 10% reduce el uso de maná"
             +"\n Usa pociones de maná automáticamente cuando sea necesario"
             +"\n aumentar el rango de recolección de estrellas de maná"
              +"\n eres inmune a la locura de maná");

          
          /* 
            DisplayName.AddTranslation(GameCulture.Russian, "Заряд маны");
          DisplayName.AddTranslation(GameCulture.Chinese, "魔力充能器");
          
          Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает регенерацию маны в обмен на магический урон на 5%"
             +"\n На 10% снижает расход маны"
             +"\n Автоматически использовать зелья маны при необходимости"
             +"\n увеличить дальность сбора маны-звезд"
              +"\n вы невосприимчивы к болезни маны");
            Tooltip.AddTranslation(GameCulture.Chinese, "使魔力回复提高5％以换取魔法伤害"
             +"\n 10％减少法力消耗"
             +"\n 需要时自动使用法力药水"
             +"\n 增加法力星的拾取范围"
              +"\n 你对法力病免疫"); */
		}

        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 14;
            Item.value = 300000;
            Item.rare = 7;
            Item.accessory = true;
        }
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.LihzahrdBrick, 10)
            .AddIngredient(ItemID.ManaRegenerationBand, 1)
            .AddIngredient(ItemID.ManaFlower, 1)
            .AddIngredient(ItemID.CelestialMagnet, 1)
            .AddIngredient(ItemID.ManaCrystal, 10)  //you need 10 Wood
            .AddTile(TileID.LihzahrdFurnace)  //at work bench
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) -= 0.05f;
             player.manaRegenBonus = 700;
             player.manaFlower = true;
             player.manaMagnet = true;
             player.manaCost = 0.9f;
             player.buffImmune[BuffID.ManaSickness] = true;

        }
    }
}