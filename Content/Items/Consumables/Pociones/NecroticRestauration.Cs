using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class NecroticRestauration : ModItem
    {
        
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Necrotic Restauration Potion");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de restauration nécrotique");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de restauración necrótica");
            //Tooltip.SetDefault("Teleports you to your last death");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous téléporte à votre dernière mort");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te teletransporta a tu última muerte ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IronskinPotion);
            Item.buffType = 0;
            Item.buffTime = -1;
        }
        public override bool CanUseItem(Player player) => player.showLastDeath;

        public override bool? UseItem(Player player)
        {
            Vector2 newPos = new(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
            player.Teleport(newPos);
            return base.ConsumeItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup("Tumbas", 1)
            .AddIngredient(ItemID.Fireblossom, 2)
            .AddIngredient(ItemID.Deathweed, 2)
            .AddTile(TileID.Bottles)
            .Register();

        }
    }

    public class EndlessNecroticRestauration : ModItem
    {
        public override string Texture => "RemnantOfTheAncientsMod/Content/Items/Consumables/Pociones/NecroticRestauration";
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("ModelEndless Necrotic Restauration Potion");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de restauration nécrotique sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de restauración necrótica eterna");
            //Tooltip.SetDefault("Teleports you to your last death");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous téléporte à votre dernière mort");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te teletransporta a tu última muerte ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<NecroticRestauration>());
            Item.consumable = false;
            Item.maxStack = 1;
        }
        public override bool? UseItem(Player player)
        {
            Vector2 newPos = new(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
            player.Teleport(newPos);
            return base.ConsumeItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<NecroticRestauration>(), 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
