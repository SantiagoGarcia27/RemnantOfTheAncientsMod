using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows
{
    public class TuxoniteBow: ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Tuxonite Bow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Arc Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arco de Tusonita");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = 12000;
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 10f;
            Item.autoReuse = false;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 16;
            }
        }
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ModContent.ItemType<TuxoniteBar>(),7)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}
