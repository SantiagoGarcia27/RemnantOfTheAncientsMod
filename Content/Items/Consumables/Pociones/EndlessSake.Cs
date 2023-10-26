using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class EndlessSake : BasePotions
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Items/Consumables/Pociones/EndlessSake";
        public override Item ItemBase => new(ItemID.Sake);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Sake");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sake eterno");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup("anyBeer", 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}