using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles
{
	public class BossMark : ModProjectile
	{
		public float Scale { get; }

		public string texture { get; set; }

		public override string Texture => getTexture();

		public override void SetDefaults()
		{
			Projectile.width = 36;
			Projectile.height = 36;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 0;
			Projectile.timeLeft = 4;
			Projectile.light = 1.75f;
			Projectile.scale = Scale;
			Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
		}
		private string getTexture()
		{
			if (texture != "")
			{
				return "RemnantOfTheAncientsMod/Content/Projectiles/Textures/PlaceHolder";
			}
			else
			{
				return texture;
			}
		}

	}
}