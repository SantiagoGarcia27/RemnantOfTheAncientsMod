using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;

class DesertAnnihilator_Intro
{
    public NPC npc { get; set; }
    public bool NoAI = true;
    private readonly List<TornadoParticle> TornadoParticles = new();
    public float ScreenAnimationTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
    public float SpawnerAnimationTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);

    public DesertAnnihilator_Intro(NPC npc)
    {
        this.npc = npc;
    }

    public void SpawnAnimationAI()
    {
        if (npc == null) return;
        UpdateTornado();
        npc.velocity = new Vector2(0, 10);

        if (DificultyUtils.InfernumMode && ScreenAnimationTimer > 0) ScreenAnimationTimer--;
        else ScreenAnimationTimer = 0;

        if (ScreenAnimationTimer <= 0 && SpawnerAnimationTimer >= 0)
        {
            if (SpawnerAnimationTimer <= 0)
            {
                NoAI = false;
                npc.alpha = 0;
                npc.netUpdate = true;

                Sandstorm.Happening = false;
                Sandstorm.TimeLeft = 0;
                Sandstorm.IntendedSeverity = (Sandstorm.Happening ? (0.4f + Main.rand.NextFloat()) : ((Main.rand.Next(3) != 0) ? (Main.rand.NextFloat() * 0.3f) : 0f));
                NetMessage.SendData(MessageID.WorldData);
            }
            else
            {
                if (npc.alpha > 0) npc.alpha--;
                Sandstorm.Happening = true;
                Sandstorm.TimeLeft = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
                Sandstorm.IntendedSeverity = (Sandstorm.Happening ? (0.4f + Main.rand.NextFloat()) : ((Main.rand.Next(3) != 0) ? (Main.rand.NextFloat() * 0.3f) : 0f));
                NetMessage.SendData(MessageID.WorldData);
                SpawnerAnimationTimer--;

            }

        }
    }
    // Kept client-side: particles and camera are cosmetic, but must never write NPC state.
    public void UpdateVisuals()
    {
        if (npc == null) return;

        UpdateTornado();

        if (!NoAI)
        {
            Main.LocalPlayer.GetModPlayer<CameraPlayer>().ResetCameraPosition();
            return;
        }

        Main.LocalPlayer.GetModPlayer<CameraPlayer>().SetCameraPosition(npc.Center);
        if (SpawnerAnimationTimer > 0)
            SpawnAnimation(npc.alpha);
    }

    private void SpawnAnimation(int alpha)
    {   
        for (int i = 0; i < RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.ParticleMeter(alpha); i++)
        {
            float angle = Main.rand.NextFloat(MathHelper.TwoPi);

            // Ancho del tornado
            float radius = Main.rand.NextFloat(25f, 75f);

            // Posición vertical
            float height = Main.rand.NextFloat(-npc.height * 1.2f, npc.height * 1.2f);

            // Posición inicial siguiendo una elipse horizontal
            Vector2 position = npc.Center + new Vector2(MathF.Cos(angle) * radius, height);

            int dustid = Main.rand.Next(0, 4) switch
            {
                0 => DustID.Smoke,
                1 => DustID.Sandnado,
                2 => DustID.Sandstorm,
                3 => DustID.Sandstorm,
                _ => DustID.SandstormInABottle
            };
            Dust dust = Dust.NewDustPerfect(position, dustid);

            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            dust.scale = Main.rand.NextFloat(1.2f, 2.2f);
            dust.alpha = 30;

            TornadoParticles.Add(new TornadoParticle
            {
                Dust = dust,

                Angle = angle,
                Radius = radius,
                Height = height,

                RotationSpeed = Main.rand.NextFloat(0.035f, 0.09f),
                VerticalSpeed = Main.rand.NextFloat(0.3f, 0.8f),

                Lifetime = 0,
                MaxLifetime = Main.rand.Next(45, 80)
            });
        }
    }
    private void UpdateTornado()
    {
        for (int i = TornadoParticles.Count - 1; i >= 0; i--)
        {
            TornadoParticle p = TornadoParticles[i];

            if (p.Dust == null || !p.Dust.active)
            {
                TornadoParticles.RemoveAt(i);
                continue;
            }

            p.Lifetime++;

            // Girar alrededor del eje horizontal
            p.Angle += p.RotationSpeed;

            // Subir lentamente
            p.Height -= p.VerticalSpeed;

            // Cuando llega demasiado arriba,
            // vuelve abajo para mantener el flujo continuo.
            if (p.Height < -npc.height * 1.2f)
            {
                p.Height = npc.height * 1.2f;
            }

            /*
             * La parte importante:
             *
             * El radio cambia según la altura.
             * Cerca del centro es estrecho,
             * arriba y abajo se abre.
             */
            float normalizedHeight = MathHelper.Clamp(Math.Abs(p.Height) / (npc.height * 1.2f), 0f, 1f);


            //Tamaño de ancho primer numero es minimo y el segundo maximo
            float minAncho = Main.rand.NextFloat(10f, 50f) * npc.scale;
            float maxAncho = Main.rand.NextFloat(80f, 90f) * npc.scale;
            float currentRadius = MathHelper.Lerp(minAncho, maxAncho, normalizedHeight);//15 85

            Vector2 offset = new Vector2(MathF.Cos(p.Angle) * currentRadius, p.Height);

            p.Dust.position = npc.Center + offset;

            // Evitamos que el Dust se vaya por su cuenta.
            p.Dust.velocity = Vector2.Zero;

            // Fade
            float fadeIn = MathHelper.Clamp(p.Lifetime / 10f, 0f, 1f);
            float fadeOut = MathHelper.Clamp((p.MaxLifetime - p.Lifetime) / 15f, 0f, 1f);

            p.Dust.scale *= 0.98f;

            p.Dust.color = Color.White * (fadeIn * fadeOut);

            if (p.Lifetime >= p.MaxLifetime)
            {
                p.Dust.active = false;
                TornadoParticles.RemoveAt(i);
                continue;
            }

            TornadoParticles[i] = p;
        }
    }
    public class TornadoParticle
    {
        public Dust Dust;

        public float Angle;
        public float Radius;
        public float Height;

        public float RotationSpeed;
        public float VerticalSpeed;

        public int Lifetime;
        public int MaxLifetime;
    }
}
