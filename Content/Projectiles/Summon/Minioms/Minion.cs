using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms
{
	public abstract class Minion : ModProjectile
	{
		public override void AI()
		{
			CheckActive();
			Behavior();
		}

		public abstract void CheckActive();

		public abstract void Behavior();

		public virtual void SelectFrame()
		{
		}
        public virtual void AnimateTexture(int framesDelay = 9)
        {
            if (++Projectile.frameCounter >= framesDelay)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }

        public virtual void CreateDust()
		{
		}
		public virtual void Shoot(Vector2 targetPos) { }
	}
}