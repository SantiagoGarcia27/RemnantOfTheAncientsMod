using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    [AutoloadEquip(EquipType.Balloon)]
    public class Frost_core : ModItem
    {

        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Frozen Assaulter Core");
            //Tooltip.SetDefault("Increases magic damage by 10%"
            //+ "\n Movement speed increases and you become immune to frozen and chilled debuffs while in snow");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Noyau d'assaillant gelé");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts magiques de 10%"
            //+ "\n La vitesse de déplacement est augmentée et vous êtes immunisé contre le debuff Frozen et Chilled lorsque vous êtes dans la neige");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Núcleo de asaltante congelado");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño magico en un 25%"
            //+ "\n La velocidad de movimiento aumenta y eres inmune a la desventaja de Congelado y Helado cuando estás en la nieve.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 45000;
            Item.rare = ItemRarityID.Red;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) *= 1.10f;
            if (player.ZoneSnow)
            {
                player.moveSpeed += 1.50f;
                player.buffImmune[BuffID.Chilled] = true;
                player.buffImmune[BuffID.Frozen] = true;
            }
        }
    }
}