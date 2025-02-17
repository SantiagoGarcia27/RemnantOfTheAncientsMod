using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Chat;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common;
using FargowiltasSouls.Core.ModPlayers;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger
{
    public class Ftoggler : ModItem
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
           
            if (!Utils1.IsAnyBossAlive())
            {
                if (!Reaper.ReaperMode && !DificultyUtils.ReaperMode)
                {
                   // Reaper.ReaperMode = true;
                   // DificultyUtils.ReaperMode = true;
                    Item.buffTime = 1;
                    Color gray = Color.DarkSlateGray;
                    string text = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Messages.Reaper.On");
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), gray);
                    var modPlayer = player.GetModPlayer<ReaperPlayer>();
                    if (!modPlayer.ChaliceOn)
                    {
                        modPlayer.DropReaperStarterKit();
                        ReaperPlayer.ReaperFirstTime = true;
                    }
                }
                else
                {
                    //Reaper.ReaperMode = false;
                   // DificultyUtils.ReaperMode = false;
                    Color gray = Color.DarkSlateGray;
                    string text = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Messages.Reaper.Off");
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), gray);
                }
               Reaper.UpdateReaper();
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}
