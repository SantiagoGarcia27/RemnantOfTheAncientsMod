using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.NPCs.frozen_assaulter;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.NPCs.ITyrant;

namespace RemnantOfTheAncientsMod.Items.BossSummon
{
    public class FrozenArtifact : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Artifact");
            Tooltip.SetDefault("Summons the Frozen Assaulter");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Artefact gelé");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoquer l'assaillant gelé");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Artefacto congelado");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca al asaltante congelado");
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
            return player.ZoneSnow && !NPC.AnyNPCs(ModContent.NPCType<frozen_assaulter>()) && !Main.dayTime;
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            if (Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<frozen_assaulter>());
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, ModContent.NPCType<frozen_assaulter>(), 0f, 0f, 0, 0, 0);
            }
            return true;
        }
    
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SoulofLight, 5)
            .AddIngredient(ItemID.SoulofNight, 5)
            .AddIngredient(ItemID.IceBlock, 30)
            .AddIngredient(ItemID.SnowBlock, 30)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
