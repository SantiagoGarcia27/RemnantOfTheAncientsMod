using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{

    public class Graveyard_proj : ModProjectile
    {

        public override string Texture => $"Terraria/Images/Item_{ItemID.Tombstone}";
        public override void SetDefaults()
        {
            Projectile.height = 8;
            Projectile.width = 12;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.knockBack = 1.2f;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = 0;
            Projectile.timeLeft = 120;
            
        }
        public override void AI()           
        {                                                      
            //Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);


            if(Projectile.ai[2] == 27)
            {
                if (Main.tile[(int)Projectile.position.X/16, (int)Projectile.position.Y/16].HasTile)
                {
                    Projectile.velocity = new Vector2(0, -0.5f);
                }
                if (!Main.tile[(int)Projectile.position.X / 16, (int)Projectile.position.Y / 16].HasTile && !Main.tile[(int)Projectile.position.X / 16, (int)Projectile.position.Y / 16 + 1].HasTile)
                {
                    Projectile.velocity = new Vector2(0, 5f);
                }
                else Projectile.velocity = Vector2.Zero;
            }
            else
            {
                Projectile.velocity += new Vector2(0,0.4f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];
            if (Projectile.ai[2] != 27)
            {
                Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;

                for (int i = 0; i < 2; i++)
                {
                    Vector2 pos1 = Projectile.position + new Vector2((i + 2 * i) * 16, 3 - 1 * 16);
                    Vector2 pos2 = Projectile.position - new Vector2((i + 2 * i) * 16, -3 + 1 * 16);
                    pos1 = DistanceUtils.setPositionOnSolidFloor(pos1);
                    pos2 = DistanceUtils.setPositionOnSolidFloor(pos2);
                    
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos1, Vector2.Zero, ModContent.ProjectileType<Graveyard_proj>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 0, 0,27);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos2, Vector2.Zero, ModContent.ProjectileType<Graveyard_proj>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 0, 0,27);
                }
            }
            base.OnKill(timeLeft);
        }
    }
}
