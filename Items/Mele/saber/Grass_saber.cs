using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
	public class Grass_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grass Saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre d'herbe");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Hierba");
			Tooltip.SetDefault("Inflict poison on enemies");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du poison aux ennemis");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije veneno a los enemigos");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item Base = new Item(ItemID.BladeofGrass);
			Item.damage = Base.damage - 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Base.value;
			Item.rare = Base.rare;
			Item.scale = 1.28f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 80);
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
