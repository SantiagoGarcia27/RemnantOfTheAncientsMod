using RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon
{
    public class StrikingGeode : ModItem
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
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = RemnantOfTheAncientsMod.CalamityMod == null;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ZoneRockLayerHeight && !NPC.AnyNPCs(ModContent.NPCType<GemstoneCrusher>());
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<GemstoneCrusher>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float dir = Main.rand.NextBool() ? 1f : -1f;
                    float spawnX = player.Center.X + (dir * 200f);
                    NPC.NewNPC(Item.GetSource_FromThis(), (int)spawnX, (int)player.Center.Y, type,Target:player.whoAmI);
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
            .AddIngredient(ItemID.StoneBlock, 50)
            .AddIngredient(ItemID.Topaz, 1)
            .AddIngredient(ItemID.Amethyst, 1)
            .AddIngredient(ItemID.Sapphire, 1)
            .AddIngredient(ItemID.Emerald, 1)
            .AddIngredient(ItemID.Ruby, 1)
            .AddIngredient(ItemID.Diamond, 1)
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}
