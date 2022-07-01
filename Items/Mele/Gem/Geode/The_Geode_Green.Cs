using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem.Geode
{
	public class The_Geode_Green : ModItem
	{
		public override void SetStaticDefaults()
		{
	        DisplayName.SetDefault("The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
			Tooltip.SetDefault("Inflict poisoned on enemies and heal you with hits");
           	Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infligez du poison aux ennemis et vous soignez avec des coups");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije veneno a los enemigos y te curas cuando atacas");
		}
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1200;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			player.statLife += 2;
			target.AddBuff(BuffID.Poisoned, 400);
		}	
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"The_Geode",1)
            .AddIngredient(ItemID.LargeEmerald,1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
