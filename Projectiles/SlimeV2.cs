using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
	//ported from my tAPI mod because I don't want to make artwork
	public class SlimeV2 : ModProjectile
	{
		public override void SetStaticDefaults() {
		Main.projFrames[Projectile.type] = 9;
		}
		public override void SetDefaults() {
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true; //so you can't hit enemies through walls
		}

		public override void AI() {
		}

	}
}