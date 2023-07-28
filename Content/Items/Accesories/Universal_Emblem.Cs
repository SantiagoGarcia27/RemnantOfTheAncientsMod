using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ReLogic.Utilities;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Universal_Emblem : ModItem
    {
        int multiplier;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Universal Emblem");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème universel");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema Universal");
            //Tooltip.SetDefault(//Tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        //private string //Tooltip()
        //{
        //    return LocalizationHelper.IncreasedDamageBy//Tooltip(15, DamageClass.Generic);
        //}

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
            player.GetDamage(DamageClass.Generic) *= (ModLoader.TryGetMod("CalamityMod", out Mod mod))?1.15f : 1.15f;
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