using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite
{
	[AutoloadEquip(EquipType.Legs)]
	public class Tuxonite_Legging : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tuxonite Greaves");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Grebas de tusonita");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grèves Tuxonite");
            //Tooltip.SetDefault(//Tooltip());
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        //public string //Tooltip()
        //{
        //	return LocalizationHelper.IncreasedCritBy//Tooltip(5, DamageClass.Ranged);
        //      }
        public int RangerCritBonus = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangerCritBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000;
			Item.rare = ItemRarityID.White;
			Item.defense = 4;
		}
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 5f;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), 25)//30
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

