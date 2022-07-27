using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Summon.Buf
{
	public class WFScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Psychic Scroll");
			 Tooltip.SetDefault("You become invunerable for 4s");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zwój psychiczny");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"jesteś niewrażliwy na 4 s");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin psychique");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"tu es invulnérable pendant 4s");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino psíquico");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"eres invulnerable durante 4s");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 200;
			Item.rare = 3;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 499;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 499;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<Buffs.AoD>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 250;
			Item.expert = true;
		
		}
	}
}