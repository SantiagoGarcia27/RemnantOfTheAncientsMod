using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.BossSummon
{
    public class ElderSpeaker : ModItem
    {
       public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elder Speaker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Orateur aîné");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Altavoz Anciano");
            Tooltip.SetDefault("Summons Skeletron");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque le Squelette");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al Esqueletrón");
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
            return !NPC.AnyNPCs(NPCID.SkeletronHead);  
        }
        public override bool? UseItem(Player player)
        {
            if (!Main.dayTime)
            {
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronHead);
                SoundEngine.PlaySound(SoundID.Roar, player.position);
            }

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
