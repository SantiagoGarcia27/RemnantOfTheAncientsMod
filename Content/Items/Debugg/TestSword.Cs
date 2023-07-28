using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Debugg
{
	public class TestSword : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("OpswordsIIDebugMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("TestSword");
		}
		public override void SetDefaults()
		{
			Item.damage = 1;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<InterceptionDrone>();
			Item.shootSpeed = 5f;

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(source,position,velocity,type,damage,knockback);
            Projectile.NewProjectile(source, position + new Vector2(-70f,0f), velocity, ModContent.ProjectileType<InterceptionDrone>(), damage, knockback);
            return true;
        }
        public override bool? UseItem(Player player)
        {
           // Main.NewText(player.maxMinions);
            return base.UseItem(player);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			
				
			
		}
	}
}