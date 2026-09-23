using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Daylight
{
	[AutoloadEquip(EquipType.Head)]
	public class DaylightHat : ModItem
	{
		public override void SetStaticDefaults()
		{
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        private readonly int IncreasedMagicDamage = 3;
        private readonly int IncreasedSummonDamage = 3;
        private readonly int IncreasesMaxMana = 20;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMagicDamage, IncreasedSummonDamage, IncreasesMaxMana);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0,0,5,0);
			Item.rare = ItemRarityID.White;
			Item.defense = 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Daylight_chesplate>() && legs.type == ModContent.ItemType<Daylight_Legging>();
		}
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) *= 1f + IncreasedMagicDamage/100f;
            player.GetDamage(DamageClass.Summon) *= 1f + IncreasedSummonDamage / 100f;
            player.statManaMax2 += IncreasesMaxMana;	
        }
        public override void UpdateArmorSet(Player player)
		{

			player.setBonus = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.SetBonus.Daylight");

			if (Main.dayTime) player.lifeRegen = player.lifeRegen > 0 ? player.lifeRegen * 2 : 2;
			else player.statManaMax2 += 20;
			
			RemnantPlayer.DaylightArmorSetBonus = true;			
		}

		public override void AddRecipes()
		{
            CreateRecipe()
             .AddIngredient(ItemID.Daybloom, 4)
             .AddIngredient(ItemID.FallenStar, 1)
             .AddTile(TileID.Anvils)
             .Register();
        }
	}
}