using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class ultrablade : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("UltraBlade");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 340;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item100;
			Item.value = Item.sellPrice(gold: 11);
			Item.rare = ItemRarityID.Red;
			Item.scale = 1.20f;
			Item.shoot = ModContent.ProjectileType<UltraBladeS>();
			Item.shootSpeed = 10f;
			Item.noUseGraphic = false;
		}
        public override void AddRecipes()	   
     	{
            CreateRecipe()		
	        .AddIngredient(ItemID.InfluxWaver, 1)
	        .AddIngredient(ItemID.Meowmere, 1)
	        .AddIngredient(ItemID.TerraBlade, 1)
	        .AddTile(TileID.LunarCraftingStation)
			.Register();
	    }
	}
}
