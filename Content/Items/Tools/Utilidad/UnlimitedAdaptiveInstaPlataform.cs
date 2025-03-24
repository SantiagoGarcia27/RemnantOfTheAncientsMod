using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Enums;
using static System.Net.Mime.MediaTypeNames;
using Terraria.Chat;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles.StructureBuilder;
using Terraria.DataStructures;

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
            Item.width = 10;
            Item.height = 32;
            Item.maxStack = 99;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.value = Item.buyPrice(0, 0, 3);
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<InstantPlataformProj>();
        }

		public int Range = 200;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int[] biomes = PlataformModel.getBiomeBlocks(player);
            if (!player.noBuilding)
            {
                Vector2 mouse = Main.MouseWorld;
                Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), mouse, Vector2.Zero, type, 0, 0, player.whoAmI, biomes[0], biomes[1],Range);
            }
            return false;
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
