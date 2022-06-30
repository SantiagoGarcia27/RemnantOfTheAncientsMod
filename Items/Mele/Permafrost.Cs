using opswordsII.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Projectiles;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Mele
{
	public class Permafrost : ModItem
	{
           public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Permafrost");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wieczna zmarzlina");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le pergélisol");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Permafrost");
		}

		public override void SetDefaults() {
			Item.damage = 45;
			Item.useStyle = 1;
			Item.width = 130;
			Item.height = 160;
			Item.useTime = 40;
			Item.useAnimation = 25;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(silver: 10);
			Item.DamageType = DamageClass.Melee;
			Item.noUseGraphic = false; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this Item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
			Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<FrostBeamF>(); 
		
		}
	}
}
