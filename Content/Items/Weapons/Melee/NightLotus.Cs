using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.DataSet;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee.Swing;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class NightLotus : ModItem
	{
        public int attackType = 0;
        public int comboExpireTimer = 0;
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
            Item.shootSpeed = 9f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
            RemnantItemTags.SecondClickWeapons.Add(ModContent.ItemType<GravediggersSickle>());
            //Item.GetGlobalItem<RemnantGlobalItem>().SecondClickWeapons.Add(ModContent.ItemType<GravediggersSickle>());
            Item.shoot = ModContent.ProjectileType<NightLotusSickleSwingProjectile>();
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<NightLotusSickleProjectile>()] > 0) return false;
            return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)
        {
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NightLotusSickleProjectile>()] > 0) return false;

           
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                 Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<NightLotusSickleProjectile>(), damage, knockback, player.whoAmI,ai2: Type);

            }
            else
            {
                // Using the shoot function, we override the swing projectile to set ai[0] (which attack it is)
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, attackType);
                attackType = (attackType + 1) % 2; // Increment attackType to make sure next swing is different
                comboExpireTimer = 0; // Every time the weapon is used, we reset this so the combo does not expire
            }
            return false; // return false to prevent original projectile from being shot
        }

        public override void UpdateInventory(Player player)
        {
            if (comboExpireTimer++ >= 120) // after 120 ticks (== 2 seconds) in inventory, reset the attack pattern
                attackType = 0;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<GravediggersSickle>()
            .AddIngredient<NightBar>(5)
            .AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}

