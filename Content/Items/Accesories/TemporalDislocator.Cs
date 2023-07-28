/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.World;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class TemporalDislocator : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Temporal Dislocator");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Dislocateur temporal");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Dislocador Temporal");
            
            //Tooltip.SetDefault("Can generate unstable time alterations");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Peut générer des altérations temporelles instables");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Puede generar alteraciones temporales inestables");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 350000;
            Item.rare = ItemRarityID.LightRed;
           // Item.defense = 5;
            Item.accessory = true;
        }
        public override void AddRecipes() 
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            RemanntWorld.TimeDilocated = true;
        }
    }
}*/