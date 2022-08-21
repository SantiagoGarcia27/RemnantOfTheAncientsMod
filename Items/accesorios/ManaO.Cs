using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

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
            + "\n 10% reduce mana usage"
            + "\n Automatically use mana potions when needed"
            + "\n increases pickup range for mana stars"
             + "\n you are immune to Mana Sickness");

            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la régénération de mana en échange de dégâts magiques de 5%"
             + "\n 10% de réduction de l'utilisation du mana"
             + "\n Utilisez automatiquement les potions de mana si nécessaire"
             + "\n augmenter la portée de ramassage des étoiles de mana"
              + "\n vous êtes immunisé contre la maladie du mana");

            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la regeneración de maná a cambio de daño mágico en un 5%."
             + "\n 10% reduce el uso de maná"
             + "\n Usa pociones de maná automáticamente cuando sea necesario"
             + "\n aumentar el rango de recolección de estrellas de maná"
              + "\n eres inmune a la locura de maná");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 300000;
            Item.rare = ItemRarityID.Lime;
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