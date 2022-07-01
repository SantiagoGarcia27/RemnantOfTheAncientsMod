using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem.Geode
{
	public class The_Geode_Red : ModItem
	{
		public override void SetStaticDefaults()
		{
			
	        DisplayName.SetDefault("The Geode");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");

			Tooltip.SetDefault("Inflict fire and Hell fire on enemies");
           	Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Inflige le feu et le feu de l'enfer aux ennemis");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego y fuego infernal a los enemigos");
		}
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1200;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 400);
			target.AddBuff(ModContent.BuffType<Buffs.Hell_Fire>(), 400);
	
		}	
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"The_Geode",1)
            .AddIngredient(ItemID.LargeRuby,1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
