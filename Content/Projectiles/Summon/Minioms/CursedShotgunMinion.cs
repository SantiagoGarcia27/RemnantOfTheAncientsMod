using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions;
using RemnantOfTheAncientsMod.Common.TmodClassOverride;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms
{
    public class CursedShotgunMinion : MinionModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 62;
            Projectile.height = 22;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1f;
            Projectile.alpha = 1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = -1;
            Projectile.tileCollide = false;
        }
        enum Attacks
        {
            Spin,
            Shoot,
            Dash,
            MoveToOposite
        }

        float shootTimmer = 0;
        float shootTimmerMax = Utils1.FormatTimeToTick(0, 0, 0, 3);
        float spinTimmer = 0;
        float spinTimmerMax = Utils1.FormatTimeToTick(0, 0, 0, 0.5f);
        float dashTimmer = 0;


        Attacks currentAttck = 0;

        NPC target = null;
        bool FoundTarget = false;

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (!CheckActive(owner))
                return;

            if (target == null || !target.active || !FoundTarget || owner.HasMinionAttackTargetNPC)
            {            
                float ViewDistance = 500;
                target = FindTarget(owner, ViewDistance,true);
                FoundTarget = target != null;
            }

            Color color = owner.ZoneCrimson ? Color.Red : Color.Purple;
            Lighting.AddLight(Projectile.Center, color.ToVector3());

            int shootDistance = 2 * 16;
            if (target != null)
            {
                float distanceToTarget = Vector2.Distance(Projectile.Center, target.Center);
                bool inRangeToShoot = distanceToTarget < shootDistance;
                bool inRangeUnbind = distanceToTarget > 500;

                if (inRangeUnbind)
                {
                    target = null;
                    return;
                }
                switch (currentAttck)
                {
                    case Attacks.Spin:
                        Spin(inRangeToShoot);
                        break;
                    case Attacks.Shoot:
                        ShootAttack(owner);
                        break;
                    case Attacks.Dash:
                        Dash(inRangeToShoot);
                        break;
                    case Attacks.MoveToOposite:
                        MoveToOposite(inRangeToShoot);
                        break;
                }
                if (currentAttck == Attacks.Shoot || currentAttck == Attacks.Dash)
                {
                    FaceToObjetive(target.Center);
                }
            }
            else
            {
                MoveToPlayer(owner);
            }
            base.AI();
        }
        float rotation = 0;
        public void MoveToPlayer(Player owner)
        {
            float DistanceToOwner = owner.Center.Distance(Projectile.Center);
            FaceToObjetive(owner.Center, 90f);
            if (DistanceToOwner > 600)
            {
                Projectile.position = owner.position;
            }
            else
            {
                if (DistanceToOwner > 200)
                {
                    Projectile.velocity = (owner.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 8.5f;
                }
                else
                {
                    float circleRadius = 100f; // distancia predefinida alrededor del jugador

                    Projectile.velocity = Vector2.Zero;
                   // Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.AngleTo(owner.Center) + MathF.Tau, 0.2f) + 90f;
                   
                    int numMinions = owner.ownedProjectileCounts[ModContent.ProjectileType<CursedShotgunMinion>()];
                    if (numMinions < 1) numMinions = 1;
                    float angleStep = (2 * MathF.PI) / numMinions; // paso de ángulo entre cada instancia

                    int index = Projectile.identity % numMinions; // índice de la instancia actual
                    float angle = index * angleStep; // ángulo de la instancia actual
                    angle += rotation * 0.01f;
                  
                    Projectile.Center = owner.Center + new Vector2(circleRadius, 0).RotatedBy(angle);
                    rotation += 0.1f; // incrementa la rotación
                }
            }
        }

        void MoveToOposite(bool inRangeToShoot)
        {
            if (!inRangeToShoot)
                currentAttck = Attacks.Dash;
            else
            {
                Vector2 newPos = Projectile.Center;

                float moveStrenght = 1;
                if (target.position.X < Projectile.position.X)
                {
                    newPos.X -= moveStrenght * 16;
                }
                else
                {
                    newPos.X += moveStrenght * 16;
                }
                Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.AngleTo(newPos) + MathF.Tau, 0.2f);
                Projectile.velocity = newPos - Projectile.Center;
                Projectile.velocity.Normalize();
                currentAttck = Attacks.Shoot;
            }
        }
        void Dash(bool inRangeToShoot)
        {
            if (!inRangeToShoot)
            {
                float dashTimmerMax = Utils1.FormatTimeToTick(0, 0, 0, 0.4f);
                if (dashTimmer <= dashTimmerMax)
                {
                    Projectile.velocity = Vector2.Normalize(target.Center - Projectile.Center) * 8.5f;
                    dashTimmer++;
                }
                else
                {
                    dashTimmer = 0;
                    currentAttck = Attacks.Spin;
                }
            }
            else
            {
                dashTimmer = 0;
                currentAttck = Attacks.Shoot;
                shootTimmer = (int)Utils1.GetValueFromPorcentage(shootTimmerMax, 75);
            }

        }
        void Spin(bool inRangeToShoot)
        {
            Projectile.velocity = Vector2.Zero;         
            Spin(ref spinTimmer, spinTimmerMax, 0.3f);
            currentAttck = !inRangeToShoot ? Attacks.Dash : Attacks.MoveToOposite;      
        }
        void ShootAttack(Player owner)
        {
            if (shootTimmer >= shootTimmerMax)
            {
                int ProjectileAmmount = 5;
                shootTimmer = 0;

                int projType = ProjectileID.Bullet;
                Item playerAmmo = Utils1.ChooseAmmo(new Item(ItemID.Shotgun), AmmoID.Bullet);
                if (playerAmmo != null)
                {
                    int index = owner.FindItem(playerAmmo.type);

                    if (index != -1)
                    {
                        if (playerAmmo.consumable)
                        {
                            Utils1.ConsumeItem(owner, owner.inventory, index);
                        }

                        projType = playerAmmo.shoot;
                        if (projType == ProjectileID.Bullet)
                            applyCorrutpBullet();
                    }
                }
                else
                {
                    applyCorrutpBullet();
                }
                void applyCorrutpBullet()
                {
                    if (Main.rand.NextBool(4))
                    {
                        if (owner.ZoneCorrupt)
                            projType = ProjectileID.CursedBullet;
                        else if (owner.ZoneCrimson)
                            projType = ProjectileID.IchorBullet;
                    }
                }

                for (int i = 0; i < ProjectileAmmount; i++)
                {
                    Vector2 Velocity = Vector2.Normalize(target.Center - Projectile.Center) * 4.5f;
                    Vector2 newVelocity = Velocity.RotatedByRandom(MathHelper.ToRadians(15));
               
                    // Decrease velocity randomly for nicer visuals.
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);


                    float rangerBonus = 1 + owner.GetDamage(DamageClass.Ranged).Flat;
                    int Damage = (int)(Projectile.damage * rangerBonus);
                    float scale = 1;
                    if (playerAmmo == null)
                    {
                        Damage /= 2;
                        scale = 0.5f;
                    }

                   
                    var p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, newVelocity * 2f, projType, Damage, 10, Main.myPlayer);
                    Main.projectile[p].tileCollide = false;
                    Main.projectile[p].scale = scale;
                }
                currentAttck = Attacks.Spin;
            }
            else
            {
                shootTimmer++;
            }
        }
    
        //Vector2 targetPos;
        //float targetDist;
        //public NPC FindTarget(Player player)
        //{
        //    targetPos = Projectile.position;
        //    targetDist = ViewDistance;
        //    if (player.HasMinionAttackTargetNPC)
        //    {
        //        NPC target = Main.npc[player.MinionAttackTargetNPC];
        //        if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
        //        {
        //            targetDist = Vector2.Distance(Projectile.Center, targetPos);
        //            targetPos = target.Center;
        //            FoundTarget = true;
        //            return target;
        //        }
        //    }
        //    else
        //    {
        //        foreach (NPC npc in Main.ActiveNPCs)
        //        {
        //            if (npc.CanBeChasedBy(this, false) && npc.active)
        //            {
        //                float distance = Vector2.Distance(npc.Center, Projectile.Center);
        //                if ((distance < targetDist /*|| !FoundTarget*/) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
        //                {
        //                    targetDist = distance;
        //                    targetPos = npc.Center;
        //                    FoundTarget = true;
        //                    return npc;
        //                }
        //            }
        //        }
        //    }
        //    FoundTarget = false;
        //    return null;
        //}
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<CursedShotgunMinionBuff>());
                return false;
            }
          
            if (owner.HasBuff(ModContent.BuffType<CursedShotgunMinionBuff>()))
            {
                Projectile.timeLeft = 4;
            }
            return true;
        }
    }

}
