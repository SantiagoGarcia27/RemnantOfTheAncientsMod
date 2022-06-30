using opswordsII.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.pets
{
	public class Yt : ModItem
	{
		public override void SetStaticDefaults() 
		{	
		 DisplayName.SetDefault("Yt"); 
		}
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ProjectileType<Projectiles.Pets.YtPet>();
			Item.buffType = BuffType<Buffs.YtBuff>();
		}
		/*public override void UseStyle(Player player) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(Item.buffType, 3600, true);}
		}*/
					public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.AdamantiteBar, 2)
			.AddIngredient(ItemID.CrimsonHeart, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		
		}
	}
}