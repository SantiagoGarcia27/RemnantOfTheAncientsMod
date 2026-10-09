using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite
{
    [AutoloadEquip(EquipType.Head)]
	public class TuxoniteHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        private readonly int RangerDamageBonus = 3;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangerDamageBonus);
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
            player.GetDamage(DamageClass.Ranged) += .03f;
        }
        public override void UpdateArmorSet(Player player)
		{

			player.setBonus = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.SetBonus.Tuxonite");

            RemnantPlayer remnantPlayer = player.GetModPlayer<RemnantPlayer>();

			if (remnantPlayer.tuxoniteStealthCounter-- > 0) return;
			
			player.shroomiteStealth = true;

			if (remnantPlayer.tuxoniteStealthDuration++ >= Utils1.FormatTimeToTick(0, 0, 0, 10))
			{
                remnantPlayer.tuxoniteStealthCounter = (int)Main.rand.NextFloat(Utils1.FormatTimeToTick(0, 0, 0, 30));
                remnantPlayer.tuxoniteStealthDuration = 0;
            }
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuxoniteBar>(), RecipeUtils.SearchAmmountRecipe(ItemID.GoldHelmet, ItemID.GoldBar))
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}