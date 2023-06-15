using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Desert
{
	[AutoloadEquip(EquipType.Head)]
	public class Desert_herald_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Herald Helmet");
			Tooltip.SetDefault(tooltip());
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Casque de héraut du désert");	
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Casco del heraldo del desierto");
		
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public static string tooltip()
        {
			return Utils1.IncreasedDamageByTooltip(8, DamageClass.Summon) +
				"\n" + Utils1.IncreasedMinionTooltip(1);
        }
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = 3;
			Item.defense = 4;//5
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) *= 1.08f;
			player.maxMinions++;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Desert_herald_chesplate>() && legs.type == ModContent.ItemType<Desert_herald_Greaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Summons a sand tornado when hit";
			player.GetModPlayer<RemnantPlayer>().DesertHeraldSetBonus = true;
		}

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Sand_escense>(), 3)
            .AddIngredient(ItemID.SandBlock, 5)
            .AddIngredient(ItemID.Sandstone, 2)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}