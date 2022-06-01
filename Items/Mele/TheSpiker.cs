using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.Mele
{
	public class TheSpiker : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			
				DisplayName.SetDefault("The Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La punzante");
		}
		public override void SetDefaults()
		{
			Item.damage = 90;
			Item.DamageType = DamageClass.Melee;
			Item.width = 10;
			Item.height = 10;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 3;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(gold: 30);
			Item.rare = 12;
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<InfernalSpikeF_f>();

			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) 
		 {	
				target.defense = target.defense/2;
		 }
	}
}
