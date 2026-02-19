using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using SangarUtilities.Common;
using SangarUtilities.Common.UtilsTweaks;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
    public class ReaperSoulsUIState : UIState
    {
        public static DragableUIPanel BackgroundPanel;
        public static UIPanel LeftListPanel;
        public static UIPanel RightDetailPanel;
        public static UIList BossCardList;
        public static UIScrollbar BossScrollbar;
        public static Dictionary<Mod, Dictionary<int, UIHoverImageButton>> Buttons = [];
        public static List<int> BannedIds = [];

        public static int SelectedNpcType = -1;
        public static Mod SelectedMod;

        private const int PanelWidth = 900;
        private const int PanelHeight = 750;
        private const int LeftPanelWidth = 220;
        private const int Padding = 10;
        private const int TitleHeight = 100;

        public static List<UIBossCard> AllCards = [];
        private static string _searchText = "";
        private static int _modFilterIndex;
        private static int _effectFilterIndex;
        private static List<string> _availableMods = ["All"];
        private static readonly string[] EffectCategories = ["All", "Stats", "Summons", "Immunity", "Mixed"];

        public override void OnInitialize()
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);

            ReaperSoulUIExtras.OnInitialize();

            BannedIds = ReaperSoulUIExtras.BannedIds;

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
                ContentSamples.NpcsByNetId[ModContent.NPCType<DesertAnnihilator>()],
                ContentSamples.NpcsByNetId[ModContent.NPCType<FrozenAssaulter>()],
                ContentSamples.NpcsByNetId[ModContent.NPCType<InfernalTyrantHead>()]
            ];

            ListUtils.AddSecure(ref NpcList.BossList, RemnantOfTheAncientsM, RemnantsNpc.ToList());

            if (mod != null)
            {
                int count = RemnantGlobalNPC.CountBoss(mod, BannedIds);
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
                var searchPanel = new UISearchFilterPanel();
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
                            return GetProgressionValue(cardA.Npc).CompareTo(GetProgressionValue(cardB.Npc));
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

                // Populate boss cards
                PopulateBossCards(TerrariaMod, CalamityMod, RemnantOfTheAncientsM);
                BuildModFilterList();
                FilterBossCards();

                SetupBackground(ref BackgroundPanel);
            }

            base.OnActivate();
        }

        private void PopulateBossCards(Mod terrariaMod, Mod calamityMod, Mod remnantMod)
        {
            AllCards.Clear();
            _searchText = "";
            _modFilterIndex = 0;
            _effectFilterIndex = 0;

            bool[] loadedSouls = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded;
            Dictionary<int, bool> maybeSouls = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded;

            VerifyButtomsIndex(terrariaMod);
            VerifyButtomsIndex(remnantMod);

            // Vanilla bosses
            for (int j = 0; j < loadedSouls.Length - 3; j++)
            {
                int type = ReaperSoulsPlayer.GetLoadedBossIdByIndex(j);
                AddBossCard(terrariaMod, type);
            }

            // Remnant bosses
            for (int j = loadedSouls.Length - 3; j < loadedSouls.Length; j++)
            {
                int type = ReaperSoulsPlayer.GetLoadedBossIdByIndex(j);
                AddBossCard(remnantMod, type);
            }

            // Calamity bosses
            if (calamityMod != null)
            {
                VerifyButtomsIndex(calamityMod);
                foreach (KeyValuePair<int, bool> soul in maybeSouls)
                {
                    AddBossCard(calamityMod, soul.Key);
                }
            }

            // Sort all cards by game progression order
            InitProgressionOrder();
            AllCards.Sort((a, b) =>
            {
                float orderA = GetProgressionValue(a.Npc);
                float orderB = GetProgressionValue(b.Npc);
                return orderA.CompareTo(orderB);
            });
        }

        private static readonly Dictionary<int, float> ProgressionOrder = [];

        private static void InitProgressionOrder()
        {
            ProgressionOrder.Clear();

            // ── Pre-Hardmode ──
            ProgressionOrder.TryAdd(NPCID.KingSlime, 1f);
            TryAddCalamityBoss("DesertScourgeHead", 1.5f);
            ProgressionOrder.TryAdd(NPCID.EyeofCthulhu, 2f);
            TryAddCalamityBoss("Crabulon", 2.5f);
            ProgressionOrder.TryAdd(NPCID.EaterofWorldsHead, 3f);
            ProgressionOrder.TryAdd(NPCID.BrainofCthulhu, 3f);
            TryAddCalamityBoss("HiveMind", 3.5f);
            ProgressionOrder.TryAdd(NPCID.QueenBee, 4f);
            ProgressionOrder.TryAdd(NPCID.SkeletronHead, 5f);
            ProgressionOrder.TryAdd(NPCID.Deerclops, 6f);
            ProgressionOrder.TryAdd(ModContent.NPCType<DesertAnnihilator>(), 6.7f);
            TryAddCalamityBoss("SlimeGodCore", 6.8f);
            ProgressionOrder.TryAdd(NPCID.WallofFlesh, 7f);

            // ── Hardmode ──
            ProgressionOrder.TryAdd(ModContent.NPCType<FrozenAssaulter>(), 7.2f);
            ProgressionOrder.TryAdd(NPCID.QueenSlimeBoss, 8f);
            TryAddCalamityBoss("Cryogen", 8.5f);
            ProgressionOrder.TryAdd(NPCID.Spazmatism, 9f);
            ProgressionOrder.TryAdd(NPCID.Retinazer, 9f);
            TryAddCalamityBoss("AquaticScourgeHead", 9.3f);
            TryAddCalamityBoss("BrimstoneElemental", 9.6f);
            ProgressionOrder.TryAdd(NPCID.TheDestroyer, 10f);
            TryAddCalamityBoss("CalamitasClone", 10.5f);
            TryAddCalamityBoss("GreatSandShark", 10.7f);
            ProgressionOrder.TryAdd(NPCID.SkeletronPrime, 11f);
            TryAddCalamityBoss("Leviathan", 11.5f);
            ProgressionOrder.TryAdd(NPCID.Plantera, 12f);
            TryAddCalamityBoss("AstrumAureus", 12.5f);
            ProgressionOrder.TryAdd(ModContent.NPCType<InfernalTyrantHead>(), 13f);
            TryAddCalamityBoss("PlaguebringerGoliath", 13.2f);
            ProgressionOrder.TryAdd(NPCID.Golem, 13.5f);
            TryAddCalamityBoss("RavagerBody", 13.8f);
            ProgressionOrder.TryAdd(NPCID.HallowBoss, 14f);
            TryAddCalamityBoss("AstrumDeusHead", 14.5f);
            ProgressionOrder.TryAdd(NPCID.DukeFishron, 15f);
            ProgressionOrder.TryAdd(NPCID.CultistBoss, 16f);
            ProgressionOrder.TryAdd(NPCID.MoonLordCore, 17f);

            // ── Post-Moon Lord (Calamity) ──
            TryAddCalamityBoss("ProfanedGuardianCommander", 17.5f);
            TryAddCalamityBoss("ProfanedGuardianDefender", 17.501f);
            TryAddCalamityBoss("ProfanedGuardianHealer", 17.502f);
            TryAddCalamityBoss("Dragonfolly", 18f);
            TryAddCalamityBoss("Providence", 18.5f);
            TryAddCalamityBoss("CeaselessVoid", 19f);
            TryAddCalamityBoss("StormWeaverHead", 19.1f);
            TryAddCalamityBoss("Signus", 19.2f);
            TryAddCalamityBoss("Polterghast", 20f);
            TryAddCalamityBoss("OldDuke", 20.5f);
            TryAddCalamityBoss("DevourerofGodsHead", 21f);
            TryAddCalamityBoss("Yharon", 22f);
            TryAddCalamityBoss("ThanatosHead", 23f);
            TryAddCalamityBoss("SupremeCalamitas", 24f);
        }

        private static void TryAddCalamityBoss(string npcName, float progression)
        {
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                return;

            int type = CallUtils.TryGetNpcFromMod(calamity, npcName);
            if (type > 0)
                ProgressionOrder.TryAdd(type, progression);
        }

        private static float GetProgressionValue(NPC npc)
        {
            if (ProgressionOrder.TryGetValue(npc.type, out float val))
                return val;

            return float.MaxValue;
        }

        private void AddBossCard(Mod mod, int npcType)
        {
            if (BannedIds.Contains(npcType))
                return;

            NPC npc = ContentSamples.NpcsByNetId[npcType];
            if (RemnantGlobalNPC.GetMod(npc.type) != mod)
                return;

            Asset<Texture2D> headTexture = ReaperSoulUIExtras.GetBossHead(npc);
            var card = new UIBossCard(npc, mod, headTexture);
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

            const float bestiaryHeight = 140f;
            const float nameHeight = 35f;
            const float toggleHeight = 40f;
            const float toggleMargin = 8f;
            const float sectionGap = 6f;

            float descAreaTop = bestiaryHeight + nameHeight + sectionGap * 2;
            float descAreaHeight = panelInnerHeight - descAreaTop - toggleHeight - toggleMargin * 2;
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

            // Toggle button - anchored to the bottom
            bool hasSoul = ReaperSoulUIExtras.SelectList(SelectedMod, SelectedNpcType);
            float activeValue = ReaperSoulUIExtras.SelectActiveList(SelectedMod, SelectedNpcType);
            bool isActive = activeValue > 0;

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

            var toggleText = new UIText(toggleLabel, 0.9f);
            toggleText.HAlign = 0.5f;
            toggleText.VAlign = 0.5f;
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

        public static void ToggleSoul(int npcType)
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
            ReaperSoulsPlayer reaperPlayer = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>();
            int index = ReaperSoulsPlayer.GetIndexFromLoadedBossById(npcType);

            if (index != -1 && reaperPlayer.SoulsUpgradesLoaded[index])
            {
                reaperPlayer.SoulsUpgradesLoadedActive[index] = reaperPlayer.SoulsUpgradesLoadedActive[index] > 0 ? 0f : 1f;
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
            else if (CalamityMod != null && reaperPlayer.SoulsUpgradesMaybeLoaded.TryGetValue(npcType, out bool hasSoul) && hasSoul)
            {
                reaperPlayer.SoulsUpgradesMaybeLoadedActive[npcType] = reaperPlayer.SoulsUpgradesMaybeLoadedActive[npcType] > 0 ? 0f : 1f;
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
        }

        private static void BuildModFilterList()
        {
            var modNames = AllCards.Select(c => c.Mod?.Name ?? "Unknown").Distinct().ToList();
            _availableMods = ["All", .. modNames];
        }

        public static void FilterBossCards()
        {
            if (BossCardList == null)
                return;

            BossCardList.Clear();

            string modFilter = _modFilterIndex < _availableMods.Count ? _availableMods[_modFilterIndex] : "All";
            string effectFilter = _effectFilterIndex < EffectCategories.Length ? EffectCategories[_effectFilterIndex] : "All";
            string search = _searchText?.ToLowerInvariant() ?? "";

            var filtered = new List<UIBossCard>();

            foreach (var card in AllCards)
            {
                if (!string.IsNullOrWhiteSpace(search))
                {
                    string name = Lang.GetNPCNameValue(card.Npc.type).ToLowerInvariant();
                    string modName = (card.Mod?.Name ?? "").ToLowerInvariant();
                    string desc = GetSoulDescription(card.Npc).ToLowerInvariant();
                    if (!name.Contains(search) && !modName.Contains(search) && !desc.Contains(search))
                        continue;
                }

                if (modFilter != "All")
                {
                    string cardMod = card.Mod?.Name ?? "";
                    if (!string.Equals(cardMod, modFilter, System.StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                if (effectFilter != "All")
                {
                    string category = GetEffectCategory(card.Npc);
                    if (category != effectFilter)
                        continue;
                }

                filtered.Add(card);
            }

            filtered.Sort((a, b) => GetProgressionValue(a.Npc).CompareTo(GetProgressionValue(b.Npc)));

            foreach (var card in filtered)
                BossCardList.Add(card);
        }

        private static string GetEffectCategory(NPC npc)
        {
            string desc = GetSoulDescription(npc).ToLowerInvariant();

            string[] summonKw = ["invoca", "summon", "tornado", "esporas", "spores", "niebla", "mist", "manos", "hands", "minion", "shark", "tiburón"];
            string[] statsKw = ["damage", "daño", "defense", "defensa", "speed", "velocidad", "life", "vida", "mining", "minería", "vuelo", "flight", "crit", "máxima", "maximum"];
            string[] immunityKw = ["inmunidad", "immunity", "inmune", "immune"];

            bool hasSummon = summonKw.Any(k => desc.Contains(k));
            bool hasStats = statsKw.Any(k => desc.Contains(k));
            bool hasImmunity = immunityKw.Any(k => desc.Contains(k));

            int count = (hasSummon ? 1 : 0) + (hasStats ? 1 : 0) + (hasImmunity ? 1 : 0);

            if (count >= 2) return "Mixed";
            if (hasSummon) return "Summons";
            if (hasImmunity) return "Immunity";
            if (hasStats) return "Stats";
            return "Mixed";
        }

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
                    FilterBossCards();
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
                    FilterBossCards();
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
                    if (_availableMods.Count > 0)
                    {
                        _modFilterIndex = (_modFilterIndex + 1) % _availableMods.Count;
                        UpdateLabels();
                        FilterBossCards();
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
                    _effectFilterIndex = (_effectFilterIndex + 1) % EffectCategories.Length;
                    UpdateLabels();
                    FilterBossCards();
                    SoundEngine.PlaySound(SoundID.MenuTick);
                };
                Append(_effectBtn);

                _effectBtnText = new UIText("All", 0.65f);
                _effectBtnText.HAlign = 0.5f;
                _effectBtnText.VAlign = 0.5f;
                _effectBtn.Append(_effectBtnText);
            }

            private void UpdateLabels()
            {
                string modName = _modFilterIndex < _availableMods.Count ? _availableMods[_modFilterIndex] : "All";
                _modBtnText.SetText(GetModDisplayName(modName));

                string effectName = _effectFilterIndex < EffectCategories.Length ? EffectCategories[_effectFilterIndex] : "All";
                _effectBtnText.SetText(effectName);
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

        /// <summary>
        /// A boss card shown in the left-side list. Shows the boss head icon and name.
        /// </summary>
        public class UIBossCard : UIPanel
        {
            public UIHoverImageButton HeadButton;
            public NPC Npc;
            public Mod Mod;

            public UIBossCard(NPC npc, Mod mod, Asset<Texture2D> headTexture)
            {
                Npc = npc;
                Mod = mod;
                SetPadding(4);
                BackgroundColor = new Color(60, 60, 80, 180);
                BorderColor = new Color(100, 100, 120, 200);

                HeadButton = new UIHoverImageButton(headTexture, Lang.GetNPCNameValue(npc.type), npc, true);
                HeadButton.Left.Set(4f, 0f);
                HeadButton.VAlign = 0.5f;
                HeadButton.Width.Set(28f, 0f);
                HeadButton.Height.Set(28f, 0f);
                HeadButton.OnLeftClick += (evt, el) =>
                {
                    ReaperSoulsUIState.SelectedNpcType = npc.type;
                    ReaperSoulsUIState.SelectedMod = mod;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    RefreshDetailPanel();
                };
                Append(HeadButton);

                string name = Lang.GetNPCNameValue(npc.type);
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
                bool isSelected = ReaperSoulsUIState.SelectedNpcType == Npc.type;
                bool hasSoul = ReaperSoulUIExtras.SelectList(Mod, Npc.type);
                bool isActive = hasSoul && ReaperSoulUIExtras.SelectActiveList(Mod, Npc.type) > 0;

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

        /// <summary>
        /// Draws the NPC bestiary portrait uniformly scaled and centered.
        /// Uses the bestiary icon system with a reduced iconbox to normalize sizes,
        /// and falls back to manual sprite drawing if no bestiary entry exists.
        /// </summary>
        public class UIBossNpcDisplay : UIElement
        {
            private readonly int _npcType;
            private readonly BestiaryEntry _bestiaryEntry;
            private const float UniformBoxSize = 100f;

            public UIBossNpcDisplay(NPC npc)
            {
                _npcType = npc.type;
                _bestiaryEntry = Main.BestiaryDB.FindEntryByNPCID(_npcType);
                OverflowHidden = true;
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                CalculatedStyle dims = GetDimensions();

                if (_bestiaryEntry != null)
                {
                    // Center a fixed-size box inside the element so every NPC renders at the same scale
                    int boxX = (int)(dims.X + (dims.Width - UniformBoxSize) / 2f);
                    int boxY = (int)(dims.Y + (dims.Height - UniformBoxSize) / 2f);
                    Rectangle iconBox = new(boxX, boxY, (int)UniformBoxSize, (int)UniformBoxSize);

                    BestiaryUICollectionInfo collectionInfo = _bestiaryEntry.UIInfoProvider.GetEntryUICollectionInfo();

                    EntryIconDrawSettings drawSettings = new()
                    {
                        iconbox = iconBox,
                        IsPortrait = true,
                        IsHovered = false
                    };

                    _bestiaryEntry.Icon.Update(collectionInfo, iconBox, drawSettings);
                    _bestiaryEntry.Icon.Draw(collectionInfo, spriteBatch, drawSettings);
                }
                else
                {
                    // Fallback: draw the NPC texture first frame
                    Texture2D npcTexture = TextureAssets.Npc[_npcType].Value;
                    int frameCount = Main.npcFrameCount[_npcType];
                    int frameHeight = npcTexture.Height / frameCount;
                    Rectangle sourceRect = new(0, 0, npcTexture.Width, frameHeight);

                    float maxW = dims.Width - 20f;
                    float maxH = dims.Height - 10f;

                    float fitTarget = UniformBoxSize / System.Math.Max(sourceRect.Width, sourceRect.Height);
                    float fitArea = System.Math.Min(maxW / sourceRect.Width, maxH / sourceRect.Height);
                    float scale = System.Math.Min(fitTarget, fitArea);

                    Vector2 center = new(dims.X + dims.Width / 2f, dims.Y + dims.Height / 2f);
                    Vector2 origin = new(sourceRect.Width / 2f, sourceRect.Height / 2f);

                    spriteBatch.Draw(npcTexture, center, sourceRect, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
                }
            }
        }

        public class UISoulDisplay : UIElement
        {
            public UISoulDisplay()
            {
            }

            public static void UpdateButtons(int npcId, Mod mod)
            {
                if (!Main.gameMenu)
                {
                    if (!BannedIds.Contains(npcId))
                    {
                        NPC npc = ContentSamples.NpcsByNetId[npcId];
                        Asset<Texture2D> TextureBase = ReaperSoulUIExtras.GetBossHead(npc);

                        VerifyButtomsIndex(mod);

                        bool playerHaveSoul = ReaperSoulUIExtras.SelectList(mod, npcId);
                        float playerHaveSoulActive = ReaperSoulUIExtras.SelectActiveList(mod, npcId);

                        if (Buttons.TryGetValue(mod, out var modButtons) && modButtons.TryGetValue(npc.type, out var btn) && btn != null)
                        {
                            btn.SetImage(TextureBase);
                            if (playerHaveSoul)
                            {
                                btn.Blocked(false);
                                btn.Active(playerHaveSoulActive > 0);
                            }
                            else
                            {
                                btn.Blocked(true);
                                btn.Active(false);
                            }
                        }

                        // Refresh detail panel if the selected boss state changed
                        if (SelectedNpcType == npcId)
                            RefreshDetailPanel();
                    }
                }
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                CalculatedStyle innerDimensions = GetInnerDimensions();
                DragableUIPanel bgPanel = ReaperSoulsUIState.BackgroundPanel;

                if (bgPanel != null)
                {
                    string language = LanguageManager.Instance.ActiveCulture.Name;
                    string path = "RemnantOfTheAncientsMod/Common/UI/ReaperUI/";

                    float centerX = innerDimensions.X + innerDimensions.Width / 2f;
                    float centerY = innerDimensions.Y + innerDimensions.Height / 2f;

                    ModContent.RequestIfExists(path + "TileBackground", out Asset<Texture2D> backgroundTexture);
                    ModContent.RequestIfExists(path + "SoulsText_" + language, out Asset<Texture2D> TitleTexture);

                    if (backgroundTexture != null && backgroundTexture.Value != null)
                    {
                        float texW = backgroundTexture.Value.Width;
                        float texH = backgroundTexture.Value.Height;
                        float scaleW = (innerDimensions.Width - 10f) / texW;
                        float scaleH = innerDimensions.Height / texH;
                        float scale = System.Math.Min(scaleW, scaleH);

                        Vector2 origin = backgroundTexture.Size() / 2f;
                        spriteBatch.Draw(backgroundTexture.Value, new Vector2(centerX, centerY), null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
                    }

                    if (TitleTexture != null && TitleTexture.Value != null)
                    {
                        float texW = TitleTexture.Value.Width;
                        float texH = TitleTexture.Value.Height;
                        float scaleW = (innerDimensions.Width - 20f) / texW;
                        float scaleH = (innerDimensions.Height - 8f) / texH;
                        float scale = System.Math.Min(System.Math.Min(scaleW, scaleH), 1f);

                        Vector2 origin = TitleTexture.Size() / 2f;
                        spriteBatch.Draw(TitleTexture.Value, new Vector2(centerX, centerY), null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public class CheckSouls : ModPlayer
        {
            public override void PostUpdate()
            {
                Player player = Main.player[Main.myPlayer];
                bool[] LoadedSouls = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded;
                Dictionary<int, bool> MaybeLoadedSouls = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded;

                ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
                ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
                ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
                Mod mod = TerrariaMod;

                for (int i = 0; i < LoadedSouls.Length; i++)
                {
                    int type = ReaperSoulsPlayer.GetLoadedBossIdByIndex(i);
                    NPC npc = ContentSamples.NpcsByNetId[type];

                    if (i >= LoadedSouls.Length - 3)
                        mod = RemnantOfTheAncientsM;
                    UISoulDisplay.UpdateButtons(npc.type, mod);
                }
                if (CalamityMod != null)
                {
                    foreach (int id in MaybeLoadedSouls.Keys)
                    {
                        NPC npc = ContentSamples.NpcsByNetId[id];
                        UISoulDisplay.UpdateButtons(npc.type, CalamityMod);
                    }
                }

                base.PostUpdate();
            }
        }
    }
}
