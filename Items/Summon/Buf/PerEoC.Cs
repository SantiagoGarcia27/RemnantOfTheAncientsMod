using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Summon.Buf
{
	public class PerEoC : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Optic Scroll");
			 Tooltip.SetDefault("Increases your jump, cancels fall damage and gain the ability to dash");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przewijanie optyczne");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"zwiększ skok, anuluj obrażenia od upadku i zdobądź doskok");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Défilement optique");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"augmentez votre saut, annulez les dégâts de chute et gagnez un tiret");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino óptico");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"aumenta tu salto, anula daño por caída y ganas un dash");
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
			Item.buffType = BuffType<Buffs.Eye>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 6060;
			Item.expert = true;
		
		}
	}
}