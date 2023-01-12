using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Magic
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
			Item.channel = true;
			Item.mana = 18;
			Item.rare = ItemRarityID.Orange;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item84;
			Item.shoot = ModContent.ProjectileType<Projectiles.DesertTyphoonFriendly>(); 
			Item.value = Item.sellPrice(silver: 325);
			Item.autoReuse = true;
		}
	}
}