//using Microsoft.Xna.Framework;
//using Terraria;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
	public class ReinforcedIronHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        int rangeBonus = 2;
        public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Melee;
			Item.width = 38;
			Item.height = 38;
			Item.useTime = 18;
			Item.useAnimation = 26;
			Item.hammer = 60;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 1300;
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
            if (couldown < 6)
            {
                couldown++;
                return null;
            }

            if (player.ItemTimeIsZero)
            {
                couldown = 0;
                int targetX = Player.tileTargetX;
                int targetY = Player.tileTargetY;
                if (Framing.GetTileSafely(targetX, targetY).WallType > WallID.None)
                {
                    for (int x = targetX - rangeBonus; x <= targetX + rangeBonus; x++)
                    {
                        for (int y = targetY - rangeBonus; y <= targetY + rangeBonus; y++)
                        {
                            if (x == targetX && y == targetY)
                                continue;
                            if (WorldGen.InWorld(x, y))
                                player.PickWall(x, y, Item.hammer);
                        }
                    }
                }
            }

            return null;
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