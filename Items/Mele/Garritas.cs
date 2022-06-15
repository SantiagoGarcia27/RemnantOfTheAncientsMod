using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele
{
	public class Garritas : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Claws");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "solar claws");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "griffes solaires");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Garras Solares");
		}
		public override void SetDefaults()
		{
			
			Item.damage = 800;
			Item.DamageType = DamageClass.Melee;
			Item.width = 10;
			Item.height = 10;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 1;
			Item.knockBack = 60;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = 11;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			/*Mod CalamityMod = ModLoader.TryGetMod("CalamityMod");
    		if (CalamityMod != null)
			Item.damage = 800;
			Item.scale = 1.28f;*/
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			target.AddBuff(BuffID.Daybreak, 240);
		}	
		

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.ShroomiteDiggingClaw,1)
			.AddIngredient(ItemID.FragmentSolar, 20)
			.AddIngredient(ItemID.LunarBar, 10)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
