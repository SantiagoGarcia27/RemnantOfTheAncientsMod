using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.NPCs.DAniquilator;
using Terraria.Audio;

namespace opswordsII.Items.BossSummon
{
    public class DesertChest : ModItem
    {
       public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Chest");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pustynna skrzynia");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Coffre du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cofre del desierto");
            Tooltip.SetDefault("Summons the Desert Annihilator");
           Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wezwij Pustynnego Aniquilatora");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoquer l'aniquilateur du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al aniquilador del desierto");
           
        }
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = 0;
            Item.rare = 2;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 4;
            Item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {        
            return player.ZoneDesert && !NPC.AnyNPCs(ModContent.NPCType<DesertAniquilator>());  
        }
        public override bool? UseItem(Player player)
        {
        

            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DesertAniquilator>());   //boss spawn
            SoundEngine.PlaySound(SoundID.Roar, player.position, 0);

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SandBlock, 40)
            .AddIngredient(ItemID.Sandstone, 20)
            .AddIngredient(ItemID.Cactus, 30)
            .Register();
        }
    }
}
