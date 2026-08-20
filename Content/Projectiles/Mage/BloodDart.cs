using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class BloodDart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 18;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        float scale => 2f;
        public override void SetDefaults()
        {
            Projectile.width = (int)(13 * scale);
            Projectile.height = (int)(13 * scale);
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 20000;
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.scale = scale;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;

            _trail ??= new PrimitiveTrail(TextureAssets.MagicPixel.Value, StripWidth, StripColor);
        }
        internal int stopTimer = Utils1.FormatTimeToTick(Second: 1);
        internal int graceTimeTimer = Utils1.FormatTimeToTick(Second: 0.5f);
        internal int npcindex = -1;

        private enum State
        {
            Search,
            Stay,
            Launch
        }
        private State _currentState = State.Search;
        private State currentState
        {
            get => _currentState;
            set
            {
                if (_currentState == value) return;
                _currentState = value;
                if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.netUpdate = true;
            }
        }
        private PrimitiveTrail _trail;

        public override void AI()
        {

            if (graceTimeTimer > 0)
            {
                graceTimeTimer--;
                Projectile.tileCollide = false;
            }
            else Projectile.tileCollide = true;

            if (npcindex == -1 && currentState == State.Search && graceTimeTimer <= 0)
            {
                npcindex = Projectile.FindTargetWithLineOfSight();
                if(npcindex != -1) currentState = State.Stay;
            }
            if(currentState == State.Launch || currentState == State.Search)
            {
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            else if(currentState == State.Stay)
            {
                Projectile.velocity = Vector2.Zero;
                if (npcindex < 0) return;
                if (!Main.npc[npcindex].active || Main.npc[npcindex].life <= 0) npcindex = Projectile.FindTargetWithLineOfSight();
                else
                {
                    NPC target = Main.npc[npcindex];
                    Projectile.rotation = Projectile.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver2;

                    if (stopTimer <= 0)
                    {
                        currentState = State.Launch;
                        Projectile.velocity = Projectile.DirectionTo(target.Center) * 20f;
                    }
                    else stopTimer--;
                }
            }
         
            if (Projectile.timeLeft < 20000)
            {
                if (RemnantOfTheAncientsMod.ParticleMeter(4) != 0)
                {
                    int dust5 = Dust.NewDust(Projectile.position, 1, Projectile.height, DustID.Blood);
                    Main.dust[dust5].velocity = Projectile.velocity;
                    Main.dust[dust5].noGravity = true;


                    if (Main.rand.NextBool(3))
                    {
                        int index = Main.rand.Next(Projectile.oldPos.Length / 2);

                        Vector2 pos = Projectile.oldPos[index];

                        Dust d = Dust.NewDustPerfect(pos, DustID.Blood);

                        d.velocity = new Vector2(
                            Main.rand.NextFloat(-0.6f, 0.6f),
                            Main.rand.NextFloat(0.3f, 1.2f));

                        d.noGravity = false;
                    }
                }
            }
            AnimateTexture();
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0f, 0f, 100, default(Color), 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        }

        int animationDelay = Utils1.FormatTimeToTick(Second: 0.3f);
        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= animationDelay)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        private Color StripColor(float progress)
        {
            Color c = Color.Lerp(
                new Color(120, 10, 10),
                new Color(30, 0, 0),
                progress);

            c *= 1f - progress;

            return c;
        }
        private float StripWidth(float progress)
        {
            float pulse = 1f + 0.15f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 10f);

            return MathHelper.Lerp(18f, 3f, progress) * pulse;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // _trail.Draw(Projectile.oldPos);
            DrawBloodTrail();
            return true;
        }
        private void DrawBloodTrail()
        {

            Texture2D pixel = TextureAssets.MagicPixel.Value;

            for (int i = 1; i < Projectile.oldPos.Length; i++)
            {
                Vector2 start = Projectile.oldPos[i - 1];
                Vector2 end = Projectile.oldPos[i];

                if (start == Vector2.Zero || end == Vector2.Zero)
                    continue;

                start += Projectile.Size / 2f;
                end += Projectile.Size / 2f;

                Vector2 diff = end - start;

                float length = diff.Length();

                if (length <= 0.1f)
                    continue;

                float rotation = diff.ToRotation();

                float progress = i / (float)(Projectile.oldPos.Length - 1);

                //if (i == 1) Main.NewText(length.ToString());

                float width = MathHelper.Lerp(0.01f, 0.02f, progress);

                Color color = Color.Lerp(
                    Color.DarkRed,
                    Color.Black,
                    progress);

                color *= 1f - progress;

                Main.EntitySpriteDraw(
                    pixel,
                    start - Main.screenPosition,
                    null,
                    color,
                    rotation,
                    new Vector2(0f, 0.5f),
                    new Vector2(length, width),
                    SpriteEffects.None,
                    0);
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)currentState);
            writer.Write(npcindex);
            writer.Write(stopTimer);
            writer.Write(graceTimeTimer);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            currentState = (State)reader.ReadInt32();
            npcindex = reader.ReadInt32();
            stopTimer = reader.ReadInt32();
            graceTimeTimer = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }
    }
}
