using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Summon.Buf
{
	public class PerSktn : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Undead Scroll");
			 Tooltip.SetDefault("You have a super sense"
			+"\n Grants immunity to most debuffs"
			+"\n Increases throwing damage by 20% and 50% chance to not consume ammo");
			 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Nieumarły Zwój");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"masz super zmysł"
			+"\n Zapewnia odporność na większość osłabień"
			+"\n Zwiększa obrażenia zadawane przez miot o 20% i nie powoduje marnowania amunicji o 50%");
			

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Défilement des morts-vivants");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"tu as un super sens"
			+"\n Accorde l'immunité à la plupart des affaiblissements"
			+"\n Augmente les dégâts jetables de 20% et ne gaspille pas de munitions à 50%");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino de muertos vivientes");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"tienes una súper sentido"
			+"\n Otorga inmunidad a la mayoría de las desventajas."
			+"\n Incrementa el daño lanzable en 20% y posees un 50% de no gastar municion");
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 30;
			Item.rare = 2;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 99;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 99;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<Buffs.Skeleton>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 8060;
			Item.expert = true;
		
		}
	}
}