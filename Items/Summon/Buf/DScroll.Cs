using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Buffs;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Items.Summon.Buf
{
	public class DScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Desert Scroll");
			 Tooltip.SetDefault("Increases movement speed and max minion by 2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin du désert");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"augmente la vitesse de déplacement et augmente votre serviteur maximum de 2 pendant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino desertico");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"aumenta la velocidad de movimiento y aumenta tu minion máximo en 2 ");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 50;
			Item.rare = ItemRarityID.Orange;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 299;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 199;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<MasterD>(); 
            Item.buffTime = 8000;
			Item.expert = true;
		
		}
	}
}