using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Menu;

internal class MenuBackground : ModSurfaceBackgroundStyle
{
	private static readonly string TexPath = RemnantOfTheAncientsMod.PlaceHolderPath;
    private const string menuAssetPath = "Common/Menu";

    public override void Load()
    {
        BackgroundTextureLoader.AddBackgroundTexture(Mod, TexPath);
        //BackgroundTextureLoader.AddBackgroundTexture(Mod, $"{Mod.Name}/Common/Menu/BackgroundFar");
        On_Main.DrawBG += DrawMenuSky;
        base.Load();
    }

    public override void Unload()
    {
        On_Main.DrawBG -= DrawMenuSky;
        base.Unload();
    }

    private void DrawMenuSky(On_Main.orig_DrawBG orig, Main self)
    {
        bool isROTAMenu = Main.gameMenu && MenuLoader.CurrentMenu is RemnantModMenu;

        if (isROTAMenu) ReplaceClouds();
        else RestoreClouds();


        if (!Main.gameMenu || MenuLoader.CurrentMenu is not RemnantModMenu)
        {
            orig(self);
            return;
        }

        Color originalColor = Main.ColorOfTheSkies;

        Main.ColorOfTheSkies = new Color(111, 170, 183);

        try
        {
            orig(self);
        }
        finally
        {
            Main.ColorOfTheSkies = originalColor;
        }
    }

    public override void ModifyFarFades(float[] fades, float transitionSpeed)
	{
		for (int i = 0; i < fades.Length; i++)
		{
			if (i == Slot)
			{
				fades[i] += transitionSpeed;
				if (fades[i] > 1f) fades[i] = 1f;
			}
			else
			{
				fades[i] -= transitionSpeed;
				if (fades[i] < 0f) fades[i] = 0f;
			}
		}
	}
	

	public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
	{
        return BackgroundTextureLoader.GetBackgroundSlot(Mod,RemnantOfTheAncientsMod.PlaceHolderWithoutModPath);
	}

	/*public override int ChooseFarTexture()
	{    
        return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Common/Menu/BackgroundFar");
	}*/

	public override int ChooseMiddleTexture()
	{
        return BackgroundTextureLoader.GetBackgroundSlot(Mod, RemnantOfTheAncientsMod.PlaceHolderWithoutModPath);
    }

	public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
	{
		return false;
	}


    private Asset<Texture2D>[] customCloud;
    private Asset<Texture2D>[] originalClouds;

    private void ReplaceClouds()
    {
        if (originalClouds != null)
            return;

        customCloud =
        [
            Mod.Assets.Request<Texture2D>("Common/Menu/RemnantCloud"),
            Mod.Assets.Request<Texture2D>("Common/Menu/RemnantCloud_3")
        ];

        originalClouds = new Asset<Texture2D>[CloudID.Count];

        for (int i = 0; i < originalClouds.Length; i++)
        {
            originalClouds[i] = TextureAssets.Cloud[i];
            TextureAssets.Cloud[i] = customCloud[i % 2 == 0 ? 0 : 1];
        }
    }

    private void RestoreClouds()
    {
        if (originalClouds == null)
            return;

        for (int i = 0; i < originalClouds.Length; i++)
        {
            TextureAssets.Cloud[i] = originalClouds[i];
        }

        originalClouds = null;
    }
}
