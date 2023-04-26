using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.accesorios
{
    public class magic_wand : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Magic Wand");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Daguette magique");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Vara M�gica");

           // //Tooltip.SetDefault("15% Increased magic damage"
              //  + "\nIncreases critical strike chance by 10");
           // //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les d�g�ts magiques de 15%"
             //   + "\nAugmente les chances de coup critique de 10");
           // //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el da�o magico en un 15%"
              //  + "\nAumenta la provabilidad de critico en 10");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 2, 70, 72);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        { 
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.GetCritChance(DamageClass.Magic) += 10;
        }
        public override void AddRecipes()  
        {
            CreateRecipe()
            .AddIngredient(ItemID.SoulofLight, 15)
            .AddIngredient(ItemID.SoulofMight, 3)
            .AddIngredient(ModContent.ItemType<magic_stick>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}