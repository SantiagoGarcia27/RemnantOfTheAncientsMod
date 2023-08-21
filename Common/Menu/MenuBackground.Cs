using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Menu;

internal class MenuBackground : ModSurfaceBackgroundStyle
{
	private static readonly string TexPath = "RemnantOfTheAncientsMod/Common/Menu/Banner";

	public override void ModifyFarFades(float[] fades, float transitionSpeed)
	{
		for (int i = 0; i < fades.Length; i++)
		{
			if (i == base.Slot)
			{
				fades[i] += transitionSpeed;
				if (fades[i] > 1f)
				{
					fades[i] = 1f;
				}
			}
			else
			{
				fades[i] -= transitionSpeed;
				if (fades[i] < 0f)
				{
					fades[i] = 0f;
				}
			}
		}
	}

	public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
	{
		return BackgroundTextureLoader.GetBackgroundSlot(TexPath);
	}

	public override int ChooseFarTexture()
	{
		return BackgroundTextureLoader.GetBackgroundSlot(TexPath);
	}

	public override int ChooseMiddleTexture()
	{
		return BackgroundTextureLoader.GetBackgroundSlot(TexPath);
	}

	public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
	{
		return false;
	}
}
