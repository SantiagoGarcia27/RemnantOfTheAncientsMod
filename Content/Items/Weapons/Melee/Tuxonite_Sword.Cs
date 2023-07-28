using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class Tuxonite_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tuxonite Broadsword");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée Tuxonite");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Tusonita");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.PlatinumBroadsword);
            Item.damage += 3;
            Item.DamageType = DamageClass.Melee;
			Item.width += 10;
			Item.height += 10;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.scale = RemnantOfTheAncientsMod.CalamityMod != null ? 2f : 1.5f;     
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<TuxoniteBar>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

