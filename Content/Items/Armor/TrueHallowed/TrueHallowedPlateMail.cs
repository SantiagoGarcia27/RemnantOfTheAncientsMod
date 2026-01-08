using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueHallowed
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueHallowedPlateMail : ModItem
	{
		int critBonus = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(critBonus);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ArmorIDs.Body.Sets.IncludedCapeBack[Item.bodySlot] = CapeEquipTexture;
            ArmorIDs.Body.Sets.IncludedCapeBackFemale[Item.bodySlot] = CapeEquipTexture;
        }
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 17;
		}
        public int CapeEquipTexture;
        public override void Load()
        {
            if (Main.dedServ) return;
            string texture = $"{Texture}_{EquipType.Back}";
            CapeEquipTexture = EquipLoader.AddEquipTexture(Mod, texture, EquipType.Back, this);
        }

        public override void UpdateEquip(Player player)
		{
            player.GetCritChance(DamageClass.Generic) += 10f;
		}

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HallowedPlateMail)
            .AddIngredient(ItemID.ChlorophyteBar, 24)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}

