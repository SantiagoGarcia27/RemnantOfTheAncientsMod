using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.NPCs.DAniquilator;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.World;
using System;

namespace RemnantOfTheAncientsMod.Items.BossSummon
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
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            if (Main.netMode != 1)
            {  
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DesertAniquilator>());     
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, ModContent.NPCType<DesertAniquilator>(), 0f, 0f, 0, 0, 0);
            }
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
