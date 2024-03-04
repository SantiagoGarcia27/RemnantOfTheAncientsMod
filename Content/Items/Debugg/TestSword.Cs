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
            //Item.damage = 1;
            //Item.width = 40;
            //Item.height = 80;
            //Item.useTime = 20;
            //Item.useAnimation = 20;
            //Item.useStyle = ItemUseStyleID.Swing;
            //Item.knockBack = 8;
            //Item.value = Item.sellPrice(silver: 27);
            //Item.rare = ItemRarityID.White;
            //Item.accessory = true;
            //Item.UseSound = SoundID.Item1;
            //Item.autoReuse = false;
            //Item.shoot = ModContent.ProjectileType<InterceptionDrone>();
            //Item.shootSpeed = 5f;

            Item.damage = 91;
            Item.DamageType = DamageClass.Melee;
            Item.width = 600;
            Item.height = 600;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 1;
            Item.knockBack = 3;
            Item.value = 25700;
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.EnchantedBeam;
            Item.shootSpeed = 10;

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

			Main.NewText(player.GetModPlayer<RemnantFargosSoulsPlayer>().FrostBarrier);
			
		}
	}
}