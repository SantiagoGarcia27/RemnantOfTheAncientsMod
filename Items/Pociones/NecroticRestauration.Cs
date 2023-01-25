using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Pociones
{
    public class NecroticRestauration : BaseNecrotic
    {
        public override bool Endless => false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrotic Restauration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de restauration nécrotique");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de restauración necrótica");
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


    public class EndlessNecroticRestauration : BaseNecrotic
    {
        public override bool Endless => true;
        public override string Texture => "RemnantOfTheAncientsMod/Items/Pociones/NecroticRestauration";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Necrotic Restauration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de restauration nécrotique sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de restauración necrótica eterna");
            Tooltip.SetDefault("Teleports you to your last death");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous téléporte à votre dernière mort");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te teletransporta a tu última muerte ");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<NecroticRestauration>(), 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
    public abstract class BaseNecrotic : ModItem
    {
        public abstract bool Endless { get; }
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Teleports you to your last death");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous téléporte à votre dernière mort");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te teletransporta a tu última muerte ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = Endless ? 1 : 30;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IronskinPotion);
            Item.width = 26;
            Item.height = 32;
            Item.consumable = Endless? false: true;
            Item.useAnimation = 15;
            Item.useTime = 45;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = Endless? 1:9999;
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
    }
}
