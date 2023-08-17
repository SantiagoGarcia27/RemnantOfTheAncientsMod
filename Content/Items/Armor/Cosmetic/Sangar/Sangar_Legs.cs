using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Sangar
{
    [AutoloadEquip(EquipType.Legs)]
    public class Sangar_Legs : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Sangar´s legs");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Les jambes de Sangar");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Botas de Sangar");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.Size = new(30, 30);
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Cyan;
        }
    }
}

