using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Bloques.Relics
{
    internal class Frozen_Relic : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Master_Relic.Frozen_Relic_t>(), 0);
            Item.width = 30;
            Item.height = 44;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Assaulter relic");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Relique de l'Agresseur gelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Reliquia del Asaltante Congelado");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
}