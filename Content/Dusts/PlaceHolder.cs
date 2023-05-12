using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Dusts
{
	public class PlaceHolder : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1.5f;
			dust.noLightEmittence= true;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;
			float light = 0.0f * dust.scale;
			//Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}