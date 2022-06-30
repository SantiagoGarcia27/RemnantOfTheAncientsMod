using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using opswordsII.Projectiles;
using opswordsII.Items.Items;

namespace opswordsII.Items.Ranger.Bows
{
public class ReinforcedIronBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Bow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Arc en fer renforcé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arco de Hierro Reforzado");
			
		}
         public override void SetDefaults()
        {
          Item.damage = 20;
          Item.DamageType = DamageClass.Ranged;
          Item.width = 12;
          Item.height = 38;
          Item.maxStack = 1;
          Item.useTime = 42;
          Item.useAnimation = 42;
          Item.useStyle = 5;
          Item.knockBack = 0;
          Item.value = 12000;
          Item.rare = 3;
          Item.UseSound = SoundID.Item5;
          Item.noMelee = true;
          Item.shoot = 1;
          Item.useAmmo = AmmoID.Arrow;
          Item.shootSpeed = 10f;
          Item.autoReuse = false;
        }
        public override void AddRecipes()
        { 
			CreateRecipe()
            .AddIngredient(ItemType<Reinforced_ironBar>(), 7)
            .AddTile(TileID.Anvils)
            .Register();
        }
	}
}
