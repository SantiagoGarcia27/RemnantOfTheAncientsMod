using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reinforced_Iron
{
	[AutoloadEquip(EquipType.Body)]
	public class Reforced_iron_chesplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Breastplate");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cuirasse en fer renforcé");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Corasa de hierro reforzada");
            Tooltip.SetDefault("Increases damage reduction by 10%"
            + "\nDecrease the maximum speed by 1/4");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la réduction des dégâts de 10%"
                 + "\nDisminulle la velocidad maxima en 1/4");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta un 10% la reducción de daño"
                + "\nDiminuez la vitesse maximale de 1/4");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 13;//5
		}
        public override void UpdateEquip(Player player)
        {
			player.endurance += 0.10f;
            player.moveSpeed -= 0.25f;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(), 5)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

