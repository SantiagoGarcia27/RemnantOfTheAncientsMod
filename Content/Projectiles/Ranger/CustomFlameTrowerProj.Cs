using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    public class CustomFlameTrowerProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40; // The width of projectile hitbox
            Projectile.height = 40; // The height of projectile hitbox

            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.penetrate = -1; // Look at comments ExamplePiercingProjectile

            Projectile.alpha = 0; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
            Projectile.ArmorPenetration = 5;
        }    
        public override void AI()
        {
            int frameDelay = 2;
            Projectile.localAI[0] += 1f;
            Projectile.alpha += 1;
            //// Slow down
            Projectile.velocity *= 0.98f;

            

            // Loop through the 4 animation frames, spending 5 ticks on each
            // Projectile.frame — index of current frame
            if (++Projectile.frameCounter >= Main.projFrames[Projectile.type] + frameDelay)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            // Despawn this projectile after 1 second (60 ticks)
            // You can use Projectile.timeLeft = 60f in SetDefaults() for same goal
            if (Projectile.localAI[0] >= 60f)
                Projectile.Kill();

            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;

            Projectile.rotation = Projectile.velocity.ToRotation();
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
                // For vertical sprites use MathHelper.PiOver2
            }

            base.AI();
        }
        public static List<float> phase =
        [
            0,0,0,0,0,0,0
        ];
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 1; i <= 7; i++)
            {
                phase[i-1] = (Projectile.ai[1] / 7) * i;
            }
            base.OnSpawn(source);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Utils1.GetColorByID((int)Projectile.ai[2]) == Color.DarkOrange)
                target.AddBuff(BuffID.OnFire, Utils1.FormatTimeToTick(0, 0, 0, 3));

            else if (Utils1.GetColorByID((int)Projectile.ai[2]) == Color.GreenYellow)
                target.AddBuff(BuffID.CursedInferno, Utils1.FormatTimeToTick(0, 0, 0, 3));
            base.OnHitNPC(target, hit, damageDone);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // SoundEngine.PlaySound(Main.rand.NextBool() ? SoundID.Item57 : SoundID.Item58, Projectile.position);
            return false;
        }
        int idex = 0;
        Color color = Color.White;
        public override bool PreDraw(ref Color lightColor)
        {
            List<Color> colors = [Color.White];
            if (Utils1.GetColorByID((int)Projectile.ai[2]) == Color.DarkOrange)
            {
                int maxTimeLeft = 60; 
                colors = Utils1.GetColorGradiant(Color.OrangeRed, Color.Yellow, maxTimeLeft);       
              
            }
            if (Utils1.GetColorByID((int)Projectile.ai[2]) == Color.GreenYellow)
            {
                int maxTimeLeft = 60;
                Color curseFlameColorInternal = new Color(218, 253, 9);
                Color curseFlameColorExternal = new Color(55, 200, 26);

                colors = Utils1.GetColorGradiant(curseFlameColorInternal, curseFlameColorExternal, maxTimeLeft);  
            }
            if(idex < colors.Count) color = colors[idex];
            idex++;
            lightColor = color;
            return true;
        }
      
    }
}
