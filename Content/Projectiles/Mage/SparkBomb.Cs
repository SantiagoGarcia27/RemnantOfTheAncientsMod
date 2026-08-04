using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static System.Net.Mime.MediaTypeNames;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class SparkBomb : ModProjectile
    {
        public override string Texture => GetTexture();
        internal WandType wandType;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
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
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                Main.NewText("PreDraw");
            }
            string texture = "Terraria/Images/Projectile_" + ProjectileID.BallofFire;      
            return texture;
        }
        int noCollideTimmer;
        public override void AI()
        {
           /* Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);*/
            Projectile.rotation++;
            Projectile.scale = Projectile.ai[1];
            Dictionary<int, float> dustScale = new()
            {
                { wandType.dustID, 8f },
                { DustID.MeteorHead, 2f },
                { DustID.Meteorite, 4f }

            };
            if (noCollideTimmer > 0) noCollideTimmer--;
            Projectile.tileCollide = noCollideTimmer <= 0;

            if (Projectile.timeLeft < 20000)
            {
                if (RemnantOfTheAncientsMod.ParticleMeter(4) != 0)
                {
                    wandType ??= new WandType((int)Projectile.ai[2]);
                    foreach(int dustID in dustScale.Keys)
                    {
                        Vector2 pos = new(Projectile.Center.X + Main.rand.Next(-Projectile.height/2, Projectile.height/2), Projectile.Center.Y + Main.rand.Next(-2, 2).ToCoordinatePosition());
                        Vector2 vel = new(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(0f, -2f));
                        float scale = Main.rand.NextFloat(0.5f, dustScale[dustID]);
                        Dust currentDust = Dust.NewDustDirect(pos, 10, Projectile.height, dustID, SpeedX: vel.X, SpeedY: vel.Y, 0, default, scale);
                        currentDust.velocity = Projectile.velocity;
                        currentDust.noGravity = true;
                    }
                }
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            wandType = new WandType((int)Projectile.ai[2]);
            Projectile.scale = Projectile.ai[1];
            Projectile.Size *= Projectile.scale;
            Projectile.netUpdate = true;
            noCollideTimmer = Utils1.FormatTimeToTick(Second: 3); 
            base.OnSpawn(source);
        }
     

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 10;
            int range = 10;
            
            ApplyDamage(range);
           

            for (int i = -range; i < range; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int antiLagg = NUM_DUSTS + 1 - RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS);
                    if (Main.LocalPlayer.infernoCounter % antiLagg == 0)
                    {
                        Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(20));
                        Vector2 pos = Projectile.Bottom + DistanceUtils.ToCoordenatePosition(new Vector2(i, j));
                        int dustId = Dust.NewDust(pos, 4, 4, wandType.dustID, -velocity.X, -velocity.Y, 0, wandType.color, Main.rand.NextFloat(0.5f, 10f));
                        Main.dust[dustId].noGravity = true;
                    }
                }
            }

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode with {Pitch = -0.8f,Volume = 2f});
            SoundEngine.PlaySound(SoundID.Item14 with {Pitch = -1.5f, Volume = 1.2f});
            SoundEngine.PlaySound(SoundID.Item62 with {Pitch = -0.3f,Volume = 0.8f});

            Vector2 shakeCenter = Projectile.Center;
            Vector2 shakeDirection = Vector2.UnitY;
            float shakeStrenght = 50f;
            float shakeVivrationCycles = 6f;
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(shakeCenter, shakeDirection, shakeStrenght, shakeVivrationCycles, 25,1500f));
            base.OnKill(timeLeft);
        }
        internal void ApplyDamage(int range)
        {
            int ownerCrit = (int)Main.player[Projectile.owner].GetCritChance(DamageClass.Magic);
            int damage = wandType.damage * 6;

            foreach (NPC npc in Main.ActiveNPCs)
            {
                float distance = npc.Distance(Projectile.Center);
                if (distance >= (range + 1).ToCoordinatePosition() || npc.friendly || npc.immortal) continue;

                int hitDirection = npc.Center.X < Projectile.Center.X ? -1 : 1;
                bool canCrit = Main.rand.Next(100) < ownerCrit;
                int reductionFactor = (int)distance.ToTilePosition()/2;
                int finalDamage = damage / (1 + reductionFactor);
                npc.AddBuff(wandType.debuffID, Utils1.FormatTimeToTick(0, 0, 0, 5));
                npc.SimpleStrikeNPC(finalDamage, hitDirection, canCrit, knockBack: 34, DamageClass.Magic, true);
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(noCollideTimmer);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            noCollideTimmer = reader.ReadInt32();
            wandType = new WandType((int)Projectile.ai[2]);
            base.ReceiveExtraAI(reader);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            string texturePath = "Terraria/Images/Projectile_" + 
                ((int)Projectile.ai[2] == ItemID.WandofSparking ? ProjectileID.BallofFire : ProjectileID.BallofFrost);
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(texturePath);
          
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(sourceRectangle), Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Vector2 drawOrigin = frame.Size() / 2f;
            lightColor = Main.rand.Next(0, 2) switch
            {
                0 => Color.Gray,
                1 => Color.White,
                2 => Color.DarkBlue,
                _ => Color.White
            };
            for (int k = 2; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos2 = Projectile.oldPos[k] + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY -10);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos2, frame, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return false;//base.PreDraw(ref lightColor);
        }
    }

    class WandType
    {
        public WandType(int itemBase)
        {
            Item item = new(itemBase);
            damage = item.damage;
            projectileID = item.shoot;
            color = itemBase == ItemID.WandofSparking ? Color.Orange : Color.Cyan;
            debuffID = itemBase == ItemID.WandofSparking ?BuffID.OnFire : BuffID.Frostburn;
            dustID = itemBase == ItemID.WandofSparking ? DustID.Torch : DustID.IceTorch;
        }
        public int damage { get; set; }
        public Color color { get; set; }
        public int projectileID { get; set; }
        public int debuffID { get; set; }
        public int dustID { get; set; }
    }
}
