using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon
{
    public class SoulBurner : ModItem
    {
       
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        private static readonly Color rarityColorOne = Utils1.GetReaperColor(1);

        private static readonly Color rarityColorTwo = Utils1.GetReaperColor(2);

        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 9f);
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 38;
            Item.maxStack = 999;
            Item.value = 100;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.GetGlobalItem<CustomTooltip>().ReaperItem = true;
            Item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCID.WallofFlesh);
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = NPCID.WallofFlesh;

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
        public override bool ConsumeItem(Player player)
        {
            return !Reaper.ReaperMode;
        }
    }
}
