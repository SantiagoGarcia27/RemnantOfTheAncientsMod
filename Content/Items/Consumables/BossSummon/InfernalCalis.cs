using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon
{
    public class InfernalCalis : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Infernal Chalice");
            //Tooltip.SetDefault("Summons the Infernal Tyrant " +
                //"\nNot work on multiplayer");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Calice infernal");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque le tyran infernal" +
               // "\nNe fonctionne pas en multijoueur");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cáliz infernal");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al Tirano Infernal" + 
               // "\nNo funciona en multijugador");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            if (ModLoader.TryGetMod("CalamityMod", out Mod mod)) Item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ZoneUnderworldHeight && !NPC.AnyNPCs(ModContent.NPCType<InfernalTyrantHead>());
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<InfernalTyrantHead>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.AshBlock, 5)
            .AddIngredient(ItemID.SoulofNight, 5)
            .AddRecipeGroup("GoldBar")
            .AddTile(TileID.Hellforge)
            .Register();
        }
    }
}
