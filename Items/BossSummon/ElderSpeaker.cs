using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Audio;

namespace opswordsII.Items.BossSummon
{
    public class ElderSpeaker : ModItem
    {
       public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elder Speaker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Orateur aîné");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Altavoz Anciano");
            Tooltip.SetDefault("Summons the Old Man");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque le vieil homme");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al Anciano");
           
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
           /* Mod CalamityMod = ModLoader.GetMod("CalamityMod");
            if (CalamityMod != null) Item.consumable = false;*/
        }
        public override bool CanUseItem(Player player)
        {        
            return !NPC.AnyNPCs(NPCID.OldMan);  
        }
        public override bool? UseItem(Player player)
        {

            NPC.SpawnOnPlayer(player.whoAmI, NPCID.OldMan);   //boss spawn
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            return true;
        }
        public override void AddRecipes()
        {
           CreateRecipe()
           .AddRecipeGroup("anyDungeonBrick",10)
           .AddIngredient(ItemID.Cobweb, 20)
           .AddTile(TileID.Anvils)
           .Register();
        }
    }
}
