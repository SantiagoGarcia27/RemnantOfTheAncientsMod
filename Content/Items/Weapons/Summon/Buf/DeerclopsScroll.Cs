/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Buffs;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Buf
{
	public class DeerclopsScroll: ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Strange Scroll");
//Tooltip.SetDefault("Increases your jump, cancels fall damage and gain the ability to dash");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Défilement optique");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "augmentez votre saut, annulez les dégâts de chute et gagnez un tiret");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino óptico");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "aumenta tu salto, anula daño por caída y ganas un dash");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
Item.damage = 0;
Item.DamageType = DamageClass.Summon;
Item.noMelee = true;
Item.mana = 200;
Item.rare = ItemRarityID.Green;
Item.width = 46;
Item.height = 54;
Item.useTime = 99;
Item.UseSound = SoundID.Item34;
Item.useStyle = ItemUseStyleID.HoldUp;
Item.useAnimation = 99;
Item.value = Item.sellPrice(gold: 1);
Item.autoReuse = false;
Item.buffType = BuffType<Eye>(); 
            		Item.buffTime = 9999999;
Item.expert = true;
		
		}
	}
}*/