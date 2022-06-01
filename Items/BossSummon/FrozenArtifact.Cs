using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.NPCs.frozen_assaulter;
using Terraria.Audio;

namespace opswordsII.Items.BossSummon
{
    public class FrozenArtifact : ModItem
    {
       public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Artifact");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Frozen Artefact");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Artefact gelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Artefacto congelado");
            Tooltip.SetDefault("Summons the Frozen Assaulter");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołaj Frozen Assaulter");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoquer l'assaillant gelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al asaltante congelado");
          
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
            return player.ZoneSnow && !NPC.AnyNPCs(ModContent.NPCType<frozen_assaulter>()) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)
        {
        

            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<frozen_assaulter>());   //boss spawn
            SoundEngine.PlaySound(SoundID.Roar, player.position, 0);

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SoulofLight, 5)
            .AddIngredient(ItemID.SoulofNight, 5)
            .AddIngredient(ItemID.IceBlock, 30)
            .AddIngredient(ItemID.SnowBlock, 30)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
