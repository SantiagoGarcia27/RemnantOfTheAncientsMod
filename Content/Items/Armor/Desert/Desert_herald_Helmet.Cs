using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Desert
{
	[AutoloadEquip(EquipType.Head)]
	public class Desert_herald_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Desert Herald Helmet");
			//Tooltip.SetDefault(//Tooltip());
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Casque de héraut du désert");	
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Casco del heraldo del desierto");
		
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		//     public static string //Tooltip()
		//     {
		//return LocalizationHelper.IncreasedDamageBy//Tooltip(8, DamageClass.Summon) +
		//	"\n" + LocalizationHelper.IncreasedMinion//Tooltip(1);
		//     }
		public int SummonDamageBonus = 8;
        public int MinionMaxBonus = 1;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBonus,MinionMaxBonus);
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
            .AddIngredient(ModContent.ItemType<Sand_escense>(), 10)
            .AddIngredient(ItemID.SandBlock, 5)
            .AddIngredient(ItemID.Sandstone, 2)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}