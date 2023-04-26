using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Buffs;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using RemnantOfTheAncientsMod.Buffs.Scrolls;

namespace RemnantOfTheAncientsMod.Items.Summon.Buf
{
	public class KingSlimeScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Royal Scroll");
//Tooltip.SetDefault("increases movement speed by 50% and minion damage by 5%");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin royal");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "augmente la vitesse de déplacement de 50% et les dégâts des sbires de 5%");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino real");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "aumenta la velocidad de movimiento en un 50% y el daño de los súbditos en un 5%");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 0;
Item.DamageType = DamageClass.Summon;
Item.noMelee = true;
Item.mana = 50;
Item.rare = ItemRarityID.Green;
Item.width = 46;
Item.height = 54;
Item.useTime = 99;
Item.UseSound = SoundID.Item34;
Item.useStyle = ItemUseStyleID.HoldUp;
Item.useAnimation = 99;
Item.value = Item.sellPrice(gold: 1);
Item.autoReuse = false;
Item.buffType = BuffType<Slim>();
Item.buffTime = 9999999;
Item.expert = true;
		}
	}
}