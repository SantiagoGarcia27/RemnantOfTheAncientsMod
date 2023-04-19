using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Linq;
using CalamityMod.Tiles.Furniture.CraftingStations;
using RemnantOfTheAncientsMod.Projectiles.Melee;
using Mono.Cecil;

namespace RemnantOfTheAncientsMod.Items.Mele
{
	public class GarritasDoG : ModItem
	{
		public static int Attacks = 0;
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
			Item.damage = 500;//900
			Item.DamageType = DamageClass.Melee;
			Item.width = 10;
			Item.height = 60;
			Item.useTime = 10;
			Item.useAnimation = 10;//8
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(gold: 100);
			Item.rare = ItemRarityID.Red;
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
            {
                if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ProjectileType<GodClaws>();
            }
            else Item.shoot = ProjectileType<GodClaws>();
			//Item.shootSpeed = 5f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			var p = Projectile.NewProjectile(source, player.position - new Vector2(((7*16)*-player.direction), ((1f * 16) * Item.scale)), velocity, ProjectileType<GodClaws>(), Item.damage, Item.knockBack, Main.myPlayer);
			Main.projectile[p].direction = player.direction;
            return false;
        }
		public override bool CanShoot(Player player)
		{
			if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
			{
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;
                var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.position - new Vector2(((7 * 16) * -player.direction), ((1f * 16) * Item.scale)), velocity, ProjectileType<GodClaws>(), Item.damage, Item.knockBack, Main.myPlayer);
				Main.projectile[p].direction = player.direction;
			}
			return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ProjectileType<GodClaws>() && (n.ai[0] != 1f || n.ai[1] != 1f));
		}
		[JITWhenModsEnabled("CalamityMod")]
        public override void AddRecipes()
		{
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Recipe recipe = CreateRecipe()
				.AddIngredient(ItemType<Garritas>())
				.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("CosmiliteBar"), 15);
				//recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("EndothermicEnergy"), 10);
				//recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("NightmareFuel"), 10);
				recipe.AddTile(ModContent.TileType<CosmicAnvil>());
				recipe.Register();
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (RemnantOfTheAncientsMod.CalamityMod.TryFind("GodSlayerInferno", out ModBuff buff)) target.AddBuff(buff.Type, 300);
		}
	}
}
