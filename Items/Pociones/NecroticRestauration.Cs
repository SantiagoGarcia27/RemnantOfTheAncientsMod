using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Pociones
{
    public class NecroticRestauration : ModItem
    {
    	private Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
	    {
	    	DisplayName.SetDefault("Necrotic Restauration Potion");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Potion de restauration nécrotique");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Poción de restauración necrótica");
	    	Tooltip.SetDefault("Teleports you to your last death");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous téléporte à votre dernière mort");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Te teletransporta a tu última muerte ");
	    } 

    	public override void SetDefaults()
	    {
	    	Item.CloneDefaults(ItemID.IronskinPotion);
	    	Item.width = 26;
	    	Item.height = 32;
	    	Item.consumable = true;
	    	Item.useAnimation = 15;
	    	Item.useTime = 45;
	    	Item.rare = ItemRarityID.Blue;
	    	Item.maxStack = 30;
	    	Item.healLife = 0;
	    	Item.buffType = 0;
	    	Item.buffTime = -1;
	    }

    	public override bool CanUseItem(Player player)
    	{
    		return player.showLastDeath;
    	}

    	public override bool? UseItem(Player player)
    	{
    		Vector2 newPos = new Vector2(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
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
}
