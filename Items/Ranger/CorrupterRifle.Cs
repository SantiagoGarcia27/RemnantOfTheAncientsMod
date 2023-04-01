using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Ranger
{
    public class CorrupterRifle: ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Corruptor Musket");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Mousquet de corruption");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mosquete corruptor");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
Item.damage = 70;
Item.DamageType = DamageClass.Ranged;
Item.width = 2;
Item.height = 2;
Item.useTime = 30;
Item.useAnimation = 30;
Item.useStyle = ItemUseStyleID.Shoot;
Item.noMelee = true;
Item.knockBack = 5;
Item.value = 20000;
Item.rare = ItemRarityID.Yellow;
Item.UseSound = SoundID.Item38;
Item.autoReuse = true;
Item.shoot = ProjectileID.PurificationPowder;
Item.shootSpeed = 2f;
Item.useAmmo = AmmoID.Bullet;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 60;
            }
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
if (Main.rand.NextFloat() <= (float)1 / 1) Projectile.NewProjectile(source, position, velocity, ProjectileID.EatersBite, Damegscale(damage), 1, player.whoAmI);
return true;
		}
		public int Damegscale(int damage)
		{
if (RemnantOfTheAncientsMod.CalamityMod != null) return damage * 2;
else return damage;
		}
		public override Vector2? HoldoutOffset()
		{
return new Vector2(-10, 0);
		}
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.Musket, 1)
            .AddIngredient(ItemID.ScourgeoftheCorruptor, 1)
.AddTile(TileID.MythrilAnvil)
.Register();
		}
	}
}
 