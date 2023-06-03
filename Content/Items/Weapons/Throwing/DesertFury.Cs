using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.Trower;
using CalamityMod;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Throwing
{
    public class DesertFury : ModItem
	{
		public override void SetStaticDefaults()
		{
            string textEn = RemnantOfTheAncientsMod.CalamityMod != null ? "\nStealth Strike fires two more copies of the spear" : "";
            string textEs = RemnantOfTheAncientsMod.CalamityMod != null ? "\nEl ataque con sigilo hace un diparo triple" : "";
            string textFr = RemnantOfTheAncientsMod.CalamityMod != null ? "\nStealth Strike tire deux autres copies de la lance" : "";

            DisplayName.SetDefault("Desert Fury");
            Tooltip.SetDefault("The power of the earth will indulge in this weapon." + textEn);

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"fureur du désert");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La puissance de la terre se livrera à cette arme" + textFr);

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Furia desertica");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"El poder de la tierra se consentra en este arma" + textEs);
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{ 
	        Item.damage = 210;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                SetDefaultCalamity();
            }
            else Item.DamageType = DamageClass.Throwing;       
            Item.noMelee = true;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 20;       
            Item.useAnimation = 20;   
            Item.useStyle = ItemUseStyleID.Swing;      
            Item.knockBack = 6;
            Item.value = Item.sellPrice(silver:1100);
            Item.rare = ItemRarityID.Cyan;
            Item.reuseDelay = 20;   
            Item.UseSound = SoundID.Item18;
            Item.autoReuse = true;
	        Item.shoot = ModContent.ProjectileType<DesertFuryP>();
            Item.shootSpeed = 28f;     
            Item.useTurn = true;
            Item.maxStack = 1;     
            Item.consumable = false; 
            Item.noUseGraphic = true;
		}
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefaultCalamity()
        {
            Item.DamageType = Utils1.GetDamageClassFromMod(RemnantOfTheAncientsMod.CalamityMod,"RogueDamageClass");
        }
        [JITWhenModsEnabled("CalamityMod")]
        private void StealStrike(Player player, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Calamity().StealthStrikeAvailable())
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(10);

                position += Vector2.Normalize(velocity) * 1f;

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    Projectile.NewProjectile(Projectile.GetSource_None(), position, perturbedSpeed * 2, Item.shoot, damage / 2, knockback, player.whoAmI);
                }
                Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, type, damage, 1, player.whoAmI);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (RemnantOfTheAncientsMod.CalamityMod != null) StealStrike(player, position, velocity, type, damage, knockback);
            return true;
        }
    }
}
