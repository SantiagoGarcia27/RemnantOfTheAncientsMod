using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Buffs;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using RemnantOfTheAncientsMod.Buffs.Scrolls;
using static Humanizer.On;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Summon.Buf
{
	public class QueenSlimeScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Queen Scroll");
            Tooltip.SetDefault("50% increases movement speed and minion damage by 5%, increase your max number of minions by 1 and increases your max life by 20%");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin royal");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente votre vie max de 50 %");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino de la reina");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la velocidad de movimiento en un 50%."
                + "\n Aumentael daño de los súbditos en un 5%, aumenta tu minion máximo en 1"
                + "\n Aumenta la vida maxima un 20%");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.mana = 100;
			Item.rare = ItemRarityID.Pink;
			Item.width = 46;
			Item.height = 54;
			Item.useTime = 99;
			Item.UseSound = SoundID.Item34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 99;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = false;
			Item.buffType = BuffType<QueenSlimeScrollBuff>();
			Item.buffTime = 9999999;
			Item.expert = true;
		}
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<DesertAniquilatorScroll>(), 1)
            .AddIngredient(ItemID.GelBalloon, 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}