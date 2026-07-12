using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Projectiles
{
    public class VanillaProjectilesOverride : GlobalProjectile     
    {
        Player target = null;
        NPC owner = null;
        bool FoundTarget = false;
        static int timmer = 0;
        int timmerMax = 0;
        public override void AI(Projectile projectile)
        {

            if(projectile.type == ProjectileID.CrystalLeaf)
            {
                if(projectile.hostile == true || projectile.ai[2] != 0)
                {
                    projectile.Size = new Vector2(32, 32);
                    owner =  Main.npc[(int)projectile.ai[2] - 1];
                    if (owner != null)
                    {
                        projectile.Center = owner.Center - new Vector2(0, 20);
                        projectile.timeLeft = 2;
                        if (target == null || !target.active)
                        {
                            float ViewDistance = 200;
                            target = FindTarget(projectile, ViewDistance);
                            FoundTarget = target != null;
                        }

                        timmerMax = Utils1.FormatTimeToTick(0, 0, 0, 10);
                        if (timmer >= timmerMax)
                        {
                            if (FoundTarget )
                            {
                                Vector2 velocity = target.Center - projectile.Center;
                                velocity.Normalize();
                                var p = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, velocity, ProjectileID.CrystalLeafShot, projectile.damage, projectile.knockBack, projectile.owner);
                                Main.projectile[p].hostile = true;
                                Main.projectile[p].friendly = false;
                                timmer = 0;
                            }
                        }
                        else
                        {
                            timmer++;
                        }
                    }
                    return;
                }

                
            }


            base.AI(projectile);
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ProjectileID.AbigailMinion)
            {
                if(Main.rand.NextBool(10))
                    target.AddBuff(ModContent.BuffType<CurseMarkBuff>(), Utils1.FormatTimeToTick(Second: 2));
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public Player FindTarget(Projectile projectile, float ViewDistance)
        {
            foreach(Player player in Main.ActivePlayers)
            {
                if(projectile.Distance(player.Center) <= ViewDistance)
                {
                    return player;
                }
            }
            return null;
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {  
            //if (projectile.type == ProjectileID.CrystalLeaf)
            //{

            //    if (projectile.hostile == true || projectile.ai[2] != 0)
            //    {
            //        NPC owner = Main.npc[(int)projectile.ai[2]];
            //        Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Projectile_" + ProjectileID.CrystalLeaf).Value;

            //        //Main.spriteBatch.Draw(texture, (owner.Center - new Vector2(0, 5) * 16) - Main.screenPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            //        Main.EntitySpriteDraw(texture, owner.Center - Main.screenPosition + new Vector2(0f, owner.gfxOffY) - new Vector2(0, 5) * 16, null, Color.White, owner.rotation, new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), owner.scale, SpriteEffects.None, 0);
            //    }
            //}

            return base.PreDraw(projectile, ref lightColor);
        }

        public override bool InstancePerEntity => true;
    }
}
