using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.UI;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using SangarUtilities.Common;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.ReaperSystem.ReaperUI
{
    public class ReaperSoulsUIState : UIState
    {
        public static DragableUIPanel BackgroundPanel;
        public static UIPanel LeftListPanel;
        public static UIPanel RightDetailPanel;
        public static UIList BossCardList;
        public static UIScrollbar BossScrollbar;
        public static Dictionary<Mod, Dictionary<int, UIHoverImageButton>> Buttons = [];
        public static int SelectedNpcType = -1;
        public static Mod SelectedMod;
        private const int PanelWidth = 900;
        private const int PanelHeight = 750;
        private const int LeftPanelWidth = 220;
        private const int Padding = 10;
        private const int TitleHeight = 100;

        public static List<UIBossCard> AllCards = [];

        public override void OnInitialize()
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);

            Buttons = ReaperSoulUIExtras.Buttons;
            Buttons.TryAdd(TerrariaMod, []);
            Buttons.TryAdd(RemnantOfTheAncientsM, []);

            SetupBackground(ref BackgroundPanel);
            Append(BackgroundPanel);
        }

        public static void VerifyButtomsIndex(Mod mod)
        {
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);

            NPC[] RemnantsNpc =
            [
                ContentSamples.NpcsByNetId[ModContent.NPCType<GemstoneCrusher>()],
                ContentSamples.NpcsByNetId[ModContent.NPCType<DesertAnnihilator>()],
                ContentSamples.NpcsByNetId[ModContent.NPCType<FrozenAssaulter>()],
                ContentSamples.NpcsByNetId[ModContent.NPCType<InfernalTyrantHead>()]
            ];

            ListUtils.AddSecure(ref NpcList.BossList, RemnantOfTheAncientsM, RemnantsNpc.ToList());

            if (mod != null)
            {
                int count = RemnantGlobalNPC.CountBoss(mod);
                if (Buttons[mod].Count < count)
                {
                    foreach (NPC npc in NpcList.BossList[mod])
                    {
                        Buttons[mod].TryAdd(npc.type, null);
                    }
                }
            }
        }

        public override void OnActivate()
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            if (!Main.gameMenu)
            {
                BackgroundPanel?.RemoveAllChildren();

                // Title display (90px = 60 * 1.5 for 50% larger banner)
                var soulsDisplay = new UISoulDisplay();
                UIUtils.SetRectangle(soulsDisplay, 15f, 5f, PanelWidth - 30f, 90f);
                BackgroundPanel.Append(soulsDisplay);

                // Left panel: scrollable boss card list
                LeftListPanel = new UIPanel();
                LeftListPanel.SetPadding(4);
                UIUtils.SetPanel(ref LeftListPanel, Padding, TitleHeight, LeftPanelWidth, PanelHeight - TitleHeight - Padding, new Color(40, 40, 60, 200));
                BackgroundPanel.Append(LeftListPanel);

                const float searchPanelHeight = 60f;
                UISearchFilterPanel searchPanel = new UISearchFilterPanel();
                searchPanel.SearchChanged += FilterBossCards;
                searchPanel.Width.Set(0f, 1f);
                searchPanel.Height.Set(searchPanelHeight, 0f);
                LeftListPanel.Append(searchPanel);

                BossCardList = new UIList();
                BossCardList.Width.Set(0f, 1f);
                BossCardList.Top.Set(searchPanelHeight + 4f, 0f);
                BossCardList.Height.Set(-(searchPanelHeight + 4f), 1f);
                BossCardList.ListPadding = 4f;
                BossCardList.ManualSortMethod = (list) =>
                {
                    list.Sort((a, b) =>
                    {
                        if (a is UIBossCard cardA && b is UIBossCard cardB)
                        {
                            return cardA.soulData.Progression.CompareTo(
                                cardB.soulData.Progression
                            );
                        }

                        return 0;
                    });
                };
                LeftListPanel.Append(BossCardList);

                BossScrollbar = new UIScrollbar();
                BossScrollbar.SetView(100f, 1000f);
                BossScrollbar.Height.Set(0f, 1f);
                BossScrollbar.HAlign = 1.1f;
                LeftListPanel.Append(BossScrollbar);
                BossCardList.SetScrollbar(BossScrollbar);

                // Right panel: detail view
                int rightLeft = Padding + LeftPanelWidth + Padding;
                int rightWidth = PanelWidth - rightLeft - Padding;
                RightDetailPanel = new UIPanel();
                RightDetailPanel.SetPadding(8);
                RightDetailPanel.OverflowHidden = true;
                UIUtils.SetPanel(ref RightDetailPanel, rightLeft, TitleHeight, rightWidth, PanelHeight - TitleHeight - Padding, new Color(30, 30, 50, 200));
                BackgroundPanel.Append(RightDetailPanel);

                PopulateBossCards(TerrariaMod, CalamityMod, RemnantOfTheAncientsM);

                SetupBackground(ref BackgroundPanel);
            }

            base.OnActivate();
        }

        private void PopulateBossCards(Mod terrariaMod, Mod calamityMod, Mod remnantMod)
        {
            AllCards.Clear();

            bool[] loadedSouls = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded;
            Dictionary<int, bool> maybeSouls = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded;

            VerifyButtomsIndex(terrariaMod);
            VerifyButtomsIndex(remnantMod);

            foreach (ReaperUpgradeRegister data in Reaper.soulData.OrderBy(s => s.Progression))
            {
                AddBossCard(data);
            }
            FilterBossCards("");
        }

        private void AddBossCard(ReaperUpgradeRegister soulData)
        {
            NPC npc = soulData.npc;
            Mod mod = soulData.mod;

            if (!npc.boss)
                return;

            Asset<Texture2D> headTexture = ReaperSoulUIExtras.GetBossHead(npc);

            var card = new UIBossCard(soulData, headTexture);
            card.Width.Set(0f, 1f);
            card.Height.Set(60f, 0f);

            card.OnLeftClick += (evt, el) =>
            {
                SelectedNpcType = npc.type;
                SelectedMod = mod;
                SoundEngine.PlaySound(SoundID.MenuTick);
                RefreshDetailPanel();
            };

            Buttons[mod][npc.type] = card.HeadButton;
            AllCards.Add(card);
        }

        public static void RefreshDetailPanel()
        {
            if (RightDetailPanel == null)
                return;

            RightDetailPanel.RemoveAllChildren();

            if (SelectedNpcType == -1)
                return;

            NPC npc = ContentSamples.NpcsByNetId[SelectedNpcType];

            float panelInnerHeight = RightDetailPanel.GetInnerDimensions().Height;
            if (panelInnerHeight <= 0f)
                panelInnerHeight = PanelHeight - TitleHeight - Padding - 16f;

            bool hasSoul = ReaperSoulUIExtras.SelectList(SelectedMod, SelectedNpcType);
            float activeValue = ReaperSoulUIExtras.SelectActiveList(SelectedMod, SelectedNpcType);
            bool isActive = activeValue > 0;


            ReaperUpgradeRegister soul = Reaper.soulData.FirstOrDefault(s => s.npcType == SelectedNpcType);

            bool hasSlider = hasSoul && soul != null && soul.MaxValue > 1f;

            var sliderRange = (
                min: 0f,
                max: soul?.MaxValue ?? 1f,
                increment: soul?.Increment ?? 1f
            );

            const float bestiaryHeight = 140f;
            const float nameHeight = 35f;
            const float toggleHeight = 40f;
            const float toggleMargin = 8f;
            const float sectionGap = 46f;
            const float sliderAreaHeight = 48f;

            float sliderSpace = hasSlider ? sliderAreaHeight + toggleMargin : 0f;
            float descAreaTop = bestiaryHeight + nameHeight + sectionGap * 2;
            float descAreaHeight = panelInnerHeight - descAreaTop - toggleHeight - toggleMargin * 2 - sliderSpace;
            if (descAreaHeight < 40f)
                descAreaHeight = 40f;

            // Boss NPC sprite
            var bossImage = new UIBossNpcDisplay(npc);
            bossImage.Width.Set(0f, 1f);
            bossImage.Height.Set(bestiaryHeight, 0f);
            bossImage.Top.Set(0f, 0f);
            RightDetailPanel.Append(bossImage);

            // Boss name
            string bossName = Lang.GetNPCNameValue(npc.type);
            var nameText = new UIText(bossName, 1.0f, true);
            nameText.Top.Set(bestiaryHeight + sectionGap, 0f);
            nameText.Width.Set(0f, 1f);
            nameText.HAlign = 0.5f;
            RightDetailPanel.Append(nameText);

            // Soul description inside a clipped scrollable area
            var descContainer = new UIPanel();
            descContainer.SetPadding(6);
            descContainer.OverflowHidden = true;
            descContainer.BackgroundColor = Color.Transparent;
            descContainer.BorderColor = Color.Transparent;
            descContainer.Top.Set(descAreaTop, 0f);
            descContainer.Width.Set(0f, 1f);
            descContainer.Height.Set(descAreaHeight, 0f);
            RightDetailPanel.Append(descContainer);

            string description = GetSoulDescription(npc);
            var descText = new UIText(description, 0.85f);
            descText.Width.Set(0f, 1f);
            descText.IsWrapped = true;
            descText.DynamicallyScaleDownToWidth = true;

            var descList = new UIList();
            descList.Width.Set(0f, 1f);
            descList.Height.Set(0f, 1f);
            descList.Add(descText);
            descContainer.Append(descList);

            // Only show scrollbar if the wrapped text overflows the container
            float panelInnerWidth = RightDetailPanel.GetInnerDimensions().Width;
            if (panelInnerWidth <= 0f)
                panelInnerWidth = PanelWidth - (Padding + LeftPanelWidth + Padding) - Padding - 16f;
            float descInnerWidth = panelInnerWidth - 12f;

            var font = FontAssets.MouseText.Value;
            float textScale = 0.85f;
            Utils.WordwrapString(description, font, (int)(descInnerWidth / textScale), 100, out int lineCount);
            float totalTextHeight = (lineCount + 1) * font.LineSpacing * textScale;

            if (totalTextHeight > descAreaHeight - 12f)
            {
                var descScrollbar = new UIScrollbar();
                descScrollbar.Height.Set(0f, 1f);
                descScrollbar.HAlign = 1f;
                descContainer.Append(descScrollbar);
                descList.SetScrollbar(descScrollbar);
            }

            // Slider bar for non-boolean soul values
            if (hasSlider)
            {
                float sliderTop = panelInnerHeight - toggleHeight - toggleMargin - sliderAreaHeight;
                var slider = new UIValueSlider(sliderRange.min, sliderRange.max, sliderRange.increment, activeValue);
                slider.Width.Set(-40f, 1f);
                slider.Height.Set(sliderAreaHeight, 0f);
                slider.Top.Set(sliderTop, 0f);
                slider.HAlign = 0.5f;
                int capturedNpcType = SelectedNpcType;
                Mod capturedMod = SelectedMod;
                slider.OnValueChanged += (newValue) =>
                {
                    SetSoulValue(capturedNpcType, newValue);
                };
                RightDetailPanel.Append(slider);
            }

            // Toggle button - anchored to the bottom
            string toggleLabel = !hasSoul ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.SoulLocked")
                : isActive ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.SoulActive")
                : Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.SoulInactive");

            Color btnColor = !hasSoul ? new Color(80, 80, 80, 200)
                : isActive ? new Color(50, 140, 50, 220)
                : new Color(140, 50, 50, 220);

            var togglePanel = new UIPanel();
            togglePanel.SetPadding(6);
            togglePanel.Width.Set(160f, 0f);
            togglePanel.Height.Set(toggleHeight, 0f);
            togglePanel.Top.Set(panelInnerHeight - toggleHeight - toggleMargin, 0f);
            togglePanel.HAlign = 0.5f;
            togglePanel.BackgroundColor = btnColor;

            if (hasSoul)
            {
                togglePanel.OnLeftClick += (evt, el) =>
                {
                    ToggleSoul(SelectedNpcType);
                    RefreshDetailPanel();
                };
            }

            var toggleText = new UIText(toggleLabel, 0.9f)
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };
            togglePanel.Append(toggleText);
            RightDetailPanel.Append(togglePanel);
        }

        private static readonly Dictionary<int, string> SoulDescKeys = [];

        private static void InitSoulDescKeys()
        {
            if (SoulDescKeys.Count > 0)
                return;

            SoulDescKeys[NPCID.KingSlime] = "KingSlime";
            SoulDescKeys[NPCID.EyeofCthulhu] = "EyeOfCthulhu";
            SoulDescKeys[NPCID.BrainofCthulhu] = "BrainOfCthulhu";
            SoulDescKeys[NPCID.QueenBee] = "QueenBee";
            SoulDescKeys[NPCID.SkeletronHead] = "Skeletron";
            SoulDescKeys[NPCID.Deerclops] = "Deerclops";
            SoulDescKeys[NPCID.WallofFlesh] = "WallOfFlesh";
            SoulDescKeys[NPCID.QueenSlimeBoss] = "QueenSlime";
            SoulDescKeys[NPCID.Spazmatism] = "Spazmatism";
            SoulDescKeys[NPCID.Retinazer] = "Retinazer";
            SoulDescKeys[NPCID.SkeletronPrime] = "SkeletronPrime";
            SoulDescKeys[NPCID.TheDestroyer] = "Destroyer";
            SoulDescKeys[NPCID.Plantera] = "Plantera";
            SoulDescKeys[NPCID.Golem] = "Golem";
            SoulDescKeys[NPCID.HallowBoss] = "EmpressOfLight";
            SoulDescKeys[NPCID.DukeFishron] = "DukeFishron";
            SoulDescKeys[NPCID.CultistBoss] = "LunaticCultist";
            SoulDescKeys[NPCID.MoonLordCore] = "MoonLord";
            SoulDescKeys[ModContent.NPCType<DesertAnnihilator>()] = "DesertAnnihilator";
            SoulDescKeys[ModContent.NPCType<FrozenAssaulter>()] = "FrozenAssaulter";
            SoulDescKeys[ModContent.NPCType<InfernalTyrantHead>()] = "InfernalTyrant";
        }

        private static string GetSoulDescription(NPC npc)
        {
            InitSoulDescKeys();

            if (SoulDescKeys.TryGetValue(npc.type, out string descKey))
            {
                string suffix = "UI.SoulDesc." + descKey;
                LocalizedText locText = ModContent.GetInstance<RemnantOfTheAncientsMod>()?.GetLocalization(suffix);
                if (locText != null && locText.Value != locText.Key)
                    return locText.Value;

                // Secondary attempt using full key path
                string fullKey = "Mods.RemnantOfTheAncientsMod." + suffix;
                if (Language.Exists(fullKey))
                    return Language.GetTextValue(fullKey);
            }

            // Fallback: try to use the hover text from the button
            if (SelectedMod != null && Buttons.TryGetValue(SelectedMod, out var modButtons))
            {
                if (modButtons.TryGetValue(npc.type, out var btn) && btn != null && !string.IsNullOrEmpty(btn.hoverText))
                    return btn.hoverText;
            }

            return "Wip";
        }

        public static void SetSoulValue(int npcType, float value)
        {
            ReaperSoulsPlayer reaperPlayer = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>();
            int index = ReaperSoulsPlayer.GetIndexFromLoadedBossById(npcType);

            if (index != -1)
            {
                reaperPlayer.SoulsUpgradesLoadedActive[index] = value;
            }
            else if (RemnantOfTheAncientsMod.CalamityMod != null && reaperPlayer.SoulsUpgradesMaybeLoadedActive.ContainsKey(npcType))
            {
                reaperPlayer.SoulsUpgradesMaybeLoadedActive[npcType] = value;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                reaperPlayer.SendSoulState();
            }
        }

        public static void ToggleSoul(int npcType)
        {
            ReaperSoulsPlayer reaperPlayer = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>();

            int index = ReaperSoulsPlayer.GetIndexFromLoadedBossById(npcType);

            float toggleMax = Reaper.soulData.FirstOrDefault(s => s.npcType == npcType)?.MaxValue ?? 1f;

            bool changed = false;

            if (index != -1 && reaperPlayer.SoulsUpgradesLoaded[index])
            {
                reaperPlayer.SoulsUpgradesLoadedActive[index] = reaperPlayer.SoulsUpgradesLoadedActive[index] > 0f ? 0f : toggleMax;
                changed = true;
            }
            else if (RemnantOfTheAncientsMod.CalamityMod != null && reaperPlayer.SoulsUpgradesMaybeLoaded.TryGetValue(npcType, out bool hasSoul) && hasSoul)
            {
                float current = reaperPlayer.SoulsUpgradesMaybeLoadedActive.GetValueOrDefault(npcType);
                reaperPlayer.SoulsUpgradesMaybeLoadedActive[npcType] = current > 0f ? 0f : toggleMax;
                changed = true;
            }

            if (changed)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                if (Main.netMode != NetmodeID.MultiplayerClient) return;
                reaperPlayer.SendSoulState();
            }
        }

        private static void FilterBossCards(string searchText)
        {
            if (BossCardList == null)
                return;

            BossCardList.Clear();

            string search = searchText?.Trim() ?? "";

            foreach (UIBossCard card in AllCards)
            {
                string npcName = Lang.GetNPCNameValue(card.soulData.npcType);

                if (string.IsNullOrEmpty(search) ||
                    npcName.Contains(search, StringComparison.OrdinalIgnoreCase))
                {
                    BossCardList.Add(card);
                }
            }

            BossCardList.Recalculate();
        }

        public static void SetupBackground(ref DragableUIPanel panel)
        {
            if (panel == null)
            {
                Asset<Texture2D> BackgroundTexture = ModContent.GetInstance<RemnantOfTheAncientsMod>().Assets.Request<Texture2D>("Common/UI/BookUI/PanelBackground");
                panel = new DragableUIPanel(BackgroundTexture);
                panel.SetPadding(0);
            }
            UIUtils.SetPanel(ref panel, left: 400f, top: 100f, PanelWidth, PanelHeight, new Color(73, 94, 171));
        }

        // Kept for backwards compat - old click handler redirects to new logic
        public static void SetSoulClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement is not UIHoverImageButton button || button._Npc == null)
                return;

            SelectedNpcType = button._Npc.type;
            // Find the mod for this NPC
            foreach (var kvp in Buttons)
            {
                if (kvp.Value.ContainsKey(button._Npc.type))
                {
                    SelectedMod = kvp.Key;
                    break;
                }
            }
            SoundEngine.PlaySound(SoundID.MenuTick);
            RefreshDetailPanel();
        }
    }
}
