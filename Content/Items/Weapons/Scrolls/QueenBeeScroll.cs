using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Scrolls
{
	public class QueenBeeScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Generic;
			Item.noMelee = true;
			Item.mana = 30;
			Item.rare = ItemRarityID.Green;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 99;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 99;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<Bee>();
			Item.buffTime = 9999999;
			Item.expert = true;
		}
        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<RemnantPlayer>().PlayerHaveScroll())
            {
                int currentScrollId = player.GetModPlayer<RemnantPlayer>().SearchCurrenScrollEffect();

                if (currentScrollId != -1)
                {
                    player.ClearBuff(currentScrollId);
                    player.AddBuff(Item.buffType, Utils1.FormatTimeToTick(14, 0, 0, 0));
                    return false;
                }
            }
            else
            {
                player.AddBuff(Item.buffType, Utils1.FormatTimeToTick(14, 0, 0, 0));
                return false;
            }
            return base.UseItem(player);
        }
    }
}