using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

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
			int[] biomes = getBiomeBlocks(player);

			for (int i = 0; i <= Range; i++)
			{
				int x = Player.tileTargetX + (i * player.direction);
				int y = Player.tileTargetY;

				if (!Main.tile[x, y].HasTile)
				{
					
					WorldGen.PlaceTile(x, y, TileID.Platforms, false, false, -1, biomes[0]);

                    if (i % 10 == 0)
                    {
                        TileSafe(x, y - 1);
                        WorldGen.PlaceTile(x, y - 1, TileID.Torches, false, false, -1, biomes[1]);

                    }
                }
			}

				
				Netcode.SyncWorld();
			return true;
		}
		public int[] getBiomeBlocks(Player player) 
		{
			if (player.ZoneForest)
			{
				return new int[] { 0, 0 };
			}
			else if (player.ZoneCorrupt)
			{
				if (WorldGen.crimson)
				{
					return new int[] { 1, 19 };
				}
				else
				{
                    return new int[] { 5, 18 };
                }
            }
            else if (player.ZoneJungle)
            {
                return new int[] { 2, 21 };
            }
            else if (player.ZoneHallow)
            {
                return new int[] { 3, 20 };
            }
            else if (player.ZoneDungeon)
            {
                return new int[] { 9, 13 };
            }
            else if (player.ZoneSkyHeight)
            {
                return new int[] { 31, 12 };
            }
            else if (player.ZoneMarble)
            {
                return new int[] { 29, 12 };
            }
            else if (player.ZoneBeach)
            {
                return new int[] { 17, 17 };
            }
            else if (player.ZoneSnow)
            {
                return new int[] { 19, 9 };
            }
            else if (player.ZoneDesert)
            {
                return new int[] { 25, 16 };
            }
			else if (player.ZoneUnderworldHeight)
			{
                return new int[] { 13, 7 };
            }
            return new int[] { 0, 12 };
        }

        public static void TileSafe(int x, int y)
        {
            if (Main.tile[x, y] == null)
            {
                Tile val = Main.tile[x, y];
                val.ResetToType(0);
            }
        }

        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i <= Range; i++)
                {
                    int x = Player.tileTargetX + (i * player.direction);
                    int y = Player.tileTargetY;

                    if (!Main.tile[x, y].HasTile)
                    {
                        int d = Dust.NewDust(new Vector2(x, y) * 16, 10, 10, DustID.WoodFurniture, 0, 0, 0, default, 1);
                        Main.dust[d].noGravity = true;
                    }
                }
            }
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
