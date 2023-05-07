using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
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
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 35;
Item.noMelee = true;
Item.DamageType = DamageClass.Magic;
Item.channel = true;
Item.mana = 2;
Item.rare = ItemRarityID.LightRed;
Item.width = 28;
Item.height = 30;
Item.useTime = 8;
Item.useStyle = ItemUseStyleID.Shoot;
Item.shootSpeed = 14f;
Item.useAnimation = 5;
Item.shoot = ModContent.ProjectileType<frozen_p_f>(); 
Item.UseSound = SoundID.Item39;  
Item.value = Item.sellPrice(gold: 3);
Item.autoReuse = true;
		}
	}
}