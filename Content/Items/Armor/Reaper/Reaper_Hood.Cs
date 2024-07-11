using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reaper
{
	[AutoloadEquip(EquipType.Head)]
	public class Reaper_Hood : ModItem
	{
        private static readonly Color rarityColorOne = Utils1.GetReaperColor(1);

        private static readonly Color rarityColorTwo = Utils1.GetReaperColor(2);

		int DamageBonus = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBonus);
        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("True Reaper Hood");
			//Tooltip.SetDefault(LocalizationHelper.IncreasedDamageBy//Tooltip(20,DamageClass.Generic)
   //         +"\nLuck increased in 2"
   //         +"\nNot moving puts you in stealth," 
			//+"\nincreasing ranged ability and reducing chance for enemies to target you");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Capuche de la vraie faucheuse");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), LocalizationHelper.IncreasedDamageBy//Tooltip(20,DamageClass.Generic)
   //         //+ "\nLuck increased in 2"
   //         //+ "\nNot moving puts you in stealth," 
			//+"\nincreasing ranged ability and reducing chance for enemies to target you");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Verdadera capucha de parca");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), LocalizationHelper.IncreasedDamageBy//Tooltip(20, DamageClass.Generic)
            ////+ "\nAumenta la suerte en 2"
            ////+ "\nEntras en sigilo al quedarte quieto,"
            ////+ "\naumenta el rango y es menos probable que los enemigos te detecten");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.GetGlobalItem<CustomTooltip>().ReaperItem = true;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 25;

		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) *= 1.2f;
			player.shroomiteStealth = true;
			player.luck += 2;
			//player.GetDamage(DamageClass.Melee) *= 1.05f;
           // player.moveSpeed -= 0.25f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Reaper_Robe>() && legs.type == ModContent.ItemType<Reaper_Pants>();
		}
		int LifeMaxBonus = 100;
		int GlobalDamageSetBonus = 20;
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = this.GetLocalization("SetBonus").WithFormatArgs(LifeMaxBonus, GlobalDamageSetBonus).Value;//"+100 life, + 20% damage, Soul effects are boosted";
            player.statLifeMax2 += LifeMaxBonus;
			player.GetDamage(DamageClass.Generic) *= 1f + (GlobalDamageSetBonus / 100f) ;
            player.GetModPlayer<ReaperPlayer>().ReaperSoulBoost = true;
            player.GetModPlayer<ReaperPlayer>().ReaperSoulsBoost(Item);
            player.GetModPlayer<ReaperPlayer>().ReaperSoulsBoost();
        }
	}
}