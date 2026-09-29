using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class EndlessSake : BasePotions
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Items/Consumables/Pociones/EndlessSake";
        public override Item ItemBase => new(ItemID.Sake);
        public override char GramaticalCorrection => 'M';
        public override void SetStaticDefaults()
        {
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