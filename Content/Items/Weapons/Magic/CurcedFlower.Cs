using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class CurcedFlower : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Flower of Cursed Flame");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kwiat przeklętego płomienia");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fleur de flamme maudite");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Flor de llama maldita");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }
		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 18;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 23;
			Item.useAnimation = 23;
			Item.useStyle = 1;
			Item.knockBack = 1;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item20;
			Item.value = 50000;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = ProjectileID.CursedFlameFriendly;
			Item.shootSpeed = 10f;
			Item.noUseGraphic = false;
            Item.channel = true;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;

            if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.damage = 80;
			}
		}
        public override bool AltFunctionUse(Player player) => true;


		
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                Item.damage = RemnantOfTheAncientsMod.CalamityMod != null ? 80 : 60;
                Item.useTime = 23;
                Item.useAnimation = 23;
                Item.mana = 18;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = ProjectileID.CursedFlameFriendly;
                Item.shootSpeed = 10f;
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
            else
            {
				if (player.whoAmI == Main.myPlayer)
				{
					Item.damage = RemnantOfTheAncientsMod.CalamityMod != null ? 40 : 35;
					Item.useTime = 6;
					Item.useAnimation = 6;
					Item.useStyle = 5;
					Item.shoot = ProjectileID.Flames;
					//Item.shoot = ModContent.ProjectileType<CustomFlameTrowerProj>();
					Item.shootSpeed = 2f;
					Item.channel = true;
					Item.mana = 3;
					int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 2, 3000);
					Main.projectile[p].DamageType = DamageClass.Magic;
					if (player.ownedProjectileCounts[ModContent.ProjectileType<FlowerHeldProj>()] <= 0)
					{
						Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FlowerHeldProj>(), 0, 0, player.whoAmI, 3);
					}
				}
                return false;
            }
           

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			if(type == ProjectileID.Flames)
			{

			}
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CursedFlame, 20)
            .AddIngredient(ItemID.SoulofNight, 5)
            .AddIngredient(ItemID.FlowerofFire, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}