using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.RemPlayer
{
    public class CameraPlayer : ModPlayer
    {
        private Vector2 cameraPosition;
        private bool cameraLocked;

        private Vector2 startPosition;
        private Vector2 targetPosition;

        private int duration;
        private int timer;

        private bool moving;

        public void SetCameraPosition(Vector2 position)
        {
            cameraPosition = position;
            cameraLocked = true;
        }

        public void MoveCameraToPosition(Vector2 position, int timeInTicks)
        {
            startPosition = cameraLocked
                ? cameraPosition
                : Main.screenPosition + Main.ScreenSize.ToVector2() * 0.5f;

            cameraPosition = startPosition; // Importante

            targetPosition = position;

            duration = Math.Max(1, timeInTicks);
            timer = 0;

            moving = true;
            cameraLocked = true;
        }


        public void ResetCameraPosition()
        {
            cameraLocked = false;
        }
        public override void ModifyScreenPosition()
        {
            if (!cameraLocked)
                return;

            if (moving)
            {
                timer++;

                float progress = MathHelper.Clamp(
                    (float)timer / duration, 0f, 1f
                );

                progress = progress * progress * (3f - 2f * progress);

                cameraPosition = Vector2.Lerp(
                    startPosition,
                    targetPosition,
                    progress
                );

                if (timer >= duration)
                {
                    cameraPosition = targetPosition;
                    moving = false;

                    Main.NewText($"Camera finished: {cameraPosition}");
                }
            }

            Main.screenPosition = cameraPosition
                - Main.ScreenSize.ToVector2() * 0.5f;
        }
    }
}
