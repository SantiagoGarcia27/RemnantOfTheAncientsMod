using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.World;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class Corrupt_Heart : ModItem
    {
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("Exotic Amulet");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Amulette Exotique");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Amuleto Exótico");

            //Tooltip.SetDefault("Increases maximum mana by 20,"
            //+ "\nmana regeneration rate"
            //+ "\nand armor penetration by 5."
            //+ "\nprovide immunity to lava and fire blocks"
            //+ "\nyou have the effect of Sweetheart Necklace, Charm of Myths,"
            //+ "\nArctic Diving Gear and Star Veil");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente le maximum de mana de 20,"
            //+ "\ntaux de régénération du mana"
            //+ "\net pénétration d'armure de 5."
            //+ "\noffre une immunité à la lave et aux blocs de feu"
            //+ "\nvous avez l'effet de Collier d'amoureux, Charme des mythes,"
            //+ "\n Arctic Diving Gear et Star Veil" );
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el maná máximo en 20,"
            //+ "\nla regeneración de maná"
            //+ "\ny penetración de armadura en 5."
            //+ "\nproporciona inmunidad a la lava y los bloques de fuego"
            //+ "\notorga el efecto de Collar de cariño, talismán de mitos"
            //+ "\nEquipo de buceo ártico y Velo de estrella");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public int LifeMaxBonus = DificultyUtils.ReaperMode ? 10 : 60;
        public int DamageReduction = 12;
        Item ItemDoge = new Item(ItemID.BrainOfConfusion);
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeMaxBonus, DamageReduction);//, ItemDoge.ToolTip);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 3, 70, 72);
            Item.rare = ItemRarityID.LightRed;
            Item.GetGlobalItem<CustomTooltip>().CompletistItem = true;
            Item.defense = 5;
            Item.accessory = true;
            Item.master = true;
            Item.masterOnly = true;
        }
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.BrainOfConfusion, 1)
            .AddIngredient(ItemID.WormScarf, 1)
            .AddIngredient(ItemID.LifeCrystal, 10)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register(); 
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RemnantPlayer>().BrainDogde = true;
            player.endurance += 0.12f;
            player.statLifeMax2 += DificultyUtils.ReaperMode? 10:60;
        }
    }
}