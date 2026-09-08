using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SangarUtilities.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using static RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator.DesertAnnihilator;

class DesertAnnihilator_Animation
{
    public NPC npc { get; set; }
    Vector2 originalSize = new();
    public enum TextureType
    {
        Default,
        Jump,
        Shoot
    }

    float tickCounter => npc.ai[0];
    internal TextureType _CurrentTexture = TextureType.Default;
    public TextureType CurrentTexture
    {
        get => _CurrentTexture;
        set
        {
            if (_CurrentTexture != value)
            {
                _CurrentTexture = value;
                npc.netUpdate = true;
            }
        }
    }

    public DesertAnnihilator_Animation(NPC npc)
    {
        this.npc = npc;
    }

    public void setOriginalSize(Vector2 size)
    {
        originalSize = size;
    }
    public void UpdateScale()
    {
        if (npc == null) return;
        float scale = LifeSize();
        if (Math.Abs(npc.scale - scale) < 0.001f) return;

        npc.scale = scale;

        Vector2 oldCenter = npc.Center;

        npc.width = (int)(originalSize.X * npc.scale);
        npc.height = (int)(originalSize.Y * npc.scale);

        npc.Center = oldCenter;
        npc.netUpdate = true;
    }

    private float LifeSize()
    {
        if (npc == null) return 1f;
        float percentage = MathUtils.GetPorcentage(npc.life, npc.lifeMax);
        float maxValue = 1.25f;

        if (DificultyUtils.MasochistMode) maxValue = 4f;
        else if (DificultyUtils.EternityMode) maxValue = 2.5f;
        else if (DificultyUtils.InfernumMode) maxValue = 2.3f;
        else if (DificultyUtils.Death) maxValue = 2f;
        else if (DificultyUtils.Revengeance || DificultyUtils.ReaperMode) maxValue = 1.5f;
        else if (Main.masterMode) maxValue = 1.35f;
        else if (Main.expertMode) maxValue = 1.3f;
        return ApplyLifeSize(percentage, maxValue);
    }
    private float ApplyLifeSize(float percentage, float maxValue)
    {
        float valorMinimo = MathUtils.GetValueFromPorcentage(maxValue, 30);

        if (percentage < 0 || percentage > 100) return valorMinimo;

        float valorActual = MathUtils.GetValueFromPorcentage(maxValue, percentage);
        return Math.Max(valorActual, valorMinimo);
    }

    int frame = 0;
    public void UpdateAnimation()
    {
        if (npc == null || Main.dedServ) return;

        Texture2D Texture = TextureAssets.Npc[npc.type].Value;

        if (tickCounter % 5 == 0) if (auraFrameCounter++ == auraFrameAmmount - 1) auraFrameCounter = 0;

        if (tickCounter % 10 != 0) return;

        if (CurrentTexture == TextureType.Shoot)
        {
            if (frame == 0 || frame == 5) frame = 3;
            else if (frame == 3) frame = 4;
            else if (frame == 4) frame = 5;
        }
        else if (CurrentTexture == TextureType.Default) frame = 0;
        else frame = 0;

        npc.frame.Y = (Texture.Height / Main.npcFrameCount[npc.type]) * frame;
    }

    int auraFrameCounter = 0;
    int auraFrameAmmount = 6;
    public void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (npc == null) return;
        string baseTexture = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{TextureAssets.Npc[npc.type].Name}_Eternity" : TextureAssets.Npc[npc.type].Name;
        baseTexture = baseTexture.Replace("\\", "/");
        baseTexture = "RemnantOfTheAncientsMod/" + baseTexture;
        //Aura
        Texture2D TextureAura = (Texture2D)ModContent.Request<Texture2D>(baseTexture + "_Aura");
        int heightPerFrame = TextureAura.Height / auraFrameAmmount;
        Rectangle auraFrame = new Rectangle(x: 0, y: heightPerFrame * auraFrameCounter, width: TextureAura.Width, height: heightPerFrame);
        Color colorAura = npc.GetAlpha(drawColor);
        Vector2 positionAura = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY + npc.height / 10);
        Vector2 originAura = auraFrame.Size() * 0.5f;
        Main.EntitySpriteDraw(TextureAura, positionAura, auraFrame, colorAura, npc.rotation, originAura, npc.scale, SpriteEffects.None, 0);

        //Core
        Texture2D TextureCore = (Texture2D)ModContent.Request<Texture2D>(baseTexture + "_Core");
        Color colorCore = npc.GetAlpha(drawColor);
        Vector2 positionCore = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY + npc.height / 10);
        Vector2 originCore = TextureCore.Size() * 0.5f;
        Main.EntitySpriteDraw(TextureCore, positionCore, null, colorCore, npc.rotation, originCore, npc.scale, SpriteEffects.None, 1);

    }

    public bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (npc == null) return true;
        if (npc.IsABestiaryIconDummy) return true;
        string baseTexture = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{TextureAssets.Npc[npc.type].Name}_Eternity" : TextureAssets.Npc[npc.type].Name;
        baseTexture = baseTexture.Replace("\\", "/");
        baseTexture = "RemnantOfTheAncientsMod/" + baseTexture;

        Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(baseTexture);
        Color color = npc.GetAlpha(drawColor);

        Vector2 position = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
        Vector2 origin = npc.frame.Size() * 0.5f;
        Main.EntitySpriteDraw(Texture, position, npc.frame, color, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);

        return false;
    }
}
