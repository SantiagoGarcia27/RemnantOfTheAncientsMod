using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon
{
    public class OutlawSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;

            Item.consumable = true;
            Item.maxStack = 999;

            Item.value = Item.buyPrice(gold: 1);
        }

        public override bool CanUseItem(Player player)
        {
            return !OutlawInvasionSystem.EventActive;
        }

        public override bool? UseItem(Player player)
        {
            OutlawInvasionSystem.StartEvent();

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Wood, 10)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
            base.AddRecipes();
        }
    }
}