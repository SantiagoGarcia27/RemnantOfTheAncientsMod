using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using CalamityMod.Rarities;
using CalamityMod.Items.Materials;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
	[JITWhenModsEnabled("CalamityMod")]
    public class ultratiburonDOG : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("CalamityMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 210;//155
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 40;
			Item.useTime = 2;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(1, 50, 2, 0);
			Item.rare = ModContent.RarityType<DarkBlue>();
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
            if (Main.rand.NextFloat() <= (float)5 / 100) Projectile.NewProjectile(source, position, velocity / 4, ProjectileType<MisilDevourer>(), damage * 2, 1, player.whoAmI,0,8); 
            return true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public override void AddRecipes()
		{
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Recipe recipe = CreateRecipe()

				.AddIngredient(ItemType<Ultratiburon>());
				recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("CosmiliteBar"), 15);
				recipe.AddIngredient(RemnantOfTheAncientsMod.CalamityMod.Find<ModItem>("ShadowspecBar"), 5);
                recipe.AddTile(ExternalModCallUtils.GetTileFromMod(RemnantOfTheAncientsMod.CalamityMod, "DraedonsForge"));
                recipe.Register();
			}
		}

	}
}
