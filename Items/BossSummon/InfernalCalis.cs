using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.NPCs.ITyrant;
using Terraria.Audio;
using Microsoft.Xna.Framework;

namespace opswordsII.Items.BossSummon
{
    public class InfernalCalis : ModItem
    {
       public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infernal Chalice");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Piekielny Kielich");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Calice infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cáliz infernal");
            Tooltip.SetDefault("Summons the Infernal Tyrant");
           Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przyzywa Piekielnego Tyrana");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque le tyran infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al Tirano Infernal");
           
        }
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = 100;
            Item.rare = 2;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 4;
            Item.consumable = true;
           /* Mod CalamityMod = ModLoader.GetMod("CalamityMod");
            if (CalamityMod != null) Item.consumable = false;*/
        }
        public override bool CanUseItem(Player player)
        {        
            return player.ZoneUnderworldHeight && !NPC.AnyNPCs(ModContent.NPCType<InfernalTyrantHead>());  
        }
        public override bool? UseItem(Player player)
        {
        

            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<InfernalTyrantHead>());   //boss spawn
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.AshBlock, 5)
            .AddIngredient(ItemID.SoulBottleNight, 5)
            .AddRecipeGroup("GoldBar")
            .AddTile(TileID.Hellforge)
            .Register();
        }
    }
}
