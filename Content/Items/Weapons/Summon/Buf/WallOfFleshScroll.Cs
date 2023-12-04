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
	public class WallOfFleshScroll : ModItem
	{
        int ExtraDamage = 10;
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ExtraDamage);

        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Infernal Scroll");
			//Tooltip.SetDefault("You gain 10% of damage and an infernal power covers you");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin infernal");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous gagnez + 10% de dégâts et un pouvoir infernal vous couvre");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino infernal");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Obtienes + 10% de daño y un poder infernal te cubre");
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
			Item.buffType = BuffType<Infernal>();
			//Item.buffTime = 0;
			Item.expert = true;

		}
        public override bool? UseItem(Player player)
        {
            if (GetInstance<RemnantPlayer>().PlayerHaveScroll())
            {
                int currentScrollId = GetInstance<RemnantPlayer>().SearchCurrenScrollEffect();

                if (currentScrollId != -1)
                {
                    player.ClearBuff(currentScrollId);
                    player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(14, 0, 0, 0));
                    return false;
                }
            }
            else
            {
                player.AddBuff(Item.buffType, (int)Utils1.FormatTimeToTick(14, 0, 0, 0));
                return false;
            }
            return base.UseItem(player);
        }
    }
}