using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class Granite_Broadsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Granite Broadsword");
			//Tooltip.SetDefault("Doubles its stats while in the granite biome.");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Epée large en granit");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Double ses statistiques dans le biome de granit.");
			// //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de granito");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Duplica sus estadisticas al estar en el bioma de granito");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		static float ogScale;
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			ogScale = Item.scale;
		}
		public override bool AltFunctionUse(Player player) => true;

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (player.ZoneGranite)
			{
				Item.damage = 24;
				Item.useTime = 15;
				Item.useAnimation = 15;
				Item.knockBack = 16;
				Item.scale = 2f;
			}
			else
			{
				Item.damage = 12;
				Item.useTime = 30;
				Item.useAnimation = 30;
				Item.knockBack = 8;
				Item.scale = ogScale;

			}
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.GraniteBlock, 20)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

