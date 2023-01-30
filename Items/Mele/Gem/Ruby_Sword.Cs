using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem
{
	public class Ruby_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruby Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée rubis");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de rubí");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.knockBack = 1;
			Item.value = 1400;
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
            Projectile.NewProjectile(Projectile.GetSource_None(), new Vector2(target.position.X + 5 * 16, target.position.Y), (player.position - target.position) * 0.25f, ProjectileID.RubyBolt, damage, Main.myPlayer, player.whoAmI);
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.GoldBar,8)
            .AddIngredient(ItemID.Ruby,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}