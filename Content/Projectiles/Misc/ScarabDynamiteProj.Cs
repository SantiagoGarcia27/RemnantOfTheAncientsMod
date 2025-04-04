using CalamityMod.NPCs.TownNPCs;
using FargowiltasSouls.Content.Tiles;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.StructureBuilder
{
    public class ScarabDynamiteProj : ModProjectile
    {
        public override string Texture => ItemLoader.GetItem(ModContent.ItemType<ScarabDynamite>()).Texture;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Instabridge");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 16;
            Projectile.timeLeft = 300;
        }

        public override bool? CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 TrialDirection = Projectile.DirectionTo(player.Center).SafeNormalize(Vector2.UnitX * player.direction);
            float OldRotation = Projectile.rotation;
            float num9 = TrialDirection.ToRotation() + (float)Math.PI / 2f;
            Projectile.rotation = Projectile.rotation.AngleLerp(num9, 0.2f);
            Projectile.rotation = Projectile.rotation.AngleTowards(num9, 0.05f);
            Vector2 vector2 = (Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
            SpawnDust();

            void SpawnDust()
            {
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + vector2 * 10f, DustID.BlueTorch, vector2 * 2f + Main.rand.NextVector2Circular(0.25f, 0.25f), 0, default, 2f);
                    dust.noGravity = true;
                    if (Main.rand.NextBool(3))
                    {
                        dust.velocity *= 1.5f;
                        dust.noGravity = false;
                        dust.scale /= 2f;
                    }
                }
                if (Main.rand.NextBool(3))
                {
                    Point scarabBombDigDirectionSnap = GetScarabBombDigDirectionSnap8();
                    Dust.NewDustPerfect(Projectile.Center + vector2 * -10f, DustID.BlueTorch, scarabBombDigDirectionSnap.ToVector2() * 1.5f, 0, default, 2f).noGravity = true;
                }
                if (Main.rand.NextBool(15))
                {
                    Dust dust2 = Dust.NewDustPerfect(Projectile.Center + vector2 * 10f, 88, vector2 * 3f + Main.rand.NextVector2Circular(0.25f, 0.25f), 0, default, 2f);
                    dust2.noGravity = true;
                    if (Main.rand.NextBool(3))
                    {
                        dust2.velocity *= 1.5f;
                    }
                }
                bool DibujarRastro = Main.rand.NextBool(30);
                if (OldRotation != Projectile.rotation && Main.rand.NextBool(40))
                {
                    DibujarRastro = true;
                }
                if (DibujarRastro)
                {
                    float num10 = (float)Math.PI * 2f * Main.rand.NextFloat();
                    for (float num11 = 0f; num11 < 1f; num11 += 1f / 7f)
                    {
                        Vector2 spinningpoint = (num11 * ((float)Math.PI * 2f) + num10).ToRotationVector2();
                        spinningpoint *= new Vector2(1f, 0.3f);
                        spinningpoint = spinningpoint.RotatedBy(num9);
                        Dust dust3 = Dust.NewDustPerfect(Projectile.Center + spinningpoint + vector2 * 8f, DustID.BlueTorch, vector2 * 3f + spinningpoint);
                        dust3.noGravity = true;
                        dust3.fadeIn = 1.6f;
                    }
                }
            }

            base.AI();
        }


        public override void OnKill(int timeLeft)
        {
            Player player2 = Main.player[Projectile.owner];
            Vector2 v = Projectile.DirectionTo(player2.Center);
            float num938 = v.ToRotation() + (float)Math.PI / 2f;
            num938 += (float)Math.PI;
            Point scarabBombDigDirectionSnap = GetScarabBombDigDirectionSnap8();
            num938 = scarabBombDigDirectionSnap.ToVector2().ToRotation() + (float)Math.PI / 2f;


            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            Projectile.Resize(80, 80);
            for (int num939 = 0; num939 < 60; num939++)
            {
                Dust dust70 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
                dust70.velocity.Y -= 0.5f;
                Dust dust2 = dust70;
                dust2.velocity *= 1.2f;
                dust70.color = Color.Black * 0.9f;
                if (Main.rand.NextBool(2))
                {
                    dust70.scale = 0.5f;
                    dust70.fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    dust70.color = Color.Black * 0.8f;
                }
            }
            for (int num940 = 0; num940 < 30; num940++)
            {
                Dust dust71 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 59, 0f, 0f, 100);
                dust71.noGravity = true;
                if (Math.Abs(Projectile.velocity.X) > 0.25f)
                {
                    Projectile.velocity.X *= 0.25f / Math.Abs(Projectile.velocity.X);
                }
                dust71.velocity.Y -= 0.5f;
                dust71.fadeIn = 1.2f;
                Dust dust2 = dust71;
                dust2.velocity *= 8f;
                dust71.velocity = dust71.velocity.RotatedBy(num938);
                dust71 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 59, 0f, 0f, 100, default(Color), 1.5f);
                dust2 = dust71;
                dust2.velocity *= 2f;
                dust71.velocity = dust71.velocity.RotatedBy(num938);
                dust71.velocity.Y -= 1.5f;
                dust71 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 59, 0f, 0f, 100, default(Color), 1.5f);
                dust71.noGravity = true;
                dust71.velocity.Y -= 1f;
                dust71.fadeIn = 2f;
                dust2 = dust71;
                dust2.velocity *= 4f;
                dust71.velocity = dust71.velocity.RotatedBy(num938);
            }
            bool flag4 = Math.Abs(scarabBombDigDirectionSnap.X) + Math.Abs(scarabBombDigDirectionSnap.Y) == 1;
            for (int num941 = 1; num941 <= 3; num941++)
            {
                float num942 = (float)Math.PI * 2f * Main.rand.NextFloat();
                for (float num943 = 0f; num943 < 1f; num943 += 1f / 12f)
                {
                    float f6 = (float)Math.PI * 2f * num943 + num942;
                    Vector2 value13 = f6.ToRotationVector2();
                    value13 *= new Vector2(0.7f, 0.3f);
                    Dust dust72 = Dust.NewDustPerfect(Projectile.Center, 59, value13);
                    dust72.fadeIn = 2f;
                    dust72.noGravity = true;
                    Dust dust2 = dust72;
                    dust2.velocity *= num941 + Main.rand.NextFloat() * 0.6f;
                    dust72.velocity.Y -= num941 * 0.8f;
                    dust72.velocity = dust72.velocity.RotatedBy(num938);
                    if (flag4)
                    {
                        dust2 = dust72;
                        dust2.velocity += scarabBombDigDirectionSnap.ToVector2() * (Main.rand.NextFloat() * 11f + 6f);
                    }
                    else
                    {
                        dust2 = dust72;
                        dust2.velocity += scarabBombDigDirectionSnap.ToVector2() * (Main.rand.NextFloat() * 7f + 5f);
                    }
                }
            }
            Projectile.Resize(10, 10);
            if (Projectile.owner == Main.myPlayer)
            {
                Point centerTile = Projectile.Center.ToTileCoordinates();
                Vector2 centerWorld = centerTile.ToWorldCoordinates();
                bool wallSplode = false;
                int HoleDeep = 50;//15
                if (scarabBombDigDirectionSnap.X == 0 || scarabBombDigDirectionSnap.Y == 0)
                {
                    HoleDeep = (int)(HoleDeep * Math.Sqrt(2.0));
                }
                for (int num948 = 0; num948 < HoleDeep; num948++)
                {
                    Vector2 vec = centerWorld + scarabBombDigDirectionSnap.ToVector2() * 16f * num948 * 1f;
                    Point p2 = vec.ToTileCoordinates();
                    if (Projectile.ShouldWallExplode(p2.ToWorldCoordinates(), 9999, p2.X - 1, p2.X + 1, p2.Y - 1, p2.Y + 1))
                    {
                        wallSplode = true;
                        break;
                    }
                }
                for (int num949 = 0; num949 < HoleDeep; num949++)
                {
                    Vector2 vec2 = centerWorld + scarabBombDigDirectionSnap.ToVector2() * 16f * num949 * 1f;
                    Point p3 = vec2.ToTileCoordinates();
                    Projectile.ExplodeTiles(p3.ToWorldCoordinates(), 9999, p3.X - 1, p3.X + 1, p3.Y - 1, p3.Y + 1, wallSplode);
                }
            }
        }
        private Point GetScarabBombDigDirectionSnap8()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = Projectile.DirectionTo(player.Center);
            Point result = new((!(vector.X > 0f)) ? 1 : (-1), (!(vector.Y > 0f)) ? 1 : (-1));
            if (Math.Abs(vector.X) > Math.Abs(vector.Y) * 2f)
            {
                result.Y = 0;
            }
            else if (Math.Abs(vector.Y) > Math.Abs(vector.X) * 2f)
            {
                result.X = 0;
            }
            return result;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return base.OnTileCollide(oldVelocity);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            if(Main.tile[(int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16].HasTile)
                Projectile.velocity = Vector2.Zero;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}

