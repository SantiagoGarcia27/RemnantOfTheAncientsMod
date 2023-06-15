using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic
{
	[AutoloadEquip(EquipType.Body)]
	public class Sangar_Body : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sangar´s body");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le corps de Sangar");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Camiza de Sangar");
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

