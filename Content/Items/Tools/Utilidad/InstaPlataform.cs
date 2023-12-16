using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
    public class InstaPlataform : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.White;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 9999;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
			Item.consumable = true;
		}

		public int Range = 200;
		public override bool? UseItem(Player player)
		{	
			for (int i = 0; i <= Range; i++)
			{
				int x = Player.tileTargetX + (i * player.direction);
				int y = Player.tileTargetY;

				if (!Main.tile[x, y].HasTile)
				{
					
					WorldGen.PlaceTile(x, y, TileID.Platforms, false, false, -1, 0);

                    if (i % 10 == 0)
                    {
                        TileSafe(x, y - 1);
                        WorldGen.PlaceTile(x, y - 1, TileID.Torches, false, false, -1, 0);

                    }
                }
			}

				
				Netcode.SyncWorld();
			return true;
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
			.AddRecipeGroup("Wood", 100)
			.AddIngredient(ItemID.Torch, 10)
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
