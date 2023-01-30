using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using Microsoft.Xna.Framework;

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
			Item.width = 40;
			Item.height = 40;
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
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X + 5 * 16, target.position.Y), (player.position - target.position) * 0.25f, ProjectileID.TopazBolt, damage, Main.myPlayer, player.whoAmI);
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
