using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Magic
{
	public class CurcedFlower : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Flower of Cursed Flame");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kwiat przeklętego płomienia");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fleur de flamme maudite");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Flor de llama maldita");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
Item.damage = 60;
Item.noMelee = true;
Item.DamageType = DamageClass.Magic;
Item.mana = 18;
Item.width = 40;
Item.height = 40;
Item.useTime = 23;
Item.useAnimation = 23;
Item.useStyle = 1;
Item.knockBack = 1;
Item.autoReuse = true;
Item.UseSound = SoundID.Item20;
Item.value = 50000;
Item.rare = ItemRarityID.LightRed;
Item.shoot = ProjectileID.CursedFlameFriendly;
Item.shootSpeed = 10f;
Item.noUseGraphic = false;

            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 80;
            }
        }
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.CursedFlame, 20)
.AddIngredient(ItemID.FlowerofFire, 1)
.AddTile(TileID.MythrilAnvil)
.Register();
		}
	}
}
