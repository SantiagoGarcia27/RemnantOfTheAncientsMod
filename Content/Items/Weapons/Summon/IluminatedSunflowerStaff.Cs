using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.SunFlower;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon
{
	public class IluminatedSunflowerStaff: ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Illuminated Sunflower Staff");
			//Tooltip.SetDefault("Summons a Sunflowr to protect you, " +
   //             "\nnearby players can gain a happiness buff,5 of defense and heal if they are in its radius");
			////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Báculo de girasol iluminado");
			////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un girasol para protegerte"+
   //             "\nlos jugadores cercanos del obtienen un beneficio de Felicidad,5 de defensa y pueden curarse si están en su radio");
   //        // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bâton Tournesol Illuminé");
   //         //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque un tournesol pour vous protéger"+
   //             "\nles joueurs à proximité peuvent gagner un buff de bonheur, 5 de défense et se soigner s'ils se trouvent dans son rayon");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.knockBack = 3f;
			Item.mana = 15;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.sentry = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 80, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<IlluminatedSunflowerMinionBuff>();
			Item.shoot = ProjectileType<IlluminatedSunFlowerMinion>();
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
            .AddIngredient(ItemType<SunflowerStaff>())
            .AddIngredient(ItemID.FallenStar, 6)
            .AddIngredient(ItemID.HealingPotion, 5)
             .AddIngredient(ItemID.Bone, 5)
            .AddTile(TileID.SkyMill)
            .Register();
        }
    }
}