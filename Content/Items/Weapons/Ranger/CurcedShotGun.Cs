using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static FargowiltasSouls.FargoSoulsSets;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
	public class CurcedShotGun : ModItem
	{
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(5);
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
        }
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 62;
			Item.height = 26;
			Item.useTime = 46;
			Item.useAnimation = 46;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = 100000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = null;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 100f;
			Item.useAmmo = AmmoID.Bullet;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            Item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
            Item.ShotgunStats(BulletAmmount: 5, Unacurency: 15,CustomProjectileShoot: ProjectileID.CursedBullet);
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{  
            if (player.altFunctionUse == 2) return false;  
            return base.Shoot(player,source,position,velocity,type,damage,knockback);   
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= 0.05f;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
        float Timmer = 0;
        float TimmerMax = 30;
        public override void HoldItem(Player player)
        {
            TimmerMax = Item.useTime / 4;

            RemnantPlayer.GenericChargeCouldownMax = (int)TimmerMax;
            RemnantPlayer.GenericChargeCouldown = (int)Timmer;     
            if (player.channel && player.altFunctionUse == 2)
            {
                if (Timmer < TimmerMax)
                {
                    Timmer += 0.5f;
                }  
            }
            else if (Main.mouseRightRelease)
            {
                Timmer = 0;
            }
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CursedFlame, 40)
			.AddIngredient(ItemID.Shotgun, 1)
			.AddTile(TileID.MythrilAnvil)

			.Register();
		}
	}
}
