using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!player.noBuilding)
            {
                Vector2 mouse = Main.MouseWorld;
                Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), mouse, Vector2.Zero, type, 0, 0, player.whoAmI, 0,0, Range);
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
			.AddRecipeGroup(RecipeGroupID.Wood, 100)
			.AddIngredient(ItemID.Torch, 10)
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
