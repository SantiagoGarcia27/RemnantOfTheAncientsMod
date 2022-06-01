using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using opswordsII.Buffs;
using opswordsII.Projectiles.Minioms;

namespace opswordsII.Items.Summon
{
    public class FrozenStafff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Frozen Staff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Báculo congelado");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Mrożony personel"); 
			Tooltip.SetDefault("Summons a Baby Frozen Asaulter to fight for you");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca a un asaltante bebé congelado para que luche por ti");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje Baby Frozen Assaulter, aby walczył dla ciebie"); 


			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}

		public override void SetDefaults() {
			Item.damage = 9;
			Item.knockBack = 3f;
			Item.mana = 10;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 4;
			Item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<FrozenMinionBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ProjectileType<FrozenMinion>();

			/*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
    		if (CalamityMod != null)
			Item.damage = 11;*/
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
		}
	}
}