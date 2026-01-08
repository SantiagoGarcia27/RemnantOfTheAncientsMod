using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Universal_EmblemV2 : ModItem
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
            Item.value = Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DamageClass damagetype = DamageClass.Summon;
            player.GetDamage(damagetype) *= 1 + damageBonus / 100f;
            player.GetCritChance(damagetype) += critBonus;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<Sorcerer_EmblemV2>())
            .AddIngredient(ItemType<Ranger_EmblemV2>())
            .AddIngredient(ItemType<Summoner_EmblemV2>())
            .AddIngredient(ItemType<TrowerEmblemV2>())
            .AddIngredient(ItemType<Warrior_EmblemV2>())
            .AddIngredient(ItemID.LunarBar, 10)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}