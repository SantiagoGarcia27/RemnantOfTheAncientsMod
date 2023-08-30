using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon
{
	public class IronBallistaStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Rolling Crossbow Staff");
			//Tooltip.SetDefault("Summons a Rolling Crossbow"+
   //             "\nUse 2 minion slot");
			////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Báculo de Ballesta con ruedas");
			////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca a una ballesta con ruedas para que luche por ti"+
			//	"\nUtiliza 2 espacios de subditos");
   //        // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bâton gelé");
   //         //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque une arbalète roulante" + 
			//	"\nUtiliser 2 emplacements de serviteur");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;	
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.knockBack = 3f;
			Item.mana = 10;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = 0;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<IronBallistaBuff>();
			Item.shoot = ProjectileType<IronBallistMinion>();
			//if (ModLoader.TryGetMod("CalamityMod", out Mod mod)) Item.damage = 11;
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
            .AddIngredient(ModContent.ItemType<Reinforced_ironBar>(),5)
            .AddIngredient(ItemID.Minecart)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}