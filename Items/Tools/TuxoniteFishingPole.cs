using RemnantOfTheAncientsMod.Projectiles.Bobbers;
using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Tools
{
    public class TuxoniteFishingPole : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Tuxonite Fishing Pole");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Canne à pêche en tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Caña de pescar de tusonita");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.width = 24;
Item.height = 28;
Item.useAnimation = 8;
Item.useTime = 8;
Item.useStyle = ItemUseStyleID.Swing;
Item.UseSound = SoundID.Item1;
Item.rare = ItemRarityID.White;
Item.fishingPole = 21;
Item.shootSpeed = 15f;
Item.shoot = ModContent.ProjectileType<TuxoniteBobber>();
Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ModContent.ItemType<TuxoniteBar>(),7)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}