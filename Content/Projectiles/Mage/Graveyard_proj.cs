using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using System;
using Terraria;
using Terraria.Audio;
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
        bool isGrounded => Projectile.ai[2] == 27;
        int timmer = 0;
        int timmerMax = Utils1.FormatTimeToTick(Second: 0.2f);
        public override void AI()           
        {                                                      
            if(isGrounded)
            {
                if(timmer++ >= timmerMax)
                {
                    Projectile.rotation = Main.rand.Next(-200, -100);
                    Projectile.tileCollide = true;
                    Projectile.velocity += new Vector2(0, 0.4f);
                }
                else
                {
                    Projectile.rotation++;
                }
            }
            else
            {
                Projectile.velocity += new Vector2(0,0.4f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<CurseMarkBuff>(), Utils1.FormatTimeToTick(Second: 3));
            base.OnHitNPC(target, hit, damageDone);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            string fargos = Projectile.ai[2] != 27 ? $"{this.Mod.Name}/Content/Items/Weapons/Magic/Graveyard_glyph" : $"Terraria/Images/Item_{ItemID.Tombstone}";
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool PreKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            if (!isGrounded)
            {
                int exraProjectileNum = 2;
                Vector2 spawnPos = Projectile.position + new Vector2(0, 1).ToCoordenatePosition();

                Vector2 baseVelocity = new Vector2(0, -6);
                
                for (int i = -exraProjectileNum/2; i <= exraProjectileNum/2; i++)
                {
                    float angle = MathHelper.ToRadians(30f * i);

                    Vector2 velocity = baseVelocity.RotatedBy(angle);

                    int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(),spawnPos, velocity, ModContent.ProjectileType<Graveyard_proj>(), Projectile.damage,Projectile.knockBack, Main.myPlayer, ai2:27);
                    Main.projectile[p].tileCollide = false;
                    Main.projectile[p].usesLocalNPCImmunity = true;
                    Main.projectile[p].rotation = angle;
                    Main.projectile[p].penetrate = -1;
                }
            }
            return base.PreKill(timeLeft);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i <= RemnantOfTheAncientsMod.ParticleMeter(10); i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
            }
            if (!isGrounded)
            {
                Projectile.Kill();
            }

           
            return base.OnTileCollide(oldVelocity);
        }
    }
}
