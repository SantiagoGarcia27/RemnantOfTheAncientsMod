using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using opswordsII.Buffs;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Summon.Buf
{
	public class PerQnBee : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Queen Scroll");
			 Tooltip.SetDefault("You gain the honey effect, your defense increases by 5"	
			 +"\n Releases bees when damaged and increases the strength of friendly bees"	
			 +"\n   Increases armor penetration by 15”");
			 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Queen Scroll");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"uzyskujesz efekt miodu, zwiększasz obronę o 5 "
			+"\n Uwalnia pszczoły po uszkodzeniu i zwiększa siłę przyjaznych pszczół"
			+"\n zwiększyć penetrację pancerza o 15");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin de la reine");
			 Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"vous gagnez l'effet miel, votre défense augmente de 5 "
			+"\n Libère les abeilles lorsqu'elles sont endommagées et augmente la force des abeilles amicales"
			+"\n augmente la pénétration de l'armure de 15");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino de la reina");
			 DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"obtienes el efecto miel, tu defensa aumenta en 5 "
			+"\n Libera abejas cuando te dañadan e incrementa la fuerza de las abejas amigas"
			+"\n aumenta la penetración de armadura en 15");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;





			/* DisplayName.AddTranslation(GameCulture.Russian, "Свиток королевы");
			  Tooltip.AddTranslation(GameCulture.Russian,"вы получаете эффект меда, ваша защита увеличивается на 5 дюймов"
			+"\n Освобождает пчел при повреждении и увеличивает жизнеспособность дружественных пчел."
			+"\n увеличить бронепробиваемость на 15");
			
			 DisplayName.AddTranslation(GameCulture.Chinese, "皇后卷轴");
			 Tooltip.AddTranslation(GameCulture.Chinese,"获得蜜糖效果，防御力提高5英寸"
			+"\n受损时释放蜜蜂，并增加友善蜜蜂的力量"
			+"\n 护甲穿透提高15");
			*/
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
			Item.buffType = BuffType<Buffs.Bee>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 8060;
			Item.expert = true;
		
		}
	}
}