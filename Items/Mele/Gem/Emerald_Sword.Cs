using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem
{
	public class Emerald_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Emerald Sword");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée d'émeraude");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de esmeralda");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.autoReuse = true;
				Item.scale = 2f;
			}
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
            //Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X + 5 * 16, target.position.Y), (player.position - target.position) * 0.25f, ProjectileID.EmeraldBolt, damage, Main.myPlayer, player.whoAmI);
            Vector2 perturbedSpeed = (player.position - target.position).RotatedBy(MathHelper.Lerp(-0, 0, 1)) * .2f;
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 6) * 16), target.position.Y), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<EmeraldSpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
        }
	

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TungstenBar,8)
            .AddIngredient(ItemID.Emerald,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}