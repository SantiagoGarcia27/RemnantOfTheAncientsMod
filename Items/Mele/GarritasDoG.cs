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

		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("CalamityMod", out mod);
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
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(gold: 100);
			Item.rare = ItemRarityID.Red;
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<GodClaws>();


		}

		public override void AddRecipes()
		{
			if (opswordsII.CalamityMod != null)
			{
				Recipe recipe = CreateRecipe()
				.AddIngredient(ItemType<Garritas>())
				.AddIngredient(opswordsII.CalamityMod.Find<ModItem>("CosmiliteBar"), 15);
				recipe.AddIngredient(opswordsII.CalamityMod.Find<ModItem>("EndothermicEnergy"), 10);
				recipe.AddIngredient(opswordsII.CalamityMod.Find<ModItem>("NightmareFuel"), 10);
				recipe.AddTile(TileID.LunarCraftingStation);
				recipe.Register();
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (opswordsII.CalamityMod.TryFind("GodSlayerInferno", out ModBuff buff)) target.AddBuff(buff.Type, 300);	
		}
	}
}
