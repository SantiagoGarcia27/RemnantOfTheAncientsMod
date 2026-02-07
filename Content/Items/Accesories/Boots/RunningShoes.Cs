using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Boots
{
    [AutoloadEquip(EquipType.Shoes)]
    public class RunningShoes : ModItem
    {
        public override void SetStaticDefaults()
        {  
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;
            Item.value = Item.buyPrice(0, 0, 5, 72);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true; 
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            List<int> FastBlocks =
            [
                TileID.SnowBlock,
                TileID.Sand,
                TileID.Ash
            ];

            if (FastBlocks.Contains(DistanceUtils.GetPlayerFlour(player)))
            {
                player.moveSpeed += 1f;
                player.DoBootsEffect(player.DoBootsEffect_PlaceFlamesOnTile);
            }
            else 
                player.accRunSpeed = 6f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlurryBoots, 1)
            .AddIngredient(ItemID.SandBoots, 1)
            .AddIngredient(ItemID.FlameWakerBoots, 1)
            .AddRecipeGroup("anyCorruptBossScale", 5)
            .AddTile(TileID.TinkerersWorkbench) 
            .Register();
        }
    }
}