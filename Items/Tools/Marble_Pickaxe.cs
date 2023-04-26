using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Tools
{
	public class Marble_Pickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Marble Pickaxe");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche marbre");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico de mármol");
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
Item.pick = 43;
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
.AddIngredient(ItemID.Marble,12)
.AddIngredient(ItemID.Wood,4)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}