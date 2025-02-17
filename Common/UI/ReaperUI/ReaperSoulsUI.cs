using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using SangarUtilities.Common;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
    internal class ReaperSoulsUIState : UIState
    {
        public static DragableUIPanel BackgroundPanel;
        public UIPanel VanillaSoulTogglePanel;
        public UIPanel RemanntsSoulTogglePanel;
        public UIPanel CalamitySoulTogglePanel;
        public UISoulDisplay SoulsDisplay;
        public static Dictionary<Mod, Dictionary<int, UIHoverImageButton>> Buttons = [];

        public static List<int> BannedIds = [395, 548, 549, 551, 564, 565, 576, 577, 344, 345, 346, 491, 493, 507, 517, 422, 327, 325, 396, 397, 664];
        public override void OnInitialize()
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);


            Buttons.TryAdd(TerrariaMod, []);
            Buttons.TryAdd(RemnantOfTheAncientsM, []);


            if (CalamityMod != null)
            {
                Buttons.TryAdd(CalamityMod, []);


                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHive"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHeadSmall"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorBodySmall"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorTailSmall"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHeadMedium"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorBodyMedium"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorTailMedium"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorHeadLarge"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorBodyLarge"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PerforatorTailLarge"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AstrumDeusBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AstrumDeusTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AquaticScourgeBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AquaticScourgeTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "SkeletronPrime2"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Cataclysm"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Catastrophe"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DesertScourgeBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "DesertScourgeTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "ThanatosBody1"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "ThanatosBody2"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "ThanatosTail"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Artemis"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Apollo"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresGaussNuke"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresLaserCannon"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresPlasmaFlamethrower"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "AresTeslaCannon"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "Anahita"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "THELORDE"));

                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PrimordialWyrmHead"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PrimordialWyrmBody"));
                GlobalUtils.AddSecure(ref BannedIds, CallUtils.GetNpcFromMod(CalamityMod, "PrimordialWyrmTail"));
            }
        }
        public static void VerifyButtomsIndex(Mod mod)
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
           

            if (mod != null)
            {  
                if (Buttons[mod].Count < RemnantGlobalNPC.CountBoss(mod,BannedIds))
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

            float baseLefthPositionIncrement = 40;
            float baseTopPositionIncrement = 40;

            if (BackgroundPanel == null)
            {
                BackgroundPanel = new DragableUIPanel();
                BackgroundPanel.SetPadding(0);
            }

            int height = 45 * 8;
            int width = 40 * 12;
            if (CalamityMod != null)
            {
                height += 40 * 3;
                width += (40 * 3) + 20;
            }

            UIUtils.SetRectangle(BackgroundPanel, left: 400f, top: 100f, width: width, height: height);//left: 400f, top: 100f, width: 170f, height: 70f
            BackgroundPanel.BackgroundColor = new Color(73, 94, 171);
            if (!Main.gameMenu)
            {
                int bottom = 110;
                setModPanel(ref VanillaSoulTogglePanel, new Color(73, 94, 171), 10, bottom, TerrariaMod);
                setButtonPosition(ref VanillaSoulTogglePanel, TerrariaMod);
                bottom += (40 * 2) + 50;
                setModPanel(ref RemanntsSoulTogglePanel, new Color(166, 123, 5), 10, bottom, RemnantOfTheAncientsM);
                setButtonPosition(ref RemanntsSoulTogglePanel, RemnantOfTheAncientsM);
                bottom += 40 + 50;
                if (CalamityMod != null)
                {
                    setModPanel(ref CalamitySoulTogglePanel, new Color(49, 32, 36, 216), 10, bottom, CalamityMod);
                    setButtonPosition(ref CalamitySoulTogglePanel, CalamityMod);
                }
            }

            base.OnActivate();

            void setModPanel(ref UIPanel panel, Color color, int left = 0, int top = 120, Mod mod = null)
            {

                float IconSizeHeight = 45f;
                float IconSizeWeight = 40f;

                ReaperPlayer reaperPlayer = Main.LocalPlayer.GetModPlayer<ReaperPlayer>();
                float iconAmmount = 0;
                foreach (int id in reaperPlayer.SoulsUpgrades.Keys)
                {
                    NPC npc = ContentSamples.NpcsByNetId[id];


                    if (ContentSamples.NpcsByNetId[id].boss && !BannedIds.Contains(id) && RemnantGlobalNPC.GetMod(id) == mod)
                    {
                        iconAmmount++;
                    }
                }

                //float iconAmmount = reaperPlayer.SoulsUpgrades[mod].Count;
                int maxButtoms = CalamityMod != null ? 12 : 10;
                int column = (int)(iconAmmount / maxButtoms);
                if (iconAmmount % maxButtoms != 0)
                    column += 1;
                float height = (IconSizeHeight * column) + 20;
                float width = (IconSizeWeight * maxButtoms) + 40;
                if (CalamityMod != null) width += 40;
                panel = new UIPanel();
                panel.SetPadding(0);

                UIUtils.SetRectangle(panel, left, top, width, height);
                panel.BackgroundColor = color;// new Color(73, 94, 171);
                BackgroundPanel.Append(panel);
            }
            void setButtonPosition(ref UIPanel panel, Mod mod)
            {
                if (!Main.gameMenu)
                {
                    VerifyButtomsIndex(mod);
                    Player player = Main.player[Main.myPlayer];
                    int i = 1;

                    Dictionary<int, bool> Souls = player.GetModPlayer<ReaperPlayer>().SoulsUpgrades;
                    Dictionary<Mod, int> SoulsCounter = new() { { TerrariaMod, 22 }, { RemnantOfTheAncientsM, 3 } };



                    float baseLefthPosition = 20f;
                    float baseTopPosition = 20f;
                    int spaceLimit = 10;

                    if (CalamityMod != null)
                    {
                        SoulsCounter.Add(CalamityMod, 24);
                        spaceLimit += 2;
                    }

                    foreach (KeyValuePair<int, bool> soul in Souls)
                    {
                        NPC npc = ContentSamples.NpcsByNetId[soul.Key];
                        if (!BannedIds.Contains(npc.type) && RemnantGlobalNPC.GetMod(npc.type) == mod)
                        {
                            string Id = mod == TerrariaMod ? npc.type.ToString() : npc.ModNPC.GetType().Name;
                            string Texture = $"RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_" + Id;

                            Asset<Texture2D> blockedTexture = ModContent.Request<Texture2D>(Texture + "_Blocked");
                            Asset<Texture2D> baseTexture = GetBossHead(npc);

                            Buttons[mod][npc.type] = UIUtils.CreateButtom(baseTexture, blockedTexture, baseLefthPosition, baseTopPosition, 22f, 22f, "", new MouseEvent(SetSoulClicked), false, npc, true);

                            if (mod == RemnantOfTheAncientsMod.Terraria)
                                VanillaSoulTogglePanel.Append(Buttons[mod][npc.type]);
                            else if (mod == RemnantOfTheAncientsMod.RemnantOfTheAncients)
                                RemanntsSoulTogglePanel.Append(Buttons[mod][npc.type]);
                            else if (mod == RemnantOfTheAncientsMod.CalamityMod)
                                CalamitySoulTogglePanel.Append(Buttons[mod][npc.type]);

                            if (i % spaceLimit == 0)
                            {
                                baseLefthPosition = 20;
                                baseTopPosition += baseTopPositionIncrement;
                            }
                            else if (checkCalamityMod())
                            {

                            }
                            else
                            {
                                baseLefthPosition += baseLefthPositionIncrement;
                            }
                            bool checkCalamityMod()
                            {
                                if (CalamityMod != null)
                                {
                                    if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Providence"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 30;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "CeaselessVoid"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 30;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Cryogen"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 10;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "DesertScourgeHead"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 10;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "HiveMind"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 20;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Crabulon"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 10;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Signus"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 10;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverHead"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement + 10;
                                        return true;
                                    }
                                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsHead"))
                                    {
                                        baseLefthPosition += baseLefthPositionIncrement - 10;
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                return false;
                            }

                            if (i < RemnantGlobalNPC.CountBoss(mod,BannedIds) + 1)
                                i++;
                            else
                                i = 1;

                            SoulsDisplay = new UISoulDisplay();
                            UIUtils.SetRectangle(SoulsDisplay, 15f, 20f, 100f, 40f);
                            BackgroundPanel.Append(SoulsDisplay);

                            Append(BackgroundPanel);
                        }
                    }
                }
            }
        }
        public static Asset<Texture2D> GetBossHead(NPC npc)
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out Mod RemnantOfTheAncientsM);
            ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);

            if (npc.type == NPCID.Golem)
                return TextureAssets.NpcHeadBoss[5];
            else if (npc.type == NPCID.BrainofCthulhu)
                return TextureAssets.NpcHeadBoss[2];
            else if (npc.type == NPCID.MoonLordCore)
                return TextureAssets.NpcHeadBoss[8];
            else if (npc.type == NPCID.DukeFishron)
                return TextureAssets.NpcHeadBoss[4];

            ModNPC ModNpc = npc.ModNPC;
            if (CalamityMod != null && ModNpc != null)
            {
                if (ModNpc.Mod == CalamityMod)
                {
                    string CalamityPath = "CalamityMod/NPCs/";

                    if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "StormWeaverHead"))
                        return ModContent.Request<Texture2D>(CalamityPath + "StormWeaver/StormWeaverHead_Head_Boss");
                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "DevourerofGodsHead"))
                        return ModContent.Request<Texture2D>(CalamityPath + "DevourerofGods/DevourerofGodsHead_Head_Boss");
                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "SupremeCalamitas"))
                        return ModContent.Request<Texture2D>(CalamityPath + "SupremeCalamitas/HoodedHeadIcon");
                    else if (npc.type == CallUtils.GetNpcFromMod(CalamityMod, "Providence"))
                        return ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_Providence");
                }
            }
            int idex = npc.GetBossHeadTextureIndex();
            return TextureAssets.NpcHeadBoss[idex];
        }

        public static void SetSoulClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.player[Main.myPlayer];
            SoundEngine.PlaySound(SoundID.MenuOpen);
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
                if (!ReaperSoulsUIState.BannedIds.Contains(npcId))
                {
                    Player player = Main.player[Main.myPlayer];
                    NPC npc = ContentSamples.NpcsByNetId[npcId];
                    string Texture = $"RemnantOfTheAncientsMod/Common/UI/ReaperUI/Textures/NPC_Head_Boss_" + (mod == RemnantOfTheAncientsMod.Terraria ? npc.type.ToString() : npc.ModNPC.GetType().Name);

                    var icon = ReaperSoulsUIState.GetBossHead(npc);
                    Asset<Texture2D> TextureBase = icon;//ModContent.Request<Texture2D>(Texture);


                    ModContent.RequestIfExists(Texture + "_Blocked", out Asset<Texture2D> TextureGray);
                    ModContent.RequestIfExists(Texture + "_Glow", out Asset<Texture2D> TextureSelected);
                    ReaperSoulsUIState.VerifyButtomsIndex(mod);


                    Dictionary<int, bool> SoulsUpgrades = player.GetModPlayer<ReaperPlayer>().SoulsUpgrades;

                    if (ReaperSoulsUIState.Buttons[mod][npc.type] != null)
                    {
                        if (SoulsUpgrades[npc.type])
                        {
                            Dictionary<int, float> SoulsUpgradesActive = player.GetModPlayer<ReaperPlayer>().SoulsUpgradesActive;

                            if (SoulsUpgradesActive[npc.type] > 0)
                            {
                                ReaperSoulsUIState.Buttons[mod][npc.type].SetImage(TextureSelected);
                            }
                            else
                            {
                                ReaperSoulsUIState.Buttons[mod][npc.type].SetImage(TextureBase);
                            }
                        }
                        else
                        {
                            ReaperSoulsUIState.Buttons[mod][npc.type].SetImage(TextureGray);
                        }
                    }
                }
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            float shopx = innerDimensions.X;
            float shopy = innerDimensions.Y;
            DragableUIPanel BackgroundPanel = ReaperSoulsUIState.BackgroundPanel;

            if (BackgroundPanel != null)
            {
                Vector2 titlePos = new(shopx + BackgroundPanel.Width.Pixels / 4 /*130*/, shopy + 25f);
                Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, Language.GetTextValue("Mods.RemnantOfTheAncientsMod.UI.ReaperTitle"), titlePos.X, titlePos.Y, Color.White, Color.Black, new Vector2(0.3f), 3.75f);
                DrawTitle(spriteBatch, titlePos);
            }
        }
        private void DrawTitle(SpriteBatch spriteBatch, Vector2 TitlePos)
        {
            for (int j = -1; j < 2; j += 2)
            {
                Main.instance.LoadItem(ModContent.ItemType<ReaperChalice>());
                Texture2D texture = TextureAssets.Item[ModContent.ItemType<ReaperChalice>()].Value;
                spriteBatch.Draw(texture, TitlePos + new Vector2((150 * Utils1.GetSign(j)) + 80, 0), null, Color.White, 0f, texture.Size() / 2f, 3.5f, SpriteEffects.None, 0f);
            }
        }

    }

    public class CheckSouls : ModPlayer
    {
        public override void PostUpdate()
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);
            Player player = Main.player[Main.myPlayer];

            foreach (KeyValuePair<int, bool> value in player.GetModPlayer<ReaperPlayer>().SoulsUpgrades)
            {
                NPC npc = ContentSamples.NpcsByNetId[value.Key];
                ModNPC modNPC = npc.ModNPC;
                Mod mod = TerrariaMod;
                if (modNPC != null)
                    mod = modNPC.Mod;

                UISoulDisplay.UpdateButtons(npc.type, mod);
            }
            base.PostUpdate();
        }
    }
}
