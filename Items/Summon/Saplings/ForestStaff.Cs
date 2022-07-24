using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using opswordsII.Buffs.acorns;
using opswordsII.Projectiles.Minioms.Saplings;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Summon.Saplings
{
    public class ForestStaff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Forest Staff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Laska lasu"); 
			Tooltip.SetDefault("Summons a Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño para que luche por ti.");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje drzewko, które będzie dla ciebie walczyć"); 
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; 
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 5;
			Item.knockBack = 3f;
			Item.mana = 5;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<SaplingMinionBuff>();
			Item.shoot = ProjectileType<SaplingMinion>();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Acorn, 5)
			.AddIngredient(ItemID.Wood, 10)
			.AddTile(TileID.WorkBenches)			
			.Register();
		}
	}
	public class ForestStaff_S : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Silver Forest Staff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del Bosque Plateado");
			Tooltip.SetDefault("Summons a Silver Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño plateado para que luche por ti.");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 7;
			Item.knockBack = 3f;
			Item.mana = 5;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<SilverAcornBuff>();
			Item.shoot = ProjectileType<SaplingSilverMinion>();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			return false;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<ForestStaff>())
			.AddIngredient(ItemID.SilverBar, 8)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}

	public class ForestStaff_T : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Tungsten Forest Staff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque de tungsteno");
			Tooltip.SetDefault("Summons a tungsten Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño de tungsteno para que luche por ti");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 8;
			Item.knockBack = 3f;
			Item.mana = 5;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<TungstenAcornBuff>();
			Item.shoot = ProjectileType<SaplingTungstenMinion>();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			return false;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<ForestStaff>())
			.AddIngredient(ItemID.TungstenBar, 8)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}

	public class ForestStaff_G : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Golden Forest Staff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque Dorado");
			Tooltip.SetDefault("Summons a Golden Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño dorado para que luche por ti.");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 9;
			Item.knockBack = 3f;
			Item.mana = 5;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<GoldenAcornBuff>();
			Item.shoot = ProjectileType<SaplingGoldMinion>();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			return false;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<ForestStaff>())
			.AddIngredient(ItemID.GoldBar, 8)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
	public class ForestStaff_P : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Platinum Forest Staff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque Platinado");
			Tooltip.SetDefault("Summons a Platinum Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño platinado para que luche por ti.");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; 
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 10;
			Item.knockBack = 3f;
			Item.mana = 5;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<PlatinumAcornBuff>();
			Item.shoot = ProjectileType<SaplingPlatinumMinion>();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
			return false;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) 
		{
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<ForestStaff>())
			.AddIngredient(ItemID.PlatinumBar, 8)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}