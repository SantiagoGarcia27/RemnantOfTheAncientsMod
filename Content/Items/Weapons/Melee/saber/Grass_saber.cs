using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class Grass_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Grass Saber");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre d'herbe");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Hierba");
			//Tooltip.SetDefault("Inflict poison on enemies");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du poison aux ennemis");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije veneno a los enemigos");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.BladeofGrass);
			Item.damage -= 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime -= 3;
			Item.useAnimation -= 3;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.scale = 1.28f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.scale = 1;
            Item.noMelee = false;
            Item.channel = false;
            Item.GetGlobalItem<CustomTooltip>().Saber = true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Poisoned, 80);
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (player.altFunctionUse == 2)
			{
				if (DistanceUtils.PlayerTouchFlour(player))
				{
					Projectile.NewProjectile(source, position + (new Vector2(3 * 16, 0) * player.direction), velocity, type, (int)(damage * 0.25f), knockback, Main.myPlayer, -0.07f * player.direction, 0, 0);
				}
			}
			else
			{
				Projectile.NewProjectile(source, position + (new Vector2(3 * 16, 0) * player.direction), velocity, type, (int)(damage * 0.25f), knockback, Main.myPlayer, -0.07f * player.direction, 0, 0);
			}
            return false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(1)) Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Grass);
		}
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile == true)
				{
					DashPlayer.JumpDash(player, 0.6f, 1.25f);
				}
			}
			return true;
		}
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.JungleSpores, 12)
			.AddIngredient(ItemID.Stinger, 12)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
