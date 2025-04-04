using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.TmodClassOverride
{
    public abstract class MinionModProjectile : ModProjectile
    {

        public virtual NPC FindTarget(Player player, float ViewDistance,bool ignoreTiles = false)
        {
            float targetDist = ViewDistance;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC target = Main.npc[player.MinionAttackTargetNPC];
                if (!ignoreTiles)
                {
                    if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
                    {
                        return target;
                    }
                }
                else
                {
                    return target;
                }
            }
            else
            {
                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (npc.CanBeChasedBy(this, true) && npc.active && npc.defDefense < 100)
                    {
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);


                        if (distance < targetDist)
                        {
                            if (!ignoreTiles)
                            { 
                                if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                                {
                                    return npc;
                                }
                            }
                            else
                            {
                                return npc;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public virtual void Orbit(Vector2 center, float distance, float speed)
        {
            // Calcula el ángulo entre el proyectil y el centro del círculo
            Vector2 direction = center - Projectile.Center;
            float angle = (float)Math.Atan2(direction.Y, direction.X);

            // Calcula la posición del proyectil en el círculo
            Projectile.Center = center + new Vector2((float)Math.Cos(angle) * distance, (float)Math.Sin(angle) * distance);

            // Calcula la velocidad del proyectil
            Projectile.velocity = new Vector2(speed * (float)Math.Cos(angle + (float)Math.PI / 2), speed * (float)Math.Sin(angle + (float)Math.PI / 2));
        }
        public virtual void Spin(ref float spinTimmer, float spinTimmerMax, float speed)
        {
            
            while (spinTimmer <= spinTimmerMax)
            {
                Projectile.rotation += speed;
                spinTimmer++;
            }
            spinTimmer = 0;
        }

        public virtual void FaceToObjetive(Vector2 objetive, float rotationAngle = 0)
        {
            Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.AngleTo(objetive) + MathF.Tau, 0.2f) + rotationAngle;
        }
    }
}
