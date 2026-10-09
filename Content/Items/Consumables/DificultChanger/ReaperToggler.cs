using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger
{
    public class ReaperToggler : ModItem
    {

        private static readonly Color rarityColorOne = Utils1.GetReaperColor(1);

        private static readonly Color rarityColorTwo = Utils1.GetReaperColor(2);

        public int MinionSlotBonus = 1;
        public int ManaBoost = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBonus, ManaBoost);

        public override void SetStaticDefaults()
        {        
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        } 
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
            Item.UseSound = SoundID.Item60;
            Item.consumable = false;
        }
        public override bool? UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.Server || player.whoAmI != Main.myPlayer)
                return true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();

                packet.Write((byte)Netcode.RemnantOfTheAncientsModMessageType.RequestToggleReaper);
                packet.Send();
            }
            else TryToggleReaper(player);
            
            return true;
        }


        public static void TryToggleReaper(Player player)
        {

            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            if (!player.active || player.dead) return;
            if (player.HeldItem.type != ModContent.ItemType<ReaperToggler>()) return;
            if (Utils1.IsAnyBossAlive()) return;

            bool activating = !Reaper.ReaperMode;

            if (activating)
            {
                ReaperPlayer modPlayer = player.GetModPlayer<ReaperPlayer>();

                if (!modPlayer.ChaliceOn)
                {
                    modPlayer.DropReaperStarterKit();
                    modPlayer.ReaperFirstTime = true;
                }
            }

            Reaper.UpdateReaper();

            string mode = activating ? "On" : "Off";
            string key = $"Mods.RemnantOfTheAncientsMod.Messages.Reaper.{mode}";

            if (Main.netMode == NetmodeID.Server) ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), Color.DarkSlateGray);
            else Main.NewText(Language.GetTextValue(key), Color.DarkSlateGray);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}
