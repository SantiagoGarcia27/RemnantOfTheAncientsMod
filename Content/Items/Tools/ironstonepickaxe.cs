using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
	public class ironstonepickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ironstone Pickaxe");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kilof Skamieniałego Żelaza");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pioche en fer pétrifié");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pico de hierro petrificado");
			//Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			/*// //DisplayName.AddTranslation(GameCulture.Russian, "Окаменелая железная кирка");
			// //DisplayName.AddTranslation(GameCulture.Chinese, "硅化铁镐");*/
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.IronPickaxe);
			Item.damage = 4;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 9; 
			Item.useAnimation = 9; 
			Item.pick -= 5;
			Item.useStyle = ItemUseStyleID.Swing; 
			Item.knockBack = 0; 
			Item.value = 1000;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
            Item.tileBoost += 2;
        }

		public override void AddRecipes() //Crafteo del objeto
		{
			CreateRecipe()
			.AddIngredient(ItemID.StoneBlock, 30)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .AddRecipeGroup(RecipeGroupID.Wood, 15)
            .AddTile(TileID.Anvils)
			.Register();
		}
	}
}