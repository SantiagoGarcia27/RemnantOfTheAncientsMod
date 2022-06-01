using opswordsII.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.pets
{
	public class Twitch : ModItem
	{
		public override void SetStaticDefaults() 
		{	
		 DisplayName.SetDefault("Follow"); 
		}
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ProjectileType<Projectiles.Pets.TwitchPet>();
			Item.buffType = BuffType<Buffs.YoutubeMoradoBuff>();
		}
		/*public override void UseStyle(Player player) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(Item.buffType, 3600, true);}
		}*/
					public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.OrichalcumBar, 5)
			.AddIngredient(ItemID.ShadowOrb, 1)
			.AddTile(TileID.MythrilAnvil)
			
			.Register();
		
		}
	}
}