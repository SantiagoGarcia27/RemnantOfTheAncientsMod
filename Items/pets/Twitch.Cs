using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Items.pets
{
    public class Twitch : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Follow");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ProjectileType<Projectiles.Pets.TwitchPet>();
			Item.buffType = BuffType<Buffs.YoutubeMoradoBuff>();
		}
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600);
			}
		}
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