using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

public class GalearisSky : CustomSky
{
    private bool active;

    public override void Activate(Vector2 position, params object[] args) => active = true;

    public override void Deactivate(params object[] args) => active = false;

    public override bool IsActive() => active;

    public override void Reset() => active = false;

    public override void Update(GameTime gameTime) { }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        if (!active || maxDepth < float.MaxValue)
            return;

        Texture2D texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Common/Background/GalearisSky").Value;

        spriteBatch.Draw(texture,new Rectangle(0, 0,Main.screenWidth,Main.screenHeight),Color.White);
    }
}