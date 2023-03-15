using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Projectiles
{
    public class AmathystSpiritGemSword : SpiritGemSwordModel
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/SpiritGemSword";
        public override Color color => Color.Violet;
    }
    public class TopazSpiritGemSword : SpiritGemSwordModel
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/SpiritGemSword";
        public override Color color => Color.Yellow;
    }
    public class SaphireSpiritGemSword : SpiritGemSwordModel
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/SpiritGemSword";
        public override Color color => Color.Blue;
    }
    public class EmeraldSpiritGemSword : SpiritGemSwordModel
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/SpiritGemSword";
        public override Color color => Color.Green;
    }
    public class RubySpiritGemSword : SpiritGemSwordModel
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/SpiritGemSword";
        public override Color color => Color.Red;
    }
    public class DiamondSpiritGemSword : SpiritGemSwordModel
    {
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/SpiritGemSword";
        public override Color color => Color.White;
    }

    public abstract class SpiritGemSwordModel : ModProjectile
    {
        public abstract Color color { get; }
        public override string Texture => "RemnantOfTheAncientsMod/Projectiles/SpiritGemSword";
        public override void SetDefaults()
        {
            Projectile.width = 36;       //projectile width
            Projectile.height = 36;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 300;   //how many time this projectile has before disepire
            Projectile.light = 1.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.7f;
            AIType = ProjectileID.InfluxWaver;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            Projectile.rotation += Projectile.direction * 0.8f;

            if (Projectile.alpha > 70)
            {
                Projectile.alpha -= 15;
                if (Projectile.alpha < 70) Projectile.alpha = 70;
            }
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 10f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 235f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
                if (target)
                {
                    AdjustMagnitude(ref move);
                    Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
                    AdjustMagnitude(ref Projectile.velocity);
                }

            }
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 106f) vector *= 26f / magnitude;
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position; // Position to use for dusts

            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2();
            usePos += rotVector * 16f;

            const int NUM_DUSTS = 20;

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                // Create a new dust
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, 81);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }
        public override Color? GetAlpha(Color lightColor) => color;

    }
}
