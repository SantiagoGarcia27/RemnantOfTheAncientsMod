using Microsoft.Xna.Framework;
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

		public virtual void CreateDust()
		{
		}
		public virtual void Shoot(Vector2 targetPos) { }
	}
}