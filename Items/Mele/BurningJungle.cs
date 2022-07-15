using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Mele
{
	public class BurningJungle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Burning Jungle");
			Tooltip.SetDefault("Inflict poison and fire on enemies");
			
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jungle brûlante");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du poison et tirer sur les ennemis");
			
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Jungla Ardiente");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije veneno y fuego a los enemigos");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee;
			Item.width = 130;
			Item.height = 160;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 1;
			Item.knockBack = 20;
			Item.rare = 3;
			Item.scale = 1.50f;
			Item.UseSound = SoundID.Item45;
			Item.autoReuse = false;
			Item.value = 1000;

			/*Mod OmniSwing = ModLoader.TryGetMod("OmniSwing");
    		if (OmniSwing != null)
			Item.damage = 38;*/
       
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			target.AddBuff(BuffID.OnFire, 40);
			target.AddBuff(BuffID.Poisoned, 40);
		}	
		public override void MeleeEffects(Player player, Rectangle hitbox) {
			if (Main.rand.NextBool(1)) {//3
				//Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,55);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,2);
			}
		}
			public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.MagmaStone, 1)
			.AddIngredient(ItemID.BladeofGrass, 1)
			.AddTile(TileID.Hellforge)
			.Register();
		
		}
	}
}
