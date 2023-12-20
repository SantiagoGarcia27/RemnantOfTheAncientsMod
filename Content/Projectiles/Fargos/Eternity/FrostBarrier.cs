using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Fargos.Eternity
{

    public class FrostBarrier : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Assets/PlaceHolder";
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Curecedball"); //projectile name

        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 999999;
            Projectile.light = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = -1;
            //Projectile.CloneDefaults(ProjectileID.BallofFire);

        }
        int debugg = 0;
        List<Vector2> points = new List<Vector2>();
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CheckActive(player);
            
            Projectile.Center = player.Center;
            Point playerPosition = new Point((int)player.position.X, (int)player.position.Y);

            int width = 20;
            int height = 20;
            Rectangle area = new Rectangle(playerPosition.X - width/2 * 16, playerPosition.Y - height / 2 *16, width * 16, height * 16);

            //if (debugg % 2 == 0)
            //{
            //    GenerarParticulasEnBordes(area.X, area.Y, area.Width, area.Height);
            //    debugg++;
            //}


            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.type != this.Type)
                {
                    if (area.Contains(new Point((int)projectile.position.X,(int)projectile.position.Y)) && (projectile.hostile || !projectile.friendly))
                    {
                        projectile.velocity *= -1;
                        projectile.friendly = true;
                        projectile.hostile = !projectile.friendly;
                        projectile.GetAlpha(Color.Blue);
                    }
                }
            }

         
            if(RemnantFargosSoulsPlayer.FrostBarrierCounter > 0)
            {
                RemnantFargosSoulsPlayer.FrostBarrierCounter--;
            }

        }

        private void GenerarParticulasEnBordes(int x, int y, int width, int height)
        {
            for (int i = x; i < x + width; i++)
            {
                SpawnParticle(i, y);                      // Borde superior
                SpawnParticle(i, y + height - 1);         // Borde inferior
            }

            for (int j = y + 1; j < y + height - 1; j++)
            {
                SpawnParticle(x, j);                     // Borde izquierdo
                SpawnParticle(x + width - 1, j);         // Borde derecho
            }
        }

        private void SpawnParticle(int x, int y)
        {
            // Cambia esto según las propiedades de tus partículas
            int particleType = 1;  // Tipo de partícula
            Color color = Color.Red;  // Color de la partícula

            Dust.NewDust(new Vector2(x, y), 1, 1, particleType, 0f, 0f, 0, color, 1f);
        }




        public void CheckActive(Player player)
        {
            if (player.HasBuff<FrostBarrierCouldown>() || RemnantFargosSoulsPlayer.FrostBarrierCounter == 0)
            {
                Projectile.Kill();
            }
         
        }
        public override void OnKill(int timeLeft)
        {
            Main.player[Projectile.owner].opacityForAnimation = 1f;
            Main.player[Projectile.owner].AddBuff(ModContent.BuffType<FrostBarrierCouldown>(), (int)Utils1.FormatTimeToTick(0, 0, 1, 0)/3);
          
            base.OnKill(timeLeft);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Main.player[Projectile.owner].opacityForAnimation = 0;
            RemnantFargosSoulsPlayer.FrostBarrierCounter = (int)Utils1.FormatTimeToTick(0, 0, 0, 10);
            base.OnSpawn(source);
        }
        public float fade = 2.6f;
        public override bool PreDraw(ref Color lightColor)
        {
               
            var texture = Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Fargos/Eternity/FrostBarrier");
            Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
            if (Main.myPlayer == Projectile.owner)
            {

                Color color = new Color(20,Color.White.R, Color.Green.G, Color.Green.B) * fade;
                Main.spriteBatch.Draw((Texture2D)texture, Projectile.Center - Main.screenPosition, null, Color.White, 0f, origin, 1, SpriteEffects.None, 1f);
            }
            return true;
        }


    }
}
