using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.NPCs.ITyrant;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.World;
using System;

namespace RemnantOfTheAncientsMod.Items.BossSummon
{
    public class InfernalCalis : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Chalice");
            Tooltip.SetDefault("Summons the Infernal Tyrant");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Calice infernal");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque le tyran infernal");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cáliz infernal");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al Tirano Infernal");
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
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            if (Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<InfernalTyrantHead>());
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, ModContent.NPCType<InfernalTyrantHead>(), 0f, 0f, 0, 0, 0);
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
