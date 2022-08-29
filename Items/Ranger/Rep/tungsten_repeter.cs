using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Items.Ranger.Rep
{
public class tungsten_repeter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tungsten Repeater");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Tungsten Repeater");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Répéteur de tungstène");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Repetidor de tungsteno");
		}
     public override void SetDefaults()
    {
    Item.damage = 8;
    Item.DamageType = DamageClass.Ranged;
    Item.width = 12;
    Item.height = 38;
    Item.maxStack = 1;
    Item.useTime = 26;
    Item.useAnimation = 26;
    Item.useStyle = 5;
    Item.knockBack = 2;
    Item.value = 12000;
    Item.rare = 2;
    Item.UseSound = SoundID.Item5;
    Item.noMelee = true;
    Item.shoot = 1;
    Item.useAmmo = AmmoID.Arrow;
    Item.shootSpeed = 10f;
    Item.autoReuse = true;
    }
         public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TungstenBar,7)
			.AddTile(TileID.Anvils)
			.Register();		
        }
    }
}