using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Buffs;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Scrolls;

namespace RemnantOfTheAncientsMod.Items.Summon.Buf
{
	public class EaterOfWorldsScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Putrid Scroll");
//Tooltip.SetDefault("Reduces damage taken by 10% and increases critical damage by 5%");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin putride");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Réduit les dégâts subis de 10% et augmente de 5% les dégâts critiques");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino pútrido");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Reduce el daño recibido en un 10% y aumenta un 5% el daño crítico");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 0;
Item.DamageType = DamageClass.Summon;
Item.noMelee = true;
Item.mana = 100;
Item.rare = ItemRarityID.Green;
Item.width = 46;
Item.height = 54;
Item.useTime = 99;
Item.UseSound = SoundID.Item34;
Item.useStyle = ItemUseStyleID.HoldUp;
Item.useAnimation = 99;
Item.value = Item.sellPrice(gold: 1);
Item.autoReuse = false;
Item.buffType = BuffType<Putrid>();
Item.buffTime = 9999999;
Item.expert = true;

		}
	}
}