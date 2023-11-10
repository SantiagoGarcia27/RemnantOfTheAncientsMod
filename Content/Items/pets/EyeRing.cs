using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Pets;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;

namespace RemnantOfTheAncientsMod.Content.Items.pets
{
    public class EyeRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Magic Lettuce");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Laitue magique");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lechuga mágica");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ProjectileType<DakoPetProjectile>();
			Item.buffType = BuffType<DakoVanityBuff>();
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
            .AddIngredient(ItemID.EyeOfCthulhuPetItem, 1)
            .AddIngredient(ItemID.BandofStarpower, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}