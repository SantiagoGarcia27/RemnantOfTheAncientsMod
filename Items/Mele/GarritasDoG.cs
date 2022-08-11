/*using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using opswordsII.Projectiles;
using Terraria.Localization;

namespace opswordsII.Items.Mele
{
	public class GarritasDoG : ModItem
	{
		
		public override bool HasMod("CalamityMod")
		{
		return ModLoader.TryGetMod("CalamityMod", out Mod result) != false;
		}
		public override void SetStaticDefaults()
		{
			
			DisplayName.SetDefault("God Slayer Claws");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pazury Zabójcy Bogów");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Griffes de tueur de dieu");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Garras del asesinas de dioses");
		}
		public override void SetDefaults()
		{
			Item.damage = 900;
			Item.DamageType = DamageClass.Melee;
			Item.width = 10;
			Item.height = 10;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = 1;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(gold: 100);
			Item.rare = 12;
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<GodClaws>();


		}
	
	public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null, "Garritas", 1)
			
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out Mod result).ItemType, "CosmiliteBar", 15)
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out Mod result).ItemType("EndothermicEnergy"), 10)
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out Mod result).ItemType("NightmareFuel"), 10)
			.AddTile(ModLoader.TryGetMod("CalamityMod", out Mod result).TileType<DraedonsForge>())
			.Register();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) 
		 {	
				  target.AddBuff(ModLoader.TryGetMod("CalamityMod", out Mod result).BuffType("GodSlayerInferno"), 300);
		 }
	}
}
*/