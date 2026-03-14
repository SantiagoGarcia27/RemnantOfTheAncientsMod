using CalamityMod.Cooldowns;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
	public class GemScavenger : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee;
			Item.width = 56;
			Item.height = 56;
			Item.useTime = 15;
			Item.useAnimation = 23;
			Item.axe = 12;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1300;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.scale = 1.28f;
			Item.autoReuse = true;
			Item.useTurn = true;
		}
        int couldown = 0;
        public override bool? UseItem(Player player)
        {
            if (couldown < 5)
            {
                couldown++;
                return null;
            }

            if (player.ItemTimeIsZero)
            {
                couldown = 0;
                int targetX = Player.tileTargetX;
                int targetY = Player.tileTargetY;
				Tile tile = Main.tile[targetX, targetY];
                
				if(TileID.Sets.IsATreeTrunk[tile.TileType])
				{
					if(Main.rand.NextBool(10))
					{
						int choice = Main.rand.Next(1,6);
						int gemId = 0;
						switch(choice)
						{
							case 1:
								gemId = ItemID.Amethyst;
								break;
                            case 2:
                                gemId = ItemID.Topaz;
                                break;
                            case 3:
                                gemId = ItemID.Sapphire;
                                break;
                            case 4:
                                gemId = ItemID.Emerald;
                                break;
                            case 5:
                                gemId = ItemID.Ruby;
                                break;
                            case 6:
                                gemId = ItemID.Diamond;
                                break;
                        }

						player.QuickSpawnItem(Item.GetSource_TileInteraction(targetX, targetY), gemId);
					}
				}
            }

            return null;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Diamond, 2)
            .AddIngredient(ItemID.Ruby, 2)
            .AddIngredient(ItemID.Emerald, 2)
            .AddIngredient(ItemID.Sapphire, 2)
            .AddIngredient(ItemID.Amethyst, 2)
            .AddIngredient(ItemID.Topaz, 2)
            .AddIngredient<Granite_axe>()
			.AddIngredient<Marble_axe>()
            .AddTile(TileID.Anvils)
			.Register();
		}
	}
}