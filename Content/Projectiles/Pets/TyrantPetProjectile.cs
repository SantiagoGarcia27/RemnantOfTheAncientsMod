using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Pets
{
	public class TyrantPetProjectile : ModProjectile
	{
        // Incluye los segmentos del cuerpo y la cola.
        private int CantidadSegmentos = 16;
        private const float Separacion = 18f;

        private Vector2[] segmentos;
        private bool inicializado;

        public override void SetStaticDefaults()
        {
            Main.projPet[Type] = true;
            Main.projFrames[Type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DestroyerPet);
            AIType = ProjectileID.DestroyerPet;

            Projectile.timeLeft = 2;
            Projectile.friendly = false;
            Projectile.hostile = false;

            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
        }

        public override bool ShouldUpdatePosition()
        {
            return true;
        }

        private void ReiniciarSegmentos()
        {
            segmentos = new Vector2[CantidadSegmentos];

            Vector2 direccion = Projectile.velocity.SafeNormalize(Vector2.UnitX);

            for (int i = 0; i < CantidadSegmentos; i++)
            {
                segmentos[i] = Projectile.Center - direccion * Separacion * (i + 1);
            }

            inicializado = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.active && !player.dead &&
                player.HasBuff(ModContent.BuffType<TyrantPetBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }

        public override void PostAI()
        {
            // Posición prevista de la cabeza.
            Vector2 cabeza = Projectile.Center + Projectile.velocity;

            if (!inicializado) ReiniciarSegmentos();

            // Evita que el cuerpo se estire al teletransportarse.
            if (Vector2.DistanceSquared(cabeza, segmentos[0]) > 500f * 500f)
            {
                ReiniciarSegmentos();
                return;
            }

            Vector2 anterior = cabeza;

            for (int i = 0; i < segmentos.Length; i++)
            {
                Vector2 diferencia = anterior - segmentos[i];

                if (diferencia.LengthSquared() > 0.001f)
                {
                    segmentos[i] = anterior - Vector2.Normalize(diferencia) * Separacion;
                }

                anterior = segmentos[i];
            }

            Player player = Main.player[Projectile.owner];
            Vector2 velocity = new(Math.Abs(player.velocity.X), Math.Abs(player.velocity.Y));

            if (velocity.X > 1f || velocity.Y > 1f) Projectile.velocity *= 2.5f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (!inicializado) return false;

            Texture2D cabeza = TextureAssets.Projectile[Type].Value;

            Texture2D cuerpo = ModContent.Request<Texture2D>(Texture + "_Body").Value;

            Texture2D cola = ModContent.Request<Texture2D>(Texture + "_Tail").Value;

            // Dibujamos de la cola hacia la cabeza.
            for (int i = segmentos.Length - 1; i >= 0; i--)
            {
                Vector2 posicion = segmentos[i];

                Vector2 anterior = i == 0 ? Projectile.Center : segmentos[i - 1];

                float rotacion = (anterior - posicion).ToRotation() + MathHelper.PiOver2;

                Texture2D textura = i == segmentos.Length - 1 ? cola : cuerpo;

                Color luz = Lighting.GetColor((int)posicion.X.ToTilePosition(),(int)posicion.Y.ToTilePosition());

                Main.EntitySpriteDraw(textura, posicion - Main.screenPosition, null, luz, rotacion, textura.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
            }

            // La cabeza se dibuja por encima.
            Main.EntitySpriteDraw(cabeza, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, cabeza.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}