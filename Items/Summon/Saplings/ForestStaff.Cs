using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using opswordsII.Buffs.acorns;

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

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
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
			Item.rare = 1;
			Item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<SaplingMinionBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ProjectileType<SaplingMinion>();
		}

		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}*/
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kostur Srebrnego Lasu"); 

			Tooltip.SetDefault("Summons a Silver Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño plateado para que luche por ti.");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje Srebrną Sadzonkę, która będzie dla ciebie walczyć"); 

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
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
			Item.rare = 1;
			Item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<SilverAcornBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ProjectileType<SaplingSilverMinion>();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"ForestStaff", 1)
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Laska Tungsten Forest"); 

			Tooltip.SetDefault("Summons a tungsten Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño de tungsteno para que luche por ti");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje wolframową sadzonkę, która będzie dla ciebie walczyć"); 

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
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
			Item.rare = 1;
			Item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<TungstenAcornBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ProjectileType<SaplingTungstenMinion>();
		}

		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}*/
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"ForestStaff", 1)
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kostur Złotego Lasu"); 

			Tooltip.SetDefault("Summons a Golden Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño dorado para que luche por ti.");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje Złotą Drzewko, która będzie dla ciebie walczyć"); 

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
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
			Item.rare = 1;
			Item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<GoldenAcornBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ProjectileType<SaplingGoldMinion>();
		}

		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}*/
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"ForestStaff", 1)
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Platynowy Lasek"); 

			Tooltip.SetDefault("Summons a Platinum Sapling to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño platinado para que luche por ti.");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje Platynową Drzewko, która będzie dla ciebie walczyć"); 

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
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
			Item.rare = 1;
			Item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<PlatinumAcornBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ProjectileType<SaplingPlatinumMinion>();
		}

		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}*/
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"ForestStaff", 1)
			.AddIngredient(ItemID.PlatinumBar, 8)
			.AddTile(TileID.Anvils)
			
			.Register();
		}
	}
}