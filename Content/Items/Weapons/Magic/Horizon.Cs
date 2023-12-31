using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;

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
			Item.damage = 100;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 12;
			Item.rare = 10;
			Item.width = 30;
			Item.height = 34;
			Item.useTime = 19;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 6f;
			Item.useAnimation = 19;
			Item.shoot = ModContent.ProjectileType<NebulaTyphoon>();
			Item.value = Item.sellPrice(gold: 10);
			Item.autoReuse = true;
			Item.reuseDelay = 0;
            Item.knockBack = 0.1f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if (player.altFunctionUse != 2)
            {
                Item.damage = 150;
                Item.useAnimation = 10;//10
                Item.useTime = Item.useAnimation;
                Item.noUseGraphic = true;
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, velocity, ProjectileID.LastPrism, damage, knockback);
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, velocity, ProjectileID.LaserMachinegun, damage, knockback);
                return false;
            }
            else
            {
                Item.noUseGraphic = false;
                Item.useAnimation = 19;
                Item.useTime = Item.useAnimation;
                if (Main.rand.NextBool(10))
                {
                    Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, velocity, ModContent.ProjectileType<NebulaTyphoon_Strong>(), (int)(damage / 2f), knockback);
                }
                else
                {
                    Projectile.NewProjectileDirect(Projectile.GetSource_None(), position, velocity, type, damage / 4, knockback);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<HorizonHeldProj>()] <= 0)
                {
                    var p = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<HorizonHeldProj>(), 0, 0, player.whoAmI, 3);
                    p.timeLeft = 70;
                }
            }
            return false;
		}
        public override bool AltFunctionUse(Player player) => true;
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
     
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.WandofSparking, 1)
            .AddIngredient(ItemID.LaserMachinegun, 1)
            .AddIngredient(ItemID.RazorbladeTyphoon, 1)
            .AddIngredient(ItemID.NebulaBlaze, 1)
            .AddIngredient<RazorpineV2>()
            .AddIngredient(ItemID.LastPrism, 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
