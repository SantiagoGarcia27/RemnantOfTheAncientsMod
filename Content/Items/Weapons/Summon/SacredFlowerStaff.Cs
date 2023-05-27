using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon
{
	public class SacredFlowerStaff: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sacred Flower Staff");
			Tooltip.SetDefault("Summons a sacred flower to protect you, " +
               "\nnearby players can gain a happiness buff,10 of defense,5% damage and heal if they are in its radius");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Báculo de flor sacra");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque une fleur sacrée pour vous protéger" +
                "\nles joueurs à proximité peuvent gagner un buff de bonheur, 10 de défense, 5% de dégâts et soigner s'ils se trouvent dans son rayon");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bâton de la fleur sacrée");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca una flor sacra para protegerte" +
                "\nlos jugadores cercanos del obtienen un beneficio de Felicidad,10 de defensa,5% de daño y pueden curarse si están en su radio");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.knockBack = 3f;
			Item.mana = 20;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.sentry = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = 5;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<SacredFlowerMinionBuff>();
			Item.shoot = ProjectileType<SacredFlowerMinion>();
			//if (ModLoader.TryGetMod("CalamityMod", out Mod mod)) Item.damage = 11;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;
            player.UpdateMaxTurrets();
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
            .AddIngredient(ItemID.GreaterHealingPotion,5)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemType<IluminatedSunflowerStaff>(), 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}