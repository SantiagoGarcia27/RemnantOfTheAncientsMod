using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Universal_Emblem : ModItem
    {
        public override void SetStaticDefaults()
        {    
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        int damageBonus = 15;    
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonus);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 4, 40, 72); ;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += damageBonus / 100f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.RangerEmblem)
            .AddIngredient(ItemID.SorcererEmblem)
            .AddIngredient(ItemID.SummonerEmblem)
            .AddIngredient(ItemID.WarriorEmblem)
            .AddIngredient(ModContent.ItemType < TrowerEmblem>())
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}