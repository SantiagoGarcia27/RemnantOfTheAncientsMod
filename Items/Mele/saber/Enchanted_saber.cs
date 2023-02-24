using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class Enchanted_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre enchanté");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable Encantado");
			Tooltip.SetDefault("Shoot an enchanted sword beam");
           	Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tirez sur un rayon d'épée enchanté");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Dispara un rayo de espada encantada");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item Base = new Item(ItemID.EnchantedSword);
            Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Base.value;
			Item.rare = Base.rare;
			Item.shoot = ProjectileID.EnchantedBeam;
			Item.shootSpeed = 10f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			if(RemnantOfTheAncientsMod.CalamityMod != null)
			{
                Item.damage = 16;
            }
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.EnchantedSword,1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
