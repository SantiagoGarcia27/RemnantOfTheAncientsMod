using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using opswordsII.Projectiles;
using Terraria.DataStructures;
using opswordsII.Items.Items;

namespace opswordsII.Items.Ranger
{
	public class ultratiburonDOG : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("CalamityMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Punisher of Gods");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Punisher of Gods");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Castigadora de Dioses");
			
			Tooltip.SetDefault(""
			+ "\n90% chance of not spending ammo ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
			+ "\n90% de chances de ne pas dépenser de munitions ");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
			+ "\nProbabilidad del 90% de no gastar munición ");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item.damage = 530;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; 
            Item.height = 40; 
            Item.useTime = 2;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(1, 50, 2, 0);
            Item.rare = ItemRarityID.Red;
   		    Item.UseSound = SoundID.Item12;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder; 
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
			
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .90f;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 0);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat () <= (float) 1 / 100) Projectile.NewProjectile(source, position, velocity, ProjectileType<MisilUltra>(), 10955, 1, player.whoAmI);
			return true;
		}
		/*public override void AddRecipes()
		{

			CreateRecipe()
			.AddIngredient(null, "ultratiburon", 1)
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out Mod mod).ItemType("CosmiliteBar"), 15)
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out mod).ItemType("Phantoplasm"), 5)
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out  mod).ItemType("EndothermicEnergy"), 10)
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out  mod).ItemType("NightmareFuel"), 10)
			.AddIngredient(ModLoader.TryGetMod("CalamityMod", out  mod).ItemType("GalacticaSingularity"), 15)
			.AddIngredient(ItemType<CelestialAmalgamate>(), 3)
			.AddTile(ModLoader.TryGetMod("CalamityMod", out mod).TileType<DraedonsForge>())
			.Register();
			
		}*/
			
	}
}
