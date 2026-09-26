using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon
{
    public class BulbOnAPot : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 38;
            Item.maxStack = 999;
            Item.value = 100;
            Item.rare = ItemRarityID.Lime;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = RemnantOfTheAncientsMod.CalamityMod == null;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = NPCID.Plantera;

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
            return RemnantOfTheAncientsMod.CalamityMod == null;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LifeFruit, 1)
            .AddIngredient(ItemID.ChlorophyteOre, 5)
            .AddTile(TileID.PlanteraBulb)
            .Register();
        }
    }
}
