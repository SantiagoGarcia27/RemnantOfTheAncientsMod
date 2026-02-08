using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Mounts;
using Terraria.GameContent.Creative;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Mounts
{
	public class StarRampage : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
            Item itemBase = new(ItemID.Starfury);
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
			Item.height = 30;
			Item.useTime = 30;
			Item.useAnimation = 30;
            Item.knockBack = 8;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(0, 3, 0, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = itemBase.UseSound;
            Item.autoReuse = true;
            Item.shoot = itemBase.shoot;
            Item.shootSpeed = 25f;
          //  Item.mountType = ModContent.MountType<Sword>();
            Item.shootsEveryUse = false;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.rand.Next(0, 4); i++)
            { 
                
                var p = Projectile.NewProjectile(source, position + new Vector2(Main.rand.Next(-10,10), 0) *16, velocity.RotatedByRandom(MathHelper.ToRadians(10)), type, damage, knockback, player.whoAmI,0,-100);
                Main.projectile[p].tileCollide = false;
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                player.mount.SetMount(ModContent.MountType<Sword>(), player);
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Starfury, 1)
			.AddIngredient(ItemID.CobaltBar,10)
			.AddIngredient(ItemID.IronBar, 10)
            .AddIngredient(ItemID.SoulofFlight, 40)
			.AddTile(TileID.Anvils)
			.Register();

            CreateRecipe()
            .AddIngredient(ItemID.Starfury, 1)
            .AddIngredient(ItemID.CobaltBar,10)
            .AddIngredient(ItemID.LeadBar, 10)
            .AddIngredient(ItemID.SoulofFlight, 40)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.Starfury, 1)
            .AddIngredient(ItemID.PalladiumBar, 10)
            .AddIngredient(ItemID.IronBar, 10)
            .AddIngredient(ItemID.SoulofFlight, 40)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.Starfury, 1)
            .AddIngredient(ItemID.PalladiumBar, 10)
            .AddIngredient(ItemID.LeadBar, 10)
            .AddIngredient(ItemID.SoulofFlight, 40)
            .AddTile(TileID.Anvils)
            .Register();
        }
	}
}