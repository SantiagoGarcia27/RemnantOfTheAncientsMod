using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles.Melee;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class Permafrost : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Permafrost");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le pergélisol");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Permafrost");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.width = 72;
			Item.height = 72;
			Item.useTime = 40;
			Item.useAnimation = 25;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(silver: 10);
			Item.DamageType = DamageClass.Melee;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<FrostBeamF>();
			Item.shootSpeed = 7f;

            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 145;
            }

        }
        public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player)
		{
            if (player.altFunctionUse != 2)
            {
                Item.shoot = ProjectileType<FrostBeamF>();
                Item.useTime = 40;
                Item.useAnimation = 25;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.staff[Item.type] = false;
            }
            else
            {
                Item.shoot = ProjectileType<PermafrostRain>();
                Item.useTime = 85;
                Item.useAnimation = 85;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.staff[Item.type] = true;
                Item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }
            else
            {
                Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float ceilingLimit = target.Y;
                if (ceilingLimit > player.Center.Y - 200f)
                {
                    ceilingLimit = player.Center.Y - 200f;
                }
                // Loop these functions 3 times.
                for (int i = 0; i < 3; i++)
                {
                    position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                    position.Y -= 100 * i;
                    Vector2 heading = target - position;

                    if (heading.Y < 0f)
                    {
                        heading.Y *= -1f;
                    }

                    if (heading.Y < 20f)
                    {
                        heading.Y = 20f;
                    }

                    heading.Normalize();
                    heading *= velocity.Length();
                    heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
                }
            }
            return false;
        }
    }
}
