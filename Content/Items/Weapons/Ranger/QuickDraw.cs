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

		int Usetime = 30;
		public override void SetDefaults()
		{
			Usetime = 30;

            Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 2;
			Item.height = 2;
			Item.useTime = Usetime;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.scale = 0.68f;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 20f;
			//Item.useAmmo = AmmoID.Bullet;
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoMax = 7;
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType = [14, 14, 14, 14];
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
			CurrentAmunation = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count;
			MaxAmunation = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoMax;
			ReloadCounter = Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter;
			

            Item ammo = Utils1.ChooseAmmo(player.HeldItem,AmmoID.Bullet);
			if (CurrentAmunation < MaxAmunation && ReloadCounter == ReloadCounterMax && ammo != null)
			{
				//Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo++;
				int index = player.FindItem(ammo.type);

				if (index != -1)
				{
					if(ammo.consumable) Utils1.ConsumeItem(player, player.inventory, index);
					if (Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count < MaxAmunation)
					{
						Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Add(ammo.shoot);
                    }	
				}
				

			}
            if (Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter < ReloadCounterMax)
            {
                Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter++;
            }
            else
            {
                Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter = 0;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			int currentAmmo = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count-1;
			Item.shoot = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType[currentAmmo];
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.RemoveAt(currentAmmo);
            return base.Shoot(player,source,position,velocity,type,damage,knockback);
        }
        public override bool CanShoot(Player player)
        {
			if (Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count <= 0)
			{   
                return false;
			}
			else
			{	
                return true;
			}
        }
        public override bool CanUseItem(Player player)
        {
			if (Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count > 0)
			{
				if (Main.mouseRight)
				{
					Item.useTime = Usetime / 3;

                    Item.useAnimation = Item.useTime * Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count;
				}
				else if (Main.mouseLeft)
				{
					Item.useTime = Usetime;
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
