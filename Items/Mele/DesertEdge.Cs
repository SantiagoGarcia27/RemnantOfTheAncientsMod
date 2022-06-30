using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using opswordsII.Projectiles;

namespace opswordsII.Items.Mele
{
	public class DesertEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Desert Edge");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Krawędź pustyni");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée des désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Filo del desierto");
		}
	    public override void SetDefaults()
		{ 
	    Item.damage = 20;                                         
		Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
	    Item.useTime = 38;
	    Item.useAnimation = 38;
        Item.useStyle = 1;
        Item.knockBack =1;
        Item.autoReuse = true;
      	Item.UseSound = SoundID.Item39; 
		Item.value = 100900;
		Item.rare = 2;
	    Item.shoot = ModContent.ProjectileType<DesertS>(); 
        Item.shootSpeed = 10f;
	    Item.noUseGraphic = false;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) 
		 {	
				  target.AddBuff(BuffType<Buffs.Burning_Sand>(), 300);
		 }


	}
}
