using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Tools
{
	public class Tuxonite_pickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Tuxonite Pickaxe");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche Tuxonite");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico de tusonita");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 8;
Item.DamageType = DamageClass.Melee;
Item.width = 40;
Item.height = 40;
Item.useTime = 13; 
Item.useAnimation = 19;  
Item.pick = 59;
Item.useStyle = ItemUseStyleID.Swing; 
Item.knockBack = 2; 
Item.value = 1600;
Item.rare = ItemRarityID.White;
Item.UseSound = SoundID.Item1; 
Item.autoReuse = true; 
Item.useTurn = true;
		}

		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ModContent.ItemType<TuxoniteBar>(),12)
.AddIngredient(ItemID.Wood,4)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}