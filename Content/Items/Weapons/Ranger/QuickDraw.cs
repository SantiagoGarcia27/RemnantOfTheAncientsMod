using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global.Items;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class QuickDraw : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public float CurrentAmunation, MaxAmunation,ReloadCounter,ReloadCounterMax;

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 2;
			Item.height = 2;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.scale = 0.68f;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = AmmoID.Bullet;
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoMax = 7;
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo = 7;
			Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
			Item.autoReuse = true;
			ReloadCounterMax = Utils1.FormatTimeToTick(0, 0, 0, 2);
			ReloadCounter = 0;
		}
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
        public override void UpdateInventory(Player player)
        {
			CurrentAmunation = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo;
			MaxAmunation = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoMax;
			ReloadCounter = Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter;


            if (CurrentAmunation < MaxAmunation && Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter == ReloadCounterMax)
			{
                Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo++;
                Item item = null;
                int index = Utils1.SearchPlayerAmmoSlot(player, player.HeldItem.useAmmo, ref item);
				if (item.consumable)
				{
					if(player.inventory[index].stack > 0)
					player.inventory[index].stack--;
					else
					{
                        player.inventory[index].active = false;
						player.inventory[index].TurnToAir();
                    }
				}
			}
			if(Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter < ReloadCounterMax)
			{
				Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter++;
            }
			else
			{
				Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter = 0;
            }
        }
		int timmer;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {			    
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo--;
            return base.Shoot(player,source,position,velocity,type,damage,knockback);
        }
        public override bool CanShoot(Player player)
        {
			if (Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo <= 0)
			{   
                return false;
			}
			else
			{	
                return base.CanShoot(player);
			}
        }
        public override bool CanUseItem(Player player)
        {
			if (Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo > 0)
			{
				if (Main.mouseRight)
				{
					Item.useAnimation = (int)(Item.useTime * Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo);
				}
				else if (Main.mouseLeft)
				{
					Item.useAnimation = Item.useTime;
				}
				return true;
			}
			else
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FlintlockPistol, 2)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
