using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Pociones
{
    public abstract class BasePotions : ModItem
    {      
        public abstract int TextureID { get; }
        public abstract int ItemId { get; }
        public abstract int BuffId { get; }
        public override string Texture => "Terraria/Images/Item_" + TextureID;

        public override void SetStaticDefaults()
        {
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffId;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemId, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }

    public abstract class BaseModPotions : ModItem
    {
        public abstract int ItemId { get; }
        public abstract int BuffId { get; }

        public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = BuffId;
            Item.buffTime = 75400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemId, 30)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}