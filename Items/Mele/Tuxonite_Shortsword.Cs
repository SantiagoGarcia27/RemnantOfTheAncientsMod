using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;


namespace opswordsII.Items.Mele
{
	public class Tuxonite_Shortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Shortsword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada corta de Tusonita");
		}
		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 3;
			Item.knockBack = 8;
			Item.value = 1000;
			Item.rare = 0;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}

		/*public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			 target.AddBuff(BuffType<Buffs.Hell_Fire>(), 300);
		}	*/
	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"TuxoniteBar",7)
			.AddTile(TileID.Anvils)	
			.Register();
		}
	}
}

