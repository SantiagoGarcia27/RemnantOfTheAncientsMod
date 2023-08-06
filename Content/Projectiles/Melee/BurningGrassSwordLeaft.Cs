using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Projectiles.Melee
{
    public class MoltenGrassSwordLeaft : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BladeOfGrass;
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Sharp Leaf");
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BladeOfGrass);
            AIType = ProjectileID.BladeOfGrass;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (new RemnantOfTheAncientsMod().ParticleMeter(4) != 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), target.position, new Vector2(0f, 0f), ProjectileID.Volcano, (int)(Projectile.damage * 0.5f), 0);
            }
            target.AddBuff(BuffID.OnFire, 120);
        }
        private List<Vector2> previousPositions = new List<Vector2>();
        private int trailLength = 10;
        public override void AI()
        {
            previousPositions.Add(Projectile.Center);       
            if (previousPositions.Count > trailLength)
            {
                previousPositions.RemoveAt(0);
            }
            base.AI();
        }
        public override void PostDraw(Color lightColor)
        {
            base.PostDraw(lightColor);

            if (previousPositions.Count >= 5)
            {
                Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/Trail"); 
                Color color = Color.DarkGreen * 0.5f; 

                for (int i = 1; i < previousPositions.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        Vector2 start = previousPositions[i - 1] - Main.screenPosition;
                        Vector2 end = previousPositions[i] - Main.screenPosition;
                        Dust.NewDustDirect(start, 3, 3, DustID.Torch, -Projectile.velocity.X, -Projectile.velocity.Y, 1, default, 15);
                        Main.spriteBatch.Draw(texture, start, null, color, Projectile.rotation, texture.Size() * 0.5f, 0.5f, SpriteEffects.None, 0f);
                       
                    }
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            lightColor= Color.OrangeRed;
            return lightColor;
        }
    }
}