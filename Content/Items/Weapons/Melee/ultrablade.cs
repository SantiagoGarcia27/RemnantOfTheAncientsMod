using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee.Swing;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
   
    public class Ultrablade : ModItem
	{
        public int attackType = 0; // keeps track of which attack it is
        public int comboExpireTimer = 0;
        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("UltraBlade");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 360;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item100;
			Item.value = Item.sellPrice(gold: 11);
			Item.rare = ItemRarityID.Red;
			Item.scale = 1.20f;
            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
            {
                if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ModContent.ProjectileType<UltraBladeS>();
            }
            else Item.shoot = ModContent.ProjectileType<UltraBladeS>();
			Item.shootSpeed = 13f;
			Item.noUseGraphic = true;
		}

		public override bool CanUseItem(Player player)
		{
			Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.position) * Item.shootSpeed;

			if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
			{
				Projectile.NewProjectile(Projectile.GetSource_None(), player.position, velocity, ModContent.ProjectileType<UltraBladeS>(), Item.damage, 2f, Main.myPlayer);
			}
			return base.CanUseItem(player);
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
            // Using the shoot function, we override the swing projectile to set ai[0] (which attack it is)
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<UltrabladeSwingProgectile>(), damage, knockback, Main.myPlayer);
            

            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<UltrabladeSwingingEnergySwordProjectile>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
            return false; // return false to prevent original projectile from being shot
        }

        public override void UpdateInventory(Player player)
        {
            //if (comboExpireTimer++ >= 120) // after 120 ticks (== 2 seconds) in inventory, reset the attack pattern
            //    attackType = 0;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.InfluxWaver, 1)
			.AddIngredient(ItemID.Meowmere, 1)
			.AddIngredient(ItemID.TerraBlade, 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
