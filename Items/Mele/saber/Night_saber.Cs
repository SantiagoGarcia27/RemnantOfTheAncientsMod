using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Mele.saber
{
	public class Night_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de la Nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de la Noche");
		}
		public override void SetDefaults()
		{
			Item.damage = 38;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = 3;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox) {
			if (Main.rand.NextBool(3)) {//3
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,14); //corrupcion
			}
			if (Main.rand.NextBool(3)) {
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,27);//llama demoniaca
			}
			if (Main.rand.NextBool(7)) {
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,29);//muramasa
			}
			if (Main.rand.NextBool(2)) {
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,98);//Nigth
				}
			if (Main.rand.NextBool(3)) {
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,104);//Nigth Suave
			}
		}
	public override void AddRecipes()
		{
			CreateRecipe()
           .AddRecipeGroup("CorruptSaber",1)
			.AddIngredient(null,"Grass_saber",1)
			.AddIngredient(ItemID.Muramasa,1)
			.AddIngredient(null,"Fire_saber",1)
			.AddTile(TileID.DemonAltar)
			.Register();
		    
		}
	}
}

