using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global.Items;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;
using System;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class QuickDraw : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public float CurrentAmunation, MaxAmunation,ReloadCounter,ReloadCounterMax;
        private bool IsBurst = false;

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
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = AmmoID.Bullet;
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoMax = 6;
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType = [14, 14, 14, 14];
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
			Item.autoReuse = true;
			ReloadCounter = 0;
		}
        public override bool AltFunctionUse(Player player) => true;

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
        public override void UpdateInventory(Player player)
        {
            if(player.HeldItem.type == ModContent.ItemType<QuickDraw>()) return;
            Recharge(player, Utils1.FormatTimeToTick(0, 0, 0, 3));
        }
        public override void HoldItem(Player player)
        {
            Recharge(player, Utils1.FormatTimeToTick(0, 0, 0, 0.9f));
            base.HoldItem(player);
        }

		void Recharge(Player player, float timmer)
		{
            CurrentAmunation = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count;
            MaxAmunation = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoMax;
            ReloadCounter = Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter;


            Item ammo = Utils1.ChooseAmmo(player, player.HeldItem);
            if (CurrentAmunation < MaxAmunation && ReloadCounter == timmer && ammo != null)
            {
                //Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmo++;
                int index = player.FindItem(ammo.type);

                if (index != -1)
                {
                    if (ammo.consumable) Utils1.ConsumeItem(player, player.inventory, index);
                    List<int> aux = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType;
                    if (aux.Count < MaxAmunation)
                    {
                        aux.Add(ammo.shoot);
                    }
                    Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType = aux;
                }
               

            }
            if (Item.GetGlobalItem<RemnantGlobalItem>().ReloadCounter < timmer)
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
			type = Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType[currentAmmo];
			Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.RemoveAt(currentAmmo);
            float damageMultiplier = IsBurst ? 0.9f : 1f;
            damage = (int)(damage * damageMultiplier);
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;//base.Shoot(player,source,position,velocity,type,damage, knockback);
        }
        public override bool CanShoot(Player player)
        {
            return Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count > 0;
        }
        public override bool CanUseItem(Player player)
        {
			if (Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count > 0)
			{
				if (Main.mouseRight)
				{
					Item.useTime = Usetime / 3;
                    Item.useAnimation = Item.useTime * Item.GetGlobalItem<RemnantGlobalItem>().CurrentAmmoType.Count;
                    IsBurst = true;
                }
				else if (Main.mouseLeft)
				{
					Item.useTime = Usetime;
                    Item.useAnimation = Item.useTime;
                    IsBurst = false;

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
	}
}
