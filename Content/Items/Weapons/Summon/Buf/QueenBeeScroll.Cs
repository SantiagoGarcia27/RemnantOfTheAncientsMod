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
	public class QueenBeeScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hive Scroll");
			//Tooltip.SetDefault("You gain the honey effect, your defense increases by 5"
			//+ "\nReleases bees when damaged and increases the strength of friendly bees"
			//+ "\nIncreases armor penetration by 15”");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin de la Ruche");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "vous gagnez l'effet miel, votre défense augmente de 5 "
					   //+ "\nLibère les abeilles lorsqu'elles sont endommagées et augmente la force des abeilles amicales"
					   //+ "\naugmente la pénétration de l'armure de 15");

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino de la colmena");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "obtienes el efecto miel, tu defensa aumenta en 5 "
			//+ "\nLibera abejas cuando te dañadan e incrementa la fuerza de las abejas amigas"
			//+ "\naumenta la penetración de armadura en 15");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
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
            if (GetInstance<RemnantPlayer>().PlayerHaveScroll())
            {
                int currentScrollId = GetInstance<RemnantPlayer>().SearchCurrenScrollEffect();

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