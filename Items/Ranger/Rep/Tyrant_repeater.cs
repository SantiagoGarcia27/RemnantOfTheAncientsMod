using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Items.Ranger.Rep
{
public class Tyrant_repeater : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Tyrant repeater"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wzmacniacz tyrana");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Répétiteur tyran");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Repetidor del Tirano");
		}
     public override void SetDefaults()
    {
    Item.damage = 42;
    Item.DamageType = DamageClass.Ranged;
    Item.width = 76;
    Item.height = 26;
    Item.maxStack = 1;
    Item.useTime = 15;
    Item.useAnimation = 30;
    Item.useStyle = 5;
    Item.knockBack = 2;
    Item.value = Item.sellPrice(gold: 6);
    Item.rare = 7;
    Item.UseSound = SoundID.Item5;
    Item.noMelee = true;
    Item.shoot = 1;
    Item.useAmmo = AmmoID.Arrow;
    Item.shootSpeed = 12f;
    Item.autoReuse = true;
    }
         public override Vector2? HoldoutOffset()
		{
return new Vector2(-10, 0);
		}
        /*public override bool CanConsumeAmmo(Player player)
		{

return !(player.itemAnimation < Item.useAnimation - 2);
		}*/
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(1);
            position += Vector2.Normalize(velocity) * 70f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.

                Projectile.NewProjectile(source, position, velocity, type, damage * 2, 1, player.whoAmI);
            }

            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
        }
    }
}