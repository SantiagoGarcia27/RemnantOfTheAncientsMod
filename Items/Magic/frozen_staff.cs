using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.Magic
{
	public class frozen_staff : ModItem
	{
		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Frozen Staff");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Frozen Staff");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bâton gelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Báculo de Escarcha");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 35;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 2;
			Item.rare = 4;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 8;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shootSpeed = 14f;
			Item.useAnimation = 5;
			Item.shoot = ModContent.ProjectileType<frozen_p_f>(); 
			Item.UseSound = SoundID.Item39;  
			Item.value = Item.sellPrice(silver: 300);
			Item.autoReuse = true;
		}
	}
}