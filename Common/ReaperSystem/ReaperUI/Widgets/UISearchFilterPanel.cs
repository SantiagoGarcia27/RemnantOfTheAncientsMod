using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

/// <summary>
/// Search bar and filter buttons at the top of the left list.
/// Uses Terraria's native UISearchBar for reliable text input handling.
/// </summary>
public class UISearchFilterPanel : UIElement
{
    private readonly UISearchBar _searchBar;
    private readonly UIPanel _searchBarPanel;
    private readonly UIPanel _modBtn;
    private readonly UIText _modBtnText;
    private readonly UIPanel _effectBtn;
    private readonly UIText _effectBtnText;
    private bool _didClickSearchBar;
    private static int _modFilterIndex;
    private static int _effectFilterIndex;
    private static List<Mod> _availableMods;

    private static string _searchText = "";
    public UISearchFilterPanel()
    {
        _searchBarPanel = new UIPanel();
        _searchBarPanel.SetPadding(0);
        _searchBarPanel.Width.Set(0f, 1f);
        _searchBarPanel.Height.Set(28f, 0f);
        _searchBarPanel.BackgroundColor = new Color(35, 35, 55, 220);
        _searchBarPanel.BorderColor = new Color(80, 80, 100, 200);
        _searchBarPanel.OnLeftClick += (_, _) => _didClickSearchBar = true;
        Append(_searchBarPanel);

        _searchBar = new UISearchBar(LocalizedText.Empty, 0.8f);
        _searchBar.Width.Set(0f, 1f);
        _searchBar.Height.Set(0f, 1f);
        _searchBar.OnContentsChanged += (contents) =>
        {
            _searchText = contents;
            //FilterBossCards();
        };
        _searchBar.OnStartTakingInput += () =>
        {
            _searchBarPanel.BorderColor = new Color(120, 120, 180, 255);
        };
        _searchBar.OnEndTakingInput += () =>
        {
            _searchBarPanel.BorderColor = new Color(80, 80, 100, 200);
        };
        _searchBar.OnCanceledTakingInput += () =>
        {
            _searchText = "";
            _searchBar.SetContents("", forced: true);
            //FilterBossCards();
            _searchBarPanel.BorderColor = new Color(80, 80, 100, 200);
        };
        _searchBarPanel.Append(_searchBar);

        _modBtn = new UIPanel();
        _modBtn.SetPadding(2);
        _modBtn.Top.Set(32f, 0f);
        _modBtn.Width.Set(-2f, 0.5f);
        _modBtn.Height.Set(24f, 0f);
        _modBtn.BackgroundColor = new Color(50, 50, 70, 220);
        _modBtn.BorderColor = new Color(80, 80, 100, 200);
        _modBtn.OnLeftClick += (_, _) =>
        {
           // if (_availableMods.Count > 0)
            {
                _modFilterIndex = (_modFilterIndex + 1) % _availableMods.Count;
                //UpdateLabels();
                //FilterBossCards();
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        };
        Append(_modBtn);

        _modBtnText = new UIText("All", 0.65f);
        _modBtnText.HAlign = 0.5f;
        _modBtnText.VAlign = 0.5f;
        _modBtn.Append(_modBtnText);

        _effectBtn = new UIPanel();
        _effectBtn.SetPadding(2);
        _effectBtn.Top.Set(32f, 0f);
        _effectBtn.Left.Set(2f, 0.5f);
        _effectBtn.Width.Set(-2f, 0.5f);
        _effectBtn.Height.Set(24f, 0f);
        _effectBtn.BackgroundColor = new Color(50, 50, 70, 220);
        _effectBtn.BorderColor = new Color(80, 80, 100, 200);
        _effectBtn.OnLeftClick += (_, _) =>
        {
            _effectFilterIndex = (_effectFilterIndex + 1) /*% EffectCategories.Length*/;
            //UpdateLabels();
            //FilterBossCards();
            SoundEngine.PlaySound(SoundID.MenuTick);
        };
        Append(_effectBtn);

        _effectBtnText = new UIText("All", 0.65f);
        _effectBtnText.HAlign = 0.5f;
        _effectBtnText.VAlign = 0.5f;
        _effectBtn.Append(_effectBtnText);
    }

   /* private void UpdateLabels()
    {
        string modName = _modFilterIndex < _availableMods.Count ? _availableMods[_modFilterIndex] : "All";
        _modBtnText.SetText(GetModDisplayName(modName));

        string effectName = _effectFilterIndex < EffectCategories.Length ? EffectCategories[_effectFilterIndex] : "All";
        _effectBtnText.SetText(effectName);
    }*/

    private static string GetModDisplayName(string modName)
    {
        return modName switch
        {
            "SangarUtilities" => "Terraria",
            "RemnantOfTheAncientsMod" => "Remnant",
            "CalamityMod" => "Calamity",
            _ => modName
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_didClickSearchBar)
        {
            _didClickSearchBar = false;
            _searchBar.ToggleTakingText();
        }

        if (_searchBar.IsWritingText && Main.mouseLeft && !_searchBarPanel.IsMouseHovering)
        {
            _searchBar.ToggleTakingText();
        }
    }
}