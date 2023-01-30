using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem
{
	public class Amethyst_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amethyst Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ametystowy miecz");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée d'améthyste");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Amatista");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.knockBack = 1;
			Item.value = 1000;
			Item.rare = 1;
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
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X + 5*16, target.position.Y), (player.position - target.position)*0.25f, ProjectileID.AmethystBolt, damage, Main.myPlayer, player.whoAmI);
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CopperBar,8)
            .AddIngredient(ItemID.Amethyst,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
