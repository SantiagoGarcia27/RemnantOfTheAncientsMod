using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class Permafrost : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Permafrost");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le pergélisol");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Permafrost");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.useStyle = ItemUseStyleID.Swing;
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
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<FrostBeamF>();
		}
	}
}
