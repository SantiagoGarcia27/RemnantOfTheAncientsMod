using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.pets
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