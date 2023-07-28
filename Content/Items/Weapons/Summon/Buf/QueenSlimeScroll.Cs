using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Buf
{
    public class QueenSlimeScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Queen Scroll");
            //Tooltip.SetDefault("50% increases player movement speed and minion damage by 5%," +
             //   "\nincrease your max number of minions by 1 and increases your max life by 20%");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin royal");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "50% augmente la vitesse de déplacement du joueur et les dégâts des sbires de 5%," +
             //   "\naugmentez votre nombre maximum de sbires de 1 et augmentez votre vie maximum de 20 %");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino de la reina");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la velocidad de movimiento en un 50%."
                //+ "\nAumentael daño de los súbditos en un 5%, aumenta tu minion máximo en 1"
                //+ "\nAumenta la vida maxima un 20%");
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