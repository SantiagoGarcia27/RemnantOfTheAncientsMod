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
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
    public class ReaperSoulsUIState : UIState
    {
        public static DragableUIPanel BackgroundPanel;
        public static UIPanel VanillaSoulTogglePanel;
        public static UIPanel RemanntsSoulTogglePanel;
        public static UIPanel CalamitySoulTogglePanel;
        public static UISoulDisplay SoulsDisplay;
        public static Dictionary<Mod, Dictionary<int, UIHoverImageButton>> Buttons = [];
        public static List<int> BannedIds = [];


        public override void OnInitialize()
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);


            ReaperSoulUIExtras.OnInitialize();

            BannedIds = ReaperSoulUIExtras.BannedIds;

            Buttons = ReaperSoulUIExtras.Buttons;
            Buttons.TryAdd(TerrariaMod, []);
            Buttons.TryAdd(RemnantOfTheAncientsM, []);

            setBackground(ref BackgroundPanel);
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



            ListUtils.AddSecure(ref  NpcList.BossList, RemnantOfTheAncientsM, RemnantsNpc.ToList());

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
                int bottom = 110;
                const int iconHeight = 40;
                const int spacing = 50;


                setFullPanel(TerrariaMod, ref VanillaSoulTogglePanel,ref bottom, new Color(73, 94, 171),(iconHeight * 2) + spacing);
                setFullPanel(RemnantOfTheAncientsM, ref RemanntsSoulTogglePanel,ref bottom, new Color(166, 123, 5), iconHeight + spacing);
                if (CalamityMod != null)
                    setFullPanel(CalamityMod, ref CalamitySoulTogglePanel,ref bottom, new Color(49, 32, 36, 216));
                
            }

            base.OnActivate();
            void setFullPanel(Mod mod,ref UIPanel panel,ref int bottom,Color color,int bottomincrement = 0)
            {
                setModPanel(ref panel, color, 10, bottom, mod);
                setButtonPosition(mod);
                bottom += bottomincrement;
            }
            
           
           
            void setButtonPosition(Mod mod)
            {

                ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
                ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
                ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
                if (!Main.gameMenu)
                {
                    VerifyButtomsIndex(mod);
                    bool[] LoadedSouls = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded;
                    Dictionary<int, bool> Souls = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded;

                    int spaceLimit = 10;
                    float baseLefthPosition = 20f;
                    float baseTopPosition = 30f;

                    if (CalamityMod != null)
                    {
                        spaceLimit += 2;
                    }
                    int i = 1;
                    int bottom = 110;

                    if (mod == TerrariaMod)
                    {
                        PreDrawButtons(mod, spaceLimit, ref baseLefthPosition, ref baseTopPosition, ref i, ref bottom, 0, LoadedSouls.Length - 3);
                    }
                    else if (mod == RemnantOfTheAncientsM)
                    {
                        PreDrawButtons(mod, spaceLimit, ref baseLefthPosition, ref baseTopPosition, ref i, ref bottom, LoadedSouls.Length - 3, LoadedSouls.Length);
                    }
                    else if (CalamityMod != null && mod == CalamityMod)
                    {
                        PreDrawButtons(mod, spaceLimit, Souls, ref baseLefthPosition, ref baseTopPosition, ref i, ref bottom);
                    }
                }
            }
        }
        public static void setBackground(ref DragableUIPanel panel)
        {
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
            int height = 45 * 8;
            int width = 40 * 12;

            if (CalamityMod != null)
            {
                height += 40 * 3;
                width += (40 * 3) + 20;
            }

            if (panel == null)
            {
                Asset<Texture2D> BackgroundTexture = ModContent.GetInstance<RemnantOfTheAncientsMod>().Assets.Request<Texture2D>("Common/UI/BookUI/PanelBackground");
                panel = new DragableUIPanel(BackgroundTexture);
                panel.SetPadding(0);
            }
            UIUtils.SetPanel(ref panel, left: 400f, top: 100f, width,height, new Color(73, 94, 171));
        }
        public static void setModPanel(ref UIPanel panel, Color color, int left = 0, int top = 120, Mod mod = null)
        {
            float IconSizeHeight = 45f;
            float IconSizeWeight = 40f;

            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            ReaperSoulsPlayer reaperPlayer = Main.LocalPlayer.GetModPlayer<ReaperSoulsPlayer>();
            float iconAmmount = mod == RemnantOfTheAncientsM ? 3 : reaperPlayer.SoulsUpgradesLoaded.Length - 3;
            if (mod == CalamityMod)
                iconAmmount = UIUtils.GetBossAmmount(CalamityMod, reaperPlayer.SoulsUpgradesMaybeLoaded.Keys.ToList(),BannedIds);

            float Height = 0, Width = 0;
            GetPanelSize(ref Height, ref Width);

            if (CalamityMod != null) Width += 40;
            panel = new UIPanel();
            panel.SetPadding(0);

            UIUtils.SetPanel(ref panel, left, top, Width, Height, color);
           
            if(BackgroundPanel == null) 
                setBackground(ref BackgroundPanel);
            BackgroundPanel.Append(panel);


            void GetPanelSize(ref float Height, ref float Width)
            {
                int maxButtoms = CalamityMod != null ? 12 : 10;
                int column = (int)(iconAmmount / maxButtoms);
                if (iconAmmount % maxButtoms != 0)
                    column += 1;
                 Height = (IconSizeHeight * column) + 20;
                 Width = (IconSizeWeight * maxButtoms) + 40;
                if (CalamityMod != null) Width += 40;
            }
        }
        public static void DrawButtons(Mod mod, int type, ref int i, int spaceLimit, ref float baseLefthPosition, ref float baseTopPosition,ref int bottom)
        {

            float baseLefthPositionIncrement = 40;
            float baseTopPositionIncrement = 40;

            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);



            NPC npc = ContentSamples.NpcsByNetId[type];
            if (!BannedIds.Contains(npc.type) && RemnantGlobalNPC.GetMod(npc.type) == mod)
            {
                string Id = mod == TerrariaMod ? npc.type.ToString() : npc.ModNPC.GetType().Name;
                string Texture = $"RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_" + Id;

                Asset<Texture2D> blockedTexture = ModContent.Request<Texture2D>(Texture + "_Blocked");
                Asset<Texture2D> baseTexture = ReaperSoulUIExtras.GetBossHead(npc);

                Buttons[mod][npc.type] = UIUtils.CreateButtom(baseTexture, blockedTexture, baseLefthPosition, baseTopPosition, 22f, 22f, "", new MouseEvent(SetSoulClicked), false, npc, true);

                if (mod == TerrariaMod)
                    FixButtonPosition(mod, ref VanillaSoulTogglePanel, new Color(73, 94, 171), ref bottom, (40 * 2) + 50);
                else if (mod == RemnantOfTheAncientsM)
                    FixButtonPosition(mod, ref RemanntsSoulTogglePanel, new Color(166, 123, 5), ref bottom, 40 + 50);
                else if (CalamityMod != null && mod == CalamityMod)
                    FixButtonPosition(mod, ref CalamitySoulTogglePanel, new Color(49, 32, 36, 216), ref bottom, 0); 

                if (i % spaceLimit == 0)
                {
                    baseLefthPosition = 20;
                    baseTopPosition += baseTopPositionIncrement;
                }

                else if (!ReaperSoulUIExtras.CheckDistanceForBigIcons(npc, CalamityMod, ref baseLefthPosition, ref baseLefthPositionIncrement))
                {
                    baseLefthPosition += baseLefthPositionIncrement;
                }



                if (i < RemnantGlobalNPC.CountBoss(mod, BannedIds) + 1)
                    i++;
                else
                    i = 1;

                SoulsDisplay = new UISoulDisplay();
                UIUtils.SetRectangle(SoulsDisplay, 15f, 20f, 100f, 40f);
                BackgroundPanel.Append(SoulsDisplay);

               

                void FixButtonPosition(Mod mod,ref UIPanel panel,Color color,ref int bottom, int bottomIncrement)
                {
                    if (panel == null)
                        setModPanel(ref panel, color, 10, bottom, mod);
                    panel.Append(Buttons[mod][npc.type]);
                    bottom += bottomIncrement;
                }
            }
        }
        public static void PreDrawButtons(Mod mod, int spaceLimit, Dictionary<int, bool> List, ref float baseLefthPosition, ref float baseTopPosition, ref int i,ref int bottom)
        {
            i = 1;
            baseLefthPosition = 20f;
            baseTopPosition = 20f;
            foreach (KeyValuePair<int, bool> soul in List)
            {
                DrawButtons(mod, soul.Key, ref i, spaceLimit, ref baseLefthPosition, ref baseTopPosition,ref bottom);
            }
        }
        public static void PreDrawButtons(Mod mod, int spaceLimit, ref float baseLefthPosition, ref float baseTopPosition, ref int i, ref int bottom, int min = 0, int max = 0)
        {
            i = 1;
            baseLefthPosition = 20f;
            baseTopPosition = 20f;
            for (int j = min; j < max; j++)
            {
                int type = ReaperSoulsPlayer.GetLoadedBossIdByIndex(j);
                DrawButtons(mod, type, ref i, spaceLimit, ref baseLefthPosition, ref baseTopPosition,ref bottom);
            }
        }
        public static void SetSoulClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuOpen);
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
                        string Texture = $"RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_" + (mod == RemnantOfTheAncientsMod.Terraria ? npc.type.ToString() : npc.ModNPC.GetType().Name);

                        Asset<Texture2D> TextureBase = ReaperSoulUIExtras.GetBossHead(npc);


                        ModContent.RequestIfExists(Texture + "_Blocked", out Asset<Texture2D> TextureGray);
                        ModContent.RequestIfExists(Texture + "_Glow", out Asset<Texture2D> TextureSelected);
                        VerifyButtomsIndex(mod);


                        bool playerHaveSoul = ReaperSoulUIExtras.SelectList(mod, npcId);
                        float playerHaveSoulActive = ReaperSoulUIExtras.SelectActiveList(mod, npcId);

                        if (Buttons[mod][npc.type] != null)
                        {
                            if (playerHaveSoul)
                            {
                                Asset<Texture2D> TextureSpecial = playerHaveSoulActive > 0 ? TextureSelected : TextureBase;
                                Buttons[mod][npc.type].SetImage(TextureSpecial);
                            }
                            else
                            {
                                if(TextureGray != null)
                                Buttons[mod][npc.type].SetImage(TextureGray);
                            }
                        }
                    }
                }
            }
            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                CalculatedStyle innerDimensions = GetInnerDimensions();
                DragableUIPanel BackgroundPanel = ReaperSoulsUIState.BackgroundPanel;

                if (BackgroundPanel != null)
                {
                    string language = LanguageManager.Instance.ActiveCulture.Name;
                    string path = "RemnantOfTheAncientsMod/Common/UI/ReaperUI/";
                    Vector2 titlePos = new(innerDimensions.X + BackgroundPanel.Width.Pixels / 4, innerDimensions.Y + 25f);

                    ModContent.RequestIfExists(path + "TileBackground", out Asset<Texture2D> backgroundTexture);
                    ModContent.RequestIfExists(path + "SoulsText_" + language, out Asset<Texture2D> TitleTexture);

                    spriteBatch.Draw((Texture2D)backgroundTexture, new(titlePos.X - 15 + BackgroundPanel.Width.Pixels / 4, titlePos.Y + 5), null, Color.White, 0f, backgroundTexture.Size() / 2f, 0.78f, SpriteEffects.None, 0f);
                    spriteBatch.Draw((Texture2D)TitleTexture, new(titlePos.X + 30 + BackgroundPanel.Width.Pixels / 2, titlePos.Y + 30), null, Color.White, 0f, backgroundTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
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
