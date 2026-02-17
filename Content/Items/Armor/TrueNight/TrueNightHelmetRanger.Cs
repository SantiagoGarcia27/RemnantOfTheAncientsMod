using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Night;
using RemnantOfTheAncientsMod.Content.Items.Items;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueNight
{
	[AutoloadEquip(EquipType.Head)]
	public class TrueNightHelmetRanger : ModItem
	{
        int RangedDamageBonus = 16;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBonus);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold:1);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 12;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) += (RangedDamageBonus / 100);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TrueNightchesplate>() && legs.type == ModContent.ItemType<TrueNightLeggings>();
		}

        int DefenseBonus = 5;
        int RangedDamageSetBonus = 6;
        int SpeedBonus = 5;
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = this.GetLocalization("SetBonus").WithFormatArgs(DefenseBonus, RangedDamageSetBonus, SpeedBonus).Value;
			player.GetModPlayer<RemnantPlayer>().TrueShadowMist = true;
			player.GetDamage(DamageClass.Ranged) += RangedDamageSetBonus / 100f;
            player.statDefense += DefenseBonus;
            player.moveSpeed += SpeedBonus / 100f;
        }

        public override void AddRecipes()
		{
            CreateRecipe()
           .AddIngredient<NightHelmetR>()
           .AddIngredient(ItemID.SoulofFright, 10)
           .AddIngredient(ItemID.SoulofMight, 10)
           .AddIngredient(ItemID.SoulofSight, 10)
           .AddTile(TileID.MythrilAnvil)
           .Register();
        }
	}
}