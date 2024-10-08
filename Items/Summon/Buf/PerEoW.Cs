using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Summon.Buf
{
	public class PerEoW : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Putrid Scroll");
			 Tooltip.SetDefault("Reduces damage taken by 10% and increases critical damage by 5%");   
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Putrid Scroll");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"Zmniejsza otrzymywane obrażenia o 10% i zwiększa obrażenia krytyczne o 5%");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin putride");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Réduit les dégâts subis de 10% et augmente de 5% les dégâts critiques");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino pútrido");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Reduce el daño recibido en un 10% y aumenta un 5% el daño crítico");
           
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 100;
			Item.rare = 2;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 99;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 99;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<Buffs.Putrid>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 4060;
			Item.expert = true;
		
		}
	}
}