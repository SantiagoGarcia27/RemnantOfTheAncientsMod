using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class FireRift : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tyrant blast");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wybuch tyrana");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Explosion de tyran");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rafaga tirana");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 10;
			Item.rare = ItemRarityID.LightRed;
			Item.width = 72;
			Item.height = 74;
			Item.useTime = 15;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 15;
			Item.shoot = ProjectileID.Flames;
			Item.value = Item.sellPrice(gold: 5);
			Item.autoReuse = true;
			Item.scale /= 1.5f;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				Item.shootSpeed = 10f;
				Vector2 pos = position + new Vector2(10 * 16f, 0f);
				for (int i = -10; i <= 10; i += 20)
				{
                    SpawnWeakAttack(i, 0, player, source, position, velocity, type, damage, knockback);
                }
                for (int i = -10; i <= 10; i += 20)
                {
                    SpawnWeakAttack(0, i, player, source, position, velocity, type, damage, knockback);
                }

                int p = Projectile.NewProjectile(source,position, velocity, type, damage/5, knockback,player.whoAmI,0);
                Main.projectile[p].scale = 10f;
				Main.projectile[p].timeLeft = 20000;          
			}
			else
			{
                Item.shootSpeed = 15f;
                int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1);         
			}
            return false;
        }
        public override bool AltFunctionUse(Player player) => true;
        public void SpawnWeakAttack(float x, float y, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!DistanceUtils.ExistTile(position + new Vector2(x * 16f, y * 16f)))
            {
                int p = Projectile.NewProjectile(source, position + new Vector2(x * 16f, y * 16f), velocity, type, damage / 5, knockback, player.whoAmI, 0);
                //Main.projectile[p].penetrate *= 3;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Flamelash, 1)
            .AddIngredient(ItemID.SoulofNight, 16)
            .AddIngredient(ItemID.DarkShard,2)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}