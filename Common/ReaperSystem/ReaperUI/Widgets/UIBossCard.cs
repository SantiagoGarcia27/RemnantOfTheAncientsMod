using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ReaperSystem.ReaperUI;
using RemnantOfTheAncientsMod.Common.UI;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

/// <summary>
/// A boss card shown in the left-side list. Shows the boss head icon and name.
/// </summary>
public class UIBossCard : UIPanel
{
    public UIHoverImageButton HeadButton;
    public ReaperUpgradeRegister soulData;

    public UIBossCard(ReaperUpgradeRegister soulData, Asset<Texture2D> headTexture)
    {
        this.soulData = soulData;
        SetPadding(4);
        BackgroundColor = new Color(60, 60, 80, 180);
        BorderColor = new Color(100, 100, 120, 200);


        HeadButton = new UIHoverImageButton(headTexture, Lang.GetNPCNameValue(soulData.npcType), soulData.npc);
        HeadButton.Left.Set(4f, 0f);
        HeadButton.VAlign = 0.5f;
        HeadButton.Width.Set(28f, 0f);
        HeadButton.Height.Set(28f, 0f);
        HeadButton.OnLeftClick += (evt, el) =>
        {
            ReaperSoulsUIState.SelectedNpcType = soulData.npcType;
            ReaperSoulsUIState.SelectedMod = soulData.mod;
            SoundEngine.PlaySound(SoundID.MenuTick);
            ReaperSoulsUIState.RefreshDetailPanel();
        };
        Append(HeadButton);

        string name = Lang.GetNPCNameValue(soulData.npcType);
        if (name.Length > 18)
            name = name[..18] + "..";
        var nameLabel = new UIText(name, 0.8f);
        nameLabel.Left.Set(38f, 0f);
        nameLabel.VAlign = 0.5f;
        Append(nameLabel);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        NPC npc = new NPC();
        npc.SetDefaults(soulData.npcType);

        if (npc == null) return;

        Mod mod = npc.ModNPC.Mod ?? RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.Terraria;

        bool isSelected = ReaperSoulsUIState.SelectedNpcType == soulData.npcType;
        bool hasSoul = ReaperSoulUIExtras.SelectList(mod, npc.type);
        bool isActive = hasSoul && ReaperSoulUIExtras.SelectActiveList(mod, npc.type) > 0;

        if (isSelected)
        {
            BackgroundColor = new Color(100, 90, 40, 220);
            BorderColor = new Color(200, 180, 60, 255);
        }
        else if (isActive)
        {
            BackgroundColor = new Color(60, 60, 80, 180);
            BorderColor = new Color(80, 200, 80, 255);
        }
        else
        {
            BackgroundColor = new Color(60, 60, 80, 180);
            BorderColor = new Color(100, 100, 120, 200);
        }
    }
}