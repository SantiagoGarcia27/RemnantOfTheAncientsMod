using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;
using RemnantOfTheAncientsMod.Projectiles.Melee;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem
{
	public class Topaz_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
	        DisplayName.SetDefault("Topaz Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée de topaze");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Topacio");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = 1200;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			if(RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.autoReuse = true;
				Item.scale = 2f;
			}
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
            //Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X + 5 * 16, target.position.Y), (player.position - target.position) * 0.25f, ProjectileID.TopazBolt, damage, Main.myPlayer, player.whoAmI);
            Vector2 perturbedSpeed = (player.position - target.position).RotatedBy(MathHelper.Lerp(-0, 0, 1)) * .2f;
            var p = Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X - ((target.direction * 6) * 16), target.position.Y), (perturbedSpeed) * 0.25f, ModContent.ProjectileType<SpiritGemSword>(), damage, Main.myPlayer, player.whoAmI);
            Main.projectile[p].GetAlpha(Color.Yellow);
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TinBar,8)
            .AddIngredient(ItemID.Topaz,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
