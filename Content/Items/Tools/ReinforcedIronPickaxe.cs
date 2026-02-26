using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
	public class ReinforcedIronPickaxe : ModItem
	{
		int rangeBonus = 1;
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Melee;
			Item.width = 38;
			Item.height = 38;
			Item.useTime = 13;
			Item.useAnimation = 19;
			Item.pick = 43;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = 1600;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}
		public override void HoldItem(Player player)
		{
			Player.tileRangeX += rangeBonus;
			Player.tileRangeY += rangeBonus;
		}
        int couldown = 0;
        public override bool? UseItem(Player player)
		{
			if(couldown < 6)
			{
				couldown++;
				return null;
			}

			if (player.ItemTimeIsZero)
			{
                couldown = 0;
                int targetX = Player.tileTargetX;
				int targetY = Player.tileTargetY;

				for (int x = targetX - rangeBonus; x <= targetX + rangeBonus; x++)
				{
					for (int y = targetY - rangeBonus; y <= targetY + rangeBonus; y++)
					{
						if (x == targetX && y == targetY)
							continue;	
						if (WorldGen.InWorld(x, y)  && CanMineTileWithPick(x,y, player.HeldItem.pick))
							player.PickTile(x, y, Item.pick);
					}
				}
			}

			return null;
		}
        public static bool CanMineTileWithPick(int x, int y, int pickPower)
        {
            Tile tile = Framing.GetTileSafely(x, y);
            if (!tile.HasTile) return false;

            int type = tile.TileType;
            int requiredPick = GetRequiredPick(type, tile); 
            return pickPower >= requiredPick && requiredPick > -1;
        }

        private static int GetRequiredPick(int type, Tile tile)
        {
            var tileInfo = TileLoader.GetTile(type);
            if (tileInfo != null)
            {
                return tileInfo.MinPick;
            }
			if (TileID.Sets.Stone[tile.TileType] || tile.TileType == TileID.Marble || tile.TileType == TileID.Granite
				) return 40;
			return -1;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<ReinforcedIronBar>(12)
			.AddIngredient(ItemID.Wood, 4)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}