using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Summon.Buf
{
	public class PerWlloFsh : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Infernal Scroll");
			 Tooltip.SetDefault("You gain 10% of damage and an infernal power covers you");
			 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Piekielny Zwój");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"Zyskujesz + 10% obrażeń i zakrywa cię piekielna moc");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin infernal");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Vous gagnez + 10% de dégâts et un pouvoir infernal vous couvre");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino infernal");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Obtienes + 10% de daño y un poder infernal te cubre");
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 200;
			Item.rare = 2;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 99;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 99;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<Buffs.Infernal>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 18060;
			Item.expert = true;
		
		}
	}
}