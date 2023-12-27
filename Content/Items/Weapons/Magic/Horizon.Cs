using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class Horizon : ModItem
	{

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.staff[Item.type] = true;
        }

		public override void SetDefaults()
		{
			Item.damage = 80;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 9;
			Item.rare = 10;
			Item.width = 30;
			Item.height = 34;
			Item.useTime = 19;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 10f;
			Item.useAnimation = 19;
			Item.shoot = ProjectileID.Typhoon;
			Item.value = Item.sellPrice(gold: 10);
			Item.autoReuse = true;
			Item.reuseDelay = 0;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if (player.altFunctionUse != 2)
            {
                Item.noUseGraphic = true;
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, velocity, ProjectileID.LastPrism, damage, knockback);
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, velocity, ProjectileID.LaserMachinegun, damage, knockback);
                return false;
            }
            else
            {
                float numberProjectiles = 5;
                float rotation = MathHelper.ToRadians(4);
                position += Vector2.Normalize(velocity) * 45f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    Projectile.NewProjectileDirect(source, position, perturbedSpeed * 5, ProjectileID.PineNeedleFriendly, Item.damage + 10, knockback, player.whoAmI); 
                }

                Item.noUseGraphic = false;
                Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float ceilingLimit = target.Y;
                position = player.Center + new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                ShootRain(player, position, target, ref velocity, 619, damage, knockback, ceilingLimit, 3, 1,600,27);      
                ShootRain(player, position - new Vector2(0,50 * 16), target, ref velocity, ProjectileID.Spark, damage, knockback, ceilingLimit, 3, -1,10,27);


                if (player.ownedProjectileCounts[ModContent.ProjectileType<HorizonHeldProj>()] <= 0)
                {
                    var p = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<HorizonHeldProj>(), 0, 0, player.whoAmI, 3);
                    p.timeLeft = 70;
                }
            }
            return true;
		}
        public override bool AltFunctionUse(Player player) => true;
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public void ShootRain(Player player, Vector2 position, Vector2 target, ref Vector2 velocity, int type, int damage, float knockback, float ceilingLimit, int numOfProjectiles, int vertical, int timeLeft, int localIa2 = 0)
        {
            if (ceilingLimit > player.Center.Y + (200f * vertical))
            {
                ceilingLimit = player.Center.Y + (200f * vertical);
            }
            for (int i = 0; i < numOfProjectiles; i++)
            {
                position.Y += (100 * i) * vertical;
                Vector2 heading = target - position;

                if (vertical == 1)
                {
                    if (heading.Y > 0f)
                    {
                        heading.Y *= -1f;
                    }

                    if (heading.Y > 20f)
                    {
                        heading.Y = 20f;
                    }
                }
                else
                {
                    if (heading.Y < 0f)
                    {
                        heading.Y *= -1f;
                    }

                    if (heading.Y < 20f)
                    {
                        heading.Y = 20f;
                    }
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                var p = Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
                p.tileCollide = false;
                p.localAI[2] = localIa2;
                p.timeLeft = timeLeft;



                //ProjectileID.NebulaArcanumSubshot
            }
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.WandofSparking, 1)
            .AddIngredient(ItemID.LaserMachinegun, 1)
            .AddIngredient(ItemID.RazorbladeTyphoon, 1)
            .AddIngredient(ItemID.NebulaArcanum, 1)
            .AddIngredient<RazorpineV2>()
            .AddIngredient(ItemID.LastPrism, 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
