using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod
{
    public class DashPlayer : ModPlayer
	{
		public const int DashDown = 0;
		public const int DashUp = 1;
		public const int DashRight = 2;
		public const int DashLeft = 3;
		public const int DashCooldown = 50;
		public const int DashDuration = 35;
		public const float DashVelocity = 10f;
		public int DashDir = -1;
		public bool DashEquipped;
		public int DashDelay = 0;
		public int DashTimer = 0;

		public override void ResetEffects()
		{
DashEquipped = false;
if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15) DashDir = DashDown;
else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[DashUp] < 15) DashDir = DashUp;
else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15) DashDir = DashRight;
else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15) DashDir = DashLeft;
else DashDir = -1;
		}
		public override void PreUpdateMovement()
		{
if (CanUseDash() && DashDir != -1 && DashDelay == 0)
{
	Vector2 newVelocity = Player.velocity;
	switch (DashDir)
	{
		case DashUp when Player.velocity.Y > -DashVelocity:
		case DashDown when Player.velocity.Y < DashVelocity:
{
	float dashDirection = DashDir == DashDown ? 1 : -1.3f;
	newVelocity.Y = dashDirection * DashVelocity;
	break;
}
		case DashLeft when Player.velocity.X > -DashVelocity:
		case DashRight when Player.velocity.X < DashVelocity:
{
	float dashDirection = DashDir == DashRight ? 1 : -1;
	newVelocity.X = dashDirection * DashVelocity;
	break;
}
		default:
return;
	}
	DashDelay = DashCooldown;
	DashTimer = DashDuration;
	Player.velocity = newVelocity;
}
if (DashDelay > 0) DashDelay--;
if (DashTimer > 0)
{
	Player.eocDash = DashTimer;
	Player.armorEffectDrawShadowEOCShield = true;
	DashTimer--;
}
		}
        private bool CanUseDash() => DashEquipped && Player.dashType == 1 && !Player.setSolar && !Player.mount.Active;
    }
}
