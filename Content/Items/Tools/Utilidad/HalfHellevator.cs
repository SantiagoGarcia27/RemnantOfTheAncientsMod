using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
    public class HalfHellevator : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 1;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 9999;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
			Item.consumable = true;
		}

		public override bool? UseItem(Player player)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int a = 0; a <= Main.maxTilesY / 4 + Main.maxTilesY / 5; a++)
				{
					int x = Player.tileTargetX + i;
					int y = Player.tileTargetY + a;


					WorldGen.KillTile(x, y, false, false, false);

					if (i == 2 && a % 10 == 0)
					{
						PlataformModel.TileSafe(x, y);
						WorldGen.PlaceTile(x, y, TileID.Torches, false, false, -1, 0);

					}

				}
			}
			Netcode.SyncWorld();
			return true;
		}
		public override void HoldItem(Player player)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int a = 0; a < 50; a++)
				{
					int x = Player.tileTargetX + i;
					int y = Player.tileTargetY + a;

					int d = Dust.NewDust(new Vector2(x, y) * 16, 10, 10, DustID.GoldCoin, 0, 0, 0, default, 1);
					Main.dust[d].noGravity = true;
				}
			}
		}

        public override void AddRecipes()
		{
			CreateRecipe()
            .AddIngredient(ItemID.Dynamite, 30)
            .AddIngredient(ItemID.Torch, 40)
            .AddIngredient(ItemID.Obsidian, 5)
            .AddTile(TileID.Hellforge)
			.Register();
		}
	}
}
