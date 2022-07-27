using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.NPCs.DAniquilator;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.BossSummon
{
    public class DesertChest : ModItem
    {
       public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Chest");
            Tooltip.SetDefault("Summons the Desert Annihilator");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Coffre du désert");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoquer l'aniquilateur du désert");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cofre del desierto");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al aniquilador del desierto");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            if (ModLoader.TryGetMod("CalamityMod", out Mod mod)) Item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {        
            return player.ZoneDesert && !NPC.AnyNPCs(ModContent.NPCType<DesertAniquilator>());  
        }
        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DesertAniquilator>()); 
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
