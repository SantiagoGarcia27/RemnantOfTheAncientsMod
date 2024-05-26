using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class Corrupted_saber : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Light´s Saber");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre Laser");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de la Luz");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LightsBane);
            Item.damage -= 3;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 80;
            Item.useTime -= 3;
            Item.useAnimation -= 3;
            Item.knockBack += 5;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 1;
            Item.shootSpeed = 1f;
            Item.shoot = ProjectileID.LightsBane;
            Item.noMelee = false;
            Item.channel = false;
            Item.GetGlobalItem<CustomTooltip>().Saber = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile == true)
                {
                    DashPlayer.JumpDash(player, 0.8f, 0.77f);

     
                    int proj = Projectile.NewProjectile(Projectile.GetSource_None(), player.MountedCenter + new Vector2(DistanceUtils.ToCoordenatePosition(8) * player.direction, 0), new Vector2(player.direction, 0f) * new Vector2(0.5f, 0.5f), ProjectileID.LightsBane, Item.damage + 10, Item.knockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, 3.4f);
                    Main.projectile[proj].ai[0] = Main.rand.NextFloat(3.5f, 4.6f); ;
                }
            }
            return true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int choice = (int)Math.Pow(RemnantOfTheAncientsMod.ParticleMeter(3, true),2);
            if (choice > 0)
            {
                if (Main.rand.NextBool(choice))
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Demonite);
                }
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj = Projectile.NewProjectile(source, position + new Vector2(DistanceUtils.ToCoordenatePosition(8) * player.direction,0), velocity * new Vector2(0.5f,0.5f), type, damage, knockback, player.whoAmI, 2.4f);
            Main.projectile[proj].ai[0] = Main.rand.NextFloat(1.5f, 1.6f);         
                // float ia0 = Main.projectile[proj].ai[0];
            //  float ia0 = Main.projectile[proj].rotation;
            return false;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.DemoniteBar, 10)
			.AddTile(TileID.Anvils)
			.Register();
		}
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            string Texture = "RemnantOfTheAncientsMod/Content/Items/Weapons/Melee/saber/Corrupted_saber_Glow";
            Item.glowMask = RemnantOfTheAncientsMod.AddGlowMask(Texture);
            Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
       
    }
}
