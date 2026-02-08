using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UI.ReaperUI;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Prefixe;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.AdvanceReforgeUI
{
    internal class AdvanceReforgeUIState : UIState
    {
        public DragableUIPanel MainPanel;
        public UIItemSlotElement InputSlot;
        public UIItemSlotElement ReforgeStoneSlot;
        public UIItemSlotElement ResultSlot;
        public UIHoverImageButton ReforgeButton;

        public override void OnInitialize()
        {
            MainPanel = new DragableUIPanel();
            MainPanel.SetPadding(10);
            UIUtils.SetRectangle(MainPanel, left: 400f, top: 200f, width: 220f, height: 190f);
            MainPanel.BackgroundColor = new Color(73, 94, 171, 200);

            ResultSlot = new UIItemSlotElement(ItemSlot.Context.BankItem);
            UIUtils.SetRectangle(ResultSlot, 74f, 15f, 52f, 52f);
            MainPanel.Append(ResultSlot);

            InputSlot = new UIItemSlotElement(ItemSlot.Context.BankItem, ItemID.CopperShortsword);
            UIUtils.SetRectangle(InputSlot, 15f, 95f, 52f, 52f);
            MainPanel.Append(InputSlot);

            Asset<Texture2D> reforgeTexture = TextureAssets.Reforge[0];
            ReforgeButton = new UIHoverImageButton(reforgeTexture, "Reforge");
            UIUtils.SetRectangle(ReforgeButton, 82f, 97f, 40f, 40f);
            ReforgeButton.OnLeftClick += OnReforgeButtonClick;
            MainPanel.Append(ReforgeButton);

            ReforgeStoneSlot = new UIItemSlotElement(ItemSlot.Context.BankItem, ModContent.ItemType<Terracoin>());
            UIUtils.SetRectangle(ReforgeStoneSlot, 145f, 95f, 52f, 52f);
            MainPanel.Append(ReforgeStoneSlot);

            Append(MainPanel);
        }

        private void OnReforgeButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!InputSlot.Item.IsAir && !ReforgeStoneSlot.Item.IsAir)
            {
                SoundEngine.PlaySound(SoundID.Item37);

                Item inputItem = InputSlot.Item;
                Item reforgeStoneItem = ReforgeStoneSlot.Item;
                Item resultItem = ResultSlot.Item;
                int reforge = -1;
                if (reforgeStoneItem.type == ModContent.ItemType<Terracoin>())
                {
                    if (inputItem.DamageType == DamageClass.Magic) { 
                        reforge = ModContent.PrefixType<Relic>();
                    }
                    else if (inputItem.DamageType == DamageClass.Summon) {
                        reforge = ModContent.PrefixType<Relic>();
                    }
                    else if (inputItem.DamageType == DamageClass.Melee || inputItem.DamageType == DamageClass.MeleeNoSpeed || inputItem.DamageType == DamageClass.SummonMeleeSpeed)
                    {
                        reforge = ModContent.PrefixType<Berserk>();
                    }
                    else if (inputItem.DamageType == DamageClass.Ranged)
                    {
                        reforge = ModContent.PrefixType<Veteran>();
                    }
                }
                if (reforge != -1)
                {
                   
                    Item result = InputSlot.Item.Clone();
                    int ogReforge = result.prefix;
                    bool canPrefix = result.Prefix(reforge);

                    if (canPrefix && ogReforge != reforge)
                    {
                        if (!ResultSlot.Item.IsAir && ResultSlot.Item.type != ModContent.ItemType<RedCrossUI>())
                        {
                            if (ResultSlot.Item == result && ResultSlot.Item.stack < ResultSlot.Item.maxStack)
                            {
                                ResultSlot.Item.stack++;
                            }
                        }
                        else
                        {
                            ResultSlot.Item = result;
                        }


                        if (!result.IsAir)
                        {
                            if ((inputItem.stack - 1) <= 0)
                                InputSlot.Item.TurnToAir();
                            else
                                InputSlot.Item.stack--;

                            if ((reforgeStoneItem.stack - 1) <= 0)
                                ReforgeStoneSlot.Item.TurnToAir();
                            else
                                ReforgeStoneSlot.Item.stack--;

                            ResultSlot.Locked = false;
                        }
                    }
                    else
                    {
                        ResultSlot.Locked = true;
                        Item errorItem = new(ModContent.ItemType<RedCrossUI>());
                        ResultSlot.Item = errorItem;
                    }
                }
                else
                {
                    ResultSlot.Locked = true;
                    Item errorItem = new(ModContent.ItemType<RedCrossUI>());
                    ResultSlot.Item = errorItem;
                }
              
            }
        }

        public override void OnDeactivate()
        {
            ReturnItemToPlayer(InputSlot);
            ReturnItemToPlayer(ReforgeStoneSlot);
            ReturnItemToPlayer(ResultSlot);
            base.OnDeactivate();
        }

        private static void ReturnItemToPlayer(UIItemSlotElement slot)
        {
            if (!slot.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_Misc("AdvanceReforgeUI"), slot.Item, slot.Item.stack);
                slot.Item.TurnToAir();
            }
        }
    }

    public class UIItemSlotElement : UIElement
    {
        public Item Item;
        private readonly int _context;
        public int HintItemType { get; }
        public bool Locked { get; set; }

        public UIItemSlotElement(int context, int hintItemType = -1)
        {
            _context = context;
            HintItemType = hintItemType;
            Item = new Item();
            Item.TurnToAir();
            Width.Set(52f, 0f);
            Height.Set(52f, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = 0.85f;

            Item[] tempInv = [Item];

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!Locked)
                    ItemSlot.Handle(tempInv, _context, 0);
            }

            Item = tempInv[0];
            ItemSlot.Draw(spriteBatch, tempInv, _context, 0, new Vector2(dimensions.X, dimensions.Y));

            if (Item.IsAir && HintItemType > 0)
            {
                DrawHintIcon(spriteBatch, dimensions);
            }

            Main.inventoryScale = oldScale;
        }

        private void DrawHintIcon(SpriteBatch spriteBatch, CalculatedStyle dimensions)
        {
            if (HintItemType <= 0) return;

            string texturePath = HintItemType < ItemID.Count
                ? $"Terraria/Images/Item_{HintItemType}"
                : ItemLoader.GetItem(HintItemType)?.Texture ?? "";

            if (string.IsNullOrEmpty(texturePath)) return;

            Texture2D hintTexture = ModContent.Request<Texture2D>(texturePath, AssetRequestMode.ImmediateLoad).Value;
            Rectangle sourceRect = Main.itemAnimations[HintItemType] != null
                ? Main.itemAnimations[HintItemType].GetFrame(hintTexture)
                : hintTexture.Frame();

            float maxSize = 32f;
            float scale = 1f;
            if (sourceRect.Width > maxSize || sourceRect.Height > maxSize)
                scale = maxSize / MathHelper.Max(sourceRect.Width, sourceRect.Height);

            float slotSize = 52f * Main.inventoryScale;
            Vector2 slotCenter = new(dimensions.X + slotSize / 2f, dimensions.Y + slotSize / 2f);
            Vector2 origin = sourceRect.Size() / 2f;

            spriteBatch.Draw(hintTexture, slotCenter, sourceRect, Color.White * 0.35f, 0f, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
