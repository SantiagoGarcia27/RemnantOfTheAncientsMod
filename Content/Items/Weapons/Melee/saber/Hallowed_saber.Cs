using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class Hallowed_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Sabebur");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabrebur");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sablebur");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Excalibur);
			Item.damage -= 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime -= 3;
			Item.useAnimation -= 3;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.scale = 1;
            Item.GetGlobalItem<CustomTooltip>().Saber = true;
			Item.shoot = ProjectileID.Excalibur;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            int p = Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile == true)
                {
                    DashPlayer.JumpDash(player, 1.5f, 0.75f);
					if (!player.HasBuff<HolyCouldownDebuff>()) 
					{
						player.AddBuff(BuffID.ShadowDodge, Utils1.FormatTime(0, 0, 0, 1));
						player.AddBuff(ModContent.BuffType<HolyCouldownDebuff>(), Utils1.FormatTime(0, 0, 0, 5));
                    }
                }
            }
            return true;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.HallowedBar, 12)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}

