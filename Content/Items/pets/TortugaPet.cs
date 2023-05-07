using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Pets;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;

namespace RemnantOfTheAncientsMod.Content.Items.pets
{
	public class TortugaPet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Lettuce");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Laitue magique");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lechuga mágica");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ProjectileType<TortugaPetProjectile>();
			Item.buffType = BuffType<TortugaPetBuff>();
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