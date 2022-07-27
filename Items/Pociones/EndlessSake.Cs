using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Pociones
{
	public class EndlessSake : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Sake");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sake eterno");
            Tooltip.SetDefault("");
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
            Item.buffType = BuffID.Tipsy; //Specify an existing buff to be applied when used.
            Item.buffTime = 15400; //The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }
       public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Sake, 30)
			.AddTile(TileID.Bottles)
			.Register();
        }
    }
}