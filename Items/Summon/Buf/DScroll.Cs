using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Summon.Buf
{
	public class DScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Desert Scroll");
			 Tooltip.SetDefault("Increases movement speed and max minion by 2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Desert Scroll");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"zwiększa prędkość ruchu i zwiększa maksymalną liczbę stronników o 2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin du désert");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"augmente la vitesse de déplacement et augmente votre serviteur maximum de 2 pendant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino desertico");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"aumenta la velocidad de movimiento y aumenta tu minion máximo en 2 ");
           
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 50;
			Item.rare = 3;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 299;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 199;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<Buffs.MasterD>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 8000;
			Item.expert = true;
		
		}
	}
}