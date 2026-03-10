using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.UI.ReaperUI;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.ReforgeCatalyst;
using RemnantOfTheAncientsMod.Prefixe;
using System;
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
        public UIMoneyDisplay MoneyDisplay;

        public override void OnInitialize()
        {
            MainPanel = new DragableUIPanel();
            MainPanel.SetPadding(10);
            UIUtils.SetRectangle(MainPanel, left: 400f, top: 200f, width: 240f, height: 160f);
            MainPanel.BackgroundColor = new Color(73, 94, 171, 200);

            float paddingLeft = 10f;
            float itemSlotSize = 52f;
            InputSlot = new UIItemSlotElement(ItemSlot.Context.BankItem, ItemID.CopperShortsword);
            UIUtils.SetRectangle(InputSlot, paddingLeft, 15f, itemSlotSize, itemSlotSize);
            MainPanel.Append(InputSlot);

            paddingLeft += itemSlotSize + 10f;
            ReforgeStoneSlot = new UIItemSlotElement(ItemSlot.Context.BankItem, ModContent.ItemType<BerserkStone>());
            ReforgeStoneSlot.OnUpdate += OnUpdateCatalyst;
            UIUtils.SetRectangle(ReforgeStoneSlot, paddingLeft, 15f, itemSlotSize, itemSlotSize);
            MainPanel.Append(ReforgeStoneSlot);

            paddingLeft += itemSlotSize + 30f;
            ResultSlot = new UIItemSlotElement(ItemSlot.Context.BankItem);
            UIUtils.SetRectangle(ResultSlot, paddingLeft, 15f, itemSlotSize, itemSlotSize);
            MainPanel.Append(ResultSlot);

            Asset<Texture2D> reforgeTexture = TextureAssets.Reforge[0];
            ReforgeButton = new UIHoverImageButton(reforgeTexture, "Reforge");
            UIUtils.SetRectangle(ReforgeButton, 10f, 87f, 40f, 40f);
            ReforgeButton.OnLeftClick += OnReforgeButtonClick;
            MainPanel.Append(ReforgeButton);

            MoneyDisplay = new UIMoneyDisplay();
            UIUtils.SetRectangle(MoneyDisplay, 60f, 97f, 120f, 20f);
            MainPanel.Append(MoneyDisplay);

           

            Append(MainPanel);
        }

        void OnUpdateCatalyst(UIElement affectedElement)
        {
            if (affectedElement != null)
            {
                MoneyDisplay.SetCoins(ReforgeStoneSlot.Item.GetApplyPrice());
            }
            else
            {
                MoneyDisplay.ResetCoins();
            }
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
                if (reforgeStoneItem.GetCatalystReforge(inputItem) != -1)
                { 
                    reforge = reforgeStoneItem.GetCatalystReforge(inputItem);   
                }
                if (reforge != -1)
                {
                   
                    Item result = InputSlot.Item.Clone();
                    int ogReforge = result.prefix;
                    bool canPrefix = result.Prefix(reforge);
                   
                    if (canPrefix && ogReforge != reforge && Main.LocalPlayer.BuyItem(reforgeStoneItem.GetApplyPrice()))
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
                            ResultSlot.Item.TurnToAir(true);
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
            if (ResultSlot.Item.type != ModContent.ItemType<RedCrossUI>())
            {
                ReturnItemToPlayer(ResultSlot);
            }
            else
            {
                ResultSlot.Item.TurnToAir();
            }
            base.OnDeactivate();
        }

        private static void ReturnItemToPlayer(UIItemSlotElement slot)
        {
            slot.ReturnItemToPlayer(Main.LocalPlayer);
        }
    }

    public class UIItemSlotElement : UIElement
    {
        public Item Item;
        private readonly int _context;
        private Item _previousItem;
        public int HintItemType { get; }
        public bool Locked { get; set; }

        public Func<Item, bool> CanPutItemCondition;
        public event Action<Item, Item> OnItemChanged;

        public bool HasItem => !Item.IsAir;

        public UIItemSlotElement(int context, int hintItemType = -1)
        {
            _context = context;
            HintItemType = hintItemType;
            Item = new Item();
            Item.TurnToAir();
            _previousItem = new Item();
            _previousItem.TurnToAir();
            Width.Set(52f, 0f);
            Height.Set(52f, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!ItemEquals(_previousItem, Item))
            {
                Item oldItem = _previousItem.Clone();
                _previousItem = Item.Clone();
                OnItemChanged?.Invoke(oldItem, Item);
            }
        }

        public void SetItem(Item item)
        {
            Item = item?.Clone() ?? new Item();
        }

        public void SetItem(int type, int stack = 1, int prefix = 0)
        {
            Item = new Item(type, stack, prefix);
        }

        public void Clear()
        {
            Item.TurnToAir();
        }

        public Item TakeItem()
        {
            Item taken = Item.Clone();
            Item.TurnToAir();
            return taken;
        }

        public bool ConsumeStack(int amount = 1)
        {
            if (Item.IsAir || Item.stack < amount)
                return false;

            Item.stack -= amount;
            if (Item.stack <= 0)
                Item.TurnToAir();

            return true;
        }

        public void ReturnItemToPlayer(Player player)
        {
            if (!Item.IsAir)
            {
                player.QuickSpawnItem(player.GetSource_Misc("UIItemSlot"), Item, Item.stack);
                Item.TurnToAir();
            }
        }

        private static bool ItemEquals(Item a, Item b)
        {
            if (a.IsAir && b.IsAir)
                return true;
            return a.type == b.type && a.stack == b.stack && a.prefix == b.prefix;
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
                {
                    if (CanPutItemCondition != null && !Main.mouseItem.IsAir && !CanPutItemCondition(Main.mouseItem))
                    {
                        // Item not accepted by filter, skip interaction
                    }
                    else
                    {
                        ItemSlot.Handle(tempInv, _context, 0);
                    }
                }
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

    public class UIMoneyDisplay : UIElement
    {
        // How many coins have been collected in copper
        public long Coins;
        // Saving coin textures to an array to make them easier to access
        private readonly Texture2D[] coinsTextures = new Texture2D[4];

        public UIMoneyDisplay()
        {
       

            for (int j = 0; j < 4; j++)
            {
                // Textures may not be loaded without it
                Main.instance.LoadItem(74 - j);
                coinsTextures[j] = TextureAssets.Item[74 - j].Value;
            }

            // This allows clicks to "pass-through" this element to the parent element and not be consumed by this element. This allows ExampleDraggableUIPanel to be dragged even when the user is clicking on the UIMoneyDisplay.
            IgnoresMouseInteraction = true;
        }
        public void SetCoins(int coins)
        {
            Coins = coins;
        }

        public long GetCoins()
        {
            return Coins;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            // Getting top left position of this UIElement
            float shopx = innerDimensions.X;
            float shopy = innerDimensions.Y;

            // Drawing first line of coins (current collected coins)
            // CoinsSplit converts the number of copper coins into an array of all types of coins
            DrawCoins(spriteBatch, shopx, shopy, Utils.CoinsSplit(Coins));

            // Drawing second line of coins (coins per minute) and text "CPM"
            //DrawCoins(spriteBatch, shopx, shopy, Utils.CoinsSplit(GetCoinsPerMinute()), 0, 25);
            //Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, "", shopx + (float)(24 * 4), shopy + 25f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
        }

        private void DrawCoins(SpriteBatch spriteBatch, float shopx, float shopy, int[] coinsArray, int xOffset = 0, int yOffset = 0)
        {
            for (int j = 0; j < 4; j++)
            {
                spriteBatch.Draw(coinsTextures[j], new Vector2(shopx + 11f + 24 * j + xOffset, shopy + yOffset), null, Color.White, 0f, coinsTextures[j].Size() / 2f, 1f, SpriteEffects.None, 0f);
                Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, coinsArray[3 - j].ToString(), shopx + 24 * j + xOffset, shopy + yOffset, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
            }
        }

        public void ResetCoins()
        {
            Coins = 0;
        }
    }
}
