using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Dusts;

namespace opswordsII.Items.Tools
{
	public class destrosamundos : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("World Crusher");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "World Crusher");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Concasseur du monde");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Trituradora de mundos");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 290;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 2; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
			Item.useAnimation = 20;  //Animacion normal de Pico
			Item.pick = 250; //Potencia de Pico. 
			Item.useStyle = 1; //Dejar en 1 para que el personaje use el arma de forma normal
			Item.knockBack = 6; //Retroceso al golpear
			Item.value = Item.sellPrice(gold:7);
			Item.rare = 10;
			Item.UseSound = SoundID.Item1; 
			Item.autoReuse = true; //Autoutilizar.  No autoutilizar -> Cambiar por "false"
		}


		public override void AddRecipes() //Crafteo del objeto
		{
			CreateRecipe()
			.AddIngredient(ItemID.SolarFlarePickaxe,1)
			.AddIngredient(ItemID.NebulaPickaxe,1)
			.AddIngredient(ItemID.StardustPickaxe,1)
			.AddIngredient(ItemID.VortexPickaxe,1)
			.AddIngredient(ItemID.Picksaw,1)
			.AddIngredient(ItemID.LaserDrill,1)
			.AddIngredient(ItemID.ShroomiteDiggingClaw,1)
			.AddIngredient(ItemID.CopperPickaxe,1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(10000) == 1000)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Sparkle>());
			}
		}
	}
}