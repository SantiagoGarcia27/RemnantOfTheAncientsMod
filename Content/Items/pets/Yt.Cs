using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Pets;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;

namespace RemnantOfTheAncientsMod.Content.Items.pets
{
	public class Yt : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Yt");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.WispinaBottle);
			Item.shoot = ProjectileType<YtPet>();
			Item.buffType = BuffType<YtBuff>();
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
			.AddIngredient(ItemID.AdamantiteBar, 2)
			.AddIngredient(ItemID.CrimsonHeart, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		}
	}
}