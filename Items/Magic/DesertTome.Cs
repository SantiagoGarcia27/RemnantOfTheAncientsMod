using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Magic
{
	public class DesertTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Tome");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Desert Tome");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tome du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tomo del Desierto");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 18;
			Item.rare = 3;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 30;
			Item.useStyle = 5;
			Item.shootSpeed = 14f;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item84;
			Item.shoot = ModContent.ProjectileType<Projectiles.DesertTyphoon>(); 
			Item.value = Item.sellPrice(silver: 325);
			Item.autoReuse = true;
		}
	}
}