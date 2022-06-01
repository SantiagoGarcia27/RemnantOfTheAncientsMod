using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Ranger
{
    public class Nigthbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Night Bow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Łuk nocny");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Arc de nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arco de la Noche");

        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.knockBack = 2;
            Item.value = 12000;
            Item.rare = 3;
            Item.reuseDelay = 14;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 10f;
            Item.autoReuse = false;
        }
    
        /* public override bool CanConsumeAmmo(Player player)
        
        {
            return !(player.itemAnimation < Item.useAnimation - 2);
        } */

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"NightBar",10)
			.AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}
