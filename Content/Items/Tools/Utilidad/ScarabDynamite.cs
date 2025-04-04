using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.StructureBuilder;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
    public class ScarabDynamite : ModItem
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
			Item.shoot = ModContent.ProjectileType<ScarabDynamiteProj>();
			Item.shootSpeed = 5f;
			Item.consumable = true;
		}

		public override bool? UseItem(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
		}

        public override void AddRecipes()
		{
			CreateRecipe()
            .AddIngredient(ItemID.Dynamite, 1)
            .AddIngredient(ItemID.FossilOre, 40)
			.Register();
		}
	}
}
