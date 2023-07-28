using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Buf
{
    public class DesertAniquilatorScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Desert Scroll");
            //Tooltip.SetDefault("50% increases movement speed and minion damage by 5% and increase your max number of minions by 1");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Parchemin du désert");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la vitesse de déplacement et augmente votre serviteur maximum de 1 pendant");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pergamino desertico");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la velocidad de movimiento en un 50%, el daño de los súbditos en un 5% y aumenta tu minion máximo en 1 ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.mana = 50;
            Item.rare = ItemRarityID.Orange;
            Item.width = 46;
            Item.height = 54;
            Item.useTime = 299;
            Item.UseSound = SoundID.Item34;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 199;
            Item.value = Item.sellPrice(gold: 1);
            Item.autoReuse = false;
            Item.buffType = BuffType<MasterD>();
            Item.buffTime = 9999999;
            Item.expert = true;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<KingSlimeScroll>(), 1)
            .AddIngredient(ItemType<Sand_escense>(), 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}