using RemnantOfTheAncientsMod.Content.Items.Armor.Night;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueNight
{
	[AutoloadEquip(EquipType.Head)]
	public class TrueNightHelmetSummoner : ModItem
	{
        private readonly int SummonDamageBonus = 16;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBonus);
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
			Item.defense = 3;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += (SummonDamageBonus / 100f);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TrueNightchesplate>() && legs.type == ModContent.ItemType<TrueNightLeggings>();
		}
        private readonly int DefenseBonus = 5;
        private readonly int SummonDamageSetBonus = 6;
        private readonly int SpeedBonus = 5;
        private readonly int MaxMinionBonus = 2;
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = this.GetLocalization("SetBonus").WithFormatArgs(DefenseBonus, SummonDamageSetBonus, SpeedBonus, MaxMinionBonus).Value;
            player.GetModPlayer<RemnantPlayer>().TrueShadowMist = true;
            player.GetDamage(DamageClass.Summon) += SummonDamageSetBonus / 100f;
            player.statDefense += DefenseBonus;
            player.moveSpeed += SpeedBonus / 100f;
            player.maxMinions += MaxMinionBonus;
        }
		public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient<NightHelmetS>()
            .AddIngredient(ItemID.SoulofFright, 10)
            .AddIngredient(ItemID.SoulofMight, 10)
            .AddIngredient(ItemID.SoulofSight, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
	}
}