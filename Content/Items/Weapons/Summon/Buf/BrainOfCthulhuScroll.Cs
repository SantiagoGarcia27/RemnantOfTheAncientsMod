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
		public override void OnConsumeMana(Player player, int manaConsumed)
		{
			if (player.GetModPlayer<RemnantPlayer>().PlayerHaveScroll())
			{
				int currentScrollId = player.GetModPlayer<RemnantPlayer>().SearchCurrenScrollEffect();

				if (currentScrollId != -1)
				{
					player.ClearBuff(currentScrollId);

                    player.AddBuff(Item.buffType, Utils1.FormatTimeToTick(0, 0, 0, 5));


                }
                player.AddBuff(Item.buffType, Utils1.FormatTimeToTick(0, 0, 0, 5));
            }
			else
			{
				player.AddBuff(Item.buffType, Utils1.FormatTimeToTick(0, 0, 0, 5));
			}
            base.OnConsumeMana(player, manaConsumed);
        }
	}
}