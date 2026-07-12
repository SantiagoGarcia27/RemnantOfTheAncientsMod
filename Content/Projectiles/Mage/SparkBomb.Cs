using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class SparkBomb : ModProjectile
    {
        public override string Texture => GetTexture();
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("SkyCutterS"); 

        }
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20000;
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.7f;
            AIType = -1;
        }
        public string GetTexture()
        {
            string a = "Terraria/Images/Projectile_" + ((int)Projectile.localAI[0] == ProjectileID.WandOfSparkingSpark? ProjectileID.BallofFire: ProjectileID.BallofFrost);
            return a;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

      
            if (Projectile.timeLeft < 20000)
            {
                if (RemnantOfTheAncientsMod.ParticleMeter(4) != 0)
                {
                    bool spark = Projectile.localAI[0] == ProjectileID.WandOfSparkingSpark;
                    int dust5 = Dust.NewDust(new Vector2(Projectile.position.X + Main.rand.Next(-1, 4) * 16,Projectile.position.Y), 1, Projectile.height, spark? DustID.Torch : DustID.IceTorch,0,0,0,default,5f);
                    Main.dust[dust5].velocity = Projectile.velocity;
                    Main.dust[dust5].noGravity = true;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 10;
            int range = 10;
            bool spark = Projectile.localAI[0] == ProjectileID.WandOfSparkingSpark;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if(npc.Distance(Projectile.Center) < ((range + 1) * 16) && !npc.friendly)
                {
                   
                    int buff = spark ? BuffID.OnFire : BuffID.Frostburn;
                    int damage = spark ? new Item(ItemID.WandofSparking).damage : new Item(ItemID.WandofFrosting).damage;
                    npc.AddBuff(buff, Utils1.FormatTimeToTick(0, 0, 0, 5));
                    int dir = npc.direction == 0? 0: -npc.direction;
                    int crit = (int)Main.player[Projectile.owner].GetCritChance(DamageClass.Magic);

                    npc.SimpleStrikeNPC(damage * 2, dir, Main.rand.NextBool(100 - crit), 4, DamageClass.Magic, true);
                }
            }

            for (int i = -range; i < range; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    int antiLagg = NUM_DUSTS + 1 - RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS);
                    if (Main.LocalPlayer.infernoCounter % antiLagg == 0)
                    {
                        Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(20));
                        Color color = Projectile.localAI[0] == ProjectileID.WandOfSparkingSpark ? Color.Orange : Color.Cyan;
                        Dust.NewDust(Projectile.position + new Vector2(i, j)*16, 4, 4, spark ? DustID.Torch : DustID.IceTorch, -velocity.X, -velocity.Y, 0, color, Main.rand.NextFloat(0.5f, 10f));
                    }
                }
            }

            base.OnKill(timeLeft);
        }   

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(GetTexture());
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(sourceRectangle), Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;//base.PreDraw(ref lightColor);
        }
    }
}
