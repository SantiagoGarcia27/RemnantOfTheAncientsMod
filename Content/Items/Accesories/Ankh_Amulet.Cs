using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Ankh_Amulet : ModItem
    {
        int ManaBoost = 20;
        int ArmorPenetration = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ManaBoost, ArmorPenetration);
        public override void SetStaticDefaults()
        { 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 6, 70, 72);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 5;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
           CreateRecipe()
           .AddIngredient(ItemID.AnkhCharm, 1)
           .AddIngredient(ModContent.ItemType<Exotic_Amulet>(), 1)
           .AddTile(TileID.MythrilAnvil)
           .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RemnantPlayer>().ExoticA(2,23,20,5,Item);
            player.GetModPlayer<RemnantPlayer>().AnkInmunity();
        }
    }
}