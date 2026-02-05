using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Boots
{
    [AutoloadEquip(EquipType.Shoes)]
    public class ExplorerBoots : ModItem
    {
        public override void SetStaticDefaults()
        {  
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 0, 5, 72);
            Item.rare = ItemRarityID.Pink;
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

            player.flowerBoots = true;
            int playerFlour = DistanceUtils.GetPlayerFlour(player);
            if (FastBlocks.Contains(playerFlour))
            {
                player.moveSpeed += 2.2f;
                player.DoBootsEffect(player.DoBootsEffect_PlaceFlamesOnTile);
            }
            else 
                player.accRunSpeed = 6f;

            player.autoJump = true;
            player.jumpSpeedBoost += 1.6f;
            player.extraFall += 10;


        }

        public override void AddRecipes() 
        {
            CreateRecipe()
            .AddIngredient<RunningShoes>()
            .AddIngredient(ItemID.FlowerBoots, 1)
            .AddIngredient(ItemID.AmphibianBoots, 1)
            .AddIngredient<BoneBar>(5)
            .AddTile(TileID.TinkerersWorkbench) 
            .Register();
        }
    }
}