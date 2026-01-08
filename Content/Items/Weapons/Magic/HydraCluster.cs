using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
	public class HydraCluster : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;  
        }

		public override void SetDefaults()
		{
			Item.damage = 180;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can hold the weapon [Important]
			Item.mana = 55;
			Item.rare = ItemRarityID.Yellow;
			Item.width = 28;
			Item.height = 30;
			Item.knockBack = 0.1f;
			Item.useTime = 90;
            Item.useAnimation = 90;
            Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;		
			Item.autoReuse = true;
			Item.shoot = ProjectileType<HydraCluster_Proj>(); //Laser
			Item.value = Item.sellPrice(gold: 3);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.StaffoftheFrostHydra, 1)
			.AddIngredient(ItemID.WandofFrosting, 1)
            .AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
