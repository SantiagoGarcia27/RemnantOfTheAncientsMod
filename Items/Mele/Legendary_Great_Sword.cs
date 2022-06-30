using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele
{
	public class Legendary_Great_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Legendary Great Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jungle brûlante");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Jungla Ardiente");
			Tooltip.SetDefault("Inflict poison and fire on enemies");
           	Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du poison et tirer sur les ennemis");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije veneno y fuego a los enemigos");
		}
		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Melee;
			Item.width = 130;
			Item.height = 160;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.knockBack = 20;
			Item.rare = 10;
			Item.scale = 1.80f;
			Item.UseSound = SoundID.Item45;
			Item.autoReuse = true;
			Item.value = Item.sellPrice(gold: 35);
			Item.expert = true;

			if (Item.prefix == PrefixID.Legendary){
				DisplayName.SetDefault("True Great Sword");
			}

		}

		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			target.AddBuff(BuffID.OnFire, 40);
		}	
		public override void MeleeEffects(Player player, Rectangle hitbox) {
			if (Main.rand.NextBool(1)) {//3
				//Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,55);
			//	Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,2);
			}
		}
			
	}
}
