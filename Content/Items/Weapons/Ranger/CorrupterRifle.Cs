using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;
using System;
using RemnantOfTheAncientsMod.Common.Global.Items;
using Terraria.Audio;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class CorrupterRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public override bool AltFunctionUse(Player player) => true;
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
			Item.UseSound = null;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 2f;
			Item.useAmmo = AmmoID.Bullet;
            Item.channel = true;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            Item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.damage = 60;
			}
		}
		bool charged = false;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			if(player.altFunctionUse == 2)
			{
				
				if (charged)
				{
					SoundEngine.PlaySound(SoundID.Item38);
                    for (int i = 0; i <= 4; i++)
					{
						Projectile.NewProjectile(source, position, velocity * 2, ProjectileID.EatersBite, Damegscale(damage * 2), 1, player.whoAmI);
					}
					charged = false;
				}
				return false;
            }
			else
			{
                SoundEngine.PlaySound(SoundID.Item38);
                Projectile.NewProjectile(source, position, velocity, ProjectileID.EatersBite, Damegscale(damage), 1, player.whoAmI);
            }
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
		public static int Damegscale(int damage)
		{
            return (RemnantOfTheAncientsMod.CalamityMod != null)? damage * 2: damage;		
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
		float Timmer = 0;
		float TimmerMax = 30;
        float TimmerDust = 0;
        float TimmerDustMax = (int)Utils1.FormatTimeToTick(0, 0, 0, 2);
        public override void HoldItem(Player player)
		{
			TimmerMax = Item.useTime / 4;

            RemnantPlayer.GenericChargeCouldownMax = (int)TimmerMax;
			RemnantPlayer.GenericChargeCouldown = (int)Timmer;
            TimmerDustMax = TimmerMax / 4;
            if (player.channel && player.altFunctionUse == 2)
			{
				if (Timmer >= TimmerMax)
				{
					charged = true;
                    Timmer = 0;
				}
				else
				{
                    Timmer++;
                    SpawnDust(player);
                }
			}
			else if (Main.mouseRightRelease)
			{
				Timmer = 0;
				charged = false;
            }
        }
        public void SpawnDust(Player player)
        {
            if (TimmerDust >= TimmerDustMax)
            {
                for (int i = 0; i < 360; i += 36)
                {
                    double angle = MathHelper.ToRadians(i);
                    Vector2 position = player.Center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 50;
                    Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                    Dust d = Dust.NewDustPerfect(position, DustID.CursedTorch, direction * 2f, 0, Color.White, 1f);
                    d.noGravity = true;
                }
                TimmerDust = 0;
            }
            else
            {
                TimmerDust++;
            }
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
 