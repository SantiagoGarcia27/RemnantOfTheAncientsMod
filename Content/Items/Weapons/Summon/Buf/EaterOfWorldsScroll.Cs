using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Buf
{
	public class EaterOfWorldsScroll : ModItem
	{
        int DamageReduction = 10;
        int CritBonus = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageReduction, CritBonus);
        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Putrid Scroll");
			//Tooltip.SetDefault("Reduces damage taken by 10% and increases critical damage by 5%");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin putride");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Réduit les dégâts subis de 10% et augmente de 5% les dégâts critiques");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino pútrido");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Reduce el daño recibido en un 10% y aumenta un 5% el daño crítico");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 100;
			Item.rare = ItemRarityID.Green;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 99;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 99;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<Putrid>();
			Item.buffTime = 9999999;
			Item.expert = true;

		}
        public override bool? UseItem(Player player)
        {
			bool playerHaveScroll = ModContent.GetInstance<RemnantPlayer>().PlayerHaveScroll();
            if (playerHaveScroll)
            {
                int currentScrollId = ModContent.GetInstance<RemnantPlayer>().SearchCurrenScrollEffect();

                if (currentScrollId != -1)
                {
                    player.ClearBuff(currentScrollId);
                    player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(1, 0, 0, 0));
					return false;
                }
            }
            else
            {
                player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(1, 0, 0, 0));
                return false;
            }
            return base.UseItem(player);
        }
    }
}