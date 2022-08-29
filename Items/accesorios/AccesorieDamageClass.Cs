using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod
{
    public class AccesorieDamageClass : GlobalItem
	{
		public float accesorieDamageCalculator(int n)
        {
			float a = 1+(n/100f);
			return a;
        }
	}
}
