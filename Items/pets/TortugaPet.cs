using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using System.Drawing;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace opswordsII.Items.pets
{
	public class TortugaPet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Lettuce");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Magiczna sałata");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Laitue magique");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lechuga mágica");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ProjectileType<Projectiles.Pets.TortugaPet>();
			Item.buffType = BuffType<Buffs.TortugaPetBuff>();
		}
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600);
			}
		}
	}
}