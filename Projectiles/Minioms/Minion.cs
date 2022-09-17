using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles.Minioms
{
    public abstract class Minion : ModProjectile
	{
		public override void AI() {
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
	}
}