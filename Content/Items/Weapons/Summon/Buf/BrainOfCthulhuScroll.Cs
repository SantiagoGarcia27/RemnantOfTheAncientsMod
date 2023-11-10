using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Buf
{
	public class BrainOfCthulhuScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Psychic Scroll");
			//Tooltip.SetDefault("You become invunerable for 4s" +
			//	"\nBut you cant use items for 8s");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Parchemin psychique");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"tu es invulnérable pendant 4s" +
			//  "\nMais vous ne pouvez pas utiliser d'objets pendant 8 secondes");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Pergamino psíquico");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Eres invulnerable durante 4s" + 
			//"\nPero no puedes usar objetos por 8s");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 200;
			Item.rare = ItemRarityID.Orange;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 499;
			Item.useAnimation = 499;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<BrainOfChutuluScrollBuff>();
			Item.buffTime = 250;
			Item.expert = true;
		}
		//public override bool? UseItem(Player player)
		//{
		//	bool active = false;
		//	if (GetInstance<RemnantPlayer>().PlayerHaveScroll())
		//	{
		//		int currentScrollId = GetInstance<RemnantPlayer>().SearchCurrenScrollEffect();

		//		if (currentScrollId != -1)
		//		{
		//			if (!active)
		//			{
		//				player.ClearBuff(currentScrollId);
		//				player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));

		//			}
		//			return false;
		//		}
		//	}
		//	else
		//	{
		//		if (!active)
		//		{
		//			player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(1, 0, 0, 5));
		//		}
		//		return false;
		//	}
		//	return base.UseItem(player);
		//}
		public override void OnConsumeMana(Player player, int manaConsumed)
		{
			if (GetInstance<RemnantPlayer>().PlayerHaveScroll())
			{
				int currentScrollId = GetInstance<RemnantPlayer>().SearchCurrenScrollEffect();

				if (currentScrollId != -1)
				{
					player.ClearBuff(currentScrollId);
					int time = (int)Utils1.FormatTimeToTick(0, 0, 0, 5);

                    player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));


                }
                player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));
            }
			else
			{
				player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(0, 0, 0, 5));
			}
            base.OnConsumeMana(player, manaConsumed);
        }
	}
}