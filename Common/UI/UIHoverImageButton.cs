using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
	// This UIHoverImageButton class inherits from UIImageButton. 
	// Inheriting is a great tool for UI design. 
	// By inheriting, we get the Image drawing, MouseOver sound, and fading for free from UIImageButton
	// We've added some code to allow the Button to show a text tooltip while hovered
	public class UIHoverImageButton : UIImageButton
	{
		// Tooltip text that will be shown on hover
		internal string hoverText;
		internal static bool _Active = false;
		internal static bool _Blocked = false;
		public int _Val;
        public bool Reaper;


        public UIHoverImageButton(Asset<Texture2D> texture, string hoverText) : base(texture) {
            this.hoverText = hoverText;
		}
        public UIHoverImageButton(Asset<Texture2D> texture, string hoverText,int val,bool reaper) : base(texture)
        {
            this.hoverText = hoverText;
			_Val = val;
            Reaper = reaper;
        }



        public bool Blocked()
		{
			return _Blocked;
        }
        public void Blocked(bool b)
        {
			_Blocked = b;
        }
        public bool Active()
        {
            return _Active;
        }
        public void Active(bool b)
        {
            _Active = b;
        }
		public void Value(int v)
		{
			_Val = v;
		}
        public int Value()
        {
            return _Val;
        }
        public override void LeftClick(UIMouseEvent evt)
        {
            Player player = Main.player[Main.myPlayer];

            float value = player.GetModPlayer<ReaperPlayer>().SoulsUpgradesActive[_Val];

            if (Reaper)
            {
                if (_Val == 0)
                {
                    player.GetModPlayer<ReaperPlayer>().SetSoulsToggle(_Val, value == 0 ? 30 : 0);
                    player.GetModPlayer<ReaperPlayer>().SoulsUpgradesActive[_Val] = ModContent.GetInstance<ConfigClient>().ToggleKingSlimeSoul;
                }
                else if (_Val == 12)
                {

                    player.GetModPlayer<ReaperPlayer>().SoulsUpgradesActive[_Val] = ModContent.GetInstance<ConfigClient>().ToggleSkeletronPrimeSoul;
                    player.GetModPlayer<ReaperPlayer>().SetSoulsToggle(_Val, value == 0 ? 10 : 0);
                }
                else
                {

                    player.GetModPlayer<ReaperPlayer>().SoulsUpgradesActive[_Val] = value == 0 ? 1 : 0;
                    player.GetModPlayer<ReaperPlayer>().SetSoulsToggle(_Val, value == 0 ? 1 : 0);
                }
            }
            base.LeftClick(evt);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch) {
			// When you override UIElement methods, don't forget call the base method
			// This helps to keep the basic behavior of the UIElement
			base.DrawSelf(spriteBatch);

			// IsMouseHovering becomes true when the mouse hovers over the current UIElement
			if (IsMouseHovering)
				Main.hoverItemName = hoverText;
		}
	}
}
