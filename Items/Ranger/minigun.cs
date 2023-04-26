using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Ranger
{
    public class minigun: ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("mini/gun");
//Tooltip.SetDefault("A small pistol");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Una pistola pequeña");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
Item.damage = 30;
Item.DamageType = DamageClass.Ranged;
Item.width = 2;
Item.height = 2;
Item.useTime = 25;
Item.useAnimation = 25;
Item.useStyle = ItemUseStyleID.Shoot;
Item.noMelee = true;
Item.scale = 0.68f;
Item.knockBack = 5;
Item.value = 20000;
Item.rare = ItemRarityID.Blue;
Item.UseSound = SoundID.Item38;
Item.autoReuse = false;
Item.shoot = ProjectileID.PurificationPowder;
Item.shootSpeed = 20f;
Item.useAmmo = AmmoID.Bullet;

//if (ModLoader.TryGetMod("OmniSwing", out Mod mod)) Item.damage = 28;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
	Item.autoReuse = true;
            }
        }
		public override Vector2? HoldoutOffset()
		{
return new Vector2(-10, 0);
		}
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.FlintlockPistol, 2)
.AddTile(TileID.WorkBenches)
.Register();
		}
	}
}
