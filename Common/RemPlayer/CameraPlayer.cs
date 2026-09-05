using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.RemPlayer
{
    public class CameraPlayer : ModPlayer
    {
        private Vector2 cameraPosition;
        private bool cameraLocked;
        public void SetCameraPosition(Vector2 position)
        {
            cameraPosition = position;
            cameraLocked = true;
        }

        public void ResetCameraPosition()
        {
            cameraLocked = false;
        }
        public override void ModifyScreenPosition()
        {
            if (!cameraLocked) return;

            Vector2 target = cameraPosition - new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;

            Main.screenPosition = Vector2.Lerp(Main.screenPosition,target,1.0f);     
        }
    }
}
