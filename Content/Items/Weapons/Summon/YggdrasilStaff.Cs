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
	public class YggdrasilStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yggdrasil Staff");
			Tooltip.SetDefault("Summons a Yggdrasil to protect you, " +
               "\nnearby players can gain a happiness buff,15 of defense,10% damage,5% damage reduction and heal if they are in its radius");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bâton de Yggdrasil");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque Yggdrasil pour vous protéger" +
                "\nles joueurs à proximité peuvent gagner un buff de bonheur, 15 de défense, 10% de dégâts, 5% de réduction des dégâts et soigner s'ils se trouvent dans son rayon");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Báculo de Yggdrasil");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al Yggdrasil para protegerte" +
                "\nlos jugadores cercanos del obtienen un beneficio de Felicidad,15 de defensa,10% de daño,5%  reducción de daño y pueden curarse si están en su radio");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.knockBack = 3f;
			Item.mana = 30;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.sentry = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = BuffType<YggdrasilMinionBuff>();
			Item.shoot = ProjectileType<YggdrasilMinion>();
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
            .AddIngredient(ItemID.Ectoplasm,15)
            .AddIngredient(ItemID.LifeFruit, 5)
            .AddIngredient(ItemType<SacredFlowerStaff>(), 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}