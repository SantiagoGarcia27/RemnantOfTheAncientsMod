using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite
{
	[AutoloadEquip(EquipType.Head)]
	public class TuxoniteHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Helmet");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Casco de tusonita");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Casque Tuxonite");
            Tooltip.SetDefault("3% increased ranged damage");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "3% d'augmentation des dégâts à distance");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta un 3% el daño a distancia");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3600;
			Item.rare = ItemRarityID.White;
			Item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Tuxonite_chesplate>() && legs.type == ModContent.ItemType<Tuxonite_Legging>();
		}
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) *= 1.03f;
        }
        public override void UpdateArmorSet(Player player)
		{

			player.setBonus = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.SetBonus.Tuxonite");

			if (RemnantPlayer.tuxoniteStealthCounter == 0)
			{
				player.shroomiteStealth = true;
				if (RemnantPlayer.tuxoniteStealthDuration < Utils1.FormatTime(0, 0, 0, 10))
				{
					RemnantPlayer.tuxoniteStealthDuration++;
				}
				else
				{
					RemnantPlayer.tuxoniteStealthCounter = (int)Main.rand.NextFloat(Utils1.FormatTime(0, 0, 0, 30));
					RemnantPlayer.tuxoniteStealthDuration = 0;
				}
			}
			else
			{
				RemnantPlayer.tuxoniteStealthCounter--;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), 20)//25
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}