using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.World;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Corrupt_Heart : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public int LifeMaxBonus = Reaper.ReaperMode ? 10 : 60;
        public int DamageReduction = 12;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeMaxBonus, DamageReduction);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 3, 70, 72);
            Item.rare = ItemRarityID.LightRed;
            Item.GetGlobalItem<CustomTooltip>().CompletistItem = true;
            Item.defense = 5;
            Item.accessory = true;
            Item.master = true;
            Item.masterOnly = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BrainOfConfusion, 1)
            .AddIngredient(ItemID.WormScarf, 1)
            .AddIngredient(ItemID.LifeCrystal, 10)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register(); 
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RemnantPlayer>().BrainDogde = true;
            player.endurance += 0.12f;
            player.statLifeMax2 += Reaper.ReaperMode? 10:60;
        }
    }
}