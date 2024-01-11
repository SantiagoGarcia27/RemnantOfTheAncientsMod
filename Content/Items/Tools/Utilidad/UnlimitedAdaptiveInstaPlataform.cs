using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
    public class UnlimitedAdaptiveInstaPlataform : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.White +1;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 1;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}

		public int Range = 200;
        public override bool? UseItem(Player player)
        {
            int[] biomes = PlataformModel.getBiomeBlocks(player);

            for (int i = 0; i <= Range; i++)
            {
                int x = Player.tileTargetX + (i * player.direction);
                int y = Player.tileTargetY;

                if (!Main.tile[x, y].HasTile)
                {

                    WorldGen.PlaceTile(x, y, TileID.Platforms, false, false, -1, biomes[0]);

                    if (i % 10 == 0)
                    {
                        PlataformModel.TileSafe(x, y - 1);
                        WorldGen.PlaceTile(x, y - 1, TileID.Torches, false, false, -1, biomes[1]);

                    }
                }
            }
            Netcode.SyncWorld();
            return true;
        }

        public override void HoldItem(Player player)
        {
            PlataformModel.HoldItem(player, Range);
            base.HoldItem(player);
        }
    
        public override void AddRecipes()
		{
			CreateRecipe()

			.AddIngredient<AdaptiveInstaPlataform>(30)
            .AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
