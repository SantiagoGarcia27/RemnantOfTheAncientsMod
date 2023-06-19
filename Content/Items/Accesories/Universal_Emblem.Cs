using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ReLogic.Utilities;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Universal_Emblem : ModItem
    {
        int multiplier;
        public override void SetStaticDefaults()
        {
            multiplier =(ModLoader.TryGetMod("CalamityMod", out Mod mod))? 10: 15;
            DisplayName.SetDefault("Universal Emblem");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Embl�me Universel");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema Universal");
            Tooltip.SetDefault("Increases all damage by " + multiplier + "%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente tous les d�g�ts de " + multiplier + "%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta todo el da�o en un " + multiplier + "%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

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
            player.GetDamage(DamageClass.Generic) *= (ModLoader.TryGetMod("CalamityMod", out Mod mod))?1.10f : 1.15f;
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