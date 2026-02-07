using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.RemPlayer;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class LeatherQuiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
       
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(10);
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 0, 70, 72);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true; 
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.arrowDamage *= 1.10f;
            player.GetModPlayer<StatPlayer>().ArrowSpeedBonus += 0.10f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Leather, 5)
            .AddIngredient(ItemID.Silk, 5)
            .AddIngredient(ItemID.WoodenArrow, 10)
            .AddTile(TileID.Loom)
            .Register();
        }
    }
}