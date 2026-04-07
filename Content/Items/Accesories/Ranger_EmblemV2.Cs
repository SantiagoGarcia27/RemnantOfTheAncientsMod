using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Ranger_EmblemV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        int damageBonus = 20;
        int critBonus = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonus, critBonus);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FragmentVortex, 10) 
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.RangerEmblem, 1)
            .AddIngredient(ItemID.LunarBar, 10) 
            .AddTile(TileID.LunarCraftingStation) 
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) +=  (float)(damageBonus/100f);
            player.GetCritChance(DamageClass.Ranged) += critBonus;
        }
    }
}