using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.accesorios
{
    public class magic_wand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Wand");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Daguette magique");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Vara Mágica");

            Tooltip.SetDefault("Increases magic damage by 12%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts magiques de 12%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño magico en un 12%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 2, 70, 72);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) *= 1.12f;
        }
        public override void AddRecipes()  
        {
            CreateRecipe()
            .AddIngredient(ItemID.SoulofLight, 15)
            .AddIngredient(ItemID.SoulofMight, 3)
            .AddIngredient(ModContent.ItemType<magic_stick>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}