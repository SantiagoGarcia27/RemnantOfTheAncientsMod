using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.accesorios
{
    [AutoloadEquip(EquipType.Waist)]
    public class The_Completist : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Completist");
            Tooltip.SetDefault("A grand prize for those who made it to the top of the mod"
            + "\n Thanks to Johy315 (Quizo hacer trampa y no le salio)");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        { 
            Item.width = 10;
            Item.height = 14;
            Item.value = 0;
            Item.rare = -12;
            Item.accessory = true;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = TileType<Tiles.Completist>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.name == "Gordo con Tetas II" || player.name == "Johy315")
            {
                Tooltip.SetDefault("Quizo hacer trampa y no le salio"
                + "\n Aumenta el daño en un 1000000000000000%     Na mentira xd");
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Wood, 10)
            .AddIngredient(ItemID.GoldBar, 10)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
