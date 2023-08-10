using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite
{
	[AutoloadEquip(EquipType.Body)]
	public class Tuxonite_chesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tuxonite Chainmail");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cotte de mailles Tuxonite");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cota de malla de tusonita");
            //Tooltip.SetDefault("3% increased ranged damage");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "3% d'augmentation des dégâts à distance");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta un 3% el daño a distancia");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public int RangerDamageBonus = 3;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangerDamageBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000;
			Item.rare = ItemRarityID.White;
			Item.defense = 5;
		}
        public override void UpdateEquip(Player player)
        {
			player.GetDamage(DamageClass.Ranged) *= 1.03f;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), 30)//35
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

