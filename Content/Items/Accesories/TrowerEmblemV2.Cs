using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class TrowerEmblemV2 : ModItem
    {
        int damageBonus = 20;
        int critBonus = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonus, critBonus);
        public override void SetStaticDefaults()
        {
            string TextUs = ModLoader.TryGetMod("CalamityMod", out _) ? "rogue" : "throwing";
            string TextEs = ModLoader.TryGetMod("CalamityMod", out _) ? "de picaro" : "lanzable";

         
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.FragmentVortex, 10) 
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ModContent.ItemType<Neutrum_Fragment>(),10)
            .AddIngredient(ModContent.ItemType<TrowerEmblem>(),1)
            .AddIngredient(ItemID.LunarBar, 10) 
            .AddTile(TileID.LunarCraftingStation)   
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DamageClass damagetype = DamageClass.Throwing;
            player.GetDamage(damagetype) *= 1 + damageBonus / 100f;
            player.GetCritChance(damagetype) += critBonus;
        }
    }
}